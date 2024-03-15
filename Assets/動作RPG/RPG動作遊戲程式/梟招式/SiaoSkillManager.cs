using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiaoSkillManager : MonoBehaviour
{
    public Siao siao;

    [Header("刀環旋刃")]
    [Header("刀環物件")]
    public GameObject knifeRingGameObject;
    [Header("刀環數量")]
    public int knifeRingAmount;
    [Header("刀環半徑")]
    public float knifeRingRadius;

    public int BladeStrikeGetHarderChance = 30;

    // Start is called before the first frame update
    void Start()
    {
        //BladeRain();
    }


    
    // Update is called once per frame
    void Update()
    {
        if (!siao.stop)
        {
            bladeRainTimer += Time.deltaTime;
            if (bladeRainTimer >= bladeRainTime)
            {
                BladeRain();
                bladeRainTimer = 0;
            }
        }

        if (openGhoseEffect == false)
        {
            return;
        }

        DrawGhost();
        Fade();
    }

    public void SpawnKnifeRing()
    {

        if (GetHarder(BladeStrikeGetHarderChance))
        {
            StartCoroutine(BladeStrike());
        }
        
        // 計算圓周上的角度間隔
        float angleStep = 360f / knifeRingAmount;

        for (int i = 0; i < knifeRingAmount; i++)
        {
            // 計算每個物件的角度
            float angle = i * angleStep;

            // 計算每個物件的位置
            Vector3 positionOffset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * knifeRingRadius;
            Vector3 spawnPosition = transform.position + positionOffset;

            // 生成物件並設置位置
            GameObject newObject = Instantiate(knifeRingGameObject, transform.position, Quaternion.identity);
            // 設置物件的移動速度
            newObject.transform.DOMove(spawnPosition, 0.5f, false);
        }
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
    public IEnumerator BladeStrikeAttack()
    {
        // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 如果敌人与玩家之间的距离小于阈值，则执行技能
        if (distanceToPlayer < activationDistance)
        {
            bool pointInRectangle = false;
            
            while (!pointInRectangle)
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
                
            }
            ghostList.Clear();
            openGhoseEffect = true;
            transform.DOMove(teleportDestination, 0.5f, false);
            //transform.position = teleportDestination;
            // 面向玩家
            //transform.LookAt(player);

            // 施放指定数量的物体

            // 在敌人位置生成物体，并朝向玩家
            yield return new WaitForSeconds(0.7f);
            openGhoseEffect = false;
            GameObject projectile = Instantiate(bladeStrikePrefab, transform.position, Quaternion.identity);
            projectile.transform.LookAt(player);
            
        }
        else
        {
            print("瞬移失敗");
        }
    }

    public IEnumerator BladeStrike()
    {
        // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 如果敌人与玩家之间的距离小于阈值，则执行技能
        if (distanceToPlayer < activationDistance)
        {
            bool pointInRectangle = false;

            while (!pointInRectangle)
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

            }
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



    [Header("龍捲風欲置物")]
    public GameObject TornadoObj;
    [Header("龍捲風生成點")]
    public Transform TornadoTransform;
    public int TornadoGetHarderChance;
    public IEnumerator Tornado()
    {

        if (GetHarder(TornadoGetHarderChance))
        {
            StartCoroutine( BladeStrike());
        }
        Instantiate(TornadoObj, TornadoTransform.position, TornadoObj.transform.rotation);

        yield return new WaitForSeconds(2f);
        if (GetHarder(TornadoGetHarderChance))
        {
            StartCoroutine(BladeStrike());
        }
        Instantiate(TornadoObj, TornadoTransform.position, TornadoObj.transform.rotation);
        yield return new WaitForSeconds(2f);
        if (GetHarder(TornadoGetHarderChance))
        {
            StartCoroutine(BladeStrike());
        }
        Instantiate(TornadoObj, TornadoTransform.position, TornadoObj.transform.rotation);
    }






    [Header("刃雨欲置物")]
    public GameObject bladeRainObj;
    public float bladeRainTime = 14;
    public float bladeRainTimer;
    public int numberOfbladeRain; // 要生成的物体数量
    public Vector2 bottomLeft; // 矩形范围左下角坐标
    public Vector2 topRight; // 矩形范围右上角坐标


    public void BladeRain()
    {
        for (int i = 0; i < numberOfbladeRain; i++)
        {
            // 在矩形范围内生成随机位置
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            Vector3 spawnPosition = new Vector3(randomX, 6f, randomY);

            // 生成物体
            Instantiate(bladeRainObj, spawnPosition, bladeRainObj.transform.rotation);
        }
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

    public bool GetHarder(int chance)
    {
        int num = Random.Range(1, 100);
        return num <= chance;
    }
}
