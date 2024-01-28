using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterEffect : MonoBehaviour
{
    public List<PlayerInfo> playerInfos = new List<PlayerInfo>(); 

    [Header("凱恩技能欲置物")]
    public List<GameObject> kyanEffects = new List<GameObject>();
    [Header("桑技能欲置物")]
    public List<GameObject> samEffects = new List<GameObject>();
    [Header("喬伊技能欲置物")]
    public List<GameObject> joeyEffects = new List<GameObject>();
    [Header("希瓦納技能欲置物")]
    public List<GameObject> evannaEffects = new List<GameObject>();


    [Header("技能生成點")]
    public Transform effectSpawnPoint;

    public RPGCharacter rPGCharacter;

    public RPGPlayerController rPGPlayerController;

    public EnemyCounter enemyCounter;

    public ParticleSystem healEffect;


    public List<GameObject> effects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayKyanEffect(int effectNum)
    {
        Instantiate(kyanEffects[effectNum], effectSpawnPoint);
    }

    public void PlaySamEffect(int effectNum)
    {
        Instantiate(samEffects[effectNum], effectSpawnPoint);
    }

    public void PlayJoryEffect(int effectNum)
    {
        Instantiate(joeyEffects[effectNum], effectSpawnPoint);
    }

    public void PlayEvannaEffect(int effectNum)
    {
        Instantiate(evannaEffects[effectNum], effectSpawnPoint);
    }






    public void PlayEffect(int effectNum)
    {
        Instantiate(effects[effectNum], effectSpawnPoint);
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
