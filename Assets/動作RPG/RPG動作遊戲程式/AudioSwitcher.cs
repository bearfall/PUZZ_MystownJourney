using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public AudioClip[] clips; // �n���������W�C��
    public float fadeDuration = 1f; // �H�X�M�H�J������ɶ�

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
        if (other.CompareTag("Player")) // �i�H��令��LĲ�o������
        {
            // ������U�@�ӭ��W
            currentClipIndex = (currentClipIndex + 1) % clips.Length;
            StartCoroutine(FadeOutAndSwitch());
        }
    }

    IEnumerator FadeOutAndSwitch()
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        // �H�X
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        // ������U�@�ӭ��W
        audioSource.clip = clips[currentClipIndex];
        audioSource.Play();

        // �H�J
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, timer / fadeDuration);
            yield return null;
        }
    }
}
