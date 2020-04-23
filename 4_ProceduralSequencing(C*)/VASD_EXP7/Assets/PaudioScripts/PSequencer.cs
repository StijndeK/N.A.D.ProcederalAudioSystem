using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSequencer
{
    public static void Sequencer()
    {
        if (PClock.nextTick)
        {
            ProceduralAudio.print("tick");

            // for every layer
            for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++)
            {
                var currentLayer = ProceduralAudio.layers[layer];

                // convert tick number into measure
                currentLayer.currentTick = currentLayer.currentTick % currentLayer.rythm.Count;

                // check if layer is active
                if (currentLayer.layerOn)
                {
                    // check rythm
                    if (currentLayer.rythm[currentLayer.currentTick] == 1)
                    {
                        // play audio
                        PAudioPlayer.PlayFile(layer, currentLayer.melody[currentLayer.currentTick]);
                    }
                }

                // next tick
                currentLayer.currentTick += 1;
            }
        }
    }
}
