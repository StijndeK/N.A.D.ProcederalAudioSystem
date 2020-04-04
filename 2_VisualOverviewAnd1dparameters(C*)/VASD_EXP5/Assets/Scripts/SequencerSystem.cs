using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerSystem : MonoBehaviour
{
    // Parameters
    public Slider parameterSettingSlider;
    public List<int> parameterX = new List<int>();
    // line position
    public Transform lineTransform;

    public void getLayerParameter(int layer, float position)
    {
        if (parameterX.Count < layer)
        {
            parameterX.Add(0);
        }
        parameterX[layer - 1] = (position >= lineTransform.position[0]) ? 1 : 0;
    }

    // TODO: button for setting new lines and lengthenig slider
}
