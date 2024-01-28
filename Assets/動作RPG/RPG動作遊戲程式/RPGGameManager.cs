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

        public GameObject CMvcam1;

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
            currentArea = AreaType.FreeExplore;
            dof.focusDistance.value = 257f;
            CMvcam1.GetComponent<CinemachineConfiner>().m_BoundingVolume = menuConfiner.GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.Mouse0) && nowRPGCharacter.canNormalAttack)
            {
                StartCoroutine(nowRPGCharacter.NormalAttack());
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && nowRPGCharacter.canHeavyAttack)
            {
                StartCoroutine(nowRPGCharacter.HeavyAttack());
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
               StartCoroutine( nowRPGCharacter.HealPlayer(50));
            }
            */
        }


        public void SetPlayer(RPGCharacter RPGCharacter)
        {
            nowRPGCharacter = RPGCharacter;
        }

        
    }
}
