using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public AudioClip[] clips; // 要切換的音頻列表
    public float fadeDuration = 1f; // 淡出和淡入的持續時間

    public AudioSource audioSource;
    public int currentClipIndex = 0;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = clips[currentClipIndex];
        //audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 可以更改成其他觸發器條件
        {
            // 切換到下一個音頻
            currentClipIndex = (currentClipIndex + 1) % clips.Length;
            StartCoroutine(FadeOutAndSwitch());
        }
    }

    IEnumerator FadeOutAndSwitch()
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        // 淡出
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        // 切換到下一個音頻
        audioSource.clip = clips[currentClipIndex];
        audioSource.Play();

        // 淡入
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, timer / fadeDuration);
            yield return null;
        }
    }
}
