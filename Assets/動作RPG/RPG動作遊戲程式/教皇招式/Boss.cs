using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // �w�q�ޯ�M��
    public enum BossSkill
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4
    }

    // �w�q�ƥ�A�� Boss �ϥΧޯ��Ĳ�o
    public event Action<BossSkill> OnSkillUsed;

    // �H���ƥͦ���
    private System.Random random = new System.Random();

    public float IntervalTime;
    public float skillTime;

    public float skill1Interval = 5f;
    public float skill2Interval = 8f;
    public float skill3Interval = 10f;
    public float skill4Interval = 10f;

    public bool isSkill1CanUse = true;
    public bool isSkill2CanUse =true;
    public bool isSkill3CanUse = true;
    public bool isSkill4CanUse = true;

    public bool canUseSkill = true;


    public bool stop;
    private void Awake()
    {
       // UseRandomSkill();
    }

    private void Start()
    {
        //InvokeRepeating("UseRandomSkill", 0f, 5f);
    }


    private void Update()
    {

        if (!stop)
        {


            IntervalTime += Time.deltaTime;
            if (IntervalTime > skillTime - 3)
            {
                UseRandomSkill();
                IntervalTime = 0;
            }
        }
    }
    // Boss �ϥΧޯ઺���
    public void UseRandomSkill()
    {

        canUseSkill = true;


        // �q�ޯ�M�椤�H����ܤ@�ӧޯ�
        BossSkill randomSkill = (BossSkill)random.Next(Enum.GetNames(typeof(BossSkill)).Length);

        // Ĳ�o�ޯ�ϥΨƥ�
        

        // �b�o�̥i�H�K�[�������ޯ��޿�A�Ҧp����ʵe�B�y���ˮ`��
        

        switch (randomSkill)
        {
            case BossSkill.Skill1:
                if (!isSkill1CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case BossSkill.Skill2:
                if (!isSkill2CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case BossSkill.Skill3:
                if (!isSkill3CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case BossSkill.Skill4:
                if (!isSkill4CanUse)
                {
                    canUseSkill = false;
                }
                break;
        }
        if (canUseSkill)
        {
            OnSkillUsed?.Invoke(randomSkill);
        }
        else if(!canUseSkill)
        {
            print("�L�k�ϥ�" + randomSkill + "�ޯ�");
            
        }
        
        /*
        switch (randomSkill)
        {
            
            case BossSkill.Skill1:
                CancelInvoke("UseRandomSkill"); // �������e�� InvokeRepeating
                InvokeRepeating("UseRandomSkill", skill1Interval, skill1Interval);
                break;
            case BossSkill.Skill2:
                CancelInvoke("UseRandomSkill");
                InvokeRepeating("UseRandomSkill", skill2Interval, skill2Interval);
                break;
            case BossSkill.Skill3:
                CancelInvoke("UseRandomSkill");
                InvokeRepeating("UseRandomSkill", skill3Interval, skill3Interval);
                break;
            case BossSkill.Skill4:
                CancelInvoke("UseRandomSkill");
                InvokeRepeating("UseRandomSkill", skill4Interval, skill4Interval);
                break;
            

        }
        */
    }
    public IEnumerator SkillCanUse(int skillNum)
    {
        switch (skillNum)
        {
            case 1:
                isSkill1CanUse = false;
                yield return new WaitForSeconds(skill1Interval);
                isSkill1CanUse = true;
                break;
            case 2:
                isSkill2CanUse = false;
                yield return new WaitForSeconds(skill2Interval);
                isSkill2CanUse = true;
                break;
            case 3:
                isSkill3CanUse = false;
                yield return new WaitForSeconds(skill3Interval);
                isSkill3CanUse = true;
                break;
            case 4:
                isSkill4CanUse = false;
                yield return new WaitForSeconds(skill4Interval);
                isSkill4CanUse = true;
                break;
        }
    }
}
