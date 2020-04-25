using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// sets text on layer boxes
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

        // add to list if not initialised yet
        // TODO: use vectors instead of lists
        while(spawnerSystem.GetComponent<SpawnerSystem>().duplicates.Count < boxNumber)
        {
            spawnerSystem.GetComponent<SpawnerSystem>().duplicates.Add(0);
        }

        // add duplicate to list with duplicate amounts
        spawnerSystem.GetComponent<SpawnerSystem>().duplicates[boxNumber - 1] += 1;

        // set current duplicate number
        duplicateNumb = spawnerSystem.GetComponent<SpawnerSystem>().duplicates[boxNumber - 1];

        // set layer to active
        soundSystem = GameObject.FindGameObjectWithTag("controler");
        soundSystem.GetComponent<SoundSystem>().layerActiveChecks[boxNumber - 1] = 1;
    }
}
