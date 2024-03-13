using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace RPGbearfall
{
    public class RPGCharacter : MonoBehaviour
    {
        //public RPGCharacterEffect rpgCharacterEffect;
        public PlayerInfo playetInfo;
        public List<PlayerInfo> playerInfos = new List<PlayerInfo>();

        [Header("復活點")]
        public RPGGameManager rpgGameManager;
        public Transform resurrectionPoint;
        public int playerAmount = 4;
        public GameObject gameOverCanva;

        public AudioSource audioSource;
        public AudioClip levelUPSound;
    
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
        public Slider heavyAttackCollDownSlider;
        public GameObject characterHeavyAttack;
        //public float heavyAttackCollDown;
        public bool canHeavyAttack;

        [Header("攻撃力")]
        public int playerAtk;

        private float enhanceTimer;
        public bool isEnhance;
        public bool triggerNormalAttackCD = false;

        [Header("回血次數")]
        public int healNum = 3;
        public Text healText;

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

        public RPGCharacterEffect rpgCharacterEffect;

        public bool canBeAttack = true;
        /*
        public float canBeAttackTimer;
        public bool isCanBeAttackTimer;
        */

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
            if (heavyAttackCollDownSlider.value != 0)
            {
                heavyAttackCollDownSlider.value -= Time.deltaTime * 10;
            }

            if (nowHP ==0 )
            {
                playetInfo.isdie = true;
                anim.SetBool("die", true);
                nowHP--;
                print("沒血了，換角");
                StartCoroutine(AutoChangeCharacter());
            }

            if (playerAmount == 0)
            {
                gameOverCanva.SetActive(true);
                playerAmount--;
            }
            /*
            if (canBeAttack)
            {
                isCanBeAttackTimer = false;
                
            }
            if (!canBeAttack)
            {
                isCanBeAttackTimer = true;
            }
            if (isCanBeAttackTimer)
            {
                canBeAttackTimer += Time.deltaTime;
            }
            if (canBeAttackTimer >= 1)
            {
                canBeAttack = true;
                canBeAttackTimer = 0;
            }
            */


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


            if (Input.GetKeyDown(KeyCode.P))
            {
                ResetPlayer();
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
            //canNormalAttack = false;
            //Instantiate(characterNormalAttack, effectSpawnPoint);
            yield return new WaitForSeconds(0);
            //canNormalAttack = true;
        }


        public IEnumerator HeavyAttack()
        {
            if (heavyAttackCollDownSlider.value <= 0)
            {
                anim.SetTrigger("HeavyAttack");
                //canHeavyAttack = false;
                // Instantiate(characterHeavyAttack, effectSpawnPoint);
                //yield return new WaitForSeconds(heavyAttackCollDown);
                yield return new WaitForSeconds(0);
                heavyAttackCollDownSlider.value = 100;
                //canHeavyAttack = true;
            }
            
        }

        public void Attack(RPGEnemyCharacter targetEnemy,int damageValue,float canBeAttackCoolDown = 0)
        {
            var damage = damageValue - beAttackEnemy.def;
           StartCoroutine(targetEnemy.TakeDamage(damage,canBeAttackCoolDown));
        }

        public IEnumerator TakeDamage(int damage, float canBeAttackCoolDown)
        {
            if (canBeAttack)
            {
                canBeAttack = false;
                


                playetInfo.nowHP -= damage;
                anim.SetTrigger("hurt");
                print("受到"+ damage + "傷害!");

                nowHP -= damage;
                if (nowHP <= 0)
                {
                    nowHP = 0;
                }
                healthBar.SetHealth(nowHP);

                //anim.SetTrigger("Hurt");

                yield return new WaitForSeconds(canBeAttackCoolDown);
                print("能被攻擊");
                if (nowHP > 0)
                {
                    canBeAttack = true;
                }
                
            }
            else
            {
                yield break;
            }
        }

        public void HealPlayer(int healAmount)
        {
            if (healNum > 0)
            {
                playetInfo.nowHP += healAmount;
                nowHP += healAmount;

                if (nowHP >= maxHP)
                {
                    nowHP = maxHP;
                }
                healthBar.SetHealth(nowHP);
                gameObject.transform.GetChild(0).GetComponent<RPGCharacterEffect>().PlayhealEffect();
                healNum--;
                healText.text = healNum.ToString();
            }
            else
            {
                healText.text = "0";
                return;
            }
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
            //heavyAttackCollDown = playetInfo.heavyAttackCD;
            anim.runtimeAnimatorController = playetInfo.animatorController;


        }


        public IEnumerator AutoChangeCharacter()
        {
            canBeAttack = false;
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
            print("選完腳色，在四秒能被攻擊");
            yield return new WaitForSeconds(4f);
            print("換角完成，能被攻擊");
            canBeAttack = true;
        }

        public IEnumerator SetPlayerNormalAttackCD(float CD)
        {
            if (triggerNormalAttackCD == false)
            {
                canNormalAttack = false;
                CD = normalAttackCollDown;
                yield return new WaitForSeconds(CD);
                canNormalAttack = true;
                triggerNormalAttackCD = false;
            }
        }

        public void NormalAttackCD()
        {
            StartCoroutine(SetPlayerNormalAttackCD(normalAttackCollDown));
        }


        public void ResetPlayer()
        {
            foreach (var playerInfo in playerInfos)
            {
                playerInfo.maxHP = playerInfo.originMaxHP;
                playerInfo.nowHP = playerInfo.originMaxHP;
                playerInfo.def = playerInfo.originDef;
                playerInfo.Attack = playerInfo.originATK;
                playerInfo.isdie = false;
            }
        }

        public void GameOverResetPlayer()
        {
            foreach (var playerInfo in playerInfos)
            {
                playerInfo.nowHP = playerInfo.maxHP;
                playerInfo.isdie = false;
            }
            playerAmount = 4;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.transform.position = rpgGameManager.nowRespawnPoint.position;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            rpgCharacterEffect.ResetImageColor();
            SetPlayer();
        }



        public void PlayerLevelUP()
        {
            foreach (var playerInfo in playerInfos)
            {
                playerInfo.maxHP += 5;
                playerInfo.def += 1;
                playerInfo.Attack += 1;
                playerInfo.isdie = false;
                playerInfo.nowHP = playerInfo.maxHP;
            }
            rpgCharacterEffect.ResetImageColor();
            audioSource.PlayOneShot(levelUPSound);
            SetPlayer();


        }


    }
}
