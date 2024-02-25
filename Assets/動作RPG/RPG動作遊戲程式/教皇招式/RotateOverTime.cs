using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RotateOverTime : MonoBehaviour
    {
        public int damageAmount;

        public float targetAngle = 120f; // �ؼб��ਤ��
        public float rotationSpeed = 30f; // ��l����t��
        public float maxRotationSpeed = 100f; // �̤j����t��
        public float accelerationRate = 20f; // ����t�ץ[�t��

        private float currentRotationSpeed = 0f; // ��e����t��

        private void Start()
        {
            float randomY = Random.Range(0.0f, 360f);
            transform.eulerAngles = new Vector3(0, randomY, 0);
        }

        void Update()
        {
            // �p�G�|���F��ؼШ���
            if (transform.rotation.eulerAngles.x < targetAngle)
            {
                // ����t�ץ[�t
                currentRotationSpeed += accelerationRate * Time.deltaTime;
                // ����̤j����t��
                currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, rotationSpeed, maxRotationSpeed);
                // �ھڷ�e����t�׶i�����
                transform.Rotate(Vector3.right, currentRotationSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                int damage = damageAmount;
                print(other.name + "����ˮ`");
                damage -= other.gameObject.GetComponent<RPGCharacter>().def;
                StartCoroutine(other.gameObject.GetComponent<RPGCharacter>().TakeDamage(damage, 1f));
            }
        }
    }
}
