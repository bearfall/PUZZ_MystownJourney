using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class BlackHoleAttraction : MonoBehaviour
    {
        public Transform blackHoleCenter;          // 黑洞的中心
        public Rigidbody playerRigidbody;         // 玩家的Rigidbody
        
        public float attractionSpeed;
        public RPGGameManager rPGGameManager;
        public RPGCharacterManager rpGCharacterManager;

        public float attractDuration;
        private float trackingTimer;

        public bool isTrack;
        public bool isEnhance;
        private void Start()
        {
            rPGGameManager = GameObject.Find("RPGGameManager").GetComponent<RPGGameManager>();
            rpGCharacterManager = GameObject.Find("RPGGameManager").GetComponent<RPGCharacterManager>();
        }
        void Update()
        {
            



            if (isTrack)
            {
                playerRigidbody = rPGGameManager.nowRPGCharacter.transform.GetComponent<Rigidbody>();

                trackingTimer += Time.deltaTime;

                if (trackingTimer >= attractDuration && isEnhance == false)
                {
                    attractionSpeed = attractionSpeed+2;
                    isEnhance = true;
                }

                if (trackingTimer >= attractDuration*2)
                {
                    isTrack = false;
                }

                print("拉扯中");
                // 計算黑洞到玩家的向量
                Vector3 attractionDirection = blackHoleCenter.position - playerRigidbody.transform.position;

                playerRigidbody.velocity = attractionDirection.normalized * attractionSpeed;
            }
            else
            {

                rPGGameManager.nowRPGCharacter.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                

                
                Destroy(gameObject);
            }
            
        }

        
    }
}
