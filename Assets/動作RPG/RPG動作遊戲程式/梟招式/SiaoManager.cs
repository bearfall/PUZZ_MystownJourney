using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiaoManager : MonoBehaviour
{
    public Siao siao;

    public SiaoSkillManager siaoSkillManager;

    private void Start()
    {
        // 訂閱 Boss 的技能使用事件
        siao.OnSkillUsed += HandleBossSkillUsed;

        // 在適當的時機呼叫 Boss 使用技能的函數
        //boss.UseRandomSkill();
    }

    // 處理 Boss 技能使用事件的函數
    private void HandleBossSkillUsed(Siao.SiaoSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Siao.SiaoSkill.Skill1:
                // 處理技能1的效果
                siaoSkillManager.SpawnKnifeRing();
                StartCoroutine(siao.SkillCanUse(1));
                siao.skillTime = siao.skill1Interval;
                Debug.Log("Boss 使用技能[刀環旋刃]");
                // 執行相應的處理邏輯
                break;
            case Siao.SiaoSkill.Skill2:
                // 處理技能2的效果
                //wizlowSkillManager.StartRush();
                StartCoroutine(siaoSkillManager.BladeStrike());
                siao.skillTime = siao.skill2Interval;
                Debug.Log("Boss 使用技能[鋒刃襲擊]");
                // 執行相應的處理邏輯
                break;
            // 添加其他技能的 case 分支
            case Siao.SiaoSkill.Skill3:
                // 處理技能2的效果
                //StartCoroutine(wizlowSkillManager.GP());
                StartCoroutine( siaoSkillManager.Tornado());
                siao.skillTime = siao.skill3Interval;
                Debug.Log("Boss 使用技能[輪迴刀鋒]");
                // 執行相應的處理邏輯
                break;
            case Siao.SiaoSkill.Skill4:
                // 處理技能2的效果
                //StartCoroutine(wizlowSkillManager.ThrowingWeapon());
                StartCoroutine(siaoSkillManager.BladeStrikeAttack());
                siao.skillTime = siao.skill4Interval;
                Debug.Log("Boss 使用技能[唬人]");
                // 執行相應的處理邏輯
                break;

        }
    }
}
