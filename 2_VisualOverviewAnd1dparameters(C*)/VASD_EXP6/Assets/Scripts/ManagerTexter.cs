using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTexter : MonoBehaviour
{
    Text txt;
    public int boxNumber;

    GameObject soundSystemObject;

    void Start()
    {
        soundSystemObject = GameObject.FindGameObjectWithTag("controler");
        txt = gameObject.GetComponent<Text>();
        boxNumber = soundSystemObject.GetComponent<SoundSystem>().setBoxText();
        txt.text = "Layer: " + boxNumber.ToString();
    }
}
