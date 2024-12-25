using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TipsSystem : MonoBehaviour
{
    public float YAmount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveObject()
    {
        transform.DOMoveY(YAmount, 0.5f, false);
        yield return new WaitForSeconds(5f);
        transform.DOMoveY(350, 0.5f, false);
    }
}
