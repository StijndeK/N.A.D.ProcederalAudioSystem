using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerBoxManager : MonoBehaviour
{
    public Button removeButton;

    void Start()
    {
        removeButton.onClick.AddListener(removeButtonPressed);
    }

    private void removeButtonPressed()
    {
        Destroy(gameObject);
    }
}
