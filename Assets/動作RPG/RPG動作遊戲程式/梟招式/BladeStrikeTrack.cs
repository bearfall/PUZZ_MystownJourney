using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeStrikeTrack : MonoBehaviour
{
    public Transform siaoTransform;
    public Vector3 targetPosition;
    public Transform PlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = GameObject.Find("´ú¸Õ¨¤¦â").GetComponent<Transform>();
        siaoTransform = GameObject.Find("±ú").GetComponent<Transform>();
        targetPosition = PlayerPosition.position;
        BladeStrikeMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BladeStrikeMove()
    {
        transform.DOMove(targetPosition, 0.5f).OnComplete(() =>
        {
            transform.DOMove(siaoTransform.position, 0.5f);
        });
    }
}
