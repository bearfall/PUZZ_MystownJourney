using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

namespace RPGbearfall
{
    public class RPGGameManager : MonoBehaviour
    {
        public static RPGGameManager instance;
        public UnityEngine.Rendering.Volume postProcessVolume;
        public UnityEngine.Rendering.Universal.DepthOfField dof;

        public Vector3 targetRotation = new Vector3(50f, 0f, 0f);
        public float rotationSpeed = 1f;

        public GameObject CMvcam1;
        public bool rotationCM;
        //public GameObject GlobalVolume;
        [Header("場景限制範圍")]
        public GameObject menuConfiner;
        public GameObject bossConfiner;

        [Header("切換到Boss相關")]
        public GameObject player;
        public Transform geusTransform;
        public Transform wizlowTransform;
        public Transform siaoTransform;
        public Transform bossTransform;

        public RPGCharacter nowRPGCharacter;

        public int enemyCount = 4;

        [Header("威茲洛重生點")]
        public Transform respawnPoint1;
        [Header("教皇重生點")]
        public Transform respawnPoint2;
        [Header("目前重生點")]
        public Transform nowRespawnPoint;
        [Header("現在戰鬥區域")]
        public GameObject nowBattleArea;

        public enum AreaType
        {
            GameMenu,
            FreeExplore,
            Dialogue,
            TurnBasedCombat
        }
        public AreaType currentArea;

