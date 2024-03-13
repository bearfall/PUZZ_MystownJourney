using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPC1_Mission : MonoBehaviour
{
    public RPGGameManager rpgGameManager;

    public GameObject canvas;

    public bool canTalk = true;

    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canTalk)
        {
            print("可對話");
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                playableDirector.Play();
                rpgGameManager.ChangeAreaToDialogue();
                canTalk = false;
                canvas.SetActive(false);
            }
        }
    }
}
