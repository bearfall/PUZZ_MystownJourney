using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRingTrack : MonoBehaviour
{
    public Transform targetPlayer;
    [Header("等待時間")]
    public float waitTime;
    private float waitTimer = 0;
    private bool isWait = true;

    [Header("追蹤速度")]
    public float trackingSpeed = 5f;
    private float trackingTimer;
    [Header("追蹤時間")]
    public float trackingDuration = 5f;
    private bool isTracking = true;
    [Header("飛走速度")]
    public float flyingSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("測試角色").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWait)
        {
            Wait();
        }
        else if (isTracking)
        {

            StartCoroutine(TrackPlayer());
        }
        else
        {
            // 當停止追蹤後，繼續以目前前進方向飛行
            transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
        }
    }
    IEnumerator TrackPlayer()
    {


        
        yield return new WaitForSeconds(0f);
        // 使用 LookAt 來追蹤玩家
        transform.LookAt(targetPlayer);

        // 追蹤一段時間後停止
        trackingTimer += Time.deltaTime;
        if (trackingTimer >= trackingDuration)
        {
            isTracking = false;
            trackingTimer = 0f;
        }

        // 移動到玩家位置
        transform.Translate(Vector3.forward * trackingSpeed * Time.deltaTime);


    }
    private void Wait()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer > waitTime)
        {
            waitTimer = 0;
            isWait = false;
            isTracking = true;
            
        }
    }
}
