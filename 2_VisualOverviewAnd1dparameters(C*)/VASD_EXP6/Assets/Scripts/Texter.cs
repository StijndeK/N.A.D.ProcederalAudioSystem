using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texter : MonoBehaviour
{
    Text txt;
    public int boxNumber;

    GameObject soundSystemObject;
    GameObject sequencerSystemObject;

    void Start()
    {
        sequencerSystemObject = GameObject.FindGameObjectWithTag("sequencer");

        soundSystemObject = GameObject.FindGameObjectWithTag("controler");
        txt = gameObject.GetComponent<Text>();
        boxNumber = soundSystemObject.GetComponent<SoundSystem>().setBoxText();
        txt.text = "Layer: " + boxNumber.ToString();
    }
}
