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
            print("�Y�N����ˮ`");
            if (player.CompareTag("Player"))
            {
                print(player.name + "����ˮ`");
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damageAmount, 0.5f));
            }
        }
    }
}
