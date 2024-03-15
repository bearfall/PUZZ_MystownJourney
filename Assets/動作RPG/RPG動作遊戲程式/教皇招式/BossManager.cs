using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    

    public Boss boss;

    public BossSkillManager bossSkillManager;

    private void Start()
    {
        // �q�\ Boss ���ޯ�ϥΨƥ�
        boss.OnSkillUsed += HandleBossSkillUsed;

        // �b�A���ɾ��I�s Boss �ϥΧޯ઺���
        //boss.UseRandomSkill();
    }

    // �B�z Boss �ޯ�ϥΨƥ󪺨��
    private void HandleBossSkillUsed(Boss.BossSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Boss.BossSkill.Skill1:
                // �B�z�ޯ�1���ĪG
                StartCoroutine( bossSkillManager.ShotDarkBall());
                StartCoroutine(boss.SkillCanUse(1));
                boss.skillTime = boss.skill1Interval;
                Debug.Log("Boss �ϥΧޯ�[�l�ܼu]");
                // ����������B�z�޿�
                break;
            case Boss.BossSkill.Skill2:
                // �B�z�ޯ�2���ĪG
                bossSkillManager.StartMove();
                StartCoroutine(boss.SkillCanUse(2));
                boss.skillTime = boss.skill2Interval;
                Debug.Log("Boss �ϥΧޯ�[�H����m�z��]");
                // ����������B�z�޿�
                break;
            // �K�[��L�ޯ઺ case ����
            case Boss.BossSkill.Skill3:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(bossSkillManager.GenerateObjects());
                StartCoroutine(boss.SkillCanUse(3));
                boss.skillTime = boss.skill3Interval;
                Debug.Log("Boss �ϥΧޯ�[�{�q]");
                // ����������B�z�޿�
                break;
            case Boss.BossSkill.Skill4:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(bossSkillManager.LaserSkill());
                StartCoroutine(boss.SkillCanUse(4));
                boss.skillTime = boss.skill4Interval;
                Debug.Log("Boss �ϥΧޯ�[�p�g]");
                // ����������B�z�޿�
                break;
            
        }
    }
}
