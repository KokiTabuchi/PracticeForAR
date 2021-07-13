using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //
    public GameObject bee;
    public GameObject Player;
    public GameObject Comb;

    //敵のスピード
    public float moveSpeed = 1.0f;
    int move_pattern_rnd ;

    //ランダムに動く際の目標地点 
    private Vector3[] wayPoints = new Vector3[5];
    int currentrout;


    //巣の周りを回るときの巣からの距離
    private Vector3 distanceFromTarget;
    //　現在の角度
    private float angle_x=0.0f;
    private float angle_y = 0.0f;
    private float angle_z = 0.0f;
    //蜂の回転する速さ
    float rotateSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        //敵の動きをランダムに決める 
        move_pattern_rnd = Random.Range(1, 4);

        //ランダムに動く目標地点を決める
        currentrout = 0;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = new Vector3(Comb.transform.position.x + Random.Range(-5.0f,5.0f), 
                                       Comb.transform.position.y + Random.Range(-5.0f, 5.0f), 
                                       Comb.transform.position.z + Random.Range(-5.0f, 5.0f)
                                       );
        }

        //巣の周りをどのくらいの距離で飛ぶかを決める
        distanceFromTarget = new Vector3((Comb.transform.localScale.x/2)+2.0f, 
                                         (Comb.transform.localScale.y / 2) - 2.0f,
                                          (Comb.transform.localScale.z / 2) + 2.0f
                                        );
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
            default:
                break;
        }
    }

    //ダイレクトにプレイヤーのほうに向かってくる敵    
    void movePatternOne()
    {
        //敵のほうを向いて一直線に向かってくる
        bee.transform.LookAt(Player.transform.position);
        bee.transform.position = bee.transform.position + bee.transform.forward * moveSpeed * Time.deltaTime;
    }

    //ランダムに動き回る敵    
    void movePatternTwo()
    {
        Debug.Log(wayPoints[0]);
        Vector3 pos =  wayPoints[currentrout];
        //目的地に近くなった場合 
        if (Vector3.Distance(bee.transform.position, pos) < 0.5f)
        {

            if (currentrout > wayPoints.Length - 2)
            {
                currentrout = 0;
            }
            else
            {
                currentrout += 1;//currentRootを+1する
            }
        }
        bee.transform.rotation = Quaternion.LookRotation(wayPoints[currentrout]);
        bee.transform.position = bee.transform.position + bee.transform.forward * moveSpeed * Time.deltaTime;
    }

    //巣の周りを守るように回る敵
    void movePatternThird()
    {
        //蜂の位置　＝　巣の位置　＋　巣の位置と蜂のある位置の角度　×　巣からユニットまでのベクトル
        //　蜂の位置 = 巣の位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
        bee.transform.position = Comb.transform.position + Quaternion.Euler(angle_x, angle_y, angle_z) * distanceFromTarget;
        //　蜂自身の角度 = 巣から見たユニットの方向の角度を計算しそれを蜂の角度に設定する
        bee.transform.rotation = Quaternion.LookRotation(bee.transform.position - new Vector3(Comb.transform.position.x, Comb.transform.position.y, Comb.transform.position.z), Vector3.up);
        bee.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        //　蜂   の角度を変更
        angle_x += rotateSpeed * Time.deltaTime;
        angle_y += rotateSpeed * Time.deltaTime;
        angle_z += rotateSpeed * Time.deltaTime;
        //　角度を0～360度の間で繰り返す
        angle_x = Mathf.Repeat(angle_y, 360f);
        angle_y = Mathf.Repeat(angle_y, 360f);
        angle_z = Mathf.Repeat(angle_y, 360f);
    }
}
