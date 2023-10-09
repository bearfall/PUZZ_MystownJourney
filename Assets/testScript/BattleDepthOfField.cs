using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDepthOfField : MonoBehaviour
{
    public GameObject globalVolume;

   // public GameObject globalVolume1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGlobalVolume()
    {
        globalVolume.SetActive(true);

    }
    
    public void StopGlobalVolume()
    {
        globalVolume.SetActive(false);
      

    }
    
}
