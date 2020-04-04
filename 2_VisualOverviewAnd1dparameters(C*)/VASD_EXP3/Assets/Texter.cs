using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texter : MonoBehaviour
{
    Text txt;
    // acces to names
    GameObject soundSystemObject;

    void Start()
    {
        soundSystemObject = GameObject.FindGameObjectWithTag("MainCamera");

        txt = gameObject.GetComponent<Text>();
        txt.text = "Layer: " + soundSystemObject.GetComponent<SoundSystem>().setBoxText().ToString();
    }
}
