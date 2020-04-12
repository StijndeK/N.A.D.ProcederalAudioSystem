# EXP6
The sixth VASD prototype. EXP6 builds further on [EXP5](https://github.com/StijndeK/VASD/tree/master/VASD_EXP5). The main goal for EXP6 is to create a 2D parameter space by adding a y-axis. Multiple axis with dynamic amounts of steps allows for a lot more possibilities in a non-linear system.

## Example
- [EXP6](https://streamable.com/uvkjxm)
<img width="1306" alt="VASD_EXP6" src="https://user-images.githubusercontent.com/31696336/79067862-d3f88280-7cc2-11ea-83de-1272f8238e1e.png">
<img width="1261" alt="VASD_EXP6_Dataflow" src="https://user-images.githubusercontent.com/31696336/79068833-1f625f00-7cca-11ea-98f7-a95e34ac824b.png">
<img width="839" alt="Screenshot 2020-04-12 at 13 59 53" src="https://user-images.githubusercontent.com/31696336/79068269-e88a4a00-7cc5-11ea-9756-c69a71d197a1.png">

## Instructions
Place audio files in the 'bouncelocation' folder in the same folder as the unity project/build or choose another location in the automatic file loader. Set the bpm and amount of beats per loop. Set the export location of the DAW to this folder to allow for quick testing and prototyping of the non-linear audio system. Add 'layerboxes' from the spawner and drag them into the sequencer grid. Move layers to either side of a 'parameter line' to set them to the specific parameter value. Add parameter steps by clicking the +step buttons and remove them with the -buttons. Move the sliders to set the current value per parameter.

### File naming
- Start the file name with the number of the layer (1 to 9) and end with '_'
- Then type the number of the loop (1 to 9) and end with '_'
- Type any information or name you like and again end with '_'
- End the file name with the numbers of the audio track that the loop can transition to

For example:
2_1_NameAndOtherInformation_124.wav
is the first track on the second layer and can loop or transition to track 2 or 4
 
## Reflection
| Improvements    | Drawback       | Missing  |
| ------------- |:-------------:| -----:|
| way more possibilities with 2 axes | | other transition techniques |
| dynamic amount of layer boxes | | Added context, both visual and interactive |
| textual feedback in the terminals | | Probability |

