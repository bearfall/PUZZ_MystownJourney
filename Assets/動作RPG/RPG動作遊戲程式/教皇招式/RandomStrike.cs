using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStrike : MonoBehaviour
{
    public GameObject objectToGenerate; // 要生成的物體

    public GameObject warningRangeObject;
    public int numberOfObjects = 5;     // 要生成的物體數量
    public float spawnRadius = 10f;     // 生成的範圍半徑
    public float minDistance = 2f;     // 物體之間的最小距離

    public List<Vector3> strikesTransform;

    public Vector2 centerPoint;
    void Start()
    {
        //GenerateObjects();
    }

    public IEnumerator GenerateObjects()
    {
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
