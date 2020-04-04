using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texter : MonoBehaviour
{
    Text txt;
    int boxNumber;

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

    void Update()
    {
        // TODO: only do this when moved
        setPosition(); // give position of box to sequencer
    }

    void setPosition()
    {
        sequencerSystemObject.GetComponent<SequencerSystem>().getLayerParameter(boxNumber, transform.position[0]);
    }
}
