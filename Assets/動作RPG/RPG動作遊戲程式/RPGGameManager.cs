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
        [Header("場景限制範圍")]
        public GameObject menuConfiner;

        public RPGCharacter nowRPGCharacter;

        public int enemyCount = 4;

        [Header("現在戰鬥區域")]
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

        
    }
}
