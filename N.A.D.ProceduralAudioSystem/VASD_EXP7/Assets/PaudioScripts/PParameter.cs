using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PParameter
{
    // the value of the parameters should be updated at their origin when it changes, so that unnecesarry listeners dont have to be created here

    public PParameterLinker.AdaptableData dataToAdapt;
    public float value;

    public PParameter(PParameterLinker.AdaptableData dataToAdapt, float value)
    {
        this.dataToAdapt = dataToAdapt;
        this.value = value;
    }
}