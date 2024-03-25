using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryPS : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
