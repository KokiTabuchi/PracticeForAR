using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using Doozy.Engine;

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
    //���Ԑ���
    private float timeLimit = 10f;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private float score;

    public int killedCounter;
    public bool getScore;

    //�����o�����邽�߂ɕK�v�ȖI�̐�����
    const int killedEnemy_upper_limit = 2;
    //�N���A�ɕK�v�ȖI�̐�����
    const int killEnemyComplete = 5;
    public bool honeyComb = false;

    //�G�ʒu�����p�ϐ�
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
        killedCounter = 0;
        score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //��莞�ԂœG�𐶐�
        if (timer > enemyInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }

        //�G�̌���
        for (int i = 0; i < 5; i++)
        {
            if (spawnedEnemy[i] != null)
            {
                spawnedEnemy[i].transform.LookAt(heart.transform.position);
            }
        }

        //�G�ɕ��킪���������Ƃ��̏���
        if (getScore == true)
        {
            StartCoroutine(ScoreAnimation(100f, 1f));
            killedCounter += 1;
            getScore = false;
        }

        //�I�̑����o�������邩�ǂ����̏���
        if (killedCounter >= killedEnemy_upper_limit)
        {
            //�����Honeycomb_Emerge�X�N���v�g�ɓn��
            honeyComb = true;
            //�J��Ԃ������̒�~��Honeycomb_Emergw�X�N���v�g������Ă���Ă�̂ŕs�v
        }

        //�N���A����
        if (killedCounter >= killEnemyComplete)
        {
            GameEventMessage.SendEvent("ClearScene");
        }

        //�Q�[���I�[�o�[����
        if (timeLimit - timer <= 0f)
        {
            GameEventMessage.SendEvent("GameOverScene");
        }
    }

    //�G�̐���
    private void SpawnEnemy()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < 5; i++)
        {
            if(!spawnedEnemy[i])
            {
                //�G�̐���
                spawnedEnemy[i] = Instantiate(enemy[Random.Range(0, 5)]);
                //�����_���ʒu�ɐ���
                spawnedEnemy[i].transform.position = GetRandomPosition();
                //�����̒���
                spawnedEnemy[i].transform.LookAt(heart.transform.position);
                return;
            }
        }
    }

    //�����_���Ȉʒu�𐶐�����֐�
    private Vector3 GetRandomPosition()
    {
        theta = Random.Range(minTheta, maxTheta);
        
        //���ꂼ��̍��W�������_���ɐ�������
        float x = enemyDistance * Mathf.Cos(theta);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = enemyDistance * Mathf.Sin(theta);

        //Vector3�^��Position��Ԃ�
        return new Vector3(x, y, z);
    }

    // �X�R�A���A�j���[�V����������
    IEnumerator ScoreAnimation(float addScore, float time)
    {
        //�O��̃X�R�A
        float befor = score;
        //����̃X�R�A
        float after = score + addScore;
        //���_���Z
        score += addScore;
        //0f���o�ߎ��Ԃɂ���
        float elapsedTime = 0f;

        //time��0�ɂȂ�܂Ń��[�v������
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            //�e�L�X�g�̍X�V
            scoreText.text = (befor + (after - befor) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;
            //0.01�b�҂�
            yield return new WaitForSeconds(0.01f);
        }

        //�ŏI�I�Ȓ��n�̃X�R�A
        scoreText.text = after.ToString();
    }
}
