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
   //�G��p�j����悤�ɓ������X�N���v�g
   public void enemyMoveRandom(int enemy_num)
   {
       Debug.Log("�ړ�");
       //wayPoints[0] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y+ Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
       Vector3 pos = wayPoints2[enemy_num][currentRoot];//Vector3�^��pos�Ɍ��݂̖ړI�n�̍��W����
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
                   if (currentRoot > wayPoints2[enemy_num].Length - 1)
                   {//����currentRoot��wayPoints�̗v�f��-1���傫���Ȃ�
                       currentRoot = 0;//currentRoot��0�ɂ���
                   }
               }
               //���̖ړI�ꏊ������
               spawnedEnemy[enemy_num].transform.LookAt(wayPoints2[enemy_num][currentRoot]);
               spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;//GetComponent<NavMeshAgent>().SetDestination(pos);//NavMeshAgent�̏����擾���ړI�n��pos�ɂ���
               break;//switch���̊e�p�^�[���̍Ō�ɂ���

           case 1://case1�̏ꍇ
               enemyMoveDirect(enemy_num);//�v���C���[�Ɍ������Đi��		
               break;//switch���̊e�p�^�[���̍Ō�ɂ���
       }

       //�G�̈ړ��ړI�|�C���g�̕ύX
       //float Targetpoint = Random.Range(-3.0f, 3.0f);
     //  wayPoints[0] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
      // wayPoints[1] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
      // wayPoints[2] = new Vector3(Player.transform.position.x + Random.Range(-6.0f, 6.0f), Player.transform.position.y + Random.Range(-6.0f, 6.0f), Player.transform.position.z + Random.Range(-6.0f, 6.0f));
   }

   public void enemyMoveDirect(int enemy_num)
   {
       //�G���v���C���[�̂ق�������
       spawnedEnemy[enemy_num].transform.LookAt(Player.transform.position);

       // �ϐ� moveSpeed ����Z�������x�ŃI�u�W�F�N�g��O�����Ɉړ�����
       spawnedEnemy[enemy_num].transform.position = spawnedEnemy[enemy_num].transform.position + spawnedEnemy[enemy_num].transform.forward * moveSpeed * Time.deltaTime;
   }
   */
}
