using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizlowManager : MonoBehaviour
{
    public Wizlow wizlow;

    public WizlowSkillManager wizlowSkillManager;

    private void Start()
    {
        // 訂閱 Boss 的技能使用事件
        wizlow.OnSkillUsed += HandleBossSkillUsed;

        // 在適當的時機呼叫 Boss 使用技能的函數
        //boss.UseRandomSkill();
    }

    // 處理 Boss 技能使用事件的函數
    private void HandleBossSkillUsed(Wizlow.WizlowSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Wizlow.WizlowSkill.Skill1:
                // 處理技能1的效果
                wizlowSkillManager.ShockWave();
                StartCoroutine(wizlow.SkillCanUse(1));
                wizlow.skillTime = wizlow.skill1Interval;
                Debug.Log("Boss 使用技能[衝擊波]");
                // 執行相應的處理邏輯
                break;
            case Wizlow.WizlowSkill.Skill2:
                // 處理技能2的效果
                wizlowSkillManager.StartRockSkill();
                StartCoroutine(wizlow.SkillCanUse(2));
                wizlow.skillTime = wizlow.skill2Interval;
                Debug.Log("Boss 使用技能[岩石]");
                // 執行相應的處理邏輯
                break;
            // 添加其他技能的 case 分支
            case Wizlow.WizlowSkill.Skill3:
                // 處理技能2的效果
                wizlowSkillManager.ShockWave();
                StartCoroutine(wizlow.SkillCanUse(3));
                wizlow.skillTime = wizlow.skill3Interval;
                Debug.Log("Boss 使用技能[衝擊波]");
                // 執行相應的處理邏輯
                break;
            case Wizlow.WizlowSkill.Skill4:
                // 處理技能2的效果
                wizlowSkillManager.StartRockSkill();
                StartCoroutine(wizlow.SkillCanUse(4));
                wizlow.skillTime = wizlow.skill4Interval;
                Debug.Log("Boss 使用技能[岩石]");
                // 執行相應的處理邏輯
                break;

        }
    }
}
