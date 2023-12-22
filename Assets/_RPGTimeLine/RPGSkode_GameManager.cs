using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System;
using Flower;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class RPGSkode_GameManager : MonoBehaviour
{
    FlowerSystem flowerSys;
    private string myName;
    private string characterName;
    public static RPGSkode_GameManager ins;

    public TextMeshProUGUI dialogueLineText;

    PlayableDirector activeDirector;

    public Canvas canvas;

    public Image image;
    public Image image2;

    public List<Sprite> charactersImage;
    private void Awake()
    {
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample", false);
        ins = this;
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


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            // Continue the messages, stoping by [w] or [lr] keywords.
            flowerSys.Next();
            
            //ResumeTimeline();
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
    }

    public void ShowImage2(List<string> _params)
    {
        image2.enabled = true;
        image2.sprite = charactersImage[int.Parse(_params[0])];
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




}