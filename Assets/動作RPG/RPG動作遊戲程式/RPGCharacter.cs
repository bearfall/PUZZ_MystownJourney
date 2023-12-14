using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class RPGCharacter : MonoBehaviour
    {
        [Header("攻撃力")]
        public int atk; // 攻撃力
        [Header("防御力")]
        public int def; // 防御力
        [Header("最大HP(初期HP)")]
        public int maxHP; // 最大HP

        public Animator anim;
        public RPGCharacter beAttackEnemy;

        public int nowHP;
        public HealthBar healthBar;
        // Start is called before the first frame update
        void Start()
        {
            nowHP = maxHP;
            healthBar.SetMaxHealth(maxHP);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                print(other.name);
                beAttackEnemy = other.GetComponent<RPGCharacter>();
                this.Attack(beAttackEnemy, atk);
            }
        }

        public void NormalAttack()
        {
            anim.SetTrigger("NormalAttack");
        }


        public void HeavyAttack()
        {
            anim.SetTrigger("HeavyAttack");
        }

        public void Attack(RPGCharacter targetChara,int damageValue)
        {
            var damage = damageValue - beAttackEnemy.def;
            targetChara.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            nowHP -= damage;

            healthBar.SetHealth(nowHP);
        }
    }
}
