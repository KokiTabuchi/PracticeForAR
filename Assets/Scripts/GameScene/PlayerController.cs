using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject muzzle;

    private GameObject shotLaser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�^�b�`�����Ԃ̎擾
        GetInputVector();
    }

    private void GetInputVector()
    {
        // Unity��ł̑���擾
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("�^�b�`�I");
                Attack();
            }

            /*
            else if (Input.GetMouseButton(0))
            {

            }
            else if (Input.GetMouseButtonUp(0))
            {

            }
            */
        }

        // �[����ł̑���擾
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("�^�b�`�I");
                    Attack();
                }

                /*
                else if (touch.phase == TouchPhase.Moved)
                {

                }
                else if (touch.phase == TouchPhase.Ended)
                {

                }
                */
            }
        }
    }

    //�G���U��
    private void Attack()
    {
        //���[�U�[�̐���
        shotLaser = Instantiate(laser, muzzle.transform);
        //���̂܂܂���muzzle���e�ɂȂ�̂ŁA�e�I�u�W�F�N�g����O��
        shotLaser.transform.parent = null;
        //���[�U�[���˕����̎w��
        Vector3 laserDirection = this.transform.rotation * Vector3.forward;
        //���̕�����AddForce�ő��x��t����
        shotLaser.GetComponent<Rigidbody>().AddForce(laserDirection, ForceMode.Impulse);
    }
}
