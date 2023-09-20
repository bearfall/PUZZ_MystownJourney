using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoByImage : MonoBehaviour
{
    public GameObject CM01;
    public GameObject Character;
    public Button button;
    public Sprite characterImage;
    public ScreenChange screenChange;
    // Start is called before the first frame update
    void Start()
    {
        screenChange = CM01.GetComponent<ScreenChange>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeCharacter()
    {
        screenChange.ChangesScreenToCharacter(Character);
    }

    public void SetInfo(GameObject character)
    {
        Character = character;
        characterImage = character.GetComponent<TestCharacter>().headSprite;
        button.GetComponent<Image>().sprite = characterImage;
    }
}
