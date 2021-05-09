using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using DG.Tweening;

public class SampleSceneManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    [SerializeField]
    Camera gameCamera;

    // �t���b�N�ŏ��ړ�����
    [SerializeField]
    private Vector2 FlickMinRange = new Vector2(500.0f, 500.0f);

    // �X���C�v�ŏ��ړ�����
    [SerializeField]
    private Vector2 SwipeMinRange = new Vector2(50.0f, 50.0f);

    // �X���C�v���͋���
    private Vector2 SwipeRange;

    // ���͕����L�^�p
    private Vector2 InputSTART;
    private Vector2 InputMOVE;
    private Vector2 InputEND;

    [SerializeField]
    private GameObject monsterBall;
    [SerializeField]
    private GameObject ballPos;
    private GameObject createdBall;

    Rigidbody rb;

    //��]�̑O��t���O
    bool positiveSpin;
    //�������t���O
    bool isThrowing;

    float time;
    float holdTime = 2f;

    //�t���b�N����̃}�W�b�N�i���o�[
    float throwHeight = 2f;
    float powerPerPixel = 0.005f;
    float sensitivity = 0.001f;

    //�X���C�v����̕ϐ�
    Vector3 lastFingerPos;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
        BallDoTween();

        isThrowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;       

        if (isThrowing == false)
        {
            if (time > holdTime)
            {
                BallDoTween();
                time = 0f;
            }
        }


        GetInputVector();
    }

    // ���͂̎擾
    private void GetInputVector()
    {
        // Unity��ł̑���擾
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("�^�b�`�I");
                isThrowing = true;
                InputSTART = Input.mousePosition;
                createdBall.transform.DOPause();
            }
            else if (Input.GetMouseButton(0))
            {
                InputMOVE = Input.mousePosition;
                SwipeCLC();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                InputEND = Input.mousePosition;
                FlickCLC();
            }
        }

        // �[����ł̑���擾
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                isThrowing = true;

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("�^�b�`�I");
                    InputSTART = touch.position;
                    lastFingerPos = InputSTART;
                    createdBall.transform.DOPause();
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    InputMOVE = touch.position;
                    SwipeCLC();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    InputEND = touch.position;
                    FlickCLC();
                }
            }
        }
    }

    // ���͓��e����t���b�N�������v�Z
    private void FlickCLC()
    {
        Vector2 _work = new Vector2(InputEND.x - InputSTART.x, InputEND.y - InputSTART.y);

        if (_work.y <= FlickMinRange.y)
        {
            createdBall.transform.position = this.transform.position;
            createdBall.transform.LookAt(ballPos.transform);

            isThrowing = false;
            return;
        }
        else
        {
            Debug.Log("�t���b�N");

            float throwPower = _work.y * powerPerPixel;
            float throwDirection = _work.x * sensitivity;

            //throwPower�̏��
            if (throwPower > 1)
            {
                throwPower = 1f;
            }

            Vector3 ballThrowing = gameCamera.transform.rotation * Vector3.forward * 1.5f;
            //ballThrowing.x += throwDirection;
            //ballThrowing.y += throwHeight;
            ballThrowing += new Vector3(throwDirection, throwHeight, 0);

            //�d�͕t���Ɠ���
            rb.useGravity = true;
            rb.AddForce(ballThrowing, ForceMode.Impulse);

            //������̏���
            StartCoroutine(afterThrow());
        }
    }

    // ���͓��e����X���C�v�������v�Z
    private void SwipeCLC()
    {
        SwipeRange = new Vector2(InputMOVE.x - InputSTART.x, InputMOVE.y - InputSTART.y);

        if (SwipeRange.x <= SwipeMinRange.x && SwipeRange.y <= SwipeMinRange.y)
        {
            return;
        }
        else
        {
            Debug.Log("�X���C�v");

            /*
            Vector3 nowFingerPos = gameCamera.ScreenToWorldPoint(InputMOVE);
            Vector3 diffPos = nowFingerPos - lastFingerPos;
            diffPos.z = 0;

            Debug.Log(nowFingerPos);

            //���ǂ̗]�n�L��
            createdBall.transform.position = nowFingerPos;
            */
        }
    }

    /*NONE�Ƀ��Z�b�g
    private void ResetParameter()
    {
        NoneCountNow++;
        if (NoneCountNow >= NoneCountMax)
        {
            NoneCountNow = 0;
            NowFlick = FlickDirection.NONE;
            NowSwipe = SwipeDirection.NONE;
            SwipeRange = new Vector2(0, 0);
        }
    }

    // �t���b�N�����̎擾
    public FlickDirection GetNowFlick()
    {
        return NowFlick;
    }

    // �X���C�v�����̎擾
    public SwipeDirection GetNowSwipe()
    {
        return NowSwipe;
    }

    // �X���C�v�ʂ̎擾
    public float GetSwipeRange()
    {
        if (SwipeRange.x > SwipeRange.y)
        {
            return SwipeRange.x;
        }
        else
        {
            return SwipeRange.y;
        }
    }

    // �X���C�v�ʂ̎擾
    public Vector2 GetSwipeRangeVec()
    {
        if (NowSwipe != SwipeDirection.NONE)
        {
            return new Vector2(InputMOVE.x - InputSTART.x, InputMOVE.y - InputSTART.y);
        }
        else
        {
            return new Vector2(0, 0);
        }
    }
    */

    //��������̏���
    private IEnumerator afterThrow()
    {
        yield return new WaitForSeconds(2f);

        Destroy(createdBall);
        SpawnBall();
    }

    //�{�[����������
    private void SpawnBall()
    {
        createdBall = Instantiate(monsterBall, this.transform);
        createdBall.transform.SetParent(gameCamera.transform, true);
        createdBall.transform.LookAt(ballPos.transform);
        rb = createdBall.GetComponent<Rigidbody>();
        isThrowing = false;
    }

    //�{�[���̑ҋ@�A�j���[�V����
    private void BallDoTween()
    {
        //�c�ړ��̕\��
        createdBall.transform.DOLocalPath(
            new[]
            {
                new Vector3(0f, -0.02f, 0f),
                new Vector3(0f, 0.12f, 0f),
                new Vector3(0f, -0.11f, 0f)
            },
            1f, PathType.Linear)
            .SetOptions(true)
            .SetRelative(true);

        //��]�̕\��
        if (positiveSpin == true)
        {
            createdBall.transform.DOLocalRotate(new Vector3(720f, 0, 0), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .SetRelative(true);

            positiveSpin = false;
        }
        else
        {
            createdBall.transform.DOLocalRotate(new Vector3(-720f, 0, 0), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .SetRelative(true);

            positiveSpin = true;
        }
    }

    private void ButtonDown()
    {
        Debug.Log("�{�^���I");
    }
}
