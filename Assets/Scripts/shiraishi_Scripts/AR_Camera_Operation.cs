using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Camera_Operation : MonoBehaviour
{
    public GameObject ARCamera;  //回転するカメラの対象
    protected float rotate_speed = 10.0f; //カメラの回転の速度
 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
        //デバック作業の効率化のため十字キーを用いてカメラを動かせるようにする
        float x_camera_rotation= Input.GetAxis("Vertical") * Time.deltaTime * rotate_speed;//上下キーが押された場合
        float y_camera_rotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotate_speed;//左右キーが押された場合
        ARCamera.transform.Rotate(x_camera_rotation*-1, y_camera_rotation, 0.0f);//カメラの十字キーによる回転
        //Debug.Log("camraplace"+ARCamera.transform.rotation.y);
    }
}
