using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 30, 0); // 旋转速度（每秒度）

    void Update()
    {
        // 按照旋转速度自转
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
