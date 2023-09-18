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
            if (agent.velocity.magnitude == 0)
            {
                // ����b���ʤ�
                GetComponent<Collider>().enabled = true;
            }
            else
            {
                // ���⥼�b����
                GetComponent<Collider>().enabled = false;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BattleArea"))
            {
                testGameManager1.nowBattleArea = other.gameObject;
                other.gameObject.SetActive(false);
                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                testGameManager1.ChangeMyTurnStart();
                musicManager.PlayBackgroundMusic(MusicManager.SoundType.battle);
                testGameManager1.currentArea = TestGameManager1.AreaType.TurnBasedCombat;
                healthBar.SetActive(true);
                

                print("�i�J�԰��ϰ�");
            }
        }
        
        public void BackToFreeMove()
        {
            this.GetComponent<NavMeshAgent>().enabled = true;
            print("�}���ۥѲ��ʤF");

        }
    }
}
