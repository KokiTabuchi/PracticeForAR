using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField] 
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject spawnablePrefab;

    GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        //touch����ĂȂ�����return
        if (Input.touchCount == 0)
            return;


        if(m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            //touch���͂��߂�Prefab����
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SpawnPrefab(m_Hits[0].pose.position);
            }
            //drag���Ă�Ԃ�Prefab�𓮂���
            else if(Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                spawnedObject.transform.position = m_Hits[0].pose.position;
            }
            //�w�𗣂�����Prefab��ݒu���ďI���
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
    }

}
