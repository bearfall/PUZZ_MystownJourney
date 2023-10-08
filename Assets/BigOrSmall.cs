using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOrSmall : MonoBehaviour
{
    public int number1;
    public int number2;


    // Start is called before the first frame update
    void Start()
    {
        NumberRatio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NumberRatio()
    {
        if (number1 > number2)
        {
            print("number1數字比較大");
        }
        else if (number2 > number1)
        {
            print("number2數字比較大");
        }
        else
        {
            print("兩個數字一樣大");
        }
    }


}
