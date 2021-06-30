using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Camera_Operation : MonoBehaviour
{
    public GameObject ARCamera;  //��]����J�����̑Ώ�
    protected float rotate_speed = 10.0f; //�J�����̉�]�̑��x
 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
        //�f�o�b�N��Ƃ̌������̂��ߏ\���L�[��p���ăJ�����𓮂�����悤�ɂ���
        float x_camera_rotation= Input.GetAxis("Vertical") * Time.deltaTime * rotate_speed;//�㉺�L�[�������ꂽ�ꍇ
        float y_camera_rotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotate_speed;//���E�L�[�������ꂽ�ꍇ
        ARCamera.transform.Rotate(x_camera_rotation*-1, y_camera_rotation, 0.0f);//�J�����̏\���L�[�ɂ���]
        //Debug.Log("camraplace"+ARCamera.transform.rotation.y);
    }
}
