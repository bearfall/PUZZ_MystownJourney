using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class ChangeCharactarSprite : MonoBehaviour
{
    public Sprite attack;
    public Sprite Breathe;
    public Sprite prepare;
    public Sprite jump;
    public Sprite down;

    public List<ParticleSystem> attackParticleSystem = new List<ParticleSystem>();

    
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
    public void ChangeToPrepareImage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = prepare;

    }

    public void ChangeToJumpImage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = jump;

    }
    public void ChangeToDownImage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = down;

    }


    public void ResetAnimatorBool()
    {
        GetComponent<Animator>().SetBool("isPrepare", false);
    }

    public void PlayAttackParticleSystem()
    {
        foreach (var ParticleSystem in attackParticleSystem)
        {
            ParticleSystem.Play();
        }
    }
}
