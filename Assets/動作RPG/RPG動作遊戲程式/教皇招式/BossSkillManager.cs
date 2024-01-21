using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillManager : MonoBehaviour
{

    [Header("追蹤彈技能")]
    [Header("追蹤彈預製物")]
    public GameObject prefab; // 需要生成的物件
    [Header("追蹤彈預製物數量")]
    public int numberOfObjects = 6; // 物件的數量
    [Header("追蹤彈圓圈的半徑")]
    public float radius = 5f; // 圓圈的半徑
    [Header("追蹤彈圓圈中心座標")]
    public Vector3 center;// 圓圈的中心位置座標


    [Header("隨機移動爆炸技能")]
    [Header("隨機移動爆炸所有可能的點")]
    public Transform[] allPoints; // 所有可能的點
    [Header("隨機移動爆炸預製物")]
    public GameObject objectsToMovePrefeb;
    [Header("隨機移動爆炸要移動的物體(無須設定)")]
    public List<GameObject> objectsToMove; // 要移動的物體
    [Header("隨機移動爆炸音效")]
    public CharacterMusicEffect characterMusicEffect;
    [Header("隨機移動爆炸每次停留的時間")]
    public float waitTime = 5f; // 每次停留的時間
    [Header("隨機移動爆炸物體數量")]
    public float amount;
    public bool canMove = true;


    [Header("黑洞技能")]
    [Header("黑洞位置")]
    public Transform blackHoleTransform;
    [Header("黑洞預置物")]
    public GameObject blackHolePrefeb;


    [Header("閃電技能")]
    [Header("閃電技能預製物")]
    public GameObject objectToGenerate; // 要生成的物體
    [Header("閃電位置(無須設定)")]
    public List<Vector3> strikesTransform;
    [Header("閃電中心")]
    public Vector2 centerPoint;
    [Header("閃電技能數量")]
    public int numberOfStrikes = 5;     // 要生成的物體數量
    [Header("閃電技能範圍半徑")]
    public float spawnRadius = 10f;     // 生成的範圍半徑
    [Header("閃電技能之間的最小距離")]
    public float minDistance = 2f;     // 物體之間的最小距離
    [Header("閃電技能總持續時間")]
    public int strikesTime;
    [Header("閃電總次數")]
    public int strikesAmount = 5;
    [Header("閃電間隔時間")]
    public float waitStrikesTime = 4;
    [Header("紅色警告範圍預製物")]
    public GameObject warningRangeObject;
    [Header("directionalLight")]
    public Light directionalLight;
    [Header("audioSource")]
    public AudioSource audioSource;
    [Header("下雨預製物")]
    public GameObject rainGameObject;
    [Header("pointLight")]
    public GameObject pointLight;
    [Header("關燈")]
    public bool start = false;



    public CircleSpawner circleSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    
    public void TrackBallSkill()
    {
        circleSpawner.SpawnObjects();

    }
    
    /*
    public void RandomMovementBallSkill()
    {
        randomMovement.StartMove();
    }
    

    public void BlackHoleSkill()
    {
        Instantiate(blackHolePrefeb, blackHoleTransform);
    }
    

    public void RandomStrikeSkill()
    {
       StartCoroutine( randomStrike.GenerateObjects());
    }
    */

    #region 追蹤彈技能
    public void SpawnObjects()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius + center.x;
            float z = Mathf.Sin(angle) * radius + center.z;

            Vector3 spawnPosition = new Vector3(x, center.y, z);
            Quaternion spawnRotation = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);


            print(spawnPosition);
            Instantiate(prefab, spawnPosition, spawnRotation);
        }
    }
    #endregion

    #region 隨機移動爆炸技能
    public IEnumerator MoveObjects()
    {
        CanMove();
        while (canMove)
        {
            canMove = false;
            print("生成");

            // 選擇四個不同的點
            List<Transform> selectedPoints = new List<Transform>();
            List<int> selectedIndices = new List<int>();

            while (selectedPoints.Count < amount)
            {
                int randomIndex = Random.Range(0, allPoints.Length);
                if (!selectedIndices.Contains(randomIndex))
                {
                    selectedIndices.Add(randomIndex);
                    selectedPoints.Add(allPoints[randomIndex]);
                }
            }

            // 生成物體
            for (int i = 0; i < amount; i++)
            {

                var temp = Instantiate(objectsToMovePrefeb, transform.position, transform.rotation);
                objectsToMove.Add(temp);
                /*
                System.Array.Resize(ref objectsToMove, objectsToMove.Length + 1);
                objectsToMove[objectsToMove.Length - 1] = temp;
                */
                objectsToMove[i].transform.position = selectedPoints[i].position;

            }

            // 等待一段時間
            yield return new WaitForSeconds(waitTime);

            // 選擇四個新的不同的點
            selectedPoints.Clear();
            selectedIndices.Clear();

            while (selectedPoints.Count < amount)
            {
                int randomIndex = Random.Range(0, allPoints.Length);
                if (!selectedIndices.Contains(randomIndex))
                {
                    selectedIndices.Add(randomIndex);
                    selectedPoints.Add(allPoints[randomIndex]);
                }
            }

            // 移動到新的點
            for (int i = 0; i < amount; i++)
            {
                StartCoroutine(MoveTo(objectsToMove[i], selectedPoints[i].position));
            }

            // 等待一段時間
            yield return new WaitForSeconds(waitTime);
            CameraShake.Shake(5f, 1.5f);
            characterMusicEffect.PlayAttackSoundEffect();
            objectsToMove.Clear();

        }
    }

    IEnumerator MoveTo(GameObject obj, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveTime = 1f; // 移動所需的時間

        Vector3 startingPosition = obj.transform.position;

        while (elapsedTime < moveTime)
        {
            obj.transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition; // 確保物體準確到達目標位置
    }

    public void StartMove()
    {
        StartCoroutine(MoveObjects());
    }


    public void CanMove()
    {
        canMove = true;
    }
    #endregion

    #region 閃電技能
    public IEnumerator GenerateObjects()
    {

        start = true;

        rainGameObject.SetActive(true);
        pointLight.SetActive(true);
        for (int j = 0; j < strikesAmount; j++)
        {

            yield return new WaitForSeconds(waitStrikesTime);

            for (int i = 0; i < numberOfStrikes; i++)
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
    #endregion
}




