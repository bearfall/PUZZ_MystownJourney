using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiaoBladeRain : MonoBehaviour
{
    public CameraShake cameraShake;

    public float moveTime;
    private float moveTimer;
    public bool startMove;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();

        moveTime = Random.Range(0.0f, 1f);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove == false)
        {
            moveTimer += Time.deltaTime;
        }
        if (moveTimer  >= moveTime)
        {
            startMove = true;
            moveTimer = 0;
            BladeRainMove();
        }
    }

    public void BladeRainMove()
    {
        transform.DOMoveY(0.63f, 0.5f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            cameraShake.ShakeCamera(0.1f, 0.5f);
        });
    }
}
