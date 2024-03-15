using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiaoManager : MonoBehaviour
{
    public Siao siao;

    public SiaoSkillManager siaoSkillManager;

    private void Start()
    {
        // �q�\ Boss ���ޯ�ϥΨƥ�
        siao.OnSkillUsed += HandleBossSkillUsed;

        // �b�A���ɾ��I�s Boss �ϥΧޯ઺���
        //boss.UseRandomSkill();
    }

    // �B�z Boss �ޯ�ϥΨƥ󪺨��
    private void HandleBossSkillUsed(Siao.SiaoSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Siao.SiaoSkill.Skill1:
                // �B�z�ޯ�1���ĪG
                siaoSkillManager.SpawnKnifeRing();
                StartCoroutine(siao.SkillCanUse(1));
                siao.skillTime = siao.skill1Interval;
                Debug.Log("Boss �ϥΧޯ�[�M���ۤb]");
                // ����������B�z�޿�
                break;
            case Siao.SiaoSkill.Skill2:
                // �B�z�ޯ�2���ĪG
                //wizlowSkillManager.StartRush();
                StartCoroutine(siaoSkillManager.BladeStrike());
                siao.skillTime = siao.skill2Interval;
                Debug.Log("Boss �ϥΧޯ�[�W�bŧ��]");
                // ����������B�z�޿�
                break;
            // �K�[��L�ޯ઺ case ����
            case Siao.SiaoSkill.Skill3:
                // �B�z�ޯ�2���ĪG
                //StartCoroutine(wizlowSkillManager.GP());
                StartCoroutine( siaoSkillManager.Tornado());
                siao.skillTime = siao.skill3Interval;
                Debug.Log("Boss �ϥΧޯ�[���j�M�W]");
                // ����������B�z�޿�
                break;
            case Siao.SiaoSkill.Skill4:
                // �B�z�ޯ�2���ĪG
                //StartCoroutine(wizlowSkillManager.ThrowingWeapon());
                StartCoroutine(siaoSkillManager.BladeStrikeAttack());
                siao.skillTime = siao.skill4Interval;
                Debug.Log("Boss �ϥΧޯ�[��H]");
                // ����������B�z�޿�
                break;

        }
    }
}
