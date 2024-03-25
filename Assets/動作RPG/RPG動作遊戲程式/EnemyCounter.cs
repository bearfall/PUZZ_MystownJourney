using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyCounter : MonoBehaviour
{
    public Canvas SettingCanva;
    public RPGGameManager rpgGameManager;
    public RPGCharacter rPGCharacter;
    public int enemyAmount = 3;
    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        SettingCanva = GameObject.Find("技能資訊畫布").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAmount <= 0)
        {
            rPGCharacter.PlayerLevelUP();

            if (playableDirector != null)
            {
                playableDirector.Play();
                SettingCanva.enabled = false;
            }
            rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
            enemyAmount = 3;

        }
    }

    


    

}
