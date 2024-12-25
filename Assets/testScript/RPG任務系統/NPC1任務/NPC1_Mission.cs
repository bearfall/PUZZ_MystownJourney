using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class NPC1_Mission : MonoBehaviour
{
    public GameObject player;

    public Canvas SettingCanva;

    public RPGGameManager rpgGameManager;

    public GameObject canvas;

    public bool canTalk = false;

    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        SettingCanva = GameObject.Find("技能資訊畫布").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("可對話");
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
    */
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canTalk)
        {
            print("可對話");
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                SettingCanva.SetActive(false);
                playableDirector.Play();
                rpgGameManager.ChangeAreaToDialogue();
                canTalk = false;
                canvas.SetActive(false);
            }
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
            canTalk = false;
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canTalk)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
                SettingCanva.enabled = false;
                canvas.SetActive(false);
                playableDirector.Play();
                other.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);

                canTalk = false;
            }
        }
    }
    */

    public void Talk(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canTalk)
        {
            rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
            SettingCanva.enabled = false;
            canvas.SetActive(false);
            playableDirector.Play();
            if (player != null)
            {
                player.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);
            }
            canTalk = false;
        }
    }
}
