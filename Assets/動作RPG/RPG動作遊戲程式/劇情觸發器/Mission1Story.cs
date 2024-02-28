using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mission1Story : MonoBehaviour
{
    public RPGPlayerController rPGPlayerController;
    public PlayableDirector playableDirector;
    public RPGGameManager rpgGameManager;
    // Start is called before the first frame update
    void Start()
    {
        rPGPlayerController = GameObject.Find("RPGGameManager").GetComponent<RPGPlayerController>();
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
            other.transform.GetChild(0).GetComponent<Animator>().SetInteger("run", 0);
            playableDirector.Play();
            Destroy(gameObject);
        }
    }
}
