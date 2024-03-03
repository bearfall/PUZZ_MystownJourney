using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillMove : MonoBehaviour
{
    public float startMoveTime;
    public float startMoveTimer;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        startMoveTimer += Time.deltaTime;
        if (startMoveTimer >= startMoveTime)
        {
            gameObject.transform.Translate(Vector3.right * duration * Time.deltaTime * 1);
        }
        
    }

    
}
