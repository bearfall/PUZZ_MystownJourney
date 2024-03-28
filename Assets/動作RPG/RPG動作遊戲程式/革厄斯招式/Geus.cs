using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geus : MonoBehaviour
{
    public Transform attakTarget;

    public enum GeusSkill
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4
    }

    // �w�q�ƥ�A�� Boss �ϥΧޯ��Ĳ�o
    public event Action<GeusSkill> OnSkillUsed;

    // �H���ƥͦ���
    private System.Random random = new System.Random();

    public float IntervalTime;
    public float skillTime;

    public float skill1Interval = 5f;
    public float skill2Interval = 8f;
    public float skill3Interval = 10f;
    public float skill4Interval = 10f;

    public bool isSkill1CanUse = true;
    public bool isSkill2CanUse = true;
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
            if (IntervalTime > skillTime + 0.3f)
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
        GeusSkill randomSkill = (GeusSkill)random.Next(Enum.GetNames(typeof(GeusSkill)).Length);

        // Ĳ�o�ޯ�ϥΨƥ�


        // �b�o�̥i�H�K�[�������ޯ��޿�A�Ҧp����ʵe�B�y���ˮ`��


        switch (randomSkill)
        {
            case GeusSkill.Skill1:
                if (!isSkill1CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case GeusSkill.Skill2:
                if (!isSkill2CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case GeusSkill.Skill3:
                if (!isSkill3CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case GeusSkill.Skill4:
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

        else if (!canUseSkill)
        {
            print("�L�k�ϥ�" + randomSkill + "�ޯ�");
            skillTime = 0.1f;
            //IntervalTime = skillTime - 1;
        }

    }



    public void StartGeusAction()
    {
        stop = false;
        IntervalTime = 0;
        skillTime = 0;

    }
    public void StopGeusAction()
    {
        stop = true;
        IntervalTime = 0;
        skillTime = 0;

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
