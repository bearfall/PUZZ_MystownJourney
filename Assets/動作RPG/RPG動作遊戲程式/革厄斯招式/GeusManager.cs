using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeusManager : MonoBehaviour
{
    public Geus geus;

    public GeusSkillManager geusSkillManager;

    private void Start()
    {
        // �q�\ Boss ���ޯ�ϥΨƥ�
        geus.OnSkillUsed += HandleBossSkillUsed;
        

        // �b�A���ɾ��I�s Boss �ϥΧޯ઺���
        //boss.UseRandomSkill();
    }

    // �B�z Boss �ޯ�ϥΨƥ󪺨��
    private void HandleBossSkillUsed(Geus.GeusSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Geus.GeusSkill.Skill1:
                // �B�z�ޯ�1���ĪG
                StartCoroutine(geusSkillManager.ShotPoision());
                StartCoroutine(geus.SkillCanUse(1));
                geus.skillTime = geus.skill1Interval;
                Debug.Log("Boss �ϥΧޯ�[�r�G]");
                // ����������B�z�޿�
                break;
            case Geus.GeusSkill.Skill2:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(geusSkillManager.EarthShatter());
                StartCoroutine(geus.SkillCanUse(2));
                geus.skillTime = geus.skill2Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;
            // �K�[��L�ޯ઺ case ����
            case Geus.GeusSkill.Skill3:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(geusSkillManager.EarthShatter());
                StartCoroutine(geus.SkillCanUse(3));
                geus.skillTime = geus.skill3Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;
            case Geus.GeusSkill.Skill4:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(geusSkillManager.EarthShatter());
                StartCoroutine(geus.SkillCanUse(4));
                geus.skillTime = geus.skill4Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;

        }
    }
}
