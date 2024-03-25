using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mission1Story : MonoBehaviour
{
    public Canvas SettingCanva;
    public RPGPlayerController rPGPlayerController;
    public PlayableDirector playableDirector;
    public RPGGameManager rpgGameManager;

    public bool set1;
    public bool set2;
    // Start is called before the first frame update
    void Start()
    {
        rPGPlayerController = GameObject.Find("RPGGameManager").GetComponent<RPGPlayerController>();
        SettingCanva = GameObject.Find("�ޯ��T�e��").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
            SettingCanva.enabled = false;
            if (set1)
            {
                rpgGameManager.SetResetPoint1();
            }
            else if(set2)
            {
                rpgGameManager.SetResetPoint2();
            }
            
            other.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);
            playableDirector.Play();
            gameObject.SetActive(false);
        }
    }
}
