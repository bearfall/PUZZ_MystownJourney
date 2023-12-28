using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class RPGGameManager : MonoBehaviour
    {
        public RPGCharacter nowRPGCharacter;

        public int enemyCount = 4;

        [Header("現在戰鬥區域")]
        public GameObject nowBattleArea;

        public enum AreaType
        {
            FreeExplore,
            TurnBasedCombat
        }
        public AreaType currentArea;
        // Start is called before the first frame update
        void Start()
        {
            currentArea = AreaType.FreeExplore;
        }

        // Update is called once per frame
        void Update()
        {
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
        }


        public void SetPlayer(RPGCharacter RPGCharacter)
        {
            nowRPGCharacter = RPGCharacter;
        }

        
    }
}
