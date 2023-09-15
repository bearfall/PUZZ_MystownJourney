using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangesScreenToCharacter(GameObject character)
    {
        GetComponent<CinemachineVirtualCamera>().Follow = character.transform;
        print("´«¨¤¦â¬Ý");



    }
}
