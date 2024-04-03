using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossSkillManager : MonoBehaviour
{

    [Header("玩家位置")]
    public Transform targetPosition;
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


    


    [Header("閃電技能")]
    [Header("閃電技能變難機率")]
    public int strikeGetHarderChance;
    [Header("閃電技能計時器")]
    public float strikesTime;
    private float strikesTimer;
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

    


    public Boss boss;
    public CameraShake cameraShake;

    public CircleSpawner circleSpawner;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CloneBoss());
    }
    [Header("閃現相關")]
    public float flashTime = 10;
    public float flashTimer;
    public bool canFlash = false;

    // Update is called once per frame
    void Update()
    {
        

        if (!boss.stop)
        {
            flashTimer += Time.deltaTime;
            strikesTimer += Time.deltaTime;
        }
        else
        {
            flashTimer = 0;
            strikesTimer = 0;
        }
        if (flashTimer >= flashTime)
        {
            canFlash = true;

            flashTimer = 0;
        }
        if (canFlash)
        {
            StartCoroutine(CloneBoss());

            GameObject[] cloneBosses = GameObject.FindGameObjectsWithTag("cloneBoss");
            foreach (var item in cloneBosses)
            {
                StartCoroutine(item.GetComponent<CloneBoss>().Flash());
            }
            StartCoroutine(Flash());
            flashTime = 10;
            canFlash = false;
        }
        if (strikesTimer >= strikesTime)
        {
            StartCoroutine(GenerateObjects());
            strikesTimer = 0;
            strikesTime = 55;
        }

        if (start)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.0f, Time.deltaTime);
        }
        else
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 2.5f, Time.deltaTime);
        }



        if (openGhoseEffect == false)
        {
            return;
        }

        DrawGhost();
        Fade();
    }
    
    public void TrackBallSkill()
    {
        circleSpawner.SpawnObjects();

    }


    #region 追蹤彈技能

    [Header("追蹤彈技能")]
    [Header("追蹤彈預製物")]
    public GameObject darkBallObj; // 需要生成的物件
    [Header("追蹤彈預製物數量")]
    public int numberOfObjects = 6; // 物件的數量
    [Header("追蹤彈圓圈的半徑")]
    public float radius = 5f; // 圓圈的半徑
    [Header("追蹤彈圓圈中心座標")]
    public Vector3 center;// 圓圈的中心位置座標

    public IEnumerator ShotDarkBall()
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
            GameObject temp =  Instantiate(darkBallObj, spawnPosition, darkBallObj.transform.rotation);
            temp.GetComponent<DarkBall>().startFlyTime = i - 0.8f*i;
        }
        yield return new WaitForSeconds(0f);
        /*
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(LittleFlash());
            yield return new WaitForSeconds(0.5f);
            GameObject obj = Instantiate(darkBallObj, transform.position, darkBallObj.transform.rotation);
            yield return new WaitForSeconds(0.5f);
            
        }
        */

        /*
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius + center.x;
            float z = Mathf.Sin(angle) * radius + center.z;

            Vector3 spawnPosition = new Vector3(x, center.y, z);
            Quaternion spawnRotation = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);


            print(spawnPosition);
            Instantiate(darkBallObj, spawnPosition, spawnRotation);
        }
        */
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
            if (objectsToMove != null)
            {
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
                cameraShake.ShakeCamera(1.5f, 5f);
                characterMusicEffect.PlayAttackSoundEffect();
            }
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

    #region 隨機選擇火焰爆炸
    [Header("火焰爆炸位置")]
    public List<Transform> randomFirePositions; // 存儲9個目標位置的陣列
    [Header("火焰爆炸物件")]
    public GameObject fireBallObj; // 要移動的物體

    public IEnumerator RandomFireBall()
    {
        // 隨機選擇一個目標位置
        int randomIndex = Random.Range(0, randomFirePositions.Count);
        Transform target = randomFirePositions[randomIndex];

        GameObject temp = Instantiate(fireBallObj,transform.position, fireBallObj.transform.rotation);
        // 使用 DOTween 將物體移動到所選擇的目標位置
        temp.transform.DOMove(target.position, 1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1.6f);
        cameraShake.ShakeCamera(1.5f, 5f);
        yield return new WaitForSeconds(0.0f);

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
            if (!boss.stop)
            {
                

                yield return new WaitForSeconds(waitStrikesTime);
                if (GetHarder(strikeGetHarderChance))
                {
                    ShotDarkBall();
                }
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
                    GameObject obj =  Instantiate(warningRangeObject, new Vector3(randomPos.x, 0.3f, randomPos.y), Quaternion.identity);
                    
                }

                yield return new WaitForSeconds(2f);
                foreach (var strikeTransform in strikesTransform)
                {
                    GameObject obj = Instantiate(objectToGenerate, strikeTransform, Quaternion.identity);
                    
                }
                audioSource.Play();
                strikesTransform.Clear();
            }
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

    #region 雷射技能

    [Header("雷射技能")]
    [Header("雷射技能預製物")]
    public GameObject laserPrefab;
    [Header("雷射生成點")]
    public Transform[] spawnPoints; // 生成點
    
    [Header("雷射速度")]
    public float laserSpeed = 10f; // 雷射速度
    [Header("技能持續時間")]
    public float duration = 3f; // 技能持續時間
    [Header("生成範圍半徑")]
    public float laserSpawnRadius = 5f; // 生成範圍半徑


    public void StartLaserSkill()
    {
        StartCoroutine(LaserSkill());
    }

    public IEnumerator LaserSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!boss.stop)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    // 在指定的範圍內隨機生成一個點
                    Vector3 randomDirection = Random.onUnitSphere;
                    Vector3 randomPoint = new Vector3(Random.Range(-laserSpawnRadius, laserSpawnRadius) + spawnPoint.position.x, spawnPoint.position.y, Random.Range(-laserSpawnRadius, laserSpawnRadius) + spawnPoint.position.z);

                    //float randomx = Random.Range(0.0f, 360f);

                    //GameObject laser = Instantiate(laserPrefab, randomPoint, new Quaternion(randomx, 0, 0, 0));
                    GameObject laser = Instantiate(laserPrefab, randomPoint, Quaternion.identity);
                    Destroy(laser, duration); // 在指定時間後銷毀雷射
                }
                yield return new WaitForSeconds(3f);
            }
        }
    }

    #endregion

    #region 複製教皇
    [Header("複製教皇暗黑球")]
    public GameObject cloneBossObj;
    [Header("複製教皇暗黑球2")]
    public GameObject cloneBossObj2;
    public Transform cloneBossTransform;

    
    public IEnumerator CloneBoss()
    {
        int cloneBossAmout = CountObjectsWithTag();
        
        

        if (cloneBossAmout == 0)
        {
            //transform.DOMove(cloneBossTransform.position, 0.8f, false);
            //yield return new WaitForSeconds(0.8f);
            GameObject obj =  Instantiate(cloneBossObj, transform.position, cloneBossObj.transform.rotation);
            
            obj = Instantiate(cloneBossObj2, transform.position, cloneBossObj.transform.rotation);
            

        }
        if (cloneBossAmout == 1)
        {
            GameObject obj = Instantiate(cloneBossObj, transform.position, cloneBossObj.transform.rotation);
            

        }
        if (cloneBossAmout == 2)
        {
            
            yield break;
        }
        print("等兩秒");
        yield return new WaitForSeconds(3f);

    }
    private int CountObjectsWithTag()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("cloneBoss");
        return objectsWithTag.Length;
    }
    #endregion

    public bool GetHarder(int chance)
    {
        int num = Random.Range(1, 100);
        return num <= chance;
    }


    public Transform player; // 玩家角色的Transform
    public GameObject bladeStrikePrefab; // 要施放的物体
    public float activationDistance = 3f; // 触发技能的距离阈值
    public float teleportRadius = 2f; // 瞬移到玩家周围的半径
    public int numberOfProjectiles = 1; // 要施放的物体数量
    public float WidthMax = 10f; // 矩形范围的宽度
    public float widthMin = 10f; // 矩形范围的高度
    public float HeightMax = 10f; // 矩形范围的宽度
    public float HeightMin = 10f; // 矩形范围的高度
    private Vector3 teleportDestination;
    public IEnumerator Flash()
    {
        for (int i = 0; i < 3; i++)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 如果敌人与玩家之间的距离小于阈值，则执行技能
            if (distanceToPlayer < activationDistance)
            {
                bool pointInRectangle = false;
                int attempts = 0;
                int maxAttempts = 10;
                while (!pointInRectangle && attempts < maxAttempts)
                {
                    Vector2 randomPoint = Random.insideUnitCircle.normalized * teleportRadius;
                    Vector3 tempteleport = player.position + new Vector3(randomPoint.x, 0, randomPoint.y);
                    //Vector3 teleportDestination = player.position + new Vector3(randomPoint.x, 0, randomPoint.y);

                    if (tempteleport.x >= widthMin && tempteleport.x <= WidthMax &&
                        tempteleport.z >= HeightMin && tempteleport.z <= HeightMax)
                    {
                        teleportDestination = tempteleport;
                        pointInRectangle = true;
                    }
                    attempts++;

                }
                if (pointInRectangle)
                {
                    ghostList.Clear();
                    openGhoseEffect = true;
                    transform.DOMove(teleportDestination, 0.5f, false);
                    yield return new WaitForSeconds(0f);
                    openGhoseEffect = false;
                }
                else
                {
                    print("瞬移失敗");
                }
            }
            else
            {
                print("瞬移失敗");
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator LittleFlash()
    {
        for (int i = 0; i < 1; i++)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 如果敌人与玩家之间的距离小于阈值，则执行技能
            if (distanceToPlayer < activationDistance)
            {
                bool pointInRectangle = false;
                int attempts = 0;
                int maxAttempts = 10;
                while (!pointInRectangle && attempts < maxAttempts)
                {
                    Vector2 randomPoint = Random.insideUnitCircle.normalized * teleportRadius;
                    Vector3 tempteleport = player.position + new Vector3(randomPoint.x, 0, randomPoint.y);
                    //Vector3 teleportDestination = player.position + new Vector3(randomPoint.x, 0, randomPoint.y);

                    if (tempteleport.x >= widthMin && tempteleport.x <= WidthMax &&
                        tempteleport.z >= HeightMin && tempteleport.z <= HeightMax)
                    {
                        teleportDestination = tempteleport;
                        pointInRectangle = true;
                    }
                    attempts++;

                }
                if (pointInRectangle)
                {
                    ghostList.Clear();
                    openGhoseEffect = true;
                    transform.DOMove(teleportDestination, 0.5f, false);
                    yield return new WaitForSeconds(0.5f);
                    openGhoseEffect = false;
                }
                else
                {
                    print("瞬移失敗");
                }
                
            }
            else
            {
                print("瞬移失敗");
            }
            yield return new WaitForSeconds(1f);
        }
    }

    [Header("黑洞技能")]
    [Header("黑洞位置")]
    public Transform blackHoleTransform;
    [Header("黑洞預置物")]
    public GameObject blackHolePrefeb;

    public void BlackHole()
    {
        Instantiate(blackHolePrefeb, targetPosition.position, blackHolePrefeb.transform.rotation);
    }
    

    [Header("是否开启残影效果")]
    public bool openGhoseEffect;
    [Header("是否开启褪色消失")]
    public bool openFade;
    [Header("显示残影的持续时间")]
    public float durationTime;
    [Header("生成残影与残影之间的时间间隔")]
    public float spawnTimeval;
    private float spawnTimer;//生成残影的时间计时器

    [Header("残影颜色")]
    public Color ghostColor;
    [Header("残影层级")]
    public int ghostSortingOrder;


    public bool isFlip;
    public SpriteRenderer sr;//SpriteRenderer
    public List<GameObject> ghostList = new List<GameObject>();//残影列表

    private void DrawGhost()
    {
        if (spawnTimer >= spawnTimeval)
        {
            //audioSource.PlayOneShot(audioSource.clip);

            spawnTimer = 0;

            GameObject _ghost = new GameObject();
            ghostList.Add(_ghost);
            _ghost.name = "ghost";
            _ghost.AddComponent<SpriteRenderer>();
            _ghost.transform.position = transform.position;
            _ghost.transform.localScale = transform.localScale;

            if (isFlip)
            {
                _ghost.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (!isFlip)
            {
                _ghost.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            SpriteRenderer _sr = _ghost.GetComponent<SpriteRenderer>();
            _sr.sprite = sr.sprite;
            _sr.material = sr.material;

            _sr.sortingOrder = ghostSortingOrder;
            _sr.color = ghostColor;



            if (openFade == false)
            {
                Destroy(_ghost, durationTime);
            }
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    private void Fade()
    {
        if (openFade == false)
        {
            return;
        }

        for (int i = 0; i < ghostList.Count; i++)
        {
            SpriteRenderer ghostSR = ghostList[i].GetComponent<SpriteRenderer>();
            if (ghostSR.color.a <= 0)
            {
                GameObject tempGhost = ghostList[i];
                ghostList.Remove(tempGhost);
                Destroy(tempGhost);
            }
            else
            {
                float fadePerSecond = (ghostColor.a / durationTime);
                Color tempColor = ghostSR.color;
                tempColor.a -= fadePerSecond * Time.deltaTime;
                ghostSR.color = tempColor;
            }
        }
    }

    

}




