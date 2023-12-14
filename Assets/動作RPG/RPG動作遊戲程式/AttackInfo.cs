using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace RPGbearfall
{
    public class AttackInfo : MonoBehaviour
    {
        public RPGCharacter attackCharacter;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
            if (other.CompareTag("Enemy"))
            {
                
                attackCharacter.beAttackEnemy = other.transform.GetChild(0).GetComponent<RPGCharacter>();
                attackCharacter.Attack(attackCharacter.beAttackEnemy, attackCharacter.atk);
            }
        }
        
        
    }
}
