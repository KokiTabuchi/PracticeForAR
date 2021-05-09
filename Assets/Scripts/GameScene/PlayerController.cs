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
        //タッチ操作状態の取得
        GetInputVector();
    }

    private void GetInputVector()
    {
        // Unity上での操作取得
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("タッチ！");
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

        // 端末上での操作取得
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("タッチ！");
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

    //敵を攻撃
    private void Attack()
    {
        //レーザーの生成
        shotLaser = Instantiate(laser, muzzle.transform);
        //このままだとmuzzleが親になるので、親オブジェクトから外す
        shotLaser.transform.parent = null;
        //レーザー発射方向の指定
        Vector3 laserDirection = this.transform.rotation * Vector3.forward;
        //その方向にAddForceで速度を付ける
        shotLaser.GetComponent<Rigidbody>().AddForce(laserDirection, ForceMode.Impulse);
    }
}
