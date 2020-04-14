using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOutput : MonoBehaviour
{
    Text txt;
    // acces to text
    GameObject fileLoaderObject;

    void Start()
    {
        // text output
        txt = gameObject.GetComponent<Text>();

        fileLoaderObject = GameObject.FindGameObjectWithTag("fileloader");
    }

    void Update()
    {
        txt.text = fileLoaderObject.GetComponent<AutoFileLoader>().textForOutput;
    }
}
