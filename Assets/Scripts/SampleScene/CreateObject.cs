using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateObject : MonoBehaviour
{
    public GameObject objectPrefab;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    // 初期化時に呼ばれる
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // フレーム毎に呼ばれる
    void Update()
    {
        // タッチ時
        if (Input.GetMouseButtonDown(0))
        {
            // レイと平面が交差時
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.PlaneWithinPolygon))
            {
                // 3Dオブジェクトの生成
                Instantiate(objectPrefab, hitResults[0].pose.position, Quaternion.identity);
            }
        }
    }
}