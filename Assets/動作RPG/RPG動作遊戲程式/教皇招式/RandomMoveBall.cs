using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RandomMoveBall : MonoBehaviour
    {

        public Collider effectCollider;
        public int damageAmount;
        public float waitTime;

        // Start is called before the first frame update
        void Start()
        {
            effectCollider.enabled = false;
            StartCoroutine(WaitTime());
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay(Collider player)
        {
            print("即將受到傷害");
            if (player.CompareTag("Player"))
            {
                int damage = damageAmount;

                print("受到傷害");
                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine( player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.2f));
            }
        }

        public IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(waitTime);
            effectCollider.enabled = true;
            yield break;
        }
    }
}
