using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class FireBallTrack : MonoBehaviour
    {
        public GameObject trailGameObject;

        public int damageAmount;

        public Transform targetPlayer; // 玩家的 Transform
        public RPGGameManager rPGGameManager;
        public float trackingSpeed = 5f; // 追蹤速度
        public float trackingDuration = 5f; // 追蹤時間
        public float flyingSpeed = 10f; // 飛行速度
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
            circleSpawner = GameObject.Find("教皇").GetComponent<CircleSpawner>();
            bossSkillManager = GameObject.Find("教皇").GetComponent<BossSkillManager>();
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
                // 當停止追蹤後，繼續以目前前進方向飛行
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
            // 使用 LookAt 來追蹤玩家
            transform.LookAt(targetPlayer);

            // 追蹤一段時間後停止
            trackingTimer += Time.deltaTime;
            if (trackingTimer >= trackingDuration)
            {
                isTracking = false;
                trackingTimer = 0f;
            }

            // 移動到玩家位置
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
            print("即將受到傷害");
            if (player.CompareTag("Player"))
            {
               int damage = damageAmount;

               print(player.name + "受到傷害");

               damage -= player.transform.GetComponent<RPGCharacter>().def;
               StartCoroutine( player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.5f));
            }
        }
    }
}