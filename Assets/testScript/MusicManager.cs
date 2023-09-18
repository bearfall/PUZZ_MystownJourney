using bearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public TestGameManager1 testGameManager1;

    public AudioSource audioSource;

    public AudioClip exploreMusic;
    public AudioClip battleMusic;

    public enum SoundType
    {
        explore,
        battle,
        
    }

   

    // Start is called before the first frame update
    void Start()
    {
        testGameManager1 = GetComponent<TestGameManager1>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBackgroundMusic(SoundType selectedSound)
    {
        switch (selectedSound)
        {
            case SoundType.explore:
                audioSource.clip =  exploreMusic;
                audioSource.Play();
                break;

            case SoundType.battle:
                audioSource.clip = battleMusic;
                audioSource.Play();
                break;

            default:
                break;
        }
    }
}
