using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloneBoss : MonoBehaviour
{
    [Header("追蹤彈預製物")]
    public GameObject darkBallObj;

    public float flashTime = 10;
    public float flashTimer;
    
    public bool canFlash = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("測試角色").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        flashTimer += Time.deltaTime;
        if (flashTimer >= flashTime)
        {
            canFlash = true;
            flashTimer = 0;
        }
        if (canFlash)
        {
            StartCoroutine(Flash());
            canFlash = false;
            flashTime = Random.Range(18, 20);
        }
        */




        if (openGhoseEffect == false)
        {
            return;
        }

        DrawGhost();
        Fade();
    }

    public Transform player; // 玩家角色的Transform
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
            Instantiate(darkBallObj, transform.position, darkBallObj.transform.rotation);
            yield return new WaitForSeconds(1f);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("skill"))
        {
            Destroy(gameObject);
        }
    }
}
