using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillManager : MonoBehaviour
{
    public CircleSpawner circleSpawner;
    public RandomMovement randomMovement;
    public RandomStrike randomStrike;

    public Transform blackHoleTransform;
    public GameObject blackHolePrefeb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrackBallSkill()
    {
        circleSpawner.SpawnObjects();

    }

    public void RandomMovementBallSkill()
    {
        randomMovement.StartMove();
    }

    public void BlackHoleSkill()
    {
        Instantiate(blackHolePrefeb, blackHoleTransform);
    }
    public void RandomStrikeSkill()
    {
       StartCoroutine( randomStrike.GenerateObjects());
    }
}
