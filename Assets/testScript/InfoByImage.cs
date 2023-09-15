using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoByImage : MonoBehaviour
{
    public GameObject CM01;
    public GameObject Character;
    public ScreenChange screenChange;
    // Start is called before the first frame update
    void Start()
    {
        screenChange = CM01.GetComponent<ScreenChange>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AssignCharacyer()
    {
        screenChange.ChangesScreenToCharacter(Character);



    }
}
