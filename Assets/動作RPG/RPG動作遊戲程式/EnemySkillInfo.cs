using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class EnemySkillInfo : MonoBehaviour
    {
        public string enemyName;

        public RPGEnemyCharacter rpgEnemyCharacter;
        public RPGEnemyController  rpgEnemyController;
        // Start is called before the first frame update

        private void Awake()
        {
            rpgEnemyCharacter = gameObject.transform.parent.parent.parent.GetComponent<RPGEnemyCharacter>();
            rpgEnemyController = gameObject.transform.parent.parent.parent.GetComponent<RPGEnemyController>();
        }

        void Start()
        {
            
            //rpgEnemyCharacter = GameObject.Find(enemyName).GetComponent<RPGEnemyCharacter>();
            //rpgEnemyController = GameObject.Find(enemyName).GetComponent<RPGEnemyController>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                print(other.name);
                //attackCharacter.beAttackEnemy = other.gameObject.GetComponent<RPGEnemyCharacter>();
                rpgEnemyCharacter.Attack(other.GetComponent<RPGCharacter>(), rpgEnemyController.atk, 2);

            }
        }
    }
}
