using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPGbearfall
{
    public class RPGEnemyCharacter : MonoBehaviour
    {
        



        [Header("角色名稱")]
        public string characterName;

        [Header("角色圖片")]
        public SpriteRenderer characterSpriteRenderer;

        [Header("技能生成點")]
        public Transform effectSpawnPoint;

        [Header("普功")]
        public GameObject characterNormalAttack;
        public float normalAttackCollDown;
        public bool canAttack;

        [Header("攻撃力")]
        public int playerAtk;

        [Header("防御力")]
        public int def; // 防御力

        [Header("最大HP(初期HP)")]
        public int maxHP; // 最大HP

        [Header("目前HP")]
        public int nowHP;

        
        public bool isDead;

        public bool triggerAttackCD;

        public Animator anim;
        public RPGCharacter beAttackEnemy;

        public bool canBeAttack = true;

        [Header("血條")]
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

            if (nowHP == 0)
            {
                isDead = true;
            }

            
        }
        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                print(other.name);
                beAttackEnemy = other.GetComponent<RPGCharacter>();
                this.Attack(beAttackEnemy, atk);
            }
        }
        */
        public IEnumerator SetEnemyAttackCD(float CD)
        {
            if (triggerAttackCD == false)
            {
                canAttack = false;
                CD = normalAttackCollDown;
                yield return new WaitForSeconds(CD);
                canAttack = true;
                triggerAttackCD = false;
            }
        }

        public void NormalAttackCD()
        {
            StartCoroutine(SetEnemyAttackCD(normalAttackCollDown));
        }

        public IEnumerator NormalAttack()
        {
            anim.SetTrigger("NormalAttack");
            yield return new WaitForSeconds(0);
        }


       

        public void Attack(RPGCharacter targetChara, int damageValue, float canBeAttackCoolDown = 0)
        {
            var damage = damageValue - targetChara.def;
            StartCoroutine(targetChara.TakeDamage(damage,canBeAttackCoolDown));
        }

        public IEnumerator TakeDamage(int damage, float canBeAttackCoolDown)
        {
            if (canBeAttack)
            {
                canBeAttack = false;
                print("受傷了!");


                //playetInfo.nowHP -= damage;
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

