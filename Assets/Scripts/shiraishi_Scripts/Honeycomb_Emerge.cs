using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using DG.Tweening;
using UnityEngine.AI;//NavMeshagent���g�����߂ɋL�q����

public class Honeycomb_Emerge : MonoBehaviour
{
    //�Q�[���̃v���C���[�̃I�u�W�F�N�g  
    public GameObject Player;
    //�I�̑��̃Q�[���I�u�W�F�N�g
    public GameObject comb;

    //�I�̎E������ Killed_Enemy_Count�N���X�̃C���X�^���X�ϐ�
    Killed_Enemy_Count killed_enemy_num = new Killed_Enemy_Count();
    GameSceneManager killed_Manager  = new GameSceneManager();

    //�I���E�����瑃���o�����邽�߂̈�����
    const int killedEnemy_upper_limit = 0;

    //�I�̑��̈ʒu��������
    float honeycomb_pos_x = 0.0f;
    float honeycomb_pos_y = 0.0f;
    float honeycomb_pos_z = 0.0f;

    //���̑���\�������邩�ǂ���
     private bool isComb = false;

    //�o��������I�̃I�u�W�F�N�g 
    [SerializeField]
    private GameObject[] bee = new GameObject[10];//�Ƃ肠�����P�O�̏o�Ă���悤�ɐݒ�
    private GameObject[] spawnedEnemy = new GameObject[10];//�X�|�[�����ꂽ�Ƃ��Ɋi�[��������

    //�G�̐����̎��ԂƃC���^�[�o��
    float timer = 0.0f;
    float enemyInterval = 5.0f;


    Vector3[] wayPoints = new Vector3[3];//�p�j����|�C���g�̍��W��������Vector3�^�̕ϐ���z��ō��
    private int currentRoot=0;//���ݖڎw���|�C���g��������ϐ�
    private int Mode;//�G�̍s���p�^�[���𕪂��邽�߂̕ϐ�
    //public Transform player;//�v���C���[�̈ʒu���擾���邽�߂�Transform�^�̕ϐ�
    //public Transform enemypos;//�G�̈ʒu���擾���邽�߂�Transform�^�̕ϐ�
    //private NavMeshAgent agent;//NavMeshAgent�̏����擾���邽�߂�Navmeshagent�^�̕ϐ�

    //�G�̑���
    float moveSpeed = 1.0f;

    public void Start()
    {
        //Player = GameObject.Find("Player");
       // agent = GetComponent<NavMeshAgent>();//NavMeshAgent�̏���agent�ɑ��
        comb.SetActive(false);//�I�̑����\���ɂ���
        setHonneyComb();//�I�̑��I�u�W�F�N�g��\������ꏊ�������_���Ō���

        //�G�̈ړ��ړI�|�C���g�R�ݒ�
        wayPoints[0] = new Vector3(Player.transform.position.x + 7.0f, Player.transform.position.y, Player.transform.position.z);
        wayPoints[1] = new Vector3(Player.transform.position.x, Player.transform.position.y+7.0f, Player.transform.position.z);
        wayPoints[2] = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z+7.0f);
    }
    public void Update()
    {
        timer += Time.deltaTime;

      
        //getkilled_Enemy���\�b�h�œ|�����G�l�~�[�̐����������A���������������Ɣ�r����
        if ((killed_enemy_num.getKilled_EnemyNum()) >= killedEnemy_upper_limit)
        {
            if (isComb == false)
            {
                Emerge_Comb();
            }
            //��莞�ԂœG�𐶐�
            //if (timer > enemyInterval)
            //{
                timer = 0.0f;
                emergeBee();//�I�𔭐������郁�\�b�h�̌Ăяo��
            //}
           
        }

    }

    //���𔭐������邽��boolean��true�ɕύX���郁�\�b�h
    public void Emerge_Comb()
    {

        Debug.Log(killed_enemy_num.getKilled_EnemyNum());
        Debug.Log(isComb);
        //����\������
        comb.SetActive(true);
        isComb = true;
    }

    //�I�̑��I�u�W�F�N�g��\������ꏊ�������_���Ō���
    public void setHonneyComb()
    {
        //����\������ꏊ�𗐐��Ō���
        honeycomb_pos_x = Random.Range(-10.0f, 10.0f);
        honeycomb_pos_y = Random.Range(-10.0f, 10.0f);
        honeycomb_pos_z = Random.Range(6.0f, 15.0f);
        comb.transform.position = new Vector3( honeycomb_pos_x, honeycomb_pos_y, honeycomb_pos_z);
    }

    //������I�𔭐�������X�N���v�g
    public void emergeBee()
    {
        //�I�𐶐�����(���͔z�񕶂����ŁA�ꏊ�͖I�̑��̊O������
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

    //�G��p�j����悤�ɓ������X�N���v�g
    public void enemyMoveRandom(int enemy_num)
    {
        Debug.Log("�ړ�");
        Vector3 pos = wayPoints[currentRoot];//Vector3�^��pos�Ɍ��݂̖ړI�n�̍��W����
        float distance = Vector3.Distance(spawnedEnemy[enemy_num].transform.position, Player.transform.position);//�G�ƃv���C���[�̋��������߂�

        if (distance > 3)
        {//�����v���C���[�ƓG�̋�����5�ȏ�Ȃ�
            Mode = 0;//Mode��0�ɂ���
        }

        if (distance < 3)
        {//�����v���C���[�ƓG�̋�����5�ȉ��Ȃ�
            Mode = 1;//Mode��1�ɂ���
        }

        switch (Mode)
        {//Mode�̐؂�ւ���

            case 0://case0�̏ꍇ

                if (Vector3.Distance(spawnedEnemy[enemy_num].transform.position, pos) < 1f)
                {//�����G�̈ʒu�ƌ��݂̖ړI�n�Ƃ̋�����1�ȉ��Ȃ�
                    currentRoot += 1;//currentRoot��+1����
                    if (currentRoot > wayPoints.Length - 1)
                    {//����currentRoot��wayPoints�̗v�f��-1���傫���Ȃ�
                        currentRoot = 0;//currentRoot��0�ɂ���
                    }
                }
                //���̖ړI�ꏊ������
                spawnedEnemy[enemy_num].transform.LookAt(wayPoints[currentRoot]);
                spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;//GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgent�̏����擾���ړI�n��pos�ɂ���
                break;//switch���̊e�p�^�[���̍Ō�ɂ���

            case 1://case1�̏ꍇ
                enemyMoveDirect(enemy_num);//�v���C���[�Ɍ������Đi��		
                break;//switch���̊e�p�^�[���̍Ō�ɂ���
        }

        //�G�̈ړ��ړI�|�C���g�̕ύX
        //float Targetpoint = Random.Range(-3.0f, 3.0f);
        wayPoints[0] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y, Player.transform.position.z);
        wayPoints[1] = new Vector3(Player.transform.position.x, Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z);
        wayPoints[2] = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z + Random.Range(-6.0f, 6.0f));
    }

    public void enemyMoveDirect(int enemy_num)
    {
        //�G���v���C���[�̂ق�������
        spawnedEnemy[enemy_num].transform.LookAt(Player.transform.position);

        // �ϐ� moveSpeed ����Z�������x�ŃI�u�W�F�N�g��O�����Ɉړ�����
        spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;
    }
}

