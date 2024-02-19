using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterEffect : MonoBehaviour
{
    public List<PlayerInfo> playerInfos = new List<PlayerInfo>();
    public AudioSource audioSource;

    [Header("�ͮ��ޯ���m��")]
    public List<GameObject> kyanEffects = new List<GameObject>();
    [Header("�ͮ��ޯ୵��")]
    public List<AudioClip> kyanMusics = new List<AudioClip>();

    [Header("��ޯ���m��")]
    public List<GameObject> samEffects = new List<GameObject>();
    [Header("��ޯ୵��")]
    public List<AudioClip> samMusics = new List<AudioClip>();

    [Header("���ޯ���m��")]
    public List<GameObject> joeyEffects = new List<GameObject>();
    [Header("���ޯ୵��")]
    public List<AudioClip> joeyMusics = new List<AudioClip>();

    [Header("�ƥ˯ǧޯ���m��")]
    public List<GameObject> evannaEffects = new List<GameObject>();
    [Header("�ƥ˯ǧޯ୵��")]
    public List<AudioClip> evannaMusics = new List<AudioClip>();

    [Header("�h�L�ޯ���m��")]
    public List<GameObject> enemyEffects = new List<GameObject>();


    [Header("�ޯ�ͦ��I")]
    public Transform effectSpawnPoint;


    public RPGCharacter rPGCharacter;
    public RPGEnemyCharacter rpGEnemyCharacter;

    public RPGPlayerController rPGPlayerController;

    public EnemyCounter enemyCounter;

    public GameObject healEffect;


    public List<GameObject> effects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    
    

    public void PlayKyanEffect(int effectNum)
    {
        Instantiate(kyanEffects[effectNum], effectSpawnPoint);
    }
    public void PlayKyanSound(int musicNum)
    { 
         audioSource.PlayOneShot(kyanMusics[musicNum]);
    }

    public void PlaySamEffect(int effectNum)
    {
        Instantiate(samEffects[effectNum], effectSpawnPoint);
    }
    public void PlaySamSound(int musicNum)
    {
        audioSource.PlayOneShot(samMusics[musicNum]);
    }

    public void PlayJoryEffect(int effectNum)
    {
        Instantiate(joeyEffects[effectNum], effectSpawnPoint);
    }
    public void PlayJoeySound(int musicNum)
    {
        audioSource.PlayOneShot(joeyMusics[musicNum]);
    }

    public void PlayEvannaEffect(int effectNum)
    {
        Instantiate(evannaEffects[effectNum], effectSpawnPoint);
    }
    public void PlayEvannaSound(int musicNum)
    {
        audioSource.PlayOneShot(evannaMusics[musicNum]);
    }

    public void PlayEnemyEffect(int effectNum)
    {
        Instantiate(enemyEffects[effectNum], effectSpawnPoint);
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
        Instantiate(healEffect, effectSpawnPoint);

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
