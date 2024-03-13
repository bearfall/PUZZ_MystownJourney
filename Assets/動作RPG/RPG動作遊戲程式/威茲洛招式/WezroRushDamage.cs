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
            print("�Y�N����ˮ`");
            if (player.CompareTag("Player"))
            {
                int damage = damageAmount;

                print(player.name + "����ˮ`");

                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.5f));
            }
        }
    }
}
