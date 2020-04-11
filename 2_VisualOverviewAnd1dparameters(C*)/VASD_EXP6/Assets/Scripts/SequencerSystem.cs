using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerSystem : MonoBehaviour
{
    // Parameters
    public Slider parameterSettingSliderX;
    public Slider parameterSettingSliderY;
    //public List<List<int>> parameters = new List<List<int>>();
    public List<List<List<int>>> parameters3d = new List<List<List<int>>>(); // list to hold per parameter, per layer, per duplicate: if there is a layerbox and what value it has
    int[] stepAmounts = {1, 1};

    List<int> intervals = new List<int>();

    List<int> initialPositions = new List<int>();

    private void Start()
    {
        // init lists
        //parameters.Add(new List<int>());
        //parameters.Add(new List<int>());

        parameters3d.Add(new List<List<int>>());
        parameters3d.Add(new List<List<int>>());

        intervals.Add(0);
        intervals.Add(0);

        initialPositions.Add(0);
        initialPositions.Add(0);
    }

    public void get2dLayerParameter(int layer, float[] positions, int duplicateNumber) // called from mover when object is moved
    {
        // get interval between boxes
        intervals[0] = (int)gameObject.GetComponentInChildren<GridManager>().tileSizeRows;
        intervals[1] = (int)gameObject.GetComponentInChildren<GridManager>().tileSizeCols;

        // set initial positions (account for intial offset)
        initialPositions[0] = intervals[0] + 200;
        initialPositions[1] = intervals[1] + 27;

        // set stepamounts
        stepAmounts[0] = gameObject.GetComponentInChildren<GridManager>().rows;
        stepAmounts[1] = gameObject.GetComponentInChildren<GridManager>().cols;

        //// for x and y
        //for (int i = 0; i < 2; i++)
        //{
        //    // add layer if it isn't initialised yet
        //    while (parameters[i].Count < layer) 
        //    {
        //        parameters[i].Add(0);
        //    }
 
        //    // sets to what value(s) a layer belongs
        //    int param = 0;
        //    for (int step = 0; step < stepAmounts[i]; step++) 
        //    {
        //        if (positions[i] >= initialPositions[i] + (step * intervals[i]))
        //        {
        //            param += 1; // add to param if position is larger then line
        //        }
        //        parameters[i][layer - 1] = param;
        //    }
        //}

        // for x and y
        for (int i = 0; i < 2; i++)
        {
            // sets to what value(s) a layer belongs
            int param = 0;
            for (int step = 0; step < stepAmounts[i]; step++)
            {
                if (positions[i] >= initialPositions[i] + (step * intervals[i]))
                {
                    param += 1; // add to param if position is larger then line
                }
            }
            parameters3d[i][layer - 1][duplicateNumber - 1] = param;
        }
    }

    public bool CheckIfLayerShouldPlay(int layer)
    {
        // return true if layer should play
        for (int i = 0; i < parameters3d[0][layer].Count; i ++)
        {
            if (parameters3d[0][layer][i] == (int)parameterSettingSliderX.value && parameters3d[1][layer][i] == (int)parameterSettingSliderY.value)
            {
                return true;
            }
        }
        return false;
    }
}