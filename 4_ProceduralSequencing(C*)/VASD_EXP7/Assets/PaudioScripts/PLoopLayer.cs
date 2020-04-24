using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class PLoopLayer : PLayer
{   
    public ProceduralAudio.LayerType layerType = new ProceduralAudio.LayerType();
    public int currentTick;

    // adaptable parameters
    public int beatsPerMeasure;    // amount of beats in a measure per layer
    public int beatLength;     // length of a beat in ticks(4th notes)
    public int noteDensity;     // average note density of rythms: 10 = playing every tick, 5 = 50% chance to play every tick
    public bool layerOn;

    public List<int> rythm;
    public List<int> melody;

    public PLoopLayer(int soundOptionsAmount, int beatsPerMeasure, int beatLength, ProceduralAudio.LayerType layerType, int noteDensity, bool layerOn, int currentTick = 0) : base(soundOptionsAmount)
    {
        this.beatsPerMeasure = beatsPerMeasure;
        this.beatLength = beatLength;
        this.layerType = layerType;
        this.noteDensity = noteDensity;
        this.layerOn = layerOn;
        this.currentTick = currentTick;
    }
}
