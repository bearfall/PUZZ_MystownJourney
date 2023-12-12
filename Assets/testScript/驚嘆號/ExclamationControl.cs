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
        // 找到最近的區域
        Transform nearestArea = FindNearestArea();

        // 檢查最近的區域
        if (nearestArea != null)
        {
            float distance = Vector3.Distance(transform.position, nearestArea.position);

            // 根據距離調整驚嘆號的顯示效果
            float normalizedDistance = Mathf.Clamp01(distance / distanceToShow);
            // 例如，你可以設置透明度： exclamationObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f - normalizedDistance);
            exclamation.material.SetFloat("_Float", 1 - normalizedDistance);
            // 更新驚嘆號的位置

        }

    }

    Transform FindNearestArea()
    {
        Transform nearestArea = null;
        float minDistance = float.MaxValue;

        // 遍歷所有區域
        foreach (Transform area in areas)
        {
            float distance = Vector3.Distance(transform.position, area.position);

            // 找到最近的區域
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestArea = area;
            }
        }

        return nearestArea;
    }
}

