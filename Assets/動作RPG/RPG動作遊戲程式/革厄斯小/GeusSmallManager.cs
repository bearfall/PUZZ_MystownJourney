using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeusSmallManager : MonoBehaviour
{
    public GeusSmall geusSmall;

    public GeusSmallSkillManager geusSmallSkillManager;

    private void Start()
    {
        // �q�\ Boss ���ޯ�ϥΨƥ�
        geusSmall.OnSkillUsed += HandleBossSkillUsed;


        // �b�A���ɾ��I�s Boss �ϥΧޯ઺���
        //boss.UseRandomSkill();
    }

    // �B�z Boss �ޯ�ϥΨƥ󪺨��
    private void HandleBossSkillUsed(GeusSmall.GeusSmallSkill usedSkill)
    {
        switch (usedSkill)
        {
            case GeusSmall.GeusSmallSkill.Skill1:
                // �B�z�ޯ�1���ĪG
                StartCoroutine(geusSmallSkillManager.ShotPoision());
                StartCoroutine(geusSmall.SkillCanUse(1));
                geusSmall.skillTime = geusSmall.skill1Interval;
                Debug.Log("Boss �ϥΧޯ�[�r�G]");
                // ����������B�z�޿�
                break;
            case GeusSmall.GeusSmallSkill.Skill2:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(geusSmallSkillManager.EarthShatter());
                StartCoroutine(geusSmall.SkillCanUse(2));
                geusSmall.skillTime = geusSmall.skill2Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;
            // �K�[��L�ޯ઺ case ����
            case GeusSmall.GeusSmallSkill.Skill3:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(geusSmallSkillManager.ClawAttack());
                StartCoroutine(geusSmall.SkillCanUse(3));
                geusSmall.skillTime = geusSmall.skill3Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;
            case GeusSmall.GeusSmallSkill.Skill4:
                // �B�z�ޯ�2���ĪG
                geusSmallSkillManager.MadAir();
                StartCoroutine(geusSmall.SkillCanUse(4));
                geusSmall.skillTime = geusSmall.skill4Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;

        }
    }
}
