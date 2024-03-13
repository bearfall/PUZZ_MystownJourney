using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingBomb : MonoBehaviour
{
    public float flyingSpeed;
    public Transform target;


    void Start()
    {
        target = GameObject.Find("測試角色").GetComponent<Transform>();
        transform.LookAt(target);

        


    }
    private void Update()
    {
        transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
    }

}
