using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOutput2 : MonoBehaviour
{
    Text txt;
    // acces to text
    GameObject soundSystemObject;

    void Start()
    {
        // text output
        txt = gameObject.GetComponent<Text>();

        soundSystemObject = GameObject.FindGameObjectWithTag("controler");
    }

    void Update()
    {
        txt.text = soundSystemObject.GetComponent<SoundSystem>().textForOutput;
    }
}
