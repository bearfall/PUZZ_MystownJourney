using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRingTrack : MonoBehaviour
{
    public Transform targetPlayer;
    [Header("���ݮɶ�")]
    public float waitTime;
    private float waitTimer = 0;
    private bool isWait = true;

    [Header("�l�ܳt��")]
    public float trackingSpeed = 5f;
    private float trackingTimer;
    [Header("�l�ܮɶ�")]
    public float trackingDuration = 5f;
    private bool isTracking = true;
    [Header("�����t��")]
    public float flyingSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("���ը���").GetComponent<Transform>();
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
            // ����l�ܫ�A�~��H�ثe�e�i��V����
            transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
        }
    }
    IEnumerator TrackPlayer()
    {


        
        yield return new WaitForSeconds(0f);
        // �ϥ� LookAt �Ӱl�ܪ��a
        transform.LookAt(targetPlayer);

        // �l�ܤ@�q�ɶ��ᰱ��
        trackingTimer += Time.deltaTime;
        if (trackingTimer >= trackingDuration)
        {
            isTracking = false;
            trackingTimer = 0f;
        }

        // ���ʨ쪱�a��m
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
