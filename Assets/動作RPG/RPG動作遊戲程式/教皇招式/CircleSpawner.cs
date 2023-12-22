using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class CircleSpawner : MonoBehaviour
    {
        public GameObject prefab; // 需要生成的物件
        public int numberOfObjects = 6; // 物件的數量
        public float radius = 5f; // 圓圈的半徑
        

        public Vector3 center;
        // 圓圈的中心位置座標

        void Start()
        {
           // SpawnObjects();
        }

        public void SpawnObjects()
        {
            center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            for (int i = 0; i < numberOfObjects; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfObjects;
                float x = Mathf.Cos(angle) * radius + center.x;
                float z = Mathf.Sin(angle) * radius + center.z;

                Vector3 spawnPosition = new Vector3(x, center.y, z);
                Quaternion spawnRotation = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);

                Instantiate(prefab, spawnPosition, spawnRotation);
            }
        }

        
    }
}
