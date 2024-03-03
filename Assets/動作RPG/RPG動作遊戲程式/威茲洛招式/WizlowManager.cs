using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizlowManager : MonoBehaviour
{
    public Wizlow wizlow;

    public WizlowSkillManager wizlowSkillManager;

    private void Start()
    {
        // �q�\ Boss ���ޯ�ϥΨƥ�
        wizlow.OnSkillUsed += HandleBossSkillUsed;

        // �b�A���ɾ��I�s Boss �ϥΧޯ઺���
        //boss.UseRandomSkill();
    }

    // �B�z Boss �ޯ�ϥΨƥ󪺨��
    private void HandleBossSkillUsed(Wizlow.WizlowSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Wizlow.WizlowSkill.Skill1:
                // �B�z�ޯ�1���ĪG
                wizlowSkillManager.ShockWave();
                StartCoroutine(wizlow.SkillCanUse(1));
                wizlow.skillTime = wizlow.skill1Interval;
                Debug.Log("Boss �ϥΧޯ�[�����i]");
                // ����������B�z�޿�
                break;
            case Wizlow.WizlowSkill.Skill2:
                // �B�z�ޯ�2���ĪG
                wizlowSkillManager.StartRockSkill();
                StartCoroutine(wizlow.SkillCanUse(2));
                wizlow.skillTime = wizlow.skill2Interval;
                Debug.Log("Boss �ϥΧޯ�[����]");
                // ����������B�z�޿�
                break;
            // �K�[��L�ޯ઺ case ����
            case Wizlow.WizlowSkill.Skill3:
                // �B�z�ޯ�2���ĪG
                wizlowSkillManager.ShockWave();
                StartCoroutine(wizlow.SkillCanUse(3));
                wizlow.skillTime = wizlow.skill3Interval;
                Debug.Log("Boss �ϥΧޯ�[�����i]");
                // ����������B�z�޿�
                break;
            case Wizlow.WizlowSkill.Skill4:
                // �B�z�ޯ�2���ĪG
                wizlowSkillManager.StartRockSkill();
                StartCoroutine(wizlow.SkillCanUse(4));
                wizlow.skillTime = wizlow.skill4Interval;
                Debug.Log("Boss �ϥΧޯ�[����]");
                // ����������B�z�޿�
                break;

        }
    }
}
