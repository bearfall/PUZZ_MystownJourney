using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeusManager : MonoBehaviour
{
    public Geus geus;

    public GeusSkillManager geusSkillManager;

    private void Start()
    {
        // 訂閱 Boss 的技能使用事件
        geus.OnSkillUsed += HandleBossSkillUsed;
        

        // 在適當的時機呼叫 Boss 使用技能的函數
        //boss.UseRandomSkill();
    }

    // 處理 Boss 技能使用事件的函數
    private void HandleBossSkillUsed(Geus.GeusSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Geus.GeusSkill.Skill1:
                // 處理技能1的效果
                StartCoroutine(geusSkillManager.ShotPoision());
                StartCoroutine(geus.SkillCanUse(1));
                geus.skillTime = geus.skill1Interval;
                Debug.Log("Boss 使用技能[毒液]");
                // 執行相應的處理邏輯
                break;
            case Geus.GeusSkill.Skill2:
                // 處理技能2的效果
                StartCoroutine(geusSkillManager.EarthShatter());
                StartCoroutine(geus.SkillCanUse(2));
                geus.skillTime = geus.skill2Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;
            // 添加其他技能的 case 分支
            case Geus.GeusSkill.Skill3:
                // 處理技能2的效果
                StartCoroutine(geusSkillManager.EarthShatter());
                StartCoroutine(geus.SkillCanUse(3));
                geus.skillTime = geus.skill3Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;
            case Geus.GeusSkill.Skill4:
                // 處理技能2的效果
                StartCoroutine(geusSkillManager.EarthShatter());
                StartCoroutine(geus.SkillCanUse(4));
                geus.skillTime = geus.skill4Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;

        }
    }
}
