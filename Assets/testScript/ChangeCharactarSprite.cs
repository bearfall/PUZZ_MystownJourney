using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharactarSprite : MonoBehaviour
{
    public Sprite attack;
    public Sprite Breathe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeToAttackImage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = attack;

    }
    public void ChangeToBreatheImage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Breathe;

    }
}
