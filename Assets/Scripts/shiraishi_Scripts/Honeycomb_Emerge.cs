using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using DG.Tweening;
using UnityEngine.AI;//NavMeshagentを使うために記述する

public class Honeycomb_Emerge : MonoBehaviour
{
    //ゲームのプレイヤーのオブジェクト  
    public GameObject Player;
    //蜂の巣のゲームオブジェクト
    public GameObject comb;

    //蜂の殺した数 Killed_Enemy_Countクラスのインスタンス変数
    Killed_Enemy_Count killed_enemy_num = new Killed_Enemy_Count();
    GameSceneManager killed_Manager  = new GameSceneManager();

    //蜂を殺したら巣が出現するための一定条件
    const int killedEnemy_upper_limit = 0;

    //蜂の巣の位置を初期化
    float honeycomb_pos_x = 0.0f;
    float honeycomb_pos_y = 0.0f;
    float honeycomb_pos_z = 0.0f;

    //鉢の巣を表示させるかどうか
     private bool isComb = false;

    //出現させる蜂のオブジェクト 
    [SerializeField]
    private GameObject[] bee = new GameObject[10];//とりあえず１０体出てくるように設定
    private GameObject[] spawnedEnemy = new GameObject[10];//スポーンされたときに格納されるもの

    //敵の生成の時間とインターバル
    float timer = 0.0f;
    float enemyInterval = 5.0f;


    Vector3[] wayPoints = new Vector3[3];//徘徊するポイントの座標を代入するVector3型の変数を配列で作る
    private int currentRoot=0;//現在目指すポイントを代入する変数
    private int Mode;//敵の行動パターンを分けるための変数
    //public Transform player;//プレイヤーの位置を取得するためのTransform型の変数
    //public Transform enemypos;//敵の位置を取得するためのTransform型の変数
    //private NavMeshAgent agent;//NavMeshAgentの情報を取得するためのNavmeshagent型の変数

    //敵の速さ
    float moveSpeed = 1.0f;

    public void Start()
    {
        //Player = GameObject.Find("Player");
       // agent = GetComponent<NavMeshAgent>();//NavMeshAgentの情報をagentに代入
        comb.SetActive(false);//蜂の巣を非表示にする
        setHonneyComb();//蜂の巣オブジェクトを表示する場所をランダムで決定

        //敵の移動目的ポイント３つ設定
        wayPoints[0] = new Vector3(Player.transform.position.x + 7.0f, Player.transform.position.y, Player.transform.position.z);
        wayPoints[1] = new Vector3(Player.transform.position.x, Player.transform.position.y+7.0f, Player.transform.position.z);
        wayPoints[2] = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z+7.0f);
    }
    public void Update()
    {
        timer += Time.deltaTime;

      
        //getkilled_Enemyメソッドで倒したエネミーの数を引き取り、巣が発生する上限と比較する
        if ((killed_enemy_num.getKilled_EnemyNum()) >= killedEnemy_upper_limit)
        {
            if (isComb == false)
            {
                Emerge_Comb();
            }
            //一定時間で敵を生成
            //if (timer > enemyInterval)
            //{
                timer = 0.0f;
                emergeBee();//蜂を発生させるメソッドの呼び出し
            //}
           
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

    //巣から蜂を発生させるスクリプト
    public void emergeBee()
    {
        //蜂を生成する(数は配列文だけで、場所は蜂の巣の外側から
        for(int i = 0; i< bee.Length; i++)
        {
            if (!spawnedEnemy[i])
            {
                spawnedEnemy[i] = Instantiate(bee[i],
                        new Vector3(honeycomb_pos_x + (comb.transform.localScale.x / 2)*Random.Range(-5.0f,5.0f),
                                    honeycomb_pos_y + comb.transform.localScale.y / 2,
                                    honeycomb_pos_z + comb.transform.localScale.z / 2
                                    ),
                         Quaternion.LookRotation(Player.transform.position));
            }
        }

        for(int i = 0; i < spawnedEnemy.Length; i++)
        {
                enemyMoveRandom(i);
        }
    }

    //敵を徘徊するように動かすスクリプト
    public void enemyMoveRandom(int enemy_num)
    {
        Debug.Log("移動");
        Vector3 pos = wayPoints[currentRoot];//Vector3型のposに現在の目的地の座標を代入
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
                    if (currentRoot > wayPoints.Length - 1)
                    {//もしcurrentRootがwayPointsの要素数-1より大きいなら
                        currentRoot = 0;//currentRootを0にする
                    }
                }
                //次の目的場所を向く
                spawnedEnemy[enemy_num].transform.LookAt(wayPoints[currentRoot]);
                spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;//GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgentの情報を取得し目的地をposにする
                break;//switch文の各パターンの最後につける

            case 1://case1の場合
                enemyMoveDirect(enemy_num);//プレイヤーに向かって進む		
                break;//switch文の各パターンの最後につける
        }

        //敵の移動目的ポイントの変更
        //float Targetpoint = Random.Range(-3.0f, 3.0f);
        wayPoints[0] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y, Player.transform.position.z);
        wayPoints[1] = new Vector3(Player.transform.position.x, Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z);
        wayPoints[2] = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + Random.Range(-6.0f, 6.0f));
    }

    public void enemyMoveDirect(int enemy_num)
    {
        //敵がプレイヤーのほうを向く
        spawnedEnemy[enemy_num].transform.LookAt(Player.transform.position);

        // 変数 moveSpeed を乗算した速度でオブジェクトを前方向に移動する
        spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;
    }
}

