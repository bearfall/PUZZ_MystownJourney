using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RollDiceTest : MonoBehaviour
{
    public TextMeshProUGUI TMP_Number;
    public GameObject[] faceDetectors;
    public int defaultFaceResult = -1;
    private Rigidbody rb;
    private Vector3 force;
    private PlayerEnergyBar playerEnergyBar;
    public bool isThrowDice = false;
    public bool diceStop = false;
    private int energyAmount;
    public bool canCharge = false;
    public GameObject nowCharacter;
    public int playerDiceNumber;


    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TMP_Number = GameObject.Find("DiceNumber").GetComponent<TextMeshProUGUI>();
       // playerEnergyBar = GameObject.Find("EnergyBarSlider").GetComponent<PlayerEnergyBar>();


    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.Rotate(Vector3.up * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.Mouse0)  /* && canCharge == true*/  )
        {
            RollTheDice();

        }



            if (isThrowDice == true)
        {
            //print("DiceStop");
            int indexResult = FindFaceResult();

            ChangeNumber(indexResult);
            

            // playerDiceNumber = indexResult + 1;
        }


        if (CheckObjectHasStopped() == true)
        {
            playerDiceNumber = FindFaceResult();
            playerDiceNumber += 1;
            isThrowDice = false;
            print("��쪺�O" + playerDiceNumber);
           // playerDiceNumber = indexResult + 1;
        }
    }


    private void SetDiceForce()
    {
        
       

       
        float z = Random.Range(3, 5);
        force = new Vector3(0, 0, z);

       

        
    }


    public void RollTheDice()
    {
        //nowCharacter.GetComponent<Animator>().SetBool("isPrepare", true);

        //this.transform.rotation = Quaternion.Euler(18, 0, 0);
        isThrowDice = true;

            SetDiceForce();
        rb.velocity = force;
        
    }

    public int FindFaceResult()
    {
        //Since we have all child objects for each face,
        //We just need to find the highest Y value
        int maxIndex = 0;
        for (int i = 1; i < faceDetectors.Length; i++)
        {
            if (faceDetectors[maxIndex].transform.position.y <
                faceDetectors[i].transform.position.y)
            {
                maxIndex = i;
            }
        }

        
        defaultFaceResult = maxIndex;
        print(maxIndex);
        return maxIndex;
    }

    public bool CheckObjectHasStopped()
    {
        if (isThrowDice)
        {
            //rb = DicePrefab.GetComponent<Rigidbody>();
            if (rb.velocity == Vector3.zero &&
                rb.angularVelocity == Vector3.zero)
            {
                diceStop = true;
                nowCharacter.GetComponent<Animator>().SetBool("isPrepare", false);
                //isThrowDice = false;
                return true;



            }
            else
            {
                diceStop = false;
                return false;
            }
        }
        else
        {
            return false;
        }
    }


    public void ChangeNumber(int faceResult)
    {
        switch (faceResult)
        {
            case 0:
                TMP_Number.text = "1";
                energyAmount = 9;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 1:
                TMP_Number.text = "2";
                energyAmount = 8;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 2:
                TMP_Number.text = "3";
                energyAmount = 7;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 3:
                TMP_Number.text = "4";
                energyAmount = 6;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 4:
                TMP_Number.text = "5";
                energyAmount = 5;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 5:
                TMP_Number.text = "6";
                energyAmount = 4;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 6:
                TMP_Number.text = "7";
                energyAmount = 3;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 7:
                TMP_Number.text = "8";
                energyAmount = 2;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 8:
                TMP_Number.text = "9";
                energyAmount = 1;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            case 9:
                TMP_Number.text = "10";
                energyAmount = 0;
                playerEnergyBar.SetEnergy(energyAmount);
                break;
            

        }



    }



    public void SetNowCharater(TestCharacter character)
    {
        nowCharacter = character.gameObject.transform.GetChild(0).gameObject;




    }
}
