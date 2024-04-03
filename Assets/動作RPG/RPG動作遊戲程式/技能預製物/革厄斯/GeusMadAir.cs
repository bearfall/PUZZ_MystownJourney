using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeusMadAir : MonoBehaviour
{
    public float targetScale = 2f; // �ؼ��Y���
    public float scaleSpeed = 0.1f; // �Y��t��

    private bool stopScaling = false; // �аO�O�_�����Y��

    void Update()
    {
        if (!stopScaling)
        {
            // �����Y��p��ؼ��Y��ɡA�����Y��
            if (transform.localScale.x < targetScale)
            {
                // �W�[�����Y��A�H�F�쥭���Y��ĪG
                transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
            }
            else
            {
                // �p�G�����Y��F��ؼЭȡA�h�����Y��
                stopScaling = true;
                Debug.Log("Reached target scale");
            }
        }
    }
}
