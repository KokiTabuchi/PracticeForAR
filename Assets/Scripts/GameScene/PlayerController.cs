using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject[] weapon = new GameObject[2];

    private GameObject muzzle;
    private ParticleSystem ps;
    private GameObject shotLaser;

    //最初はスプレー
    private int WeaponNumber =0;

    //ボタンそのもの
    [SerializeField]
    private GameObject weaponButton;
    //アイコンの種類数
    [SerializeField]
    private Sprite[] weaponIcon = new Sprite[2];
    //ボタンのアイコン
    private Image weaponButtonIcon;

    public bool isTouch;

    // Start is called before the first frame update
    void Start()
    {
        weaponButtonIcon = weaponButton.GetComponent<Image>();
        //まずはスプレーを装備した状態にする
        ChangeWeapon();
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
                isTouch = true;
                Attack();
            }

            /*
            else if (Input.GetMouseButton(0))
            {

            }
            */

            else if (Input.GetMouseButtonUp(0))
            {
                isTouch = false;
                //スプレー停止処理
                StartCoroutine(muzzleStop());
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
                    isTouch = true;
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
                    isTouch = false;
                    //スプレー停止処理
                    StartCoroutine(muzzleStop());
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
    IEnumerator muzzleStop()
    {
        //スプレー噴射のエフェクト終了
        ps.Stop();
        //ParticleのstartLifeTime分待つ
        yield return new WaitForSeconds(ps.main.startLifetime.constant * 0.95f);

        //前のパーティクルに対するmuzzleStop処理が今放出中のパーティクルを対象とするのを防ぐためのif文
        if (isTouch == false)
        {
            //衝突判定も消す
            muzzle.SetActive(false);
        }

    }

    //武器変更ボタンを押した時の処理
    public void WeaponButtonDown()
    {
        //ボタンの色や画像を変える処理
        switch (WeaponNumber)
        {
            case 0:
                weaponButtonIcon.sprite = weaponIcon[0];
                break;

            case 1:
                weaponButtonIcon.sprite = weaponIcon[1];
                break;

            default:
                break;
        }

        ChangeWeapon();
    }

    //武器変更処理
    private void ChangeWeapon()
    {
        //一旦全部の武器をfalseにする
        for (int i = 0; i < 2; i++)
        {
            weapon[i].SetActive(false);
        }

        //WeaponNumberに対応する武器だけtrueにする
        weapon[WeaponNumber].SetActive(true);
        //該当武器の一番上の階層にある子オブジェクトをmuzzleとして取得する
        muzzle = weapon[WeaponNumber].transform.GetChild(0).gameObject;
        //psをmuzzleについてるParticleSystemに変更する
        ps = muzzle.GetComponent<ParticleSystem>();

        //最初は噴射しないようにする
        muzzle.SetActive(false);

        //WeaponNumberを加算しておく
        WeaponNumber++;
        if (WeaponNumber >= 2)
        {
            WeaponNumber = 0;
        }
    }
}
