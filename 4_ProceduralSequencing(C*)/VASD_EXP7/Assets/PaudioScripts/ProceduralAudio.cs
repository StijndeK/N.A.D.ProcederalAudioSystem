using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;

public class ProceduralAudio : MonoBehaviour
{
    // TODO: INREADME: niet unityobjectcomponent based systeem
    // TODO: create scale size variable

    List<List<string>> entryList = new List<List<string>>(); // layer, tracks, filename
    List<List<string>> entryListOS = new List<List<string>>(); // layer, tracks, filename

    List<List<int>> rythms = new List<List<int>>(); // layer, rythm

    List<List<int>> melodies = new List<List<int>>(); // layer, melody

    List<int> currentTicks = new List<int>();

    public static List<int> currentScale = new List<int>();

    public static List<List<int>> chords = new List<List<int>>(); // chord, chordnotes

    List<PLayer> layers = new List<PLayer>();

    public enum LayerType { melody, countermelody, percussion, chords };

    // ---------------------------------
    // VARIABLES TO SET BEFORE RUNNING

    // set to what scale is used
    List<string> scaleInNotes = new List<string> { "c", "d", "e", "f", "g", "a", "b" };

    // total number of looping layers and oneshots
    public static int amountOfLayers = 4;
    private int amountOfSoundEffects = 1;

    private string folderLocationLoops = "../ProceduralBounceLocation/";
    private string folderLocationOneShots = "../ProceduralBounceLocationOS/";

    private int bpm = 100;

    public static int chordAmount = 4;
    public static int chordLayerAmount = 3;

    void Start()
    {
        // read looping audio
        entryList = PAutoFileLoader.ReadFiles(folderLocationLoops, amountOfLayers);
        for(int ii = 0; ii < amountOfLayers; ii++)
        {
            for (int i = 0; i < entryList[ii].Count; i++)
            {
                print(entryList[ii][i]);
            }
        }
        
        // read oneshots
        entryListOS = PAutoFileLoader.ReadFiles(folderLocationOneShots, amountOfSoundEffects);

        PClock.Init(bpm);

        PAudioPlayer.Start(); // setup fmod API

        InitialiseLayers();

        GenerateAudioData();
    }

    void InitialiseLayers()
    {
        layers.Add(new PLayer(12, 8, 1, LayerType.melody, 8, false));
        layers.Add(new PLayer(12, 4, 1, LayerType.countermelody, 4, false));
        layers.Add(new PLayer(11, 4, 4, LayerType.chords, 10, false));
        layers.Add(new PLayer(1, 4, 1, LayerType.percussion, 10, false));
    }

    void Update()
    {
        // receive ticks
        PClock.Update();

        if (PClock.nextTick)
        {
            print("tick");

            // for every layer
            for (int layer = 0; layer < amountOfLayers; layer++)
            {
                // convert tick number into measure
                currentTicks[layer] = currentTicks[layer] % rythms[layer].Count;

                // check if layer is active
                if(layers[layer].layerOn)
                {
                    // check rythm
                    if (rythms[layer][currentTicks[layer]] == 1)
                    {
                        // play audio
                        PAudioPlayer.PlayFile(folderLocationLoops, (string)entryList[layer][melodies[layer][currentTicks[layer]]]);
                    }
                }

                // next tick
                currentTicks[layer] += 1;
            }
        }

        // generate new data
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateAudioData();

        // toggle layers
        if (Input.GetKeyDown(KeyCode.Alpha1))
            layers[0].layerOn = !layers[0].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            layers[1].layerOn = !layers[1].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            layers[2].layerOn = !layers[2].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            layers[3].layerOn = !layers[3].layerOn;

        // call oneshots
        if (Input.GetKeyDown(KeyCode.A))
        {
            string soundToPlay = (string)entryListOS[0][Random.Range(0, entryListOS[0].Count)];
            POneshots.playOneShot(folderLocationOneShots, soundToPlay, true);
        }
    }

    void GenerateAudioData()
    {
        // set scale
        currentScale = PTonal.setScale(0);

        // TODO: currently there is only one layer per type. In the future this has to be a modular amount
        for (int type = 0; type < amountOfLayers; type++)
        {
            if (rythms.Count < type + 1) // check if layer is initialised
            {
                rythms.Add(PRythm.GenerateRythm(layers[type].beatsPerMeasure, layers[type].beatLength, layers[type].noteDensity));
                melodies.Add(PTonal.GenerateTonalIntervals(rythms[type], layers[type].soundOptionsAmount, layers[type].layerType));
                currentTicks.Add(0);
            }
            else
            {
                rythms[type] = PRythm.GenerateRythm(layers[type].beatsPerMeasure, layers[type].beatLength, layers[type].noteDensity);
                melodies[type] = PTonal.GenerateTonalIntervals(rythms[type], layers[type].soundOptionsAmount, layers[type].layerType);
                currentTicks[type] = 0;
            }

            print("layer " + type.ToString());

            string rythmOutput = "";
            for (int i = 0; i < rythms[type].Count; i++) rythmOutput += rythms[type][i].ToString();
            print(rythmOutput);

            string melodyOutput = "";
            for (int i = 0; i < rythms[type].Count; i++) melodyOutput += melodies[type][i].ToString();
            print(melodyOutput);
        }

        for (int chord = 0; chord < chords.Count; chord++) print("chord: " + chords[chord][0]);
    }

}
