using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public List< GameObject> buttons;
    public GameObject firstSelectedObject;
    //public EventSystem eventSystem;
    public bool isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        //eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Click(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedObject);
            isOpen = !isOpen;
            foreach (var button in buttons)
            {
                button.SetActive(isOpen);
            }
            
        }
    }

    public void Click()
    {
        
        isOpen = !isOpen;
        foreach (var button in buttons)
        {
            button.SetActive(isOpen);
        }

    }
}
