using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerBoxSpawnerManager : MonoBehaviour
{
    public Button addButton;
    public GameObject layerBox;
    public GameObject spawnerSystem;
    public GameObject sequencerSystem;

    void Start()
    {
        spawnerSystem = GameObject.FindGameObjectWithTag("spawncomponent");
        sequencerSystem = GameObject.FindGameObjectWithTag("sequencer");
        addButton.onClick.AddListener(duplicateButtonPressed);
    }

    private void duplicateButtonPressed()
    {
        // set boxnumber for new boxlayer to find
        spawnerSystem.GetComponent<SpawnerSystem>().currentLayer = gameObject.GetComponentInChildren<ManagerTexter>().boxNumber;

        // for x and y
        for (int i = 0; i < 2; i++)
        {
            // add layer if it isn't initialised yet
            while (sequencerSystem.GetComponent<SequencerSystem>().parameters3d[i].Count <= gameObject.GetComponentInChildren<ManagerTexter>().boxNumber)
            {
                sequencerSystem.GetComponent<SequencerSystem>().parameters3d[i].Add(new List<int>());
            }
            // add new layerbox duplicate to list with all parameters per layerbox
            sequencerSystem.GetComponent<SequencerSystem>().parameters3d[i][gameObject.GetComponentInChildren<ManagerTexter>().boxNumber - 1].Add(0);
        }

        // spawn new box
        GameObject Box = Instantiate(layerBox, new Vector3(300, 0, 0), Quaternion.identity) as GameObject;
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("spawncomponent").transform, false);
    }
}
