using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System;
using Flower;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class Skode_GameManager : MonoBehaviour
{
    FlowerSystem flowerSys;
    private string myName;
    public static Skode_GameManager ins;

    public TextMeshProUGUI dialogueLineText;

    PlayableDirector activeDirector;




    private void Awake()
    {
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample", false);
        ins = this;
    }
    public void Start()
    {
        myName = "Bearfall";
        flowerSys.SetVariable("MyName", myName);

        flowerSys.RegisterCommand("ResumeTimeline", ResumeTimeline);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            // Continue the messages, stoping by [w] or [lr] keywords.
            flowerSys.Next();
            
            //ResumeTimeline();
        }
    }

    //设置UI文字
    public void SetDialogue(string lineOfDialogue = null)
    {
        flowerSys.SetupDialog();
        flowerSys.ReadTextFromResource(lineOfDialogue.ToString());
        
        try
        {
            dialogueLineText.gameObject.SetActive(true);
        }

        catch (NullReferenceException) { };
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
}