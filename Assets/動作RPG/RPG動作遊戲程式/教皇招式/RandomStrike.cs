using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStrike : MonoBehaviour
{
    public Light directionalLight;

    public AudioSource audioSource;

    public GameObject rainGameObject;
    public GameObject pointLight;

    public GameObject objectToGenerate; // 要生成的物體

    public GameObject warningRangeObject;
    public int numberOfObjects = 5;     // 要生成的物體數量
    public float spawnRadius = 10f;     // 生成的範圍半徑
    public float minDistance = 2f;     // 物體之間的最小距離


    [Header("閃電技能總持續時間")]
    public int strikesTime;
    [Header("閃電總次數")]
    public int strikesAmount = 5;
    [Header("閃電間隔時間")]
    public float waitStrikesTime = 4;

    public bool start = false;

    public List<Vector3> strikesTransform;

    public Vector2 centerPoint;
    void Start()
    {
        //GenerateObjects();
    }

    private void Update()
    {
        if (start)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.0f, Time.deltaTime);

            
        }
        else
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 1.5f, Time.deltaTime);
        }

        
    }

    public IEnumerator GenerateObjects()
    {

        start = true;

        rainGameObject.SetActive(true);
        pointLight.SetActive(true);
        for (int j = 0; j < strikesAmount; j++)
        {

            yield return new WaitForSeconds(waitStrikesTime);

            for (int i = 0; i < numberOfObjects; i++)
            {
                Vector2 randomPos = centerPoint + Random.insideUnitCircle * spawnRadius;

                // 檢查與其他物體的距離
                while (IsTooCloseToOthers(randomPos))
                {
                    randomPos = centerPoint + Random.insideUnitCircle * spawnRadius;
                }

                strikesTransform.Add(new Vector3(randomPos.x, 0f, randomPos.y));
                // 實例化物體
                Instantiate(warningRangeObject, new Vector3(randomPos.x, 0.3f, randomPos.y), Quaternion.identity);
            }

            yield return new WaitForSeconds(2f);
            foreach (var strikeTransform in strikesTransform)
            {
                Instantiate(objectToGenerate, strikeTransform, Quaternion.identity);
                
            }
            audioSource.Play();
            strikesTransform.Clear();
        }

        yield return new WaitForSeconds(1f);

        rainGameObject.SetActive(false);
        pointLight.SetActive(false);
        start = false;

    }

    bool IsTooCloseToOthers(Vector2 position)
    {
        // 取得所有生成的物體
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag(objectToGenerate.tag);

        // 檢查與其他物體的距離
        foreach (var obj in generatedObjects)
        {
            float distance = Vector2.Distance(position, obj.transform.position);
            if (distance < minDistance)
            {
                return true; // 與其他物體太接近
            }
        }

        return false; // 與其他物體足夠遠
    }

}
