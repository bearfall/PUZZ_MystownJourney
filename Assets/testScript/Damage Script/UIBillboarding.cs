using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBillboarding : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = cam.transform.forward;
    }
}
