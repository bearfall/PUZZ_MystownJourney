using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RPGbearfall
{
    public class RPGPlayerController : MonoBehaviour
    {
        [Header("nowRPGCharacter")]
        public RPGCharacter nowRPGCharacter;

        [Header("角色input相關")]
        public Vector2 moveVector;

        [Header("殘影效果")]
        public GhostEffect ghostEffect;

        public bool isMovement = true;
        private NavMeshAgent agent;
        private RPGGameManager rPGGameManager;
       // private TestGUIManager testGUIManager;
        public GameObject healthBar;
        public MusicManager musicManager;

        public float horizontalinput;//水平参数
        public float Verticalinput;//垂直参数
        public float speed = 5f;//声明一个参数，没有规定
        public bool isFlip;
        [Header("閃現相關")]
        public bool canFlash = true;
        public bool usingFlash = false;
        public int maxFlash;
        public int flashAmount;
        public float flashTime;
        public float flashTimer;
        public Text flashText;

        public bool unlockHeal = false;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            rPGGameManager = GameObject.Find("RPGGameManager").GetComponent<RPGGameManager>();
            //testGUIManager = GameObject.Find("Manager").GetComponent<TestGUIManager>();
            musicManager = GameObject.Find("RPGGameManager").GetComponent<MusicManager>();
            //healthBar.SetActive(false);
        }
        private void Start()
        {
            musicManager.PlayBackgroundMusic(0);
            //MouseManager.Instance.OnMouseClicked += MoveToTarget;
        }
        public void MoveToTarget(Vector3Int target)
        {

            agent.destination = target;
        }

        private void Update()
        {
            if (rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && isMovement == true && !nowRPGCharacter.playetInfo.isdie)
            {


                horizontalinput = Input.GetAxis("Horizontal");
                //AD方向控制
                Verticalinput = Input.GetAxis("Vertical");

                /*
                if (Input.GetKey(KeyCode.A) && isMovement)
                {
                    //print("轉身");
                    //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                    gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
                    isFlip = true;
                    //gameObject.transform.GetChild(0).GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (Input.GetKey(KeyCode.D)&& isMovement)
                {
                    //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                    gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
                    isFlip = false;
                    //gameObject.transform.GetChild(0).GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
                }
                */
                if (moveVector != Vector2.zero)
                {
                    this.transform.position += new Vector3(moveVector.x, 0, moveVector.y) *Time.deltaTime *speed *0.6f;
                    gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 1);
                }
                else
                {
                    gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);
                }
                if (moveVector.x>0)
                {
                    gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
                    isFlip = false;
                }
                else if (moveVector.x < 0)
                {
                    gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
                    isFlip = true;
                }
                /*
                if (horizontalinput != 0 || Verticalinput != 0)
                {

                    gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 1);

                    horizontalinput = horizontalinput * 0.6f;
                    Verticalinput = Verticalinput * 0.6f;
                }
                else
                {
                    gameObject.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);
                }
                //WS方向控制
                this.transform.Translate(Vector3.right * horizontalinput * Time.deltaTime * speed);

                this.transform.Translate(Vector3.forward * Verticalinput * Time.deltaTime * speed);
                */
                //控制该物体向前后移动
            }
            /*
            if (Input.GetKeyDown(KeyCode.Mouse0) && nowRPGCharacter.canNormalAttack && rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && !nowRPGCharacter.playetInfo.isdie )
            {
                StartCoroutine(nowRPGCharacter.NormalAttack());
            }
            */
            /*
            if (Input.GetKeyDown(KeyCode.Mouse1) && rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && !nowRPGCharacter.playetInfo.isdie && isMovement)
            {
                StartCoroutine(nowRPGCharacter.HeavyAttack());
            }
            */
            /*
            if (Input.GetKeyDown(KeyCode.E) && rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && !nowRPGCharacter.playetInfo.isdie && unlockHeal)
            {
                nowRPGCharacter.HealPlayer(50);
            }
            */

            /*
            if (agent.velocity.magnitude == 0)
            {
                // 角色在移動中
                GetComponent<Collider>().enabled = true;
            }
            else
            {
                // 角色未在移動
                GetComponent<Collider>().enabled = false;
            }
            */
            flashTimer += Time.deltaTime;
            if (flashAmount >=1)
            {
                canFlash = true;
            }
            else
            {
                canFlash = false;
            }
            if (flashTimer >= flashTime && flashAmount < maxFlash)
            {
                flashAmount += 1;
                flashTimer = 0;
            }
            else if (flashAmount == maxFlash)
            {
                flashTimer = 0;
            }
            /*
            if (Input.GetKeyDown(KeyCode.LeftShift) && canFlash && isMovement && !usingFlash && !nowRPGCharacter.isChanging)
            {
                StartCoroutine(StartFlash());
            }
            */
            flashText.text = flashAmount.ToString();


        }

        public void Move(InputAction.CallbackContext ctx)
        {
            moveVector = ctx.ReadValue<Vector2>();
            print(moveVector);
        }

        public void Attack(InputAction.CallbackContext ctx)
        {
            if (ctx.started && nowRPGCharacter.canNormalAttack && rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && !nowRPGCharacter.playetInfo.isdie)
            {
                StartCoroutine(nowRPGCharacter.NormalAttack());
            }
        }
        public void Flash(InputAction.CallbackContext ctx)
        {
            if (ctx.started && canFlash && isMovement && !usingFlash && !nowRPGCharacter.isChanging)
            {
                StartCoroutine(StartFlash());
            }
        }
        public void Heal(InputAction.CallbackContext ctx)
        {
            if (ctx.started && rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && !nowRPGCharacter.playetInfo.isdie && unlockHeal)
            {
                nowRPGCharacter.HealPlayer(50);
            }
        }
        public void HeavyAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.started && rPGGameManager.currentArea == RPGGameManager.AreaType.FreeExplore && !nowRPGCharacter.playetInfo.isdie && isMovement)
            {
                StartCoroutine(nowRPGCharacter.HeavyAttack());
            }
        }
        public IEnumerator StartFlash()
        {
            flashAmount -= 1;
            usingFlash = true;
            ghostEffect.ghostList.Clear();
            ghostEffect.openGhoseEffect = true;
            speed = 10f;
            nowRPGCharacter.canBeAttack = false;
            yield return new WaitForSeconds(0.5f);
            ghostEffect.openGhoseEffect = false;
            speed = 5f;
            nowRPGCharacter.canBeAttack = true;
            yield return new WaitForSeconds(0.5f);
            usingFlash = false;
        }

        private IEnumerator OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BattleArea"))
            {
                rPGGameManager.nowBattleArea = other.gameObject;
                print(other.gameObject.name);
                other.gameObject.SetActive(false);
                StartCoroutine(other.gameObject.GetComponent<AreaCharacterInfo>().SetCharacterToChracters(other.gameObject.GetComponent<AreaCharacterInfo>().playerAreaCharacters, other.gameObject.GetComponent<AreaCharacterInfo>().enemyAreaCharacters));
                rPGGameManager.enemyCount = other.gameObject.GetComponent<AreaCharacterInfo>().enemyCount;
                yield return new WaitUntil(() => other.gameObject.GetComponent<AreaCharacterInfo>().areaSetDone == true);


                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<Collider>().enabled = false;

                TestCharacter testCharacter = this.GetComponent<TestCharacter>();

                rPGGameManager.currentArea = RPGGameManager.AreaType.TurnBasedCombat;

                Vector3 newPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
                transform.position = newPosition;
                //rPGGameManager.ChangeMyTurnStart();
                musicManager.PlayBackgroundMusic(0);

                healthBar.SetActive(true);


                print("進入戰鬥區域");
            }
        }

        
    }
}
