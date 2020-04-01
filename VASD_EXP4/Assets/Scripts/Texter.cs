using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texter : MonoBehaviour
{
    Text txt;
    int boxNumber;
    // acces to names
    GameObject soundSystemObject;

    void Start()
    {
        soundSystemObject = GameObject.FindGameObjectWithTag("MainCamera");
        txt = gameObject.GetComponent<Text>();
        boxNumber = soundSystemObject.GetComponent<SoundSystem>().setBoxText();
        txt.text = "Layer: " + boxNumber.ToString();
        setPosition();
    }

    void Update()
    {
        // TODO: only do this when moved
        setPosition();
    }

    void setPosition()
    {
        print(boxNumber);
        print(transform.position[0]);
        soundSystemObject.GetComponent<SoundSystem>().getLayerParameter(boxNumber - 1, transform.position[0]);
    }
}
