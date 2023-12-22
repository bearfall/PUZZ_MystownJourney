using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class CircleSpawner : MonoBehaviour
    {
        public GameObject prefab; // �ݭn�ͦ�������
        public int numberOfObjects = 6; // ���󪺼ƶq
        public float radius = 5f; // ��骺�b�|
        

        public Vector3 center;
        // ��骺���ߦ�m�y��

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
