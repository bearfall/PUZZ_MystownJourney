using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGameObjectByPS : MonoBehaviour
{
    public int bossNum;
    [Header("���γ]�w")]
    public Transform bossObj;
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
        Destroy(bossObj.gameObject);
    }
    public void ChooseBoss(int Num)
    {
        switch (Num)
        {
            case 3:
                bossObj = GameObject.Find("���̴�").GetComponent<Transform>();
                break;
            
        }
    }
}
