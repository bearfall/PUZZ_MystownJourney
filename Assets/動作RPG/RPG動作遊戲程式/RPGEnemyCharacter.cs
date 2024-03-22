using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPGbearfall
{
    public class RPGEnemyCharacter : MonoBehaviour
    {
        [Header("敵人所屬劇情觸發器")]
        public List<GameObject> storyTrigger;

        [Header("敵人所屬空氣牆")]
        public List<GameObject> airWall;

        [Header("敵人所屬視野限制")]
        public int CMConfinerNum;

        public CMConfinerManager cmConfinerManager;

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

        public CameraShake cameraShake;
        public bool canBeAttack = true;

        [Header("血條")]
        public HealthBar healthBar;


        [Header("Boss相關")]
        public Wizlow wizlow;
        public Boss boss;
        // Start is called before the first frame update
        void Start()
        {
            
            nowHP = maxHP;
            SetHealth();
        }

        // Update is called once per frame
        void Update()
        {
            

            if (nowHP == 0)
            {
                isDead = true;
                anim.SetBool("die", isDead);
                /*
                if (wizlow != null)
                {
                    wizlow.StartwizlowAction();
                }
                */
                nowHP--;
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
                DamagePopUpGenerator.current.CreatePopUp(transform.position, damage.ToString(), Color.red);
                if (nowHP <= 0)
                {
                    nowHP = 0;
                }
                healthBar.SetHealth(nowHP);

                anim.SetTrigger("Hurt");
                cameraShake.ShakeCamera(0.6f, 0.3f);
                yield return new WaitForSeconds(canBeAttackCoolDown);
                canBeAttack = true;


            }
        }

        public void SetHealth()
        {
            nowHP = maxHP;
            healthBar.SetMaxHealth(maxHP);
        }

        public void OpenStoryTrigger()
        {
            if (storyTrigger != null)
            {
                foreach (var item in storyTrigger)
                {
                    item.SetActive(true);
                }
            }
            if (airWall != null)
            {
                foreach (var item in airWall)
                {
                    item.SetActive(false);
                }
            }
            cmConfinerManager.SwitchCMConfinerObj(CMConfinerNum);
        }
    }
}

