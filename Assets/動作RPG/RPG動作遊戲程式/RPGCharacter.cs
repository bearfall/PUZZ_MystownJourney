using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class RPGCharacter : MonoBehaviour
    {
        public PlayerInfo playetInfo;
        public List<PlayerInfo> playerInfos = new List<PlayerInfo>();
        [Header("角色名稱")]
        public string characterName;
        [Header("角色圖片")]
        public SpriteRenderer characterSpriteRenderer;

        [Header("技能生成點")]
        public Transform effectSpawnPoint;
        [Header("普功")]
        public GameObject characterNormalAttack;
        public float normalAttackCollDown;
        public bool canNormalAttack;

        [Header("重攻擊")]
        public GameObject characterHeavyAttack;
        public float heavyAttackCollDown;
        public bool canHeavyAttack;

        [Header("攻撃力")]
        public int playerAtk;

        private float enhanceTimer;
        public bool isEnhance;


        /*
        private void OnEnable()
        {
            // 訂閱Scriptable Object的事件
            if (playetInfo != null)
            {
                playetInfo.OnAttackChange += UpdatePlayerAtk;
            }
        }
        private void UpdatePlayerAtk(int newAtk)
        {
            playerAtk = newAtk;
            Debug.Log($"Player atk updated: {playerAtk}");
        }
        */
        [Header("防御力")]
        public int def; // 防御力
        [Header("最大HP(初期HP)")]
        public int maxHP; // 最大HP

        

        
        public Animator anim;
        public RPGEnemyCharacter beAttackEnemy;

        public bool canBeAttack = true;

        public int nowHP;
        public HealthBar healthBar;
        // Start is called before the first frame update
        void Start()
        {
            maxHP = playetInfo.maxHP;
            
            SetPlayer();
            nowHP = maxHP;
            healthBar.SetMaxHealth(maxHP);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (nowHP ==0 )
            {
                playetInfo.isdie = true;
                anim.SetInteger("die", 1);
                StartCoroutine(AutoChangeCharacter());
            }

            if (isEnhance)
            {
                enhanceTimer += Time.deltaTime;
                if (enhanceTimer > 3)
                {
                    isEnhance = false;
                    enhanceTimer = 0f;

                    foreach (var playerInfo in playerInfos)
                    {
                        playerInfo.Attack = playerInfo.originATK;
                    }
                    SetPlayer();

                }
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

        public IEnumerator NormalAttack()
        {
            anim.SetTrigger("NormalAttack");
            canNormalAttack = false;
            //Instantiate(characterNormalAttack, effectSpawnPoint);
            yield return new WaitForSeconds(normalAttackCollDown);
            canNormalAttack = true;
        }


        public IEnumerator HeavyAttack()
        {
            anim.SetTrigger("HeavyAttack");
            canHeavyAttack = false;
            // Instantiate(characterHeavyAttack, effectSpawnPoint);
            yield return new WaitForSeconds(heavyAttackCollDown);
            canHeavyAttack = true;
        }

        public void Attack(RPGEnemyCharacter targetEnemy,int damageValue)
        {
            var damage = damageValue - beAttackEnemy.def;
           StartCoroutine(targetEnemy.TakeDamage(damage,0.3f));
        }

        public IEnumerator TakeDamage(int damage, float canBeAttackCoolDown)
        {
            if (canBeAttack)
            {
                canBeAttack = false;
                


                playetInfo.nowHP -= damage;

                print("受到"+ damage + "傷害!");

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

        public void HealPlayer(int healAmount)
        {
            nowHP += healAmount;

            if (nowHP >= maxHP)
            {
                nowHP = maxHP;
            }
            healthBar.SetHealth(nowHP);
            gameObject.transform.GetChild(0).GetComponent<RPGCharacterEffect>().PlayhealEffect();
            /*
            GetComponent<SpriteRenderer>().material.SetFloat("_LerpAmount", 1);
            GetComponent<SpriteRenderer>().material.SetFloat("_isAttack", 1);
            yield return new WaitForSeconds(3f);
            GetComponent<SpriteRenderer>().material.SetFloat("_isAttack", 0);
            GetComponent<SpriteRenderer>().material.SetFloat("_LerpAmount", 0);
            */
            

        }


        

        public void SetPlayer()
        {
            characterName = playetInfo.Name;
            // characterSpriteRenderer.sprite = playetInfo.image;
            maxHP = playetInfo.maxHP;
            nowHP = playetInfo.nowHP;

            healthBar.SetMaxHealth(maxHP);

            healthBar.SetHealth(nowHP);

            def = playetInfo.def;
            playerAtk = playetInfo.Attack;
            characterNormalAttack = playetInfo.normalAttack;
            normalAttackCollDown = playetInfo.normalAttackCD;
            characterHeavyAttack = playetInfo.heavyAttack;
            heavyAttackCollDown = playetInfo.heavyAttackCD;
            anim.runtimeAnimatorController = playetInfo.animatorController;


        }


        public IEnumerator AutoChangeCharacter()
        {
            yield return new WaitForSeconds(3f);

            foreach (var player in playerInfos)
            {
                if (!player.isdie)
                {
                    playetInfo = player;
                    SetPlayer();
                    break;
                }
            }
        }
    }
}
