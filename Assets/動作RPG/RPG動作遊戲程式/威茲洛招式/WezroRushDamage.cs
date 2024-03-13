using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class WezroRushDamage : MonoBehaviour
    {
        public int damageAmount;
        

        private void OnTriggerEnter(Collider player)
        {
            print("即將受到傷害");
            if (player.CompareTag("Player"))
            {
                int damage = damageAmount;

                print(player.name + "受到傷害");

                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.5f));
            }
        }
    }
}
