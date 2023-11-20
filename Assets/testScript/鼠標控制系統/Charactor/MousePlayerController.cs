using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace bearfall
{
    public class MousePlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private TestGameManager1 testGameManager1;
        private TestGUIManager testGUIManager;
        public GameObject healthBar;
        public MusicManager musicManager;

        public float horizontalinput;//水平参数
        public float Verticalinput;//垂直参数
        float speed = 5f;//声明一个参数，没有规定


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            testGameManager1 = GameObject.Find("Manager").GetComponent<TestGameManager1>();
            testGUIManager = GameObject.Find("Manager").GetComponent<TestGUIManager>();
            musicManager = GameObject.Find("Manager").GetComponent<MusicManager>();
            healthBar.SetActive(false);
        }
        private void Start()
        {
            musicManager.PlayBackgroundMusic(MusicManager.SoundType.explore);
            MouseManager.Instance.OnMouseClicked += MoveToTarget;
        }
        public void MoveToTarget(Vector3Int target)
        {

            agent.destination = target;
        }

        private void Update()
        {
            if (testGameManager1.currentArea == TestGameManager1.AreaType.FreeExplore)
            {


                horizontalinput = Input.GetAxis("Horizontal");
                //AD方向控制
                Verticalinput = Input.GetAxis("Vertical");


                if (Input.GetKey(KeyCode.A))
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                else if(Input.GetKey(KeyCode.D))
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }


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
                //控制该物体向前后移动
            }


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
        }


        private IEnumerator OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BattleArea"))
            {
                testGameManager1.nowBattleArea = other.gameObject;
                print(other.gameObject.name);
                other.gameObject.SetActive(false);
                StartCoroutine( other.gameObject.GetComponent<AreaCharacterInfo>().SetCharacterToChracters(other.gameObject.GetComponent<AreaCharacterInfo>().playerAreaCharacters, other.gameObject.GetComponent<AreaCharacterInfo>().enemyAreaCharacters));
                testGameManager1.enemyCount = other.gameObject.GetComponent<AreaCharacterInfo>().enemyCount;
                yield return new WaitUntil(() => other.gameObject.GetComponent<AreaCharacterInfo>().areaSetDone == true);

                
                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<Collider>().enabled = false;

                TestCharacter testCharacter = this.GetComponent<TestCharacter>();

                testGameManager1.currentArea = TestGameManager1.AreaType.TurnBasedCombat;

                Vector3 newPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
                transform.position = newPosition;
                testGameManager1.ChangeMyTurnStart();
                musicManager.PlayBackgroundMusic(MusicManager.SoundType.battle);
                
                healthBar.SetActive(true);
                

                print("進入戰鬥區域");
            }
        }
        
        public void BackToFreeMove()
        {
            this.GetComponent<NavMeshAgent>().enabled = true;
            print("腳色能自由移動了");

        }
    }
}
