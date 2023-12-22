using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyCounter : MonoBehaviour
{
    public int enemyAmount = 3;
    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAmount <= 0)
        {
            playableDirector.Play();
            enemyAmount = 3;
        }
    }
}
