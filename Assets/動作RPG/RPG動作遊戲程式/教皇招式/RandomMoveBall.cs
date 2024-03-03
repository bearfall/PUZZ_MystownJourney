using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RandomMoveBall : MonoBehaviour
    {

        public Collider effectCollider;
        public Collider effectCollider2;
        public int damageAmount;
        public float waitTime;

        // Start is called before the first frame update
        void Start()
        {
            //effectCollider.enabled = false;
            StartCoroutine(WaitTime());
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay(Collider player)
        {
            
            if (player.CompareTag("Player") && player.gameObject.GetComponent<RPGCharacter>().canBeAttack)
            {
                
                int damage = damageAmount;

                print("¨ü¨ì¶Ë®`");
                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine( player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.5f));
            }
        }

        public IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(waitTime);
            effectCollider.enabled = false;
            effectCollider2.enabled = true;
            yield break;
        }
    }
}
