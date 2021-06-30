using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject muzzle;

    private ParticleSystem ps;
    private GameObject shotLaser;

    // Start is called before the first frame update
    void Start()
    {
        ps = muzzle.GetComponent<ParticleSystem>();
        //最初は噴射しない様にする
        muzzle.SetActive(false);
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
            */

            else if (Input.GetMouseButtonUp(0))
            {
                //スプレー停止処理
                StartCoroutine(SprayStop());
            }
            
        }

        // 端末上での操作取得
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                //指でタッチ
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("タッチ！");
                    Attack();
                }

                /*指で動かす
                else if (touch.phase == TouchPhase.Moved)
                {

                }
                */

                //指が離れる
                else if (touch.phase == TouchPhase.Ended)
                {
                    //スプレー停止処理
                    StartCoroutine(SprayStop());
                }
            }
        }
    }

    //敵を攻撃
    private void Attack()
    {
        /*レーザーの生成
        shotLaser = Instantiate(laser, muzzle.transform);
        //このままだとmuzzleが親になるので、親オブジェクトから外す
        shotLaser.transform.parent = null;
        //レーザー発射方向の指定
        Vector3 laserDirection = this.transform.rotation * Vector3.forward;
        //その方向にAddForceで速度を付ける
        shotLaser.GetComponent<Rigidbody>().AddForce(laserDirection, ForceMode.Impulse);
        */

        //スプレー噴射のエフェクト開始
        muzzle.SetActive(true);
    }

    //スプレー停止処理
    IEnumerator SprayStop()
    {
        //スプレー噴射のエフェクト終了
        ps.Stop();
        //ParticleのDuration0.2秒分待つ
        yield return new WaitForSeconds(0.2f);
        //衝突判定も消す
        muzzle.SetActive(false);
    }

    //人がぶつかったときの処理
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("衝突！");
    }

}
