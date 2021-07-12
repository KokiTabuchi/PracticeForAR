using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //
    public GameObject bee;
    public GameObject Player;
    public GameObject Comb;

    //�G�̃X�s�[�h
    public float moveSpeed = 1.0f;
    int move_pattern_rnd ;

    //�����_���ɓ����ۂ̖ڕW�n�_ 
    private Vector3[] wayPoints = new Vector3[5];
    int currentrout;


    //���̎�������Ƃ��̑�����̋���
    private Vector3 distanceFromTarget;
    //�@���݂̊p�x
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        //�G�̓����������_���Ɍ��߂� 
        move_pattern_rnd = Random.Range(1, 4);

        //�����_���ɓ����ڕW�n�_�����߂�
        currentrout = 0;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = new Vector3(Comb.transform.position.x + Random.Range(-5.0f,5.0f), 
                                       Comb.transform.position.y + Random.Range(-5.0f, 5.0f), 
                                       Comb.transform.position.z + Random.Range(-5.0f, 5.0f)
                                       );
        }

        //���̎�����ǂ̂��炢�̋����Ŕ�Ԃ������߂�
        distanceFromTarget = new Vector3(Comb.transform.position.x+(Comb.transform.localScale.x/2)+1.0f, 
                                        Comb.transform.position.y + (Comb.transform.localScale.y / 2) + 1.0f,
                                        Comb.transform.position.z + (Comb.transform.localScale.z / 2) + 1.0f
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

    //�_�C���N�g�Ƀv���C���[�̂ق��Ɍ������Ă���G    
    void movePatternOne()
    {
        //�G�̂ق��������Ĉ꒼���Ɍ������Ă���
        bee.transform.LookAt(Player.transform.position);
        bee.transform.position = bee.transform.position + bee.transform.forward * moveSpeed * Time.deltaTime;
    }

    //�����_���ɓ������G    
    void movePatternTwo()
    {
        Debug.Log(wayPoints[0]);
        Vector3 pos =  wayPoints[currentrout];
        //�ړI�n�ɋ߂��Ȃ����ꍇ 
        if (Vector3.Distance(bee.transform.position, pos) < 0.5f)
        {

            if (currentrout > wayPoints.Length - 1)
            {
                currentrout = 0;
            }
            else
            {
                currentrout += 1;//currentRoot��+1����
            }
        }
        bee.transform.LookAt(wayPoints[currentrout]);
        bee.transform.position = bee.transform.position + bee.transform.forward * moveSpeed * Time.deltaTime;
    }

    //���̎�������悤�ɉ��G
    void movePatternThird()
    {
        //�@�I�̈ʒu = ���̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
        transform.position = Comb.transform.position + Quaternion.Euler(0f, angle, 0f) * distanceFromTarget;
        //�@�I���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(Comb.transform.position.x, transform.position.y, Comb.transform.position.z), Vector3.up);
        //�@�I   �̊p�x��ύX
        angle += moveSpeed * Time.deltaTime;
        //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
        angle = Mathf.Repeat(angle, 360f);
    }
}
