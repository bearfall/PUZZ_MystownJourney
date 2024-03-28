using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorManager : MonoBehaviour
{
    public Hector hector;

    public HectorSkillManager hectorSkillManager;

    private void Start()
    {
        // 訂閱 Boss 的技能使用事件
        hector.OnSkillUsed += HandleBossSkillUsed;


        // 在適當的時機呼叫 Boss 使用技能的函數
        //boss.UseRandomSkill();
    }

    // 處理 Boss 技能使用事件的函數
    private void HandleBossSkillUsed(Hector.HectorSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Hector.HectorSkill.Skill1:
                // 處理技能1的效果
                StartCoroutine(hectorSkillManager.HorizontalSlash());
                StartCoroutine(hector.SkillCanUse(1));
                hector.skillTime = hector.skill1Interval;
                Debug.Log("Boss 使用技能[毒液]");
                // 執行相應的處理邏輯
                break;
            case Hector.HectorSkill.Skill2:
                // 處理技能2的效果
                StartCoroutine(hectorSkillManager.DoubleSlash());
                StartCoroutine(hector.SkillCanUse(2));
                hector.skillTime = hector.skill2Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;
            // 添加其他技能的 case 分支
            case Hector.HectorSkill.Skill3:
                // 處理技能2的效果
                StartCoroutine(hectorSkillManager.TripleSlash());
                StartCoroutine(hector.SkillCanUse(3));
                hector.skillTime = hector.skill3Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;
            case Hector.HectorSkill.Skill4:
                // 處理技能2的效果
                StartCoroutine(hectorSkillManager.RandomTripleSlash());
                StartCoroutine(hector.SkillCanUse(4));
                hector.skillTime = hector.skill4Interval;
                Debug.Log("Boss 使用技能[裂地衝擊]");
                // 執行相應的處理邏輯
                break;

        }
    }
}
