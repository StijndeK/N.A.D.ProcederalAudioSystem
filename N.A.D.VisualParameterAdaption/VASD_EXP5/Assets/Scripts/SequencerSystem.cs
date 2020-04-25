using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerSystem : MonoBehaviour
{
    // Parameters
    public Slider parameterSettingSlider;
    public List<int> parameterX = new List<int>(); // x parameter of every layer with amount of steps
    // line position
    public Transform lineTransform; // hier moeten meer lijnen van gemaakt worden en moet gecheckt welke lijn
    int intervalBetweenLines = 150;

    // TODO: button for setting new lines, lengthenig slider and dynamically checking for parameters

    public void getLayerParameter(int layer, float position)
    {
        if (parameterX.Count < layer) // add layer if it isn't initialised yet
        {
            parameterX.Add(0);
        }

        if (position >= lineTransform.position[0] && position <= lineTransform.position[0] + intervalBetweenLines) // parameter 1
        {
            parameterX[layer - 1] = 1;
        }
        else if (position >= lineTransform.position[0] + intervalBetweenLines) // parameter 2
        {
            parameterX[layer - 1] = 2;
        }
        else // parameter 0
        {
            parameterX[layer - 1] = 0;
        }
    }
}
