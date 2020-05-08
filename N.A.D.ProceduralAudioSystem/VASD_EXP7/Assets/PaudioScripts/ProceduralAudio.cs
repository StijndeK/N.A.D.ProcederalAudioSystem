using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;

public class ProceduralAudio : MonoBehaviour
{
    public static List<List<string>> entryList = new List<List<string>>(); // layer, tracks, filename
    public static List<List<string>> entryListOS = new List<List<string>>(); // layer, tracks, filename

    public static List<PLoopLayer> layers = new List<PLoopLayer>(); // loop layers
    public static List<POSLayer> oSLayers = new List<POSLayer>(); // oneshot layers

    public enum LayerType { melody, countermelody, percussion, soundscape, chords, chords2 };

    // --- variables to set before running ---
    // - parameters to adapt to

    // total number of looping layers and oneshots
    public static int amountOfLayers = 6;
    private int amountOfSoundEffects = 1;

    private string folderLocationLoops = "../../../GNADcopy/ProceduralBounceLocation/";
    private string folderLocationOneShots = "../../../GNADcopy/ProceduralBounceLocationOS/";

    public static int chordAmount = 4;
    public static int chordLayerAmount = 3;

    void Start()
    {
        PAudioPlayer.Start(); // setup fmod API

        InitialiseLayers();

        // read looping audio
        entryList = PAutoFileLoader.ReadFiles(folderLocationLoops, amountOfLayers);
        
        // read oneshots
        entryListOS = PAutoFileLoader.ReadFiles(folderLocationOneShots, amountOfSoundEffects, true);

        PClock.Init(100);

        PParameterLinker.Start();
    }

    void InitialiseLayers()
    {
        layers.Add(new PLoopLayer(12, 8, 1, LayerType.melody, 8, false));
        layers.Add(new PLoopLayer(12, 4, 1, LayerType.countermelody, 4, false));
        layers.Add(new PLoopLayer(11, 4, 4, LayerType.chords, 10, false));
        layers.Add(new PLoopLayer(1, 4, 1, LayerType.percussion, 10, false));
        layers.Add(new PLoopLayer(1, 1, 16, LayerType.soundscape, 10, true));
        layers.Add(new PLoopLayer(11, 4, 4, LayerType.chords2, 5, true));

        oSLayers.Add(new POSLayer(4));
    }

    void Update()
    {
        // receive ticks
        PClock.Update();

        // sequence audio
        PSequencer.Sequencer();

        PAudioPlayer.Update();

        PParameterLinker.Update();
    }
}
