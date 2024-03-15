using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    

    public Boss boss;

    public BossSkillManager bossSkillManager;

    private void Start()
    {
        // 訂閱 Boss 的技能使用事件
        boss.OnSkillUsed += HandleBossSkillUsed;

        // 在適當的時機呼叫 Boss 使用技能的函數
        //boss.UseRandomSkill();
    }

    // 處理 Boss 技能使用事件的函數
    private void HandleBossSkillUsed(Boss.BossSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Boss.BossSkill.Skill1:
                // 處理技能1的效果
                StartCoroutine( bossSkillManager.ShotDarkBall());
                StartCoroutine(boss.SkillCanUse(1));
                boss.skillTime = boss.skill1Interval;
                Debug.Log("Boss 使用技能[追蹤彈]");
                // 執行相應的處理邏輯
                break;
            case Boss.BossSkill.Skill2:
                // 處理技能2的效果
                bossSkillManager.StartMove();
                StartCoroutine(boss.SkillCanUse(2));
                boss.skillTime = boss.skill2Interval;
                Debug.Log("Boss 使用技能[隨機位置爆炸]");
                // 執行相應的處理邏輯
                break;
            // 添加其他技能的 case 分支
            case Boss.BossSkill.Skill3:
                // 處理技能2的效果
                StartCoroutine(bossSkillManager.GenerateObjects());
                StartCoroutine(boss.SkillCanUse(3));
                boss.skillTime = boss.skill3Interval;
                Debug.Log("Boss 使用技能[閃電]");
                // 執行相應的處理邏輯
                break;
            case Boss.BossSkill.Skill4:
                // 處理技能2的效果
                StartCoroutine(bossSkillManager.LaserSkill());
                StartCoroutine(boss.SkillCanUse(4));
                boss.skillTime = boss.skill4Interval;
                Debug.Log("Boss 使用技能[雷射]");
                // 執行相應的處理邏輯
                break;
            
        }
    }
}
