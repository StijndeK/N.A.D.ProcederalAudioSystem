using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSequencer
{
    private static int currentChordBase;

    public static void Sequencer()
    {
        if (PClock.nextTick)
        {
            ProceduralAudio.print("tick");

            foreach (PCycleTimer cycleTimer in PParameterLinker.cycleTimers)
            {
                // check trigger timed cycle
                if (cycleTimer.currentTick % cycleTimer.lengthInTicks == cycleTimer.lengthInTicks - 1)
                {
                    if (cycleTimer.cycle != null)
                    {
                        PAudioDataSystem.CallCycle(cycleTimer.cycle);
                    }
                    else
                    {
                        PAudioDataSystem.callDynamicCycle(cycleTimer.dynamicCycle);
                    }
                }

                cycleTimer.currentTick += 1;
            }

            // for every layer
            for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++)
            {
                var currentLayer = ProceduralAudio.layers[layer];

                // convert tick number into measure
                currentLayer.currentTick %= currentLayer.rythm.Count;

                // check if layer is active
                if (currentLayer.layerOn)
                {
                    // check rythm
                    if (currentLayer.rythm[currentLayer.currentTick] == 1)
                    {
                        // TODO: set scale for all layers here instead of only the melody
                        // check if audio needs to adapt to another layer and play audio TODO: set a layer parameter to see if it needs to adapt instead of just checking if its the melody
                        if (currentLayer.layerType == ProceduralAudio.LayerType.melody)
                        {
                            // account for scale having switches
                            int chordBaseScaleIndex = (PAudioDataSystem.currentScale.IndexOf(currentChordBase) == -1) ? PAudioDataSystem.previousScale.IndexOf(currentChordBase) : PAudioDataSystem.currentScale.IndexOf(currentChordBase);
                            PAudioPlayer.PlayFile(layer, PAudioDataSystem.currentScale[(chordBaseScaleIndex + currentLayer.melody[currentLayer.currentTick]) % PAudioDataSystem.currentScale.Count]);
                        }
                        else
                        {
                            PAudioPlayer.PlayFile(layer, currentLayer.melody[currentLayer.currentTick]);
                        }

                        // set currentChord when chordlayer changes
                        if (currentLayer.layerType == ProceduralAudio.LayerType.chords)
                        {
                            currentChordBase = currentLayer.melody[currentLayer.currentTick];
                        }
                    }
                }

                // next tick
                currentLayer.currentTick += 1;
            }
        }
    }
}
