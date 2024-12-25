using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RPGCharacterEffect : MonoBehaviour
{
    public List<PlayerInfo> playerInfos = new List<PlayerInfo>();
    public AudioSource audioSource;

    [Header("凱恩技能欲置物")]
    public List<GameObject> kyanEffects = new List<GameObject>();
    [Header("凱恩技能音效")]
    public List<AudioClip> kyanMusics = new List<AudioClip>();

    [Header("桑技能欲置物")]
    public List<GameObject> samEffects = new List<GameObject>();
    [Header("桑技能音效")]
    public List<AudioClip> samMusics = new List<AudioClip>();

    [Header("喬伊技能欲置物")]
    public List<GameObject> joeyEffects = new List<GameObject>();
    [Header("喬伊技能音效")]
    public List<AudioClip> joeyMusics = new List<AudioClip>();

    [Header("希瓦納技能欲置物")]
    public List<GameObject> evannaEffects = new List<GameObject>();
    [Header("希瓦納技能音效")]
    public List<AudioClip> evannaMusics = new List<AudioClip>();

    [Header("士兵技能欲置物")]
    public List<GameObject> enemyEffects = new List<GameObject>();
    [Header("士兵音效")]
    public List<AudioClip> enemyMusicEffects = new List<AudioClip>();
    [Header("擊中士兵增加能量數量")]
    public int IncreaseHeavyAttackEnergyValue;

    [Header("革厄斯死亡欲置物")]
    public List<GameObject> geusDeathEffects = new List<GameObject>();
    [Header("革厄斯死亡音效")]
    public List<AudioClip> geusMusicEffects = new List<AudioClip>();


    [Header("技能生成點")]
    public Transform effectSpawnPoint;

    [Header("頭像圖片")]
    public List<Image> playerHeadImage = new List<Image>();
    public List<Image> playerCDImage = new List<Image>();

    [Header("獲取程式區域")]
    public RPGCharacter rPGCharacter;
    public RPGEnemyCharacter rpGEnemyCharacter;
    public RPGPlayerController rPGPlayerController;
    public EnemyCounter enemyCounter;
    public GameObject healEffect;
    public CameraShake cameraShake;


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
        GameObject childObject = Instantiate(kyanEffects[effectNum], effectSpawnPoint);
        DetachAndPreserveRotation(childObject);
    }
    public void PlayKyanSound(int musicNum)
    { 
         audioSource.PlayOneShot(kyanMusics[musicNum]);
    }

    public void PlaySamEffect(int effectNum)
    {
       GameObject childObject = Instantiate(samEffects[effectNum], effectSpawnPoint);
       DetachAndPreserveRotation(childObject);
    }
    public void PlaySamSound(int musicNum)
    {
        audioSource.PlayOneShot(samMusics[musicNum]);
    }

    public void PlayJoryEffect(int effectNum)
    {
        GameObject childObject = Instantiate(joeyEffects[effectNum], effectSpawnPoint);
        DetachAndPreserveRotation(childObject);
    }
    public void PlayJoeySound(int musicNum)
    {
        audioSource.PlayOneShot(joeyMusics[musicNum]);
    }

    public void PlayEvannaEffect(int effectNum)
    {
        GameObject childObject = Instantiate(evannaEffects[effectNum], effectSpawnPoint);
        DetachAndPreserveRotation(childObject);
    }
    public void PlayEvannaSound(int musicNum)
    {
        audioSource.PlayOneShot(evannaMusics[musicNum]);
    }

    public void PlayEnemyEffect(int effectNum)
    {
        Instantiate(enemyEffects[effectNum], effectSpawnPoint);
    }
    public void PlayEnemySound(int musicNum)
    {
        audioSource.PlayOneShot(enemyMusicEffects[musicNum]);
    }


    public void PlayGeusDeathEffect(int effectNum)
    {
        Instantiate(geusDeathEffects[effectNum], effectSpawnPoint);
    }
    public void PlayGeusSound(int musicNum)
    {
        audioSource.PlayOneShot(geusMusicEffects[musicNum]);
    }

    public void StartEnemyAttackWorning()
    {
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_isAttack", 1);
    }

    public void StopEnemyAttackWorning()
    {
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_isAttack", 0);
    }

    public void IncreaseHeavyAttackEnergy()
    {
        rPGCharacter.heavyAttackCollDownSlider.value -= IncreaseHeavyAttackEnergyValue;
    }


    void DetachAndPreserveRotation(GameObject child)
    {
        // 取得子物件的世界空間座標和旋轉
        Vector3 worldPosition = child.transform.position;
        Quaternion worldRotation = child.transform.rotation;

        // 解除子物件與父物件的父子關係
        child.transform.SetParent(null);

        // 將子物件置於世界空間中
        child.transform.position = worldPosition;
        child.transform.rotation = worldRotation;
    }

    public void SetImageRed(int imageNum)
    {
        playerHeadImage[imageNum].color = Color.red;
        playerCDImage[imageNum].color = new Color(1, 1, 1, 0);
        rPGCharacter.playerAmount--;
    }

    public void ResetImageColor()
    {
        foreach (var image in playerHeadImage)
        {
            image.color = Color.white;
        }
        foreach (var image in playerCDImage)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
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
        print(enemyCounter.name + "減少1人");
        transform.parent.gameObject.SetActive(false);

    }

    public void PlaySound(string effectName)
    {

    }

    

    public void CameraShake(float dur)
    {
        cameraShake.ShakeCamera(1.5f,dur);
    }
}
