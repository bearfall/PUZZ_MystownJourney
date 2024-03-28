using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryPS : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.isStopped)
        {
            Destroy(gameObject);
        }
    }
    
}
