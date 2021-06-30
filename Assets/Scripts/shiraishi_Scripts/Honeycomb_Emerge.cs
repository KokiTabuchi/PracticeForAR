using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using DG.Tweening;

public class Honeycomb_Emerge : MonoBehaviour
{
    //蜂の巣のゲームオブジェクト
    public GameObject comb;

    //蜂の殺した数 Killed_Enemy_Countクラスのインスタンス変数
    Killed_Enemy_Count killed_enemy_num = new Killed_Enemy_Count();

    //蜂を殺したら巣が出現するための一定条件
    const int killedEnemy_upper_limit = 0;

    //蜂の巣の位置を初期化
    float honeycomb_pos_x = 0.0f;
    float honeycomb_pos_y = 0.0f;
    float honeycomb_pos_z = 0.0f;

    //鉢の巣を表示させるかどうか
     private bool isComb = false;

    public void Start()
    {
        comb.SetActive(false);//蜂の巣を非表示にする
        setHonneyComb();//蜂の巣オブジェクトを表示する場所をランダムで決定
        
    }
    public void Update()
    {
       
        //getkilled_Enemyメソッドで倒したエネミーの数を引き取り、巣が発生する上限と比較する
        if ((killed_enemy_num.getKilled_EnemyNum()) >= killedEnemy_upper_limit)
        {
            if (isComb == false)
            {
                Emerge_Comb();
            }
        }

    }

    //巣を発生させるためbooleanをtrueに変更するメソッド
    public void Emerge_Comb()
    {

        Debug.Log(killed_enemy_num.getKilled_EnemyNum());
        Debug.Log(isComb);
        //巣を表示する
        comb.SetActive(true);
        isComb = true;
    }

    //蜂の巣オブジェクトを表示する場所をランダムで決定
    public void setHonneyComb()
    {
        //巣を表示する場所を乱数で決定
        honeycomb_pos_x = Random.Range(-10.0f, 10.0f);
        honeycomb_pos_y = Random.Range(-10.0f, 10.0f);
        honeycomb_pos_z = Random.Range(6.0f, 15.0f);
        comb.transform.position = new Vector3( honeycomb_pos_x, honeycomb_pos_y, honeycomb_pos_z);
    }
}