using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PLayer
{
    public int soundOptionsAmount;     // amount of options per layer to choose from
    public int beatsPerMeasure;    // amount of beats in a measure per layer
    public int beatLength;     // length of a beat in ticks(4th notes)
    //public int layerType;        // layer types (0=melody, 1=countermelody, 2=percussion, 3=chordslayer1, 4=chordslayer2, 5=chordslayer3 ...)
    public ProceduralAudio.LayerType layerType = new ProceduralAudio.LayerType();
    public int noteDensity;     // average note density of rythms: 10 = playing every tick, 5 = 50% chance to play every tick
    public bool layerOn;



    public PLayer(int soundOptionsAmount, int beatsPerMeasure, int beatLength, ProceduralAudio.LayerType layerType, int noteDensity, bool layerOn)
    {
        this.soundOptionsAmount = soundOptionsAmount;
        this.beatsPerMeasure = beatsPerMeasure;
        this.beatLength = beatLength;
        this.layerType = layerType;
        this.noteDensity = noteDensity;
        this.layerOn = layerOn;
    }
}
