using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeusSmallManager : MonoBehaviour
{
    public GeusSmall geusSmall;

    public GeusSmallSkillManager geusSmallSkillManager;

    private void Start()
    {
        // 訂閱 Boss 的技能使用事件
        geusSmall.OnSkillUsed += HandleBossSkillUsed;


        // 在適當的時機呼叫 Boss 使用技能的函數
        //boss.UseRandomSkill();
    }

    // 處理 Boss 技能使用事件的函數
    private void HandleBossSkillUsed(GeusSmall.GeusSmallSkill usedSkill)
    {
        switch (usedSkill)
        {
            case GeusSmall.GeusSmallSkill.Skill1:
                // 處理技能1的效果
                StartCoroutine(geusSmallSkillManager.ShotPoision());
                StartCoroutine(geusSmall.SkillCanUse(1));
                geusSmall.skillTime = geusSmall.skill1Interval;
                Debug.Log("Boss 使用技能[毒液]");
                // 執行相應的處理邏輯
                break;
            case GeusSmall.GeusSmallSkill.Skill2:
                // 處理技能2的效果
                StartCoroutine(geusSmallSkillManager.EarthShatter());
                StartCoroutine(geusSmall.SkillCanUse(2));
                geusSmall.skillTime = geusSmall.skill2Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;
            // 添加其他技能的 case 分支
            case GeusSmall.GeusSmallSkill.Skill3:
                // 處理技能2的效果
                StartCoroutine(geusSmallSkillManager.ClawAttack());
                StartCoroutine(geusSmall.SkillCanUse(3));
                geusSmall.skillTime = geusSmall.skill3Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;
            case GeusSmall.GeusSmallSkill.Skill4:
                // 處理技能2的效果
                geusSmallSkillManager.MadAir();
                StartCoroutine(geusSmall.SkillCanUse(4));
                geusSmall.skillTime = geusSmall.skill4Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;

        }
    }
}
