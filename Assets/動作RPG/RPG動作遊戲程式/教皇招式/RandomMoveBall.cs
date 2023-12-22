using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RandomMoveBall : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
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
                print("受到傷害");
                StartCoroutine( player.transform.GetChild(0).GetComponent<RPGCharacter>().TakeDamage(2,0.2f));
            }
        }

        public IEnumerator WaitTime()
        {
           yield return new WaitForSeconds(10f);
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            yield break;
        }
    }
}
