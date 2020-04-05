# EXP5
The fifth VASD prototype. EXP5 builds further on [EXP4](https://github.com/StijndeK/VASD/tree/master/VASD_EXP4). The goal for EXP5 is a more modulair framework with an easier to use GUI that also makes it easier to add and edit different components. 

## Example

## Instructions
Place audio files in the 'bouncelocation' folder in the same folder as the unity project/build or choose another location in the automatic file loader. Set the export location of the DAW to this folder to allow for quick testing and prototyping of the non-linear audio system. The amount of loops and the amount of layers are limited to 9 files (1 to 9). Move layers to either side of a 'parameter line' to set them to the specific parameter value. Add parameter steps by clicking the +step button (mock).

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
| framework and overview | | Transition techniques |
| parameter steps | | Added context, both visual and interactive |
| | | Probability |
| | | More parameters |


