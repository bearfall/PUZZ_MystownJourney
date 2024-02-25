using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class StrikeDamage : MonoBehaviour
    {
        public float destroyTimer;
        public float destroyTime = 1.5f;

        public int damageAmount;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider player)
        {
            print("即將受到傷害");

            if (player.CompareTag("Player"))
            {
                if (player.transform.GetComponent<RPGCharacter>().canBeAttack)
                {
                    int damage = damageAmount;
                    print(player.name + "受到傷害");
                    damage -= player.transform.GetComponent<RPGCharacter>().def;
                    StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 1.2f));
                    destroyTime += 1.7f;
                }
            }
        }
    }
}
