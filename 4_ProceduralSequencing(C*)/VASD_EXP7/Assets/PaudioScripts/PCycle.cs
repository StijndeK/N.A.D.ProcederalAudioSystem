using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCycle
{
    // TODO: make these lists, so that multiple parameters can be put in a cycle
    public List<PAudioDataSystem.AdaptableParameter> parameters = new List<PAudioDataSystem.AdaptableParameter>();
    public List<int> layers = new List<int>();
    public bool changeChordsAndScale;

    public PCycle(List<PAudioDataSystem.AdaptableParameter> parameters, List<int> layers, bool changeChordsAndScale = false)
    {
        this.parameters = parameters;
        this.layers = layers;
        this.changeChordsAndScale = changeChordsAndScale;
    }
}
