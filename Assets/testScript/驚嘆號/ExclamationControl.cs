using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExclamationControl : MonoBehaviour
{
    public SpriteRenderer exclamation;
    public float distanceToShow = 15;
    public List<Transform> areas = new List<Transform>();


    void Update()
    {
        // ���̪񪺰ϰ�
        Transform nearestArea = FindNearestArea();

        // �ˬd�̪񪺰ϰ�
        if (nearestArea != null)
        {
            float distance = Vector3.Distance(transform.position, nearestArea.position);

            // �ھڶZ���վ���ĸ�����ܮĪG
            float normalizedDistance = Mathf.Clamp01(distance / distanceToShow);
            // �Ҧp�A�A�i�H�]�m�z���סG exclamationObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f - normalizedDistance);
            exclamation.material.SetFloat("_Float", 1 - normalizedDistance);
            // ��s��ĸ�����m

        }

    }

    Transform FindNearestArea()
    {
        Transform nearestArea = null;
        float minDistance = float.MaxValue;

        // �M���Ҧ��ϰ�
        foreach (Transform area in areas)
        {
            float distance = Vector3.Distance(transform.position, area.position);

            // ���̪񪺰ϰ�
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestArea = area;
            }
        }

        return nearestArea;
    }
}

