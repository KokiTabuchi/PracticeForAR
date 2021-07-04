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
    private Animator animator;
    private bool is_walking;

    bool right, left;
    private float t;
    //[SerializeField]
    //private float a,b;

    private float animationX = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //gameSceneManager = GameObject.Find("GameSceneManager");
        //manager = gameSceneManager.GetComponent<GameSceneManager>();
        //Animation();
        animator = GetComponent<Animator>();
        animator.SetTrigger("is_walking");

        right = true;
        left = false;
        t = Random.Range(2.0f, 3.0f);
        Debug.Log(t);

    }

    // Update is called once per frame
    void Update()
    {


        // �ϐ� moveSpeed ����Z�������x�ŃI�u�W�F�N�g��O�����Ɉړ�����
        //this.transform.position += transform.forward * moveSpeed * Time.deltaTime;
        animator.SetFloat("Blend_Walk_Backward_Forward", 0.1f);

        if (right)
        {
            //this.transform.position += transform.right * enemySpeed * Time.deltaTime;
            animator.SetFloat("Blend_Walk_Left_Right", 1);
            Invoke("Right", t);
        }
        if (left)
        {
            //this.transform.position -= transform.right * enemySpeed * Time.deltaTime;
            animator.SetFloat("Blend_Walk_Left_Right", -1);
            Invoke("Left", t);
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
        //Player�ɂԂ������Ƃ��̋���
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("����...�I");
            Destroy(this.gameObject);
            //animator.SetBool("", true);
        }

        //Laser�ɂԂ������Ƃ��̋���
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
            manager.getScore = true;

            Destroy(this.gameObject);
        }
    }

    //�h��̃A�j���[�V�����i�������j
    private void Animation()
    {
        this.transform.DOLocalRotate(new Vector3(10f, 0, 0), 1f)
            .SetEase(Ease.InOutQuart)
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative(true);
    }
}
