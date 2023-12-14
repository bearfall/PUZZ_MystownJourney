using bearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class ChangeCharactarSprite : MonoBehaviour
{
    public TestGameManager1 testGameManager1;

    public Sprite attack;
    public Sprite Breathe;
    public Sprite prepare;
    public Sprite jump;
    public Sprite down;

    public List<ParticleSystem> prepareParticleSystem = new List<ParticleSystem>();
    public List<ParticleSystem> attackParticleSystem = new List<ParticleSystem>();
    public List<ParticleSystem> attack1ParticleSystem = new List<ParticleSystem>();
    public List<ParticleSystem> attack1UpParticleSystem = new List<ParticleSystem>();
    public List<ParticleSystem> attack1effectParticleSystem = new List<ParticleSystem>();

    public List<ParticleSystem> landingParticleSystem = new List<ParticleSystem>();

    public List<ParticleSystem> healParticleSystem = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        testGameManager1 = GameObject.Find("Manager").GetComponent<TestGameManager1>();
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



    public void PlayAttackParticleSystem()
    {
        foreach (var ParticleSystem in attackParticleSystem)
        {
            ParticleSystem.Play();
        }
    }




    public void PlayAttack1UpParticleSystem()
    {
        foreach (var ParticleSystem in attack1UpParticleSystem)
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

    public void PlayAttack1EffectParticleSystem()
    {
        foreach (var ParticleSystem in attack1effectParticleSystem)
        {
            ParticleSystem.Play();
        }
    }

    


   

    


    public void PlayLandingParticleSystem()
    {
        foreach (var ParticleSystem in landingParticleSystem)
        {
            ParticleSystem.Play();
        }
    }


    public void PlayHealParticleSystem()
    {
        foreach (var ParticleSystem in healParticleSystem)
        {
            ParticleSystem.Play();
        }
    }


    public void PlayParticleSystem(GameObject particleSystem)
    {

        particleSystem.GetComponent<ParticleSystem>().Play();
        
    }


    public void PlayerLighting()
    {
        print(gameObject.GetComponent<SpriteRenderer>().material.name);
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_isAttack", 1);
    }


    public void TeamStrengthenEffect(int number)
    {
        testGameManager1.charactersBetween[number].gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("embrave");

    }



    public void PlayEffect(string effectName)
    {
        GameObject particleObject = GameObject.Find(effectName);
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        particleSystem.Play();

    }

    public void SetActionPlayerLight()
    {
        print("強化特效出現");
        testGameManager1.nowActionPlayer.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material.SetFloat("_isAttack", 1);
    }
}
