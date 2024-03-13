using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class FireBallTrack : MonoBehaviour
    {
        public GameObject trailGameObject;

        public int damageAmount;

        public Transform targetPlayer; // ���a�� Transform
        public RPGGameManager rPGGameManager;
        public float trackingSpeed = 5f; // �l�ܳt��
        public float trackingDuration = 5f; // �l�ܮɶ�
        public float flyingSpeed = 10f; // ����t��
        public float rotationSpeed = 1f;

        private bool isRotateAround = true;
        private bool isTracking = false;


        private float rotateAroundTimer;
        private float trackingTimer;
        public CircleSpawner circleSpawner;
        public BossSkillManager bossSkillManager;


        private void Start()
        {
            rPGGameManager = GameObject.Find("RPGGameManager").GetComponent<RPGGameManager>();
            circleSpawner = GameObject.Find("�Ь�").GetComponent<CircleSpawner>();
            bossSkillManager = GameObject.Find("�Ь�").GetComponent<BossSkillManager>();
            Destroy(gameObject, 7f);
        }
        void Update()
        {
            targetPlayer = rPGGameManager.nowRPGCharacter.transform;


            if (isRotateAround)
            {
                RotateObjects();
            }

            else if (isTracking)
            {
                
                StartCoroutine( TrackPlayer());
            }
            else
            {
                // ����l�ܫ�A�~��H�ثe�e�i��V����
                transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
            }
        }

        IEnumerator TrackPlayer()
        {
            

            if (targetPlayer == null)
            {
                Debug.LogWarning("Player not found.");
                yield break;
            }
            yield return new WaitForSeconds(0f);
            // �ϥ� LookAt �Ӱl�ܪ��a
            transform.LookAt(targetPlayer);

            // �l�ܤ@�q�ɶ��ᰱ��
            trackingTimer += Time.deltaTime;
            if (trackingTimer >= trackingDuration)
            {
                isTracking = false;
                trackingTimer = 0f;
            }

            // ���ʨ쪱�a��m
            transform.Translate(Vector3.forward * trackingSpeed * Time.deltaTime);


        }

        void RotateObjects()
        {
            rotateAroundTimer += Time.deltaTime;
            if (rotateAroundTimer >= 2)
            {
                isRotateAround = false;
                isTracking = true;
                trailGameObject.SetActive(true);
                rotateAroundTimer = 0f;
            }
            transform.RotateAround(bossSkillManager.center, Vector3.up, rotationSpeed * Time.deltaTime);
        }


        private void OnTriggerEnter(Collider player)
        {
            print("�Y�N����ˮ`");
            if (player.CompareTag("Player"))
            {
               int damage = damageAmount;

               print(player.name + "����ˮ`");

               damage -= player.transform.GetComponent<RPGCharacter>().def;
               StartCoroutine( player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.5f));
            }
        }
    }
}