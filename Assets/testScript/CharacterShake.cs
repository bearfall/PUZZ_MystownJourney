using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShake : MonoBehaviour
{
    public GameObject belownCharacter;

    public int x;
    public int y;
    public int z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCharacter()
    {
        belownCharacter.GetComponent<TestCharacter>().Shake(x, y, z);
        
    }
}
