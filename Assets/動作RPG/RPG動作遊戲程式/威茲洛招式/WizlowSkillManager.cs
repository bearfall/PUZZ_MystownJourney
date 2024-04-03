using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;

public class WizlowSkillManager : MonoBehaviour
{
    public Transform playerTransform;

    [Header("衝擊波技能")]
    [Header("衝擊波預製物")]
    public GameObject shockWavePrefab; // 需要生成的物件
    [Header("衝擊波生成點(直)")]
    public List<Transform> shockWaveSpawnPoints;
    [Header("衝擊波生成點(橫)")]
    public List<Transform> randomShockWaveSpawnPoints;
    [Header("衝擊波數量")]
    public int shockWaveAmount;
    [Header("衝擊波位置")]
    public Transform wizlowShockWavePosition;
    [Header("衝擊波計時器")]
    public float shockWaveTime;
    public float shockWaveTimer;

    [Header("GP技能")]
    [Header("GP技能預製物")]
    public GameObject GPPrefab;
    public GameObject GPWorningPrefab;

    [Header("GP位置")]
    public Transform wizlowGPPosition;
    public Transform wizlowGPPosition2;
    [Header("GP位置2")]
    public Transform wizlowGPPosition3;
    public Transform wizlowGPPosition4;
    [Header("GP位置3")]
    public Transform wizlowGPPosition5;
    public Transform wizlowGPPosition6;

    [Header("衝鋒")]
    public Transform target;

    public float trackingSpeed = 5f; // 追蹤速度

    public float trackingDuration = 5f; // 追蹤時間

    private float trackingTimer;

    private bool isTracking = false;

    [Header("威茲洛衝鋒特效產生點")]
    public Transform rushEffectTransform;
    [Header("威茲洛衝鋒特效產生點")]
    public GameObject rushEffect1;
    [Header("威茲洛衝鋒特效產生點")]
    public GameObject rushEffect2;

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

    public GameObject player;
    public bool isFlip;
    public SpriteRenderer sr;//SpriteRenderer
    public List<GameObject> ghostList = new List<GameObject>();//残影列表


    [Header("Boss相關")]
    public Wizlow wizlow;
    public Animator anim;

    [Header("畫面效果相關")]
    public CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame



