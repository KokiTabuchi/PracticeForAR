using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject[] weapon = new GameObject[2];

    private GameObject muzzle;
    private ParticleSystem ps;
    private GameObject shotLaser;

    //�ŏ��̓X�v���[
    private int WeaponNumber =0;

    //�{�^�����̂���
    [SerializeField]
    private GameObject weaponButton;
    //�A�C�R���̎�ސ�
    [SerializeField]
    private Sprite[] weaponIcon = new Sprite[2];
    //�{�^���̃A�C�R��
    private Image weaponButtonIcon;

    public bool isTouch;

    // Start is called before the first frame update
    void Start()
    {
        weaponButtonIcon = weaponButton.GetComponent<Image>();
        //�܂��̓X�v���[�𑕔�������Ԃɂ���
        ChangeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //�^�b�`�����Ԃ̎擾
        GetInputVector();
    }

    private void GetInputVector()
    {
        // Unity��ł̑���擾
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("�^�b�`�I");
                isTouch = true;
                Attack();
            }

            /*
            else if (Input.GetMouseButton(0))
            {

            }
            */

            else if (Input.GetMouseButtonUp(0))
            {
                isTouch = false;
                //�X�v���[��~����
                StartCoroutine(muzzleStop());
            }
            
        }

        // �[����ł̑���擾
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                //�w�Ń^�b�`
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("�^�b�`�I");
                    isTouch = true;
                    Attack();
                }

                /*�w�œ�����
                else if (touch.phase == TouchPhase.Moved)
                {

                }
                */

                //�w�������
                else if (touch.phase == TouchPhase.Ended)
                {
                    isTouch = false;
                    //�X�v���[��~����
                    StartCoroutine(muzzleStop());
                }
            }
        }
    }

    //�G���U��
    private void Attack()
    {
        /*���[�U�[�̐���
        shotLaser = Instantiate(laser, muzzle.transform);
        //���̂܂܂���muzzle���e�ɂȂ�̂ŁA�e�I�u�W�F�N�g����O��
        shotLaser.transform.parent = null;
        //���[�U�[���˕����̎w��
        Vector3 laserDirection = this.transform.rotation * Vector3.forward;
        //���̕�����AddForce�ő��x��t����
        shotLaser.GetComponent<Rigidbody>().AddForce(laserDirection, ForceMode.Impulse);
        */

        //�X�v���[���˂̃G�t�F�N�g�J�n
        muzzle.SetActive(true);
    }

    //�X�v���[��~����
    IEnumerator muzzleStop()
    {
        //�X�v���[���˂̃G�t�F�N�g�I��
        ps.Stop();
        //Particle��startLifeTime���҂�
        yield return new WaitForSeconds(ps.main.startLifetime.constant * 0.95f);

        //�O�̃p�[�e�B�N���ɑ΂���muzzleStop�����������o���̃p�[�e�B�N����ΏۂƂ���̂�h�����߂�if��
        if (isTouch == false)
        {
            //�Փ˔��������
            muzzle.SetActive(false);
        }

    }

    //����ύX�{�^�������������̏���
    public void WeaponButtonDown()
    {
        //�{�^���̐F��摜��ς��鏈��
        switch (WeaponNumber)
        {
            case 0:
                weaponButtonIcon.sprite = weaponIcon[0];
                break;

            case 1:
                weaponButtonIcon.sprite = weaponIcon[1];
                break;

            default:
                break;
        }

        ChangeWeapon();
    }

    //����ύX����
    private void ChangeWeapon()
    {
        //��U�S���̕����false�ɂ���
        for (int i = 0; i < 2; i++)
        {
            weapon[i].SetActive(false);
        }

        //WeaponNumber�ɑΉ����镐�킾��true�ɂ���
        weapon[WeaponNumber].SetActive(true);
        //�Y������̈�ԏ�̊K�w�ɂ���q�I�u�W�F�N�g��muzzle�Ƃ��Ď擾����
        muzzle = weapon[WeaponNumber].transform.GetChild(0).gameObject;
        //ps��muzzle�ɂ��Ă�ParticleSystem�ɕύX����
        ps = muzzle.GetComponent<ParticleSystem>();

        //�ŏ��͕��˂��Ȃ��悤�ɂ���
        muzzle.SetActive(false);

        //WeaponNumber�����Z���Ă���
        WeaponNumber++;
        if (WeaponNumber >= 2)
        {
            WeaponNumber = 0;
        }
    }
}
