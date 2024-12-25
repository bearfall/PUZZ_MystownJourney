using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System;
using Flower;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using RPGbearfall;
using DG.Tweening;

public class RPGSkode_GameManager : MonoBehaviour
{
    public GameObject player;
    public FlowerSystem flowerSys;
    private string myName;
    private string characterName;
    public static RPGSkode_GameManager ins;

    public TextMeshProUGUI dialogueLineText;

    PlayableDirector activeDirector;

    public Canvas canvas;

    public Image image;
    public Image image2;

    public List<Sprite> charactersImage;

    [Header("碰撞器(全)")]
    public List<Collider> colliders;

    [Header("對話系統相關")]
    public RPGGameManager rpgGameManager;
    public RPGPlayerController rpgPlayerController;
    public CMConfinerManager cmConfinerManager;
    public CameraShake cameraShake;
    public MainMissionManager mainMissionManager;
    public Canvas SettingCanva;
    [Header("Boss相關系統")]
    public Boss boss;
    public GameObject bossHealthBar;
    public Wizlow wizlow;
    public GameObject wizlowHealthBar;
    public Siao siao;
    public GameObject siaoHealthBar;
    public Geus geus;
    public GameObject geusHealthBar;
    public GeusSmall geusSmall;
    public GameObject geusSmallHealthBar;
    public Hector hector;
    public GameObject hectorHealthBar;

    [Header("計時系統")]
    public Timer timer;

    [Header("提示系統")]
    public List<Animator> tipsAni;

