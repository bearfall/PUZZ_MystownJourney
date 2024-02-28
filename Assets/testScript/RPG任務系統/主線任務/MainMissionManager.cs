using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMissionManager : MonoBehaviour
{
    public bool mainMission1;
    public bool mainMission2;
    public bool mainMission3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockMission(int missionNum)
    {
        switch (missionNum)
        {
            case 1:
                mainMission1 = true;
                break;
            case 2:
                mainMission2 = true;
                break;
            case 3:
                mainMission3 = true;
                break;

        }
    }
}
