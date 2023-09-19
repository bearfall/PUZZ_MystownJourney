using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAreaManager : MonoBehaviour
{
    public GameObject nowBattleArea;
    public TestCharactersManager testCharactersManager;
    // Start is called before the first frame update
    void Start()
    {
        testCharactersManager = GameObject.Find("Manager").GetComponent<TestCharactersManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
