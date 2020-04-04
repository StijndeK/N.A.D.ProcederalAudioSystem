using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    GameObject spawnerObject;
    GameObject soundSystemObject;

    // Start is called before the first frame update
    public void SpawnBoxes()
    {
        soundSystemObject = GameObject.FindGameObjectWithTag("controler");
        // spawner object reference
        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        // create boxes
        for (int i = 0; i < soundSystemObject.GetComponent<SoundSystem>().layerAmount; i++)
        {
            spawnerObject.GetComponent<spawner>().SpawnBox();
        }
    }
}
