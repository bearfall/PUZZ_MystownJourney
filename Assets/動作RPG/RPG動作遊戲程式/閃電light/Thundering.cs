using UnityEngine;
using System.Collections;

public class Thundering : MonoBehaviour
{
    Light thunderLight;

    

    public Color colorLight;

    [Header("变强光停留最大时间")]
    public float maxThunderDur = 0.5f;
    float thunderDuration;
    [Header("变弱光停留最大时间")]
    public float maxThunderBreakDur = 0.5f;
    float thunderBreakDuration;
    [Header("下一循环停留最大时间")]
    public float maxThunderRestDur = 0.5f;
    float thunderRestDur;

    int serialThunderTime;
    [Header("闪光次数")]
    public int maxSerialThunderTime = 5;


    [Header("最大强度范围")]
    public float IntensityMaxOne = 6f;
    public float IntensityMaxTwo = 10f;
    float Max;
    [Header("最小强度范围")]
    public float IntensityMinOne = 1.5f;
    public float IntensityMinTwo = 3f;
    float Min;

    void Awake()
    {
        thunderLight = GetComponent<Light>();
        thunderLight.color = colorLight;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Thunder());
    }
    void FixedUpdate()
    {
        thunderLight.color = colorLight;
    }
    IEnumerator Thunder()
    {
        while (true)
        {

            serialThunderTime = Random.Range(0, maxSerialThunderTime + 1);
            for (int i = 0; i < serialThunderTime; i++)
            {
                thunderDuration = Random.Range(0, maxThunderDur);
                Max = Random.Range(IntensityMaxOne, IntensityMaxTwo);
                thunderLight.intensity = Max;
                yield return new WaitForSeconds(thunderDuration);
                Min = Random.Range(IntensityMinOne, IntensityMinTwo);
                thunderLight.intensity = Min;
                thunderBreakDuration = Random.Range(0, maxThunderBreakDur);
                yield return new WaitForSeconds(thunderBreakDuration);
            }

            thunderRestDur = Random.Range(0, maxThunderRestDur);
            yield return new WaitForSeconds(maxThunderRestDur);
        }
    }
}