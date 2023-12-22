using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCharacterEffect : MonoBehaviour
{
    public RPGPlayerController rPGPlayerController;

    public EnemyCounter enemyCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect(string effectName)
    {
        GameObject particleObject = GameObject.Find(effectName);
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        particleSystem.Play();

    }

    

    public void CanMove()
    {
        rPGPlayerController.isMovement = true;
    }
    public void CantMove()
    {
        rPGPlayerController.isMovement = false;
    }

    public void ReduceEnemy()
    {
        enemyCounter.enemyAmount--;

    }

    public void PlaySound(string effectName)
    {

    }
}
