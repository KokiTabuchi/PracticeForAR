using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateObject : MonoBehaviour
{
    public GameObject objectPrefab;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    // ���������ɌĂ΂��
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // �t���[�����ɌĂ΂��
    void Update()
    {
        // �^�b�`��
        if (Input.GetMouseButtonDown(0))
        {
            // ���C�ƕ��ʂ�������
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.PlaneWithinPolygon))
            {
                // 3D�I�u�W�F�N�g�̐���
                Instantiate(objectPrefab, hitResults[0].pose.position, Quaternion.identity);
            }
        }
    }
}