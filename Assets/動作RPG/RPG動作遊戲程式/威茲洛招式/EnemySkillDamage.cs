using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class EnemySkillDamage : MonoBehaviour
    {
        public float canDoDamageTime;
        public float canDoDamageTimer;
        public int damageAmount;
        public Collider skillCollider;

        public bool isSustained;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            canDoDamageTimer += Time.deltaTime;
            if (canDoDamageTimer >= canDoDamageTime)
            {
                skillCollider.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider player)
        {
            if (player.CompareTag("Player") && !isSustained)
            {
                int damage = damageAmount;

                print(player.name + "受到傷害");

                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.1f));
            }
        }


        private void OnTriggerStay(Collider player)
        {
            if (player.CompareTag("Player") && isSustained)
            {
                int damage = damageAmount;

                print(player.name + "受到傷害");

                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.5f));
            }
        }
    }
}
