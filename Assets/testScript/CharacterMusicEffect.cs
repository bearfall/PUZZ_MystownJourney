using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMusicEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> attackSoundEffect = new List<AudioClip>();


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttackSoundEffect()
    {
        foreach (var sound in attackSoundEffect)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
