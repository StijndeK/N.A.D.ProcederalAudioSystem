using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerSystem : MonoBehaviour
{
    // + buttons
    public Button addXButton;
    public Button addYButton;

    public GameObject toBeSpawned;
    float spawnPositionY, spawnPositionX;

    // Parameters
    public Slider parameterSettingSliderX;
    public Slider parameterSettingSliderY;
    public List<List<int>> parameters = new List<List<int>>();
    int[] stepAmounts = {2, 1};

    // line position
    public Transform lineTransformX;
    public Transform lineTransformY;
    List<Transform> lineTransforms= new List<Transform>();

    // interval moet uitgerekend worden
    // startlijn moet uitgevonden worden
    // moet verplaatst worden met de linetransforms
    int intervalBetweenLines = 150;

    private void Start()
    {
        // init lists
        parameters.Add(new List<int>());
        parameters.Add(new List<int>());
        lineTransforms.Add(lineTransformX);
        lineTransforms.Add(lineTransformY);

        // listeners
        addXButton.onClick.AddListener(addXButtonPressed);
        addYButton.onClick.AddListener(addYButtonPressed);
    }

    private void addXButtonPressed()
    {
        // add line
        // (possibly) extend component size by adding one
        // increase slider max
        // increase slider length
        // update stepAmounts

        spawnPositionX = GameObject.FindGameObjectWithTag("seqspawnloc").transform.localPosition[0];
        spawnPositionY = GameObject.FindGameObjectWithTag("seqspawnloc").transform.localPosition[1];
        print(spawnPositionX);
        GameObject Box = Instantiate(toBeSpawned, new Vector3(spawnPositionX, spawnPositionY, 0), Quaternion.identity) as GameObject;

        // set parent to canvas
        Box.transform.SetParent(GameObject.FindGameObjectWithTag("sequencer").transform, false);
    }

    private void addYButtonPressed()
    {
        // add line
        // (possibly) extend component size
        // increase slider max
        // increase slider length
        // update stepAmounts
    }

    public void get2dLayerParameter(int layer, float[] positions) // called from texter
    {
        for (int i = 0; i < 2; i++)
        {
            // add layer if it isn't initialised yet
            if (parameters[i].Count < layer) 
            {
                parameters[i].Add(0);
            }

            // sets to what value(s) a layer belongs
            int param = 0;
            for (int j = 0; j < stepAmounts[i]; j++) 
            {
                if (positions[i] >= lineTransforms[i].position[i] + (j * intervalBetweenLines))
                {
                    param += 1; // add to param if position is larger then line
                }
            }
            parameters[i][layer - 1] = param;
        }
    }

    public bool CheckIfLayerShouldPlay(int layer)
    {
        // return true if layer should play
        if (parameters[0][layer] == (int)parameterSettingSliderX.value && parameters[1][layer] == (int)parameterSettingSliderY.value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}