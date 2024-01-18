using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMConfinerArea : MonoBehaviour
{

    public int areaNumber;

    public CMConfinerManager cmConfinerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            cmConfinerManager.SwitchCMConfinerObj(areaNumber);
        }
    }
}
