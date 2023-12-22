using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class RPGCharacter : MonoBehaviour
    {
        [Header("普功冷卻時間")]
        public float normalAttackCollDown;
        public bool canNormalAttack;

        [Header("重攻擊冷卻時間")]
        public float heavyAttackCollDown;
        public bool canHeavyAttack;

        [Header("攻撃力")]
        public int atk; // 攻撃力
        [Header("防御力")]
        public int def; // 防御力
        [Header("最大HP(初期HP)")]
        public int maxHP; // 最大HP

        public bool isDead;

        public Animator anim;
        public RPGCharacter beAttackEnemy;

        public bool canBeAttack = true;

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
            if (nowHP ==0 )
            {
                isDead = true;
                anim.SetBool("Die", true);
            }
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

        public IEnumerator NormalAttack()
        {
            anim.SetTrigger("NormalAttack");
            canNormalAttack = false;
            yield return new WaitForSeconds(normalAttackCollDown);
            canNormalAttack = true;
        }


        public IEnumerator HeavyAttack()
        {
            anim.SetTrigger("HeavyAttack");
            canHeavyAttack = false;
            yield return new WaitForSeconds(heavyAttackCollDown);
            canHeavyAttack = true;
        }

        public void Attack(RPGCharacter targetChara,int damageValue)
        {
            var damage = damageValue - beAttackEnemy.def;
           StartCoroutine( targetChara. TakeDamage(damage,0.5f));
        }

        public IEnumerator TakeDamage(int damage, float canBeAttackCoolDown)
        {
            if (canBeAttack)
            {
                canBeAttack = false;
                print("受傷了!");
                nowHP -= damage;
                if (nowHP <= 0)
                {
                    nowHP = 0;
                }
                healthBar.SetHealth(nowHP);

                anim.SetTrigger("Hurt");

                yield return new WaitForSeconds(canBeAttackCoolDown);
                canBeAttack = true;


            }
        }
    }
}
