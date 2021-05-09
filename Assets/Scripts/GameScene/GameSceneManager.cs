using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemy = new GameObject[5];
    private GameObject[] spawnedEnemy = new GameObject[5];

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject heart;

    private float timer;
    private float enemyInterval = 3f;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private float score;

    public bool getScore;

    //敵位置生成用関数
    private float theta;
    private float minTheta = -180f;
    private float maxTheta = 180f;
    private float yMinPosition = 4f;
    private float yMaxPosition = 8f;

    private float enemyDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > enemyInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }

        if (getScore == true)
        {
            StartCoroutine(ScoreAnimation(100f, 1f));
            getScore = false;
        }

        //敵の向き
        for (int i = 0; i < 5; i++)
        {
            if (spawnedEnemy[i] != null)
            {
                spawnedEnemy[i].transform.LookAt(heart.transform.position);
            }
        }
    }

    //敵の生成
    private void SpawnEnemy()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < 5; i++)
        {
            if(!spawnedEnemy[i])
            {
                //敵の生成
                spawnedEnemy[i] = Instantiate(enemy[Random.Range(0, 5)]);
                //ランダム位置に生成
                spawnedEnemy[i].transform.position = GetRandomPosition();
                //向きの調製
                spawnedEnemy[i].transform.LookAt(heart.transform.position);
                return;
            }
        }
    }

    //ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        theta = Random.Range(minTheta, maxTheta);
        
        //それぞれの座標をランダムに生成する
        float x = enemyDistance * Mathf.Cos(theta);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = enemyDistance * Mathf.Sin(theta);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }

    // スコアをアニメーションさせる
    IEnumerator ScoreAnimation(float addScore, float time)
    {
        //前回のスコア
        float befor = score;
        //今回のスコア
        float after = score + addScore;
        //得点加算
        score += addScore;
        //0fを経過時間にする
        float elapsedTime = 0f;

        //timeが0になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            //テキストの更新
            scoreText.text = (befor + (after - befor) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;
            //0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }

        //最終的な着地のスコア
        scoreText.text = after.ToString();
    }
}
