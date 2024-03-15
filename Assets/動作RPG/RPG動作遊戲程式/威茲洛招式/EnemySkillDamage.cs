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
            if (player.CompareTag("Player"))
            {
                int damage = damageAmount;

                print(player.name + "¨ü¨ì¶Ë®`");

                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.2f));
            }
        }
    }
}
