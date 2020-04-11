using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texter : MonoBehaviour
{
    Text txt;
    public int boxNumber;
    public int duplicateNumb;

    GameObject soundSystem;
    GameObject spawnerSystem;

    void Start()
    {        
        txt = gameObject.GetComponent<Text>();
        spawnerSystem = GameObject.FindGameObjectWithTag("spawncomponent");
        boxNumber = spawnerSystem.GetComponent<SpawnerSystem>().currentLayer;
        txt.text = "Layer: " + boxNumber.ToString();

        while(spawnerSystem.GetComponent<SpawnerSystem>().duplicates.Count < boxNumber)
        {
            spawnerSystem.GetComponent<SpawnerSystem>().duplicates.Add(0);
        }
        spawnerSystem.GetComponent<SpawnerSystem>().duplicates[boxNumber - 1] += 1;
        duplicateNumb = spawnerSystem.GetComponent<SpawnerSystem>().duplicates[boxNumber - 1];

        // set layer to active
        soundSystem = GameObject.FindGameObjectWithTag("controler");
        soundSystem.GetComponent<SoundSystem>().layerActiveChecks[boxNumber - 1] = 1;
    }
}
