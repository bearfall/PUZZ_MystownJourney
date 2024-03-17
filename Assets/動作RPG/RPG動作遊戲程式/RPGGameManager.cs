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

        public UnityEngine.Rendering.Volume postProcessVolume;
        public UnityEngine.Rendering.Universal.DepthOfField dof;

        public Vector3 targetRotation = new Vector3(50f, 0f, 0f);
        public float rotationSpeed = 1f;

        public GameObject CMvcam1;
        public bool rotationCM;
        //public GameObject GlobalVolume;
        [Header("��������d��")]
        public GameObject menuConfiner;
        public GameObject bossConfiner;

        [Header("������Boss����")]
        public GameObject player;
        public Transform wizlowTransform;
        public Transform siaoTransform;
        public Transform bossTransform;

        public RPGCharacter nowRPGCharacter;

        public int enemyCount = 4;

        [Header("�¯��������I")]
        public Transform respawnPoint1;
        [Header("�Ьӭ����I")]
        public Transform respawnPoint2;
        [Header("�ثe�����I")]
        public Transform nowRespawnPoint;
        [Header("�{�b�԰��ϰ�")]
        public GameObject nowBattleArea;

        public enum AreaType
        {
            GameMenu,
            FreeExplore,
            Dialogue,
            TurnBasedCombat
        }
        public AreaType currentArea;
        // Start is called before the first frame update
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
            // ��e���ਤ��
        
            Vector3 currentRotation = CMvcam1.transform.eulerAngles;

            // �`���i����ઽ��F��ؼб��ਤ��
            while (currentRotation != targetRotation)
            {
                // �ϥ� Quaternion.Slerp �i�洡�ȱ���A�ϱ����[����
                Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
                Quaternion currentQuaternion = Quaternion.Euler(currentRotation);
                CMvcam1.transform.rotation = Quaternion.Slerp(currentQuaternion, targetQuaternion, Time.deltaTime * rotationSpeed);

                // ��s��e���ਤ��
                currentRotation = CMvcam1.transform.eulerAngles;

                // ���ݤ@�V
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
        [Header("Boss�M��")]
        public List<GameObject> bossList;
        [Header("�ثe����boss���X")]
        public int bossNum;

        public void SetBossNum(int num)
        {
            bossNum = num;
        }
        public void ResetBoss()
        {

            switch (bossNum)
            {
                case 0:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    break;
                case 3:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Wizlow>().stop = true;
                    break;
                case 6:
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Siao>().stop = true;
                    break;
                case 8:
                    print("���}�Ů���");
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().SetHealth();
                    bossList[bossNum].GetComponent<RPGEnemyCharacter>().OpenStoryTrigger();
                    bossList[bossNum].GetComponent<Boss>().stop = true;
                    break;
                default:
                    break;
            }
        }


    }
}
