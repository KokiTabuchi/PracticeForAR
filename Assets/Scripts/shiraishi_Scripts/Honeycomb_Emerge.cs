using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using DG.Tweening;

public class Honeycomb_Emerge : MonoBehaviour
{
    //�I�̑��̃Q�[���I�u�W�F�N�g
    public GameObject comb;

    //�I�̎E������ Killed_Enemy_Count�N���X�̃C���X�^���X�ϐ�
    Killed_Enemy_Count killed_enemy_num = new Killed_Enemy_Count();

    //�I���E�����瑃���o�����邽�߂̈�����
    const int killedEnemy_upper_limit = 0;

    //�I�̑��̈ʒu��������
    float honeycomb_pos_x = 0.0f;
    float honeycomb_pos_y = 0.0f;
    float honeycomb_pos_z = 0.0f;

    //���̑���\�������邩�ǂ���
     private bool isComb = false;

    public void Start()
    {
        comb.SetActive(false);//�I�̑����\���ɂ���
        setHonneyComb();//�I�̑��I�u�W�F�N�g��\������ꏊ�������_���Ō���
        
    }
    public void Update()
    {
       
        //getkilled_Enemy���\�b�h�œ|�����G�l�~�[�̐����������A���������������Ɣ�r����
        if ((killed_enemy_num.getKilled_EnemyNum()) >= killedEnemy_upper_limit)
        {
            if (isComb == false)
            {
                Emerge_Comb();
            }
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
}