    void Update()
    {
        if (!wizlow.stop)
        {
            shockWaveTimer += Time.deltaTime;
            if (shockWaveTimer >= shockWaveTime)
            {
                StartCoroutine(ShockWave());
                shockWaveTimer = 0;
                shockWaveTime = 15;
            }
        }
        if (isTracking)
        {
            StartCoroutine(Rush());
        }

        if (openGhoseEffect == false)
        {
            return;
        }

        DrawGhost();
        Fade();
    }
    #region 衝擊波
    public IEnumerator ShockWave()
    {
        //直
        foreach (var item in shockWaveSpawnPoints)
        {
            item.transform.eulerAngles = new Vector3(0, Random.Range(80f, 100f), 0);
        }

        //gameObject.transform.DOMove(wizlowShockWavePosition.position, 1, false);
        //yield return new WaitForSeconds(1f);
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

        yield return new WaitForSeconds(1f);

        //橫
        foreach (var item in randomShockWaveSpawnPoints)
        {
            item.transform.eulerAngles = new Vector3(0, Random.Range(-30, 30), 0);
        }
        //gameObject.transform.DOMove(wizlowShockWavePosition.position, 1, false);
        //yield return new WaitForSeconds(1f);
        selectedPoints = new List<Transform>();
        selectedIndices = new List<int>();

        while (selectedPoints.Count < shockWaveAmount)
        {
            int randomIndex = Random.Range(0, randomShockWaveSpawnPoints.Count);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedPoints.Add(randomShockWaveSpawnPoints[randomIndex]);
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

    #region 衝擊波(亂)
    public IEnumerator ShockWaveRandom()
    {
        foreach (var item in randomShockWaveSpawnPoints)
        {
            item.transform.eulerAngles = new Vector3(0, Random.Range(-30, 30), 0);
        }
        gameObject.transform.DOMove(wizlowShockWavePosition.position, 1, false);
        yield return new WaitForSeconds(1f);
        List<Transform> selectedPoints = new List<Transform>();
        List<int> selectedIndices = new List<int>();

        while (selectedPoints.Count < shockWaveAmount)
        {
            int randomIndex = Random.Range(0, randomShockWaveSpawnPoints.Count);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedPoints.Add(randomShockWaveSpawnPoints[randomIndex]);
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
    [Header("岩石預製物")]
    public List< GameObject> rockSkillPrefabs; // 技能生成的物件
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
            if (!wizlow.stop)
            {
                cameraShake.ShakeCamera(1, 0.8f);
                // 在玩家位置生成技能物件
                Instantiate(rockSkillPrefabs[Random.Range(0, 2)], playerPosition.position, Quaternion.identity);
                currentRockCount++;

                // 等待一段時間再施放下一個技能
                yield return new WaitForSeconds(betweenRockTime);
            }
            else
            {
               yield break;
            }
        }
        currentRockCount = 0;
        
    }
    #endregion

    #region 地板動作
    public IEnumerator GP()
    {
        if (!wizlow.stop)
        {
            Vector3 tempGPPosition;
            tempGPPosition = playerTransform.position;
            Vector3 tempGPPositionUp = new Vector3(tempGPPosition.x, 6, tempGPPosition.z);
            Vector3 tempGPPositionDown = new Vector3(tempGPPosition.x, 0.67f, tempGPPosition.z);
            gameObject.transform.DOMove(tempGPPositionUp, 1f);
            Instantiate(GPWorningPrefab, tempGPPositionDown, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOMove(tempGPPositionDown, 0.3f);
            yield return new WaitForSeconds(0.3f);
            Instantiate(GPPrefab, tempGPPositionDown, Quaternion.identity);
            cameraShake.ShakeCamera(3, 1);
        }
    }
    #endregion

    #region 衝刺
    public void StartRush()
    {
        ghostList.Clear();
        openGhoseEffect = true;
        isTracking = true;
        anim.SetBool("rush", true);
        Instantiate(rushEffect1, rushEffectTransform);
        Instantiate(rushEffect2, rushEffectTransform);

    }

    public IEnumerator Rush()
    {
        if (!wizlow.stop)
        {
            yield return new WaitForSeconds(0f);
            trackingTimer += Time.deltaTime;
            if (trackingTimer >= trackingDuration)
            {
                isTracking = false;
                openGhoseEffect = false;
                trackingTimer = 0f;
                anim.SetBool("rush", false);
            }

            // 移動到玩家位置
            if (Vector3.Distance(transform.position, target.position) < 0.65f)
            {
                isTracking = false;
                yield return new WaitForSeconds(0.5f);
                isTracking = true;
                print("繼續移動");
            }
            RotateEnemy();
            var direction = target.position - transform.position;//目标方向
            transform.Translate(direction.normalized * trackingSpeed * Time.deltaTime);
            //transform.Translate(Vector3.forward * trackingSpeed * Time.deltaTime);
        }
    }
    /// <summary>
    /// 绘制残影
    /// </summary>
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

    /// <summary>
    /// 褪色操作
    /// </summary>
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
    #endregion

    #region 丟長槍


    [Header("丟長槍地點")]
    public List<Transform> throwWeaponPositions;
    public GameObject weapon;
    public Transform slashTransform;
    public GameObject slashObj;


    public IEnumerator ThrowingWeapon()
    {
        int randomIndex = Random.Range(0, throwWeaponPositions.Count);
        gameObject.transform.DOMove(throwWeaponPositions[randomIndex].position, 1f, false);
        yield return new WaitForSeconds(1f);
        GameObject temp =  Instantiate(weapon, gameObject.transform);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.4f);
        RotateEnemy();
        Instantiate(slashObj, slashTransform);


        //temp.transform.eulerAngles = new Vector3(0, temp.transform.rotation.y-90, 0);
    }
    #endregion

    public IEnumerator Slash()
    {
        StartCoroutine(RushToPlayer());
        transform.GetChild(0).eulerAngles = new Vector3(0, 180, 0);
        Instantiate(slashObj, slashTransform);
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(0).eulerAngles = new Vector3(0, 0, 0);
        Instantiate(slashObj, slashTransform);
        
    }



    public float activationDistance = 3f; // 触发技能的距离阈值
    public float teleportRadius = 2f; // 瞬移到玩家周围的半径
    public int numberOfProjectiles = 1; // 要施放的物体数量
    public float WidthMax = 10f; // 矩形范围的宽度
    public float widthMin = 10f; // 矩形范围的高度
    public float HeightMax = 10f; // 矩形范围的宽度
    public float HeightMin = 10f; // 矩形范围的高度
    private Vector3 teleportDestination;
    public IEnumerator RushToPlayer()
    {
        // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 如果敌人与玩家之间的距离小于阈值，则执行技能
        if (distanceToPlayer < activationDistance)
        {
            bool pointInRectangle = false;
            int attempts = 0;
            int maxAttempts = 10;

            while (!pointInRectangle && attempts < maxAttempts)
            {
                Vector2 randomPoint = Random.insideUnitCircle.normalized * teleportRadius;
                Vector3 tempteleport = playerTransform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
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
                teleportDestination = new Vector3(teleportDestination.x, transform.position.y, teleportDestination.z);
                transform.DOMove(teleportDestination, 0.5f, false).SetEase(Ease.InCirc);
                yield return new WaitForSeconds(0.5f);
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
    }

    public void RotateEnemy()
    {

        if (playerTransform != null && playerTransform.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.GetChild(0).eulerAngles = new Vector3(0, 0, 0);
            isFlip = false;
        }
        if (playerTransform != null && playerTransform.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.GetChild(0).eulerAngles = new Vector3(0, 180, 0);
            isFlip = true;
        }
    }

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
