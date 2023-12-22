using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorySelf : MonoBehaviour
{

    public float destiryTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destiryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
