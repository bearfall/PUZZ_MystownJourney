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

    public List<ParticleSystem> prepareParticleSystem = new List<ParticleSystem>();
    public List<ParticleSystem> attackParticleSystem = new List<ParticleSystem>();
    public List<ParticleSystem> attack1ParticleSystem = new List<ParticleSystem>();

    public List<ParticleSystem> landingParticleSystem = new List<ParticleSystem>();


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

    public void PlayAttack1ParticleSystem()
    {
        foreach (var ParticleSystem in attack1ParticleSystem)
        {
            ParticleSystem.Play();
        }
    }

    public void PlayPrepareParticleSystem()
    {
        foreach (var ParticleSystem in prepareParticleSystem)
        {
            ParticleSystem.Play();
        }
    }


   

    public void StopPrepareParticleSystem()
    {
        foreach (var ParticleSystem in prepareParticleSystem)
        {
            ParticleSystem.Stop();
        }
    }


    public void PlayLandingParticleSystem()
    {
        foreach (var ParticleSystem in landingParticleSystem)
        {
            ParticleSystem.Play();
        }
    }
}
