using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : MonoBehaviour
{
    public float flyingSpeed;
    public Transform target;
    


    public float startFlyTime = 0;
    public float startFlyTimer;

    public bool timing = true;


    void Start()
    {
        target = GameObject.Find("´ú¸Õ¨¤¦â").GetComponent<Transform>();
        
    }
    private void Update()
    {
        if (timing)
        {
            startFlyTimer += Time.deltaTime;
            transform.LookAt(target);
        }
        
        if (startFlyTimer >= startFlyTime)
        {
            
            StartCoroutine(StartMove());
            timing = false;
        }
        
    }

    public IEnumerator StartMove()
    {
        transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
    }
}
