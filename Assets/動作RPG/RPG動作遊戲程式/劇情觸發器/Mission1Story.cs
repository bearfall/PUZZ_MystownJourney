using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mission1Story : MonoBehaviour
{
    public RPGPlayerController rPGPlayerController;
    public PlayableDirector playableDirector;
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
            
            playableDirector.Play();
            Destroy(gameObject);
        }
    }
}
