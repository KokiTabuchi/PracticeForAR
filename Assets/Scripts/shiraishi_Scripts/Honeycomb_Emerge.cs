using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Honeycomb_Emerge : MonoBehaviour
{
    //�Q�[���̃v���C���[�̃I�u�W�F�N�g  
    public GameObject player;
    //�I�̑��̃Q�[���I�u�W�F�N�g
    public GameObject comb;

    //GameSceneMaanger���擾
    public GameObject gameSceneManager;
    private GameSceneManager manager;

    //�I�̎E������ Killed_Enemy_Count�N���X�̃C���X�^���X�ϐ�
    Killed_Enemy_Count killed_enemy_num = new Killed_Enemy_Count();
    GameSceneManager killed_Manager = new GameSceneManager();

    //

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
    float enemyInterval = 3.0f;

    //�G�̑���
    float moveSpeed = 0.4f;

    public void Start()
    {
        comb.SetActive(false);//�I�̑����\���ɂ���
        setHonneyComb();//�I�̑��I�u�W�F�N�g��\������ꏊ�������_���Ō���

        manager = gameSceneManager.GetComponent<GameSceneManager>();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        //GameSceneManager�ŋK��l����̏㉺�𔻕ʂ���A�����Ă܂������o�Ă��Ȃ����Ƃ��m�F����
        if (manager.honeyComb == true && isComb == false)
        {
            //�����o��������
            Emerge_Comb();

            timer = 0.0f;
            emergeBee();//�I�𔭐������郁�\�b�h�̌Ăяo��
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
        honeycomb_pos_y = Random.Range(4.0f, 6.0f);
        honeycomb_pos_z = Random.Range(6.0f, 15.0f);
        comb.transform.position = new Vector3( honeycomb_pos_x, honeycomb_pos_y, honeycomb_pos_z);
    }

    //������I�𔭐�������X�N���v�g
    public void emergeBee()
    {
        Debug.Log("�o��");
        //�I�𐶐�����(���͔z�񕶂����ŁA�ꏊ�͖I�̑��̊O������
        for (int i = 0; i< bee.Length; i++)
        {
            if (!spawnedEnemy[i])
            {
               
                spawnedEnemy[i] = Instantiate(bee[i],
                        new Vector3(honeycomb_pos_x + (comb.transform.localScale.x / 2)*Random.Range(-5.0f,5.0f),
                                    honeycomb_pos_y + comb.transform.localScale.y / 2,
                                    honeycomb_pos_z + comb.transform.localScale.z / 2
                                    ),
                         Quaternion.LookRotation(player.transform.position));
                spawnedEnemy[i].transform.LookAt(player.transform.position);
            }
        }

        for(int i = 0; i < spawnedEnemy.Length; i++)
        {
                //enemyMoveRandom(i);
        }
    }

   
}

