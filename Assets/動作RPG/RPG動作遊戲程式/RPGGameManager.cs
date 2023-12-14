using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class RPGGameManager : MonoBehaviour
    {
        public RPGCharacter nowRPGCharacter;

        public int enemyCount = 4;

        [Header("�{�b�԰��ϰ�")]
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                nowRPGCharacter.NormalAttack();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                nowRPGCharacter.HeavyAttack();
            }
        }
    }
}
