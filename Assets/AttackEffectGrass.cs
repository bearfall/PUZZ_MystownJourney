using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AttackEffectGrass : MonoBehaviour
{
    public Transform GrassParent;
    public Transform VFXGrassParent;
    public GameObject character;
    public List<SpriteRenderer> Grasses;
    public List<VisualEffect> VFXGrasses;
    // Start is called before the first frame update
    void Start()
    {
        GrassParent = GameObject.Find("草集合").GetComponent<Transform>();
        Grasses = new List<SpriteRenderer>();
        GrassParent.GetComponentsInChildren(Grasses);

        VFXGrassParent = GameObject.Find("VFX草集合").GetComponent<Transform>();
        VFXGrasses = new List<VisualEffect>();
        VFXGrassParent.GetComponentsInChildren(VFXGrasses);
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



    public void ShakeVFXGrass()
    {
        foreach (var VFXgrass in VFXGrasses)
        {
           
                VFXgrass.SetFloat("GrassShake", 5);
            

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
