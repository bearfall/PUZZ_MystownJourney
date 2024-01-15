using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterEffect : MonoBehaviour
{
    public List<PlayerInfo> playerInfos = new List<PlayerInfo>(); 

    [Header("技能生成點")]
    public Transform effectSpawnPoint;

    public RPGCharacter rPGCharacter;

    public RPGPlayerController rPGPlayerController;

    public EnemyCounter enemyCounter;

    public ParticleSystem healEffect;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNormalEffect()
    {

        Instantiate(rPGCharacter.characterNormalAttack, effectSpawnPoint);

    }
    public void PlayHeavyEffect()
    {
        
       // ParticleSystem particleSystem = rPGCharacter.characterHeavyAttack.GetComponent<ParticleSystem>();
        Instantiate(rPGCharacter.characterHeavyAttack, effectSpawnPoint);
        //particleSystem.Play();

    }

    public void EnhancePlayer()
    {

        foreach (var playerInfo in playerInfos)
        {

            playerInfo.Attack += 1;
        }
        //rPGCharacter.playetInfo.Attack += 1;
        rPGCharacter.isEnhance = true;
        rPGCharacter.SetPlayer();
    }

    public void PlayhealEffect()
    {
        healEffect.Play();

    }





    public void CanMove()
    {
        rPGPlayerController.isMovement = true;
    }
    public void CantMove()
    {
        rPGPlayerController.isMovement = false;
    }

    public void ReduceEnemy()
    {
        enemyCounter.enemyAmount--;

    }

    public void PlaySound(string effectName)
    {

    }
}
