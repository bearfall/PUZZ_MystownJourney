using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamTornado : MonoBehaviour
{
    public float duration;
    public Material material;

    public float targetValue = 1f; // 目標值
    public float fadeDuration = 2f; // 變化持續時間
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<ParticleSystemRenderer>().material;
        StartCoroutine(ChangeFloatPropertyOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * duration * Time.deltaTime * 1);
        
    }

    IEnumerator ChangeFloatPropertyOverTime()
    {
        float elapsedTime = 0f;
        float startValue = material.GetFloat("_Float");
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newValue = Mathf.Lerp(startValue, targetValue, t);
            material.SetFloat("_Float", newValue);
            yield return null;
        }

        // 確保最終值為目標值
        material.SetFloat("_Float", targetValue);
    }
}
