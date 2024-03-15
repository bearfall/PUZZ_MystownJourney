using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RespawnPoint : MonoBehaviour
{
    public GameObject canvas;
    public RPGGameManager rpgGameManager;
    public Transform thisRP;
    public bool canPush = false;

    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPush)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
                playableDirector.Play();
                SetRP();
                canPush = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            canPush = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
            canPush = false;
        }
    }

    public void SetRP()
    {
        rpgGameManager.nowRespawnPoint = thisRP;
    }

}
