using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : MonoBehaviour
{
    public float flyingSpeed;
    public Transform target;


    void Start()
    {
        target = GameObject.Find("´ú¸Õ¨¤¦â").GetComponent<Transform>();
        transform.LookAt(target);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
    }
}
