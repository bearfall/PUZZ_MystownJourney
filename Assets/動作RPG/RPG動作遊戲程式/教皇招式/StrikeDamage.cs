using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class StrikeDamage : MonoBehaviour
    {

        public int damageAmount;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider player)
        {
            print("即將受到傷害");
            if (player.CompareTag("Player"))
            {
                print(player.name + "受到傷害");
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damageAmount, 0.5f));
            }
        }
    }
}
