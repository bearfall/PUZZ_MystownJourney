using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeusMadAir : MonoBehaviour
{
    public float targetScale = 2f; // 目標縮放值
    public float scaleSpeed = 0.1f; // 縮放速度

    private bool stopScaling = false; // 標記是否停止縮放

    void Update()
    {
        if (!stopScaling)
        {
            // 當物件的縮放小於目標縮放時，持續縮放
            if (transform.localScale.x < targetScale)
            {
                // 增加物件的縮放，以達到平滑縮放效果
                transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
            }
            else
            {
                // 如果物件的縮放達到目標值，則停止縮放
                stopScaling = true;
                Debug.Log("Reached target scale");
            }
        }
    }
}