        [Header("別的程式相關")]
        public RPGCharacter rpgCharacter;
        // Start is called before the first frame update
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            postProcessVolume.profile.TryGet<UnityEngine.Rendering.Universal.DepthOfField>(out dof);
            currentArea = AreaType.GameMenu;
        }

        
        public void StartGame()
        {
            StartCoroutine(RotateObject());
            currentArea = AreaType.FreeExplore;
            dof.focusDistance.value = 257f;
            CMvcam1.GetComponent<CinemachineConfiner>().m_BoundingVolume = menuConfiner.GetComponent<BoxCollider>();
        }
        public void StartGeusGame()
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = geusTransform.position;
            player.GetComponent<NavMeshAgent>().enabled = true;
            StartCoroutine(RotateObject());
            currentArea = AreaType.FreeExplore;
            dof.focusDistance.value = 257f;
            CMvcam1.GetComponent<CinemachineConfiner>().m_BoundingVolume = bossConfiner.GetComponent<BoxCollider>();
        }
        public void StartWizloeGame()
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = wizlowTransform.position;
            player.GetComponent<NavMeshAgent>().enabled = true;
            StartCoroutine(RotateObject());
            currentArea = AreaType.FreeExplore;
            dof.focusDistance.value = 257f;
            CMvcam1.GetComponent<CinemachineConfiner>().m_BoundingVolume = bossConfiner.GetComponent<BoxCollider>();
        }
        public void StartSiaoGame()
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = siaoTransform.position;
            player.GetComponent<NavMeshAgent>().enabled = true;
            StartCoroutine(RotateObject());
            currentArea = AreaType.FreeExplore;
            dof.focusDistance.value = 257f;
            CMvcam1.GetComponent<CinemachineConfiner>().m_BoundingVolume = bossConfiner.GetComponent<BoxCollider>();
        }
        public void StartBossGame()
        {
            print("開始遊戲");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = bossTransform.position;
            player.GetComponent<NavMeshAgent>().enabled = true;
            StartCoroutine(RotateObject());
            currentArea = AreaType.FreeExplore;
            dof.focusDistance.value = 257f;
            CMvcam1.GetComponent<CinemachineConfiner>().m_BoundingVolume = bossConfiner.GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {

           
        }

        IEnumerator RotateObject()
        {
            // 當前旋轉角度
        
            Vector3 currentRotation = CMvcam1.transform.eulerAngles;

            // 循環進行旋轉直到達到目標旋轉角度
            while (currentRotation != targetRotation)
            {
                // 使用 Quaternion.Slerp 進行插值旋轉，使旋轉更加平滑
                Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
                Quaternion currentQuaternion = Quaternion.Euler(currentRotation);
                CMvcam1.transform.rotation = Quaternion.Slerp(currentQuaternion, targetQuaternion, Time.deltaTime * rotationSpeed);

                // 更新當前旋轉角度
                currentRotation = CMvcam1.transform.eulerAngles;

                // 等待一幀
                yield return null;
            }
        }

        public void SetPlayer(RPGCharacter RPGCharacter)
        {
            nowRPGCharacter = RPGCharacter;
        }

        public void ChangeAreaToDialogue()
        {
            currentArea = RPGGameManager.AreaType.Dialogue;
        }

        public void SetResetPoint1()
        {
            nowRespawnPoint = respawnPoint1;
        }

        public void SetResetPoint2()
        {
            nowRespawnPoint = respawnPoint2;
        }
        [Header("Boss清單")]
        public List<GameObject> bossList;
        [Header("目前打的boss號碼")]
        public int bossNum;

        [Header("海克托小怪清單")]
        public List<GameObject> hectorMobs;

        [Header("威茲洛小怪清單")]
        public List<GameObject> wizlowMobs;

        [Header("作亂盜賊清單")]
        public List<GameObject> thiefList;


        [Header("區域管理")]
        public List<EnemyCounter> enemyCounters;

        public void SetBossNum(int num)
        {
            bossNum = num;
        }
        public IEnumerator ResetBoss()
        {

            switch (bossNum)
            {
                case 0:
                    //bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    break;
                case 1:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Hector>().stop = true;
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().ResetPosition();
                    DestroyAllOEnemySkills();
                    foreach (var item in hectorMobs)
                    {
                        item.GetComponent<RPGEnemyCharacter>().ResetSelf();
                    }
                    enemyCounters[1].enemyAmount = 3;
                    rpgCharacter.playerAmount = 4;
                    break;
                case 2:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Geus>().stop = true;
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().ResetPosition();
                    DestroyAllOEnemySkills();
                    rpgCharacter.playerAmount = 4;
                    break;
                case 3:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Wizlow>().stop = true;
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().ResetPosition();
                    DestroyAllOEnemySkills();
                    foreach (var item in wizlowMobs)
                    {
                        item.GetComponent<RPGEnemyCharacter>().ResetSelf();
                    }
                    enemyCounters[3].enemyAmount = 3;
                    rpgCharacter.playerAmount = 4;
                    break;
                case 5:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    foreach (var item in thiefList)
                    {
                        item.GetComponent<RPGEnemyCharacter>().ResetSelf();
                    }
                    enemyCounters[5].enemyAmount = 5;
                    break;
                case 6:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Siao>().stop = true;
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().ResetPosition();
                    DestroyAllOEnemySkills();
                    rpgCharacter.playerAmount = 4;
                    break;
                case 7:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<GeusSmall>().stop = true;
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().ResetPosition();
                    DestroyAllOEnemySkills();
                    rpgCharacter.playerAmount = 4;
                    break;
                case 8:
                    print("打開空氣牆");
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    yield return new WaitForSeconds(1f);
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Boss>().stop = true;
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().ResetPosition();
                    //bossList[bossNum].GetComponent<BossSkillManager>().DestroyAllObjects();
                    DestroyAllOEnemySkills();
                    rpgCharacter.playerAmount = 4;
                    break;
                default:
                    break;
            }
        }
        public void DestroyAllOEnemySkills()
        {
            // 獲取所有具有指定標籤的遊戲物件
            GameObject[] objectsWithTag1 = GameObject.FindGameObjectsWithTag("EnemySkill");
            GameObject[] objectsWithTag2 = GameObject.FindGameObjectsWithTag("cloneBoss");

            List<GameObject> objectsWithTag = new List<GameObject>();
            objectsWithTag.AddRange(objectsWithTag1);
            objectsWithTag.AddRange(objectsWithTag2);

            // 遍歷所有物件並銷毀它們
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }


    }
}
