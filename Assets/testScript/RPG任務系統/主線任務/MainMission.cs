using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMission : MonoBehaviour
{
    public MainMissionManager mainMissionManager;
    public int missionNum;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (missionNum)
            {
                case 1:
                    if (mainMissionManager.mainMission1)
                    {
                        canvas.SetActive(true);
                    }
                    break;
                case 2:
                    if (mainMissionManager.mainMission2)
                    {
                        canvas.SetActive(true);
                    }
                    break;
                case 3:
                    if (mainMissionManager.mainMission3)
                    {
                        canvas.SetActive(true);
                    }
                    break;

            }
        }
    }
}
