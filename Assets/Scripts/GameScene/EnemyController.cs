using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public GameObject gameSceneManager;
    public GameSceneManager manager;

    private float moveSpeed = 1f;
    private float enemySpeed = 5f;

    bool right, left;
    private float t;
    [SerializeField]
    private float a,b;

    private float animationX = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameSceneManager = GameObject.Find("GameSceneManager");
        manager = gameSceneManager.GetComponent<GameSceneManager>();
        //Animation();
        right = true;
        left = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        t = Random.Range(a, b);
        Debug.Log(Random.Range(a,b));
        // 変数 moveSpeed を乗算した速度でオブジェクトを前方向に移動する
        this.transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (right)
        {
            this.transform.position += transform.right * enemySpeed * Time.deltaTime;
            
            Invoke("Right", 2);
        }
        if (left)
        {
            this.transform.position -= transform.right * enemySpeed * Time.deltaTime;
            
            Invoke("Left", 2);
        }
        
    }

    private void Right()
    {
        right = false;
        left = true;
    }

    private void Left()
    {
        left = false;
        right = true;
    }
        

    private void OnCollisionEnter(Collision collision)
    {
        //Playerにぶつかったときの挙動
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("うっ...！");
            Destroy(this.gameObject);
        }

        //Laserにぶつかったときの挙動
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
            manager.getScore = true;

            Destroy(this.gameObject);
        }
    }

    //揺れのアニメーション（未完成）
    private void Animation()
    {
        this.transform.DOLocalRotate(new Vector3(10f, 0, 0),1f)
            .SetEase(Ease.InOutQuart)
            .SetLoops(-1,LoopType.Yoyo)
            .SetRelative(true);
    }
}
