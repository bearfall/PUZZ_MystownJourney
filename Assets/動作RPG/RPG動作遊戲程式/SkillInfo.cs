using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace RPGbearfall
{


    public class SkillInfo : MonoBehaviour
    {
        public RPGCharacter attackCharacter;
        public PlayerInfo playerInfo;

        public int normalDamageAmount;
        public int heavyDamageAmount;

        public int heavyMultiple;

        public float canBenormalAttackCoolDown;
        public float canBeHeavyAttackCoolDown;
        public bool isNormal;
        // Start is called before the first frame update
        void Start()
        {
            attackCharacter = GameObject.Find("´ú¸Õ¨¤¦â").GetComponent<RPGCharacter>();

            normalDamageAmount = playerInfo.Attack;
            //heavyDamageAmount = playerInfo.Attack * heavyMultiple;
        }


        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Enemy") && isNormal)
            {
                print(other.name);
                attackCharacter.beAttackEnemy = other.gameObject.GetComponent<RPGEnemyCharacter>();
                attackCharacter.Attack(attackCharacter.beAttackEnemy, normalDamageAmount, canBenormalAttackCoolDown);

            }
        }

        private void OnTriggerStay(Collider other)
        {
            
            if (other.CompareTag("Enemy") && !isNormal)
            {

                attackCharacter.beAttackEnemy = other.gameObject.GetComponent<RPGEnemyCharacter>();
                attackCharacter.Attack(attackCharacter.beAttackEnemy, heavyDamageAmount, canBeHeavyAttackCoolDown);
                

                
            }
        }
    }
}
