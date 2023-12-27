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
            print("�Y�N����ˮ`");
            if (player.CompareTag("Player"))
            {
                print("����ˮ`");
                StartCoroutine( player.transform.GetChild(0).GetComponent<RPGCharacter>().TakeDamage(damageAmount, 0.2f));
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
