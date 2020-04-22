using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PLayer
{
    public int soundOptionsAmount;     // amount of options per layer to choose from
    public int beatsPerMeasure;    // amount of beats in a measure per layer
    public int beatLength;     // length of a beat in ticks(4th notes)
    public ProceduralAudio.LayerType layerType = new ProceduralAudio.LayerType();
    public int noteDensity;     // average note density of rythms: 10 = playing every tick, 5 = 50% chance to play every tick
    public bool layerOn;
    public int currentTick;

    public List<int> rythm;
    public List<int> melody;

    public PLayer(int soundOptionsAmount, int beatsPerMeasure, int beatLength, ProceduralAudio.LayerType layerType, int noteDensity, bool layerOn, int currentTick = 0)
    {
        this.soundOptionsAmount = soundOptionsAmount;
        this.beatsPerMeasure = beatsPerMeasure;
        this.beatLength = beatLength;
        this.layerType = layerType;
        this.noteDensity = noteDensity;
        this.layerOn = layerOn;
        this.currentTick = currentTick;
    }
}
