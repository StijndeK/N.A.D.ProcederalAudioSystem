using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PParameter
{
    public PParameterLinker.AdaptableParametersCycle dataToAdapt;

    // because not every parameter has an ability to adapt to gamedata yet, only set this when there is gamedata to adapt to
    // TODO: make it possible for a parameter to adapt to multiple values
    public float? value;

    // because not every parameter has specific layers (such as bpm), only set this if specific layers or all layers need to adapt
    public List<int> layersToAdapt;

    public PParameter(PParameterLinker.AdaptableParametersCycle dataToAdapt, float? value = null, List<int> layersToAdapt = null)
    {
        this.dataToAdapt = dataToAdapt;
        this.value = value;
        this.layersToAdapt = layersToAdapt;
    }
}