    public RPGCharacter rpgCharacter;
    private void Awake()
    {
        
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample", true);
        ins = this;
        print("FlowerSystem存在");
        
    }
    public void Start()
    {
        flowerSys.SetupDialog();
        flowerSys.ReadTextFromResource("Hide");
        myName = "Bearfall";
        characterName = "666";
        flowerSys.SetVariable("MyName", myName);
        flowerSys.SetVariable("CharacterName", characterName);

        flowerSys.RegisterCommand("ResumeTimeline", ResumeTimeline);
        flowerSys.RegisterCommand("ChangeName", ChangeName);
        flowerSys.RegisterCommand("ShowImage", ShowImage);
        flowerSys.RegisterCommand("ShowImage2", ShowImage2);
        flowerSys.RegisterCommand("HideImage", HideImage);
        flowerSys.RegisterCommand("HideImage2", HideImage2);
        flowerSys.RegisterCommand("unlockHeal", unlockHeal);
        flowerSys.RegisterCommand("StartBossFight", StartBossFight);
        flowerSys.RegisterCommand("StopBossFight", StopBossFight);
        flowerSys.RegisterCommand("DoingDialogue", DoingDialogue);
        flowerSys.RegisterCommand("StopDialogue", StopDialogue);
        flowerSys.RegisterCommand("SwitchCMConfinerObj", SwitchCMConfinerObj);
        flowerSys.RegisterCommand("ShakeCamera", ShakeCamera);
        flowerSys.RegisterCommand("UnlockMission", UnlockMission);

        flowerSys.RegisterCommand("StartHectorFight", StartHectorFight);
        flowerSys.RegisterCommand("StopHectorFight", StopHectorFight);

        flowerSys.RegisterCommand("StartGeusFight", StartGeusFight);
        flowerSys.RegisterCommand("StopGeusFight", StopGeusFight);

        flowerSys.RegisterCommand("StartGeusSmallFight", StartGeusSmallFight);
        flowerSys.RegisterCommand("StopGeusSmallFight", StopGeusSmallFight);

        flowerSys.RegisterCommand("StartWizlowFight", StartWizlowFight);
        flowerSys.RegisterCommand("StopWizlowFight", StopWizlowFight);
        flowerSys.RegisterCommand("SetNowBoss", SetNowBoss);
        flowerSys.RegisterCommand("StartSiaoFight", StartSiaoFight);
        flowerSys.RegisterCommand("StopSiaoFight", StopSiaoFight);

        flowerSys.RegisterCommand("MovePlayer", MovePlayer);

        flowerSys.RegisterCommand("EndGameTimer", EndGameTimer);

        flowerSys.RegisterCommand("Tipping", Tipping);

        flowerSys.RegisterCommand("Walk", Walk);
        flowerSys.RegisterCommand("StopWalk", StopWalk);






    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            // Continue the messages, stoping by [w] or [lr] keywords.
            flowerSys.Next();
            
            //ResumeTimeline();
        }
        */
        
    }
    public void Skip(InputAction.CallbackContext ctx)
    {
        if (ctx.started && rpgGameManager.currentArea == RPGGameManager.AreaType.Dialogue)
        {
            flowerSys.Next();
        }
    }
    /*
    public void MissionStart()
    {


        isMissionStart = true;


        if (flowerSys.isCompleted && isMissionStart && !isEnemyDie)
        {
            flowerSys.SetupDialog();
            flowerSys.ReadTextFromResource("startMission 1");
            //flowerSys.RemoveDialog();

            print("任務開始");





        }
        if (flowerSys.isCompleted && isMissionStart && isEnemyDie)
        {
            //flowerSys.SetupDialog();
            flowerSys.ReadTextFromResource("Mission1_TakeBox");
            // flowerSys.RemoveDialog();

        }


    }
    */

    //设置UI文字
    public void SetDialogue(string lineOfDialogue = null)
    {
        
        flowerSys.ReadTextFromResource(lineOfDialogue.ToString());
        
        /*
        try
        {
            dialogueLineText.gameObject.SetActive(true);
        }

        catch (NullReferenceException) { };
        */
    }

    //暂停TimeLine
    public void PauseTimeline(PlayableDirector whichOne)
    {
        activeDirector = whichOne;

        activeDirector.Pause();
    }

    //恢复播放TimeLine
    public void ResumeTimeline(List<string> _params = null)
    {
        activeDirector.Resume();
    }


    public void ChangeName(List<string> _params)
    {
        characterName = _params[0].ToString();
        flowerSys.SetVariable("CharacterName", characterName);
        print(characterName);
    }

    public void ShowImage(List<string> _params)
    {
        
        image.enabled = true;
        image.sprite = charactersImage[int.Parse(_params[0])];
        image2.color = new Color(0.5f, 0.5f, 0.5f, 1);
        image.color = new Color(1f, 1f, 1f, 1);
    }

    public void ShowImage2(List<string> _params)
    {
        image2.enabled = true;
        image2.sprite = charactersImage[int.Parse(_params[0])];
        image.color = new Color(0.5f, 0.5f, 0.5f, 1);
        image2.color = new Color(1f, 1f, 1f, 1);
    }


    public void HideImage(List<string> _params = null)
    {
        image.enabled = false;
        //image.sprite = charactersImage[int.Parse(_params[0])];
    }

    public void HideImage2(List<string> _params = null)
    {
        image2.enabled = false;
        //image.sprite = charactersImage[int.Parse(_params[0])];
    }

    public void unlockHeal(List<string> _params = null)
    {
        rpgPlayerController.unlockHeal = true;
    }

    public void StartBossFight(List<string> _params = null)
    {
        boss.StartBossAction();
        bossHealthBar.SetActive(true);
    }
    public void StopBossFight(List<string> _params = null)
    {
        boss.StopBossAction();
        //bossSkillManager.DestroyAllObjects();
        RPGGameManager.instance.DestroyAllOEnemySkills();
        bossHealthBar.SetActive(false);
    }


    public void StartHectorFight(List<string> _params = null)
    {
        hector.StartHectorAction();
        hectorHealthBar.SetActive(true);
    }
    public void StopHectorFight(List<string> _params = null)
    {
        hector.StopHectorAction();
        hectorHealthBar.SetActive(false);
    }


    public void StartGeusFight(List<string> _params = null)
    {
        geus.StartGeusAction();
        geusHealthBar.SetActive(true);
    }
    public void StopGeusFight(List<string> _params = null)
    {
        geus.StopGeusAction();
        geusHealthBar.SetActive(false);
    }

    public void StartGeusSmallFight(List<string> _params = null)
    {
        geusSmall.StartGeusSmallAction();
        geusSmallHealthBar.SetActive(true);
    }
    public void StopGeusSmallFight(List<string> _params = null)
    {
        geusSmall.StopGeusSmallAction();
        geusSmallHealthBar.SetActive(false);
    }

    public void StartWizlowFight(List<string> _params = null)
    {
        wizlow.StartwizlowAction();
        wizlowHealthBar.SetActive(true);
    }
    public void StopWizlowFight(List<string> _params = null)
    {
        wizlow.StopWizlowAction();
        wizlowHealthBar.SetActive(false);
    }
    public void StartSiaoFight(List<string> _params = null)
    {
        siao.StartSiaoAction();
        siaoHealthBar.SetActive(true);
    }
    public void StopSiaoFight(List<string> _params = null)
    {
        siao.StopSiaoAction();
        siaoHealthBar.SetActive(false);
    }

    public void DoingDialogue(List<string> _params = null)
    {
        rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
        
    }

    public void StopDialogue(List<string> _params = null)
    {
        rpgGameManager.currentArea = RPGGameManager.AreaType.FreeExplore;
        SettingCanva.enabled = true;
    }

    public void SwitchCMConfinerObj(List<string> _params = null)
    {
        cmConfinerManager.SwitchCMConfinerObj(int.Parse(_params[0]));
    }

    public void ShakeCamera(List<string> _params = null)
    {
        cameraShake.ShakeCamera(float.Parse(_params[0]), float.Parse(_params[1]));
    }

    public void UnlockMission(List<string> _params = null)
    {
        mainMissionManager.UnlockMission(int.Parse(_params[0]));
    }

    public void TurnOffCollider(List<string> _params = null)
    {
        colliders[int.Parse(_params[0])].enabled = false;
    }

    public void SetNowBoss(List<string> _params = null)
    {
        rpgGameManager.SetBossNum(int.Parse(_params[0]));
    }
    public void MovePlayer(List<string> _params = null)
    {
        player.transform.DOMove(new Vector3(float.Parse(_params[0]), 0.71f, float.Parse(_params[1])), 0.8f, false);
    }

    public void EndGameTimer(List<string> _params = null)
    {
        timer.StopTimer();
    }

    public void Tipping(List<string> _params = null)
    {
        tipsAni[int.Parse(_params[0])].SetTrigger("tip");
    }
    public void Walk(List<string> _params = null)
    {
        player.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 1);
    }
    public void StopWalk(List<string> _params = null)
    {
        player.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);
    }


}