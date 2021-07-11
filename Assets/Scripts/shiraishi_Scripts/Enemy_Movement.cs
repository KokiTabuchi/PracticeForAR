using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    GameObject bee = new GameObject();
    int move_pattern_rnd = Random.Range(1, 3);
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        switch (move_pattern_rnd)
        {
            case 1:
                movePatternOne();
                break;
            case 2:
                movePatternTwo();
                break;
            case 3:
                movePatternThird();
                break;
            default:break;
        }
    }

    void movePatternOne()
    {

    }

    void movePatternTwo()
    {

    }

    void movePatternThird()
    {

    }



    /*
   //敵を徘徊するように動かすスクリプト
   public void enemyMoveRandom(int enemy_num)
   {
       Debug.Log("移動");
       //wayPoints[0] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y+ Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
       Vector3 pos = wayPoints2[enemy_num][currentRoot];//Vector3型のposに現在の目的地の座標を代入
       float distance = Vector3.Distance(spawnedEnemy[enemy_num].transform.position, Player.transform.position);//敵とプレイヤーの距離を求める

       if (distance > 3)
       {//もしプレイヤーと敵の距離が5以上なら
           Mode = 0;//Modeを0にする
       }

       if (distance < 3)
       {//もしプレイヤーと敵の距離が5以下なら
           Mode = 1;//Modeを1にする
       }

       switch (Mode)
       {//Modeの切り替えは

           case 0://case0の場合

               if (Vector3.Distance(spawnedEnemy[enemy_num].transform.position, pos) < 1f)
               {//もし敵の位置と現在の目的地との距離が1以下なら
                   currentRoot += 1;//currentRootを+1する
                   if (currentRoot > wayPoints2[enemy_num].Length - 1)
                   {//もしcurrentRootがwayPointsの要素数-1より大きいなら
                       currentRoot = 0;//currentRootを0にする
                   }
               }
               //次の目的場所を向く
               spawnedEnemy[enemy_num].transform.LookAt(wayPoints2[enemy_num][currentRoot]);
               spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;//GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgentの情報を取得し目的地をposにする
               break;//switch文の各パターンの最後につける

           case 1://case1の場合
               enemyMoveDirect(enemy_num);//プレイヤーに向かって進む		
               break;//switch文の各パターンの最後につける
       }

       //敵の移動目的ポイントの変更
       //float Targetpoint = Random.Range(-3.0f, 3.0f);
     //  wayPoints[0] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
      // wayPoints[1] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
      // wayPoints[2] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
   }

   public void enemyMoveDirect(int enemy_num)
   {
       //敵がプレイヤーのほうを向く
       spawnedEnemy[enemy_num].transform.LookAt(Player.transform.position);

       // 変数 moveSpeed を乗算した速度でオブジェクトを前方向に移動する
       spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;
   }
   */
}
