using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiaoTornado : MonoBehaviour
{
    public Transform siao;
    public float rotationSpeed;
    public float duration;
    public Material material;
    public float fadeInValue = 0f;
    public float fadeOutValue = 1f; // 目標值
    public float fadeDuration = 2f; // 變化持續時間
    // Start is called before the first frame update
    void Start()
    {
        siao = GameObject.Find("梟").GetComponent<Transform>();
        material = GetComponent<ParticleSystemRenderer>().material;
        StartCoroutine(ChangeFloatPropertyOverTime());

        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.Translate(Vector3.right * duration * Time.deltaTime * 1);
        transform.RotateAround(siao.position, Vector3.up, rotationSpeed * Time.deltaTime);

    }

    IEnumerator ChangeFloatPropertyOverTime()
    {
        float elapsedTime = 0f;
        float startValue = 1f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newValue = Mathf.Lerp(startValue, fadeInValue, t);
            material.SetFloat("_Float", newValue);
            yield return null;
        }

        // 確保最終值為目標值
        material.SetFloat("_Float", fadeInValue);

        yield return new WaitForSeconds(5f);
        StartCoroutine(ChangeFloatPropertyOverTime2());
    }
    IEnumerator ChangeFloatPropertyOverTime2()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newValue = Mathf.Lerp(startValue, fadeOutValue, t);
            material.SetFloat("_Float", newValue);
            yield return null;
        }

        // 確保最終值為目標值
        material.SetFloat("_Float", fadeOutValue);
    }
}
