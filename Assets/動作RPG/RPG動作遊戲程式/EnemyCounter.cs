using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyCounter : MonoBehaviour
{
    public int lvUpAmount;

    public Canvas SettingCanva;
    public RPGGameManager rpgGameManager;
    public RPGCharacter rPGCharacter;
    public int enemyAmount = 3;
    public PlayableDirector playableDirector;

    public List<GameObject> enemyToDestory;
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
            rPGCharacter.PlayerLevelUP(lvUpAmount);

            if (playableDirector != null)
            {
                playableDirector.Play();
                SettingCanva.enabled = false;
            }
            foreach (var item in enemyToDestory)
            {
                Destroy(item);
            }
            rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
            enemyAmount = 3;

            

        }
    }

    


    

}
