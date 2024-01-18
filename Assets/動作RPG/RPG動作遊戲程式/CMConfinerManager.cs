using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMConfinerManager : MonoBehaviour
{
    public List<GameObject> CMConfiners = new List<GameObject>();
    public CinemachineConfiner cinemachineConfiner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCMConfinerObj(int area)
    {
        cinemachineConfiner.m_BoundingVolume = CMConfiners[area].GetComponent<BoxCollider>();
    }
}
