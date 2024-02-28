using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyCounter : MonoBehaviour
{
    public RPGGameManager rpgGameManager;
    public RPGCharacter rPGCharacter;
    public int enemyAmount = 3;
    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
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
            }
            rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
            enemyAmount = 3;

        }
    }

    


    

}
