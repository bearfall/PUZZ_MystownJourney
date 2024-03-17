using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizlow : MonoBehaviour
{
    public Transform attakTarget;

    public enum WizlowSkill
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4
    }

    // 定義事件，當 Boss 使用技能時觸發
    public event Action<WizlowSkill> OnSkillUsed;

    // 隨機數生成器
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
        RotateEnemy();

        if (!stop)
        {


            IntervalTime += Time.deltaTime;
            if (IntervalTime > skillTime + 1)
            {
                UseRandomSkill();
                IntervalTime = 0;
            }
        }
    }
    // Boss 使用技能的函數
    public void UseRandomSkill()
    {

        canUseSkill = true;


        // 從技能清單中隨機選擇一個技能
        WizlowSkill randomSkill = (WizlowSkill)random.Next(Enum.GetNames(typeof(WizlowSkill)).Length);

        // 觸發技能使用事件


        // 在這裡可以添加相應的技能邏輯，例如播放動畫、造成傷害等


        switch (randomSkill)
        {
            case WizlowSkill.Skill1:
                if (!isSkill1CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case WizlowSkill.Skill2:
                if (!isSkill2CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case WizlowSkill.Skill3:
                if (!isSkill3CanUse)
                {
                    canUseSkill = false;
                }
                break;
            case WizlowSkill.Skill4:
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
            print("無法使用" + randomSkill + "技能");
            skillTime = 1;
            //IntervalTime = skillTime - 1;
        }

    }



    public void StartwizlowAction()
    {
        stop = false;
        IntervalTime = 0;
        skillTime = 0;

    }
    public void StopWizlowAction()
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
    
    public void RotateEnemy()
    {

        if (attakTarget != null && attakTarget.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (attakTarget != null && attakTarget.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    
}

