using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorManager : MonoBehaviour
{
    public Hector hector;

    public HectorSkillManager hectorSkillManager;

    private void Start()
    {
        // �q�\ Boss ���ޯ�ϥΨƥ�
        hector.OnSkillUsed += HandleBossSkillUsed;


        // �b�A���ɾ��I�s Boss �ϥΧޯ઺���
        //boss.UseRandomSkill();
    }

    // �B�z Boss �ޯ�ϥΨƥ󪺨��
    private void HandleBossSkillUsed(Hector.HectorSkill usedSkill)
    {
        switch (usedSkill)
        {
            case Hector.HectorSkill.Skill1:
                // �B�z�ޯ�1���ĪG
                StartCoroutine(hectorSkillManager.HorizontalSlash());
                StartCoroutine(hector.SkillCanUse(1));
                hector.skillTime = hector.skill1Interval;
                Debug.Log("Boss �ϥΧޯ�[�r�G]");
                // ����������B�z�޿�
                break;
            case Hector.HectorSkill.Skill2:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(hectorSkillManager.DoubleSlash());
                StartCoroutine(hector.SkillCanUse(2));
                hector.skillTime = hector.skill2Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;
            // �K�[��L�ޯ઺ case ����
            case Hector.HectorSkill.Skill3:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(hectorSkillManager.TripleSlash());
                StartCoroutine(hector.SkillCanUse(3));
                hector.skillTime = hector.skill3Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;
            case Hector.HectorSkill.Skill4:
                // �B�z�ޯ�2���ĪG
                StartCoroutine(hectorSkillManager.RandomTripleSlash());
                StartCoroutine(hector.SkillCanUse(4));
                hector.skillTime = hector.skill4Interval;
                Debug.Log("Boss �ϥΧޯ�[���a����]");
                // ����������B�z�޿�
                break;

        }
    }
}
