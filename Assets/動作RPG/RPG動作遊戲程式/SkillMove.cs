using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right*5 * Time.deltaTime * 1);
    }

    
}
