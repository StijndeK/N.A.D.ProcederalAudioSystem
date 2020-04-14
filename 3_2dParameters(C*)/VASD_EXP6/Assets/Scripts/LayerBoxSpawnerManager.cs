using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerBoxSpawnerManager : MonoBehaviour
{
    public Button addButton;
    public GameObject layerBox;
    private GameObject spawnerSystem;
    private GameObject sequencerSystem;

    private int localBoxNumber;

    void Start()
    {
        spawnerSystem = GameObject.FindGameObjectWithTag("spawncomponent");
        sequencerSystem = GameObject.FindGameObjectWithTag("sequencer");
        addButton.onClick.AddListener(duplicateButtonPressed);
    }

    private void duplicateButtonPressed()
    {
        // get boxnumber
        localBoxNumber = gameObject.GetComponentInChildren<ManagerTexter>().boxNumber;

        // set boxnumber for new boxlayer to find
        spawnerSystem.GetComponent<SpawnerSystem>().currentLayer = localBoxNumber;

        // for x and y
        for (int i = 0; i < 2; i++)
        {
            // add layer if it isn't initialised yet
            // TODO: use vectors instead of lists
            while (sequencerSystem.GetComponent<SequencerSystem>().parameters3d[i].Count <= localBoxNumber)
            {
                sequencerSystem.GetComponent<SequencerSystem>().parameters3d[i].Add(new List<int>());
            }
            // add new layerbox duplicate to list with all parameters per layerbox
            sequencerSystem.GetComponent<SequencerSystem>().parameters3d[i][localBoxNumber - 1].Add(0);
        }

        // spawn new box
        GameObject Box = Instantiate(layerBox, new Vector3(50, 0, 0), Quaternion.identity) as GameObject;
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("spawncomponent").transform, false);
    }
}
