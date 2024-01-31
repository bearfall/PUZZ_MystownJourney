using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public RPGCharacter nowRPGCharacter;

        public int enemyCount = 4;

        [Header("�{�b�԰��ϰ�")]
        public GameObject nowBattleArea;

        public enum AreaType
        {
            GameMenu,
            FreeExplore,
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

        
    }
}
