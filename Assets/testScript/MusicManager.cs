using bearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    

    public AudioSource audioSource;

    public List<AudioClip> Music;
    

    

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBackgroundMusic(int musicNum)
    {
        
        audioSource.clip = Music[musicNum];
        audioSource.Play();

    }
}
