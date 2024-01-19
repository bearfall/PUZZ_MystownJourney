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

        public bool isNormal;
        // Start is called before the first frame update
        void Start()
        {
            attackCharacter = GameObject.Find("´ú¸Õ¨¤¦â").GetComponent<RPGCharacter>();

            normalDamageAmount = playerInfo.Attack;
            heavyDamageAmount = playerInfo.Attack * 2;
        }


        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
            if (other.CompareTag("Enemy"))
            {

                attackCharacter.beAttackEnemy = other.gameObject.GetComponent<RPGEnemyCharacter>();

                if (isNormal)
                {
                    attackCharacter.Attack(attackCharacter.beAttackEnemy, normalDamageAmount);
                }
                else
                {
                    attackCharacter.Attack(attackCharacter.beAttackEnemy, heavyDamageAmount);
                }

                
            }
        }
    }
}
