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

    public static List<int> currentScale = new List<int>();

    public static List<List<int>> chords = new List<List<int>>(); // chord, chordnotes

    public static List<PLayer> layers = new List<PLayer>();

    public enum LayerType { melody, countermelody, percussion, soundscape, chords, chords2 };

    // ---------------------------------
    // VARIABLES TO SET BEFORE RUNNING

    // set to what scale is used
    List<string> scaleInNotes = new List<string> { "c", "d", "e", "f", "g", "a", "b" };

    // total number of looping layers and oneshots
    public static int amountOfLayers = 6;
    private int amountOfSoundEffects = 1;

    private string folderLocationLoops = "../ProceduralBounceLocation/";
    private string folderLocationOneShots = "../ProceduralBounceLocationOS/";

    private int bpm = 100;

    public static int chordAmount = 4;
    public static int chordLayerAmount = 3;

    void Start()
    {
        PAudioPlayer.Start(); // setup fmod API

        InitialiseLayers();

        // read looping audio
        entryList = PAutoFileLoader.ReadFiles(folderLocationLoops, amountOfLayers);
        
        // read oneshots
        entryListOS = PAutoFileLoader.ReadFiles(folderLocationOneShots, amountOfSoundEffects);

        PClock.Init(bpm);

        GenerateAudioData();
    }

    void InitialiseLayers()
    {
        layers.Add(new PLayer(12, 8, 1, LayerType.melody, 8, false));
        layers.Add(new PLayer(12, 4, 1, LayerType.countermelody, 4, false));
        layers.Add(new PLayer(11, 4, 4, LayerType.chords, 10, false));
        layers.Add(new PLayer(1, 4, 1, LayerType.percussion, 10, false));
        layers.Add(new PLayer(1, 1, 16, LayerType.soundscape, 10, true));
        layers.Add(new PLayer(11, 4, 4, LayerType.chords2, 5, true));
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
                layers[layer].currentTick = layers[layer].currentTick % layers[layer].rythm.Count;

                // check if layer is active
                if(layers[layer].layerOn)
                {
                    // check rythm
                    if (layers[layer].rythm[layers[layer].currentTick] == 1)
                    {
                        // play audio
                        PAudioPlayer.PlayFile(folderLocationLoops, (string)entryList[layer][layers[layer].melody[layers[layer].currentTick]], layer, layers[layer].melody[layers[layer].currentTick]);
                    }
                } 

                // next tick
                layers[layer].currentTick += 1;
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
        if (Input.GetKeyDown(KeyCode.Alpha5))
            layers[4].layerOn = !layers[4].layerOn;

        // call oneshots
        if (Input.GetKeyDown(KeyCode.A))
        {
            // TODO: oneshots now load over a normal audio layer
            string soundToPlay = (string)entryListOS[0][Random.Range(0, entryListOS[0].Count)];
            POneshots.playOneShot(folderLocationOneShots, soundToPlay);
        }
    }

    void GenerateAudioData()
    {
        // set scale
        currentScale = PTonal.setScale(0);

        // generate chords
        chords = ProceduralAudio.chords = PTonal.GenerateChords();

        for (int layer = 0; layer < amountOfLayers; layer++)
        {
            layers[layer].currentTick = 0;
            layers[layer].rythm = PRythm.GenerateRythm(layers[layer].beatsPerMeasure, layers[layer].beatLength, layers[layer].noteDensity); ;
            layers[layer].melody = PTonal.GenerateTonalIntervals(layers[layer].rythm, layers[layer].soundOptionsAmount, layers[layer].layerType);

            print("layer " + layer.ToString());

            string rythmOutput = "";
            string melodyOutput = "";

            for (int i = 0; i < layers[layer].rythm.Count; i++)
            {
                rythmOutput += layers[layer].rythm[i].ToString();
                melodyOutput += layers[layer].melody[i].ToString();
            }

            print(rythmOutput);
            print(melodyOutput);
        }

        for (int chord = 0; chord < chords.Count; chord++) print("chord: " + chords[chord][0]);
    }

}
