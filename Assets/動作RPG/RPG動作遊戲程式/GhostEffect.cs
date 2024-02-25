using UnityEngine;
using System.Collections.Generic;
using RPGbearfall;

public class GhostEffect : MonoBehaviour
{
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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        isFlip = player.GetComponent<RPGPlayerController>().isFlip;


        if (openGhoseEffect == false)
        {
            return;
        }

        DrawGhost();
        Fade();
    }

    /// <summary>
    /// 绘制残影
    /// </summary>
    private void DrawGhost()
    {
        if (spawnTimer >= spawnTimeval)
        {
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
}