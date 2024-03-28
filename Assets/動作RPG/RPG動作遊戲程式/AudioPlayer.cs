using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float canPlayTime;
    public float canPlayTimer;
    public bool canplay = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canplay)
        {
            canPlayTimer += Time.deltaTime;
        }
        
        if (canPlayTimer >= canPlayTime)
        {
            canPlayTimer = 0;
            audioSource.PlayOneShot(audioClip);
            canplay = false;
        }
    }
}
