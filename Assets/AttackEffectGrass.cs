using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectGrass : MonoBehaviour
{
    public Transform GrassParent;
    public GameObject character;
    public List<SpriteRenderer> Grasses;
    // Start is called before the first frame update
    void Start()
    {
        GrassParent = GameObject.Find("¯ó¶°¦X").GetComponent<Transform>();
        Grasses = new List<SpriteRenderer>();
        GrassParent.GetComponentsInChildren(Grasses);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShakeGrass()
    {
        foreach (var grass in Grasses)
        {
            if (Vector3.Distance(character.transform.position, grass.transform.position) < 3)
            {
                grass.material.SetFloat("_GrassShake", 5);
            }

        }

    }
    public void StopShakeGrass()
    {
        foreach (var grass in Grasses)
        {
           
            grass.material.SetFloat("_GrassShake", 1);
            

        }

    }
}
