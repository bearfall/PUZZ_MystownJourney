using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RotateOverTime : MonoBehaviour
    {
        public int damageAmount;

        public float targetAngle = 120f; // 目標旋轉角度
        public float rotationSpeed = 30f; // 初始旋轉速度
        public float maxRotationSpeed = 100f; // 最大旋轉速度
        public float accelerationRate = 20f; // 旋轉速度加速度

        private float currentRotationSpeed = 0f; // 當前旋轉速度

        private void Start()
        {
            float randomY = Random.Range(0.0f, 360f);
            transform.eulerAngles = new Vector3(0, randomY, 0);
        }

        void Update()
        {
            // 如果尚未達到目標角度
            if (transform.rotation.eulerAngles.x < targetAngle)
            {
                // 旋轉速度加速
                currentRotationSpeed += accelerationRate * Time.deltaTime;
                // 限制最大旋轉速度
                currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, rotationSpeed, maxRotationSpeed);
                // 根據當前旋轉速度進行旋轉
                transform.Rotate(Vector3.right, currentRotationSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                int damage = damageAmount;
                print(other.name + "受到傷害");
                damage -= other.gameObject.GetComponent<RPGCharacter>().def;
                StartCoroutine(other.gameObject.GetComponent<RPGCharacter>().TakeDamage(damage, 1f));
            }
        }
    }
}
