using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizlowSkillManager : MonoBehaviour
{
    [Header("衝擊波技能")]
    [Header("衝擊波預製物")]
    public GameObject shockWavePrefab; // 需要生成的物件
    [Header("衝擊波生成點")]
    public List<Transform> shockWaveSpawnPoints;
    [Header("衝擊波數量")]
    public int shockWaveAmount;
    [Header("衝擊波位置")]
    public Transform wizlowShockWavePosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region 衝擊波
    public void ShockWave()
    {
        gameObject.transform.position = wizlowShockWavePosition.position;
        List<Transform> selectedPoints = new List<Transform>();
        List<int> selectedIndices = new List<int>();

        while (selectedPoints.Count < shockWaveAmount)
        {
            int randomIndex = Random.Range(0, shockWaveSpawnPoints.Count);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedPoints.Add(shockWaveSpawnPoints[randomIndex]);
            }
        }

        for (int i = 0; i < shockWaveAmount; i++)
        {

            var temp = Instantiate(shockWavePrefab, selectedPoints[i]);
            //temp.transform.position = selectedPoints[i].position;
            //DetachAndPreserveRotation(temp);

        }
    }
    #endregion

    #region 岩石
    public GameObject rockSkillPrefab; // 技能生成的物件
    public Transform playerPosition;
    public int maxrockCount = 20; // 技能施放的最大次數
    public float betweenRockTime = 0.5f; // 每次施放技能的間隔
    private int currentRockCount = 0; // 目前施放的技能次數

    public void StartRockSkill()
    {
        StartCoroutine(RockSkill());
    }

    public IEnumerator RockSkill()
    {
        while (currentRockCount < maxrockCount)
        {
            // 在玩家位置生成技能物件
            Instantiate(rockSkillPrefab, playerPosition.position, Quaternion.identity);
            currentRockCount++;

            // 等待一段時間再施放下一個技能
            yield return new WaitForSeconds(betweenRockTime);
        }
        currentRockCount = 0;
    }
    #endregion

    void DetachAndPreserveRotation(GameObject child)
    {
        // 取得子物件的世界空間座標和旋轉
        Vector3 worldPosition = child.transform.position;
        Quaternion worldRotation = child.transform.rotation;

        // 解除子物件與父物件的父子關係
        child.transform.SetParent(null);

        // 將子物件置於世界空間中
        child.transform.position = worldPosition;
        child.transform.rotation = worldRotation;
    }
}
