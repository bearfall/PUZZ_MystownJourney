using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCopyMat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material _material = new Material(gameObject.GetComponent<SpriteRenderer>().material);
        gameObject.GetComponent<SpriteRenderer>().material = _material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
