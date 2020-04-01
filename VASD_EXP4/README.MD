# EXP4
The fourth VASD prototype. EXP4 builds further on [EXP3](https://github.com/StijndeK/VASD/tree/master/VASD_EXP3). The goal for EXP4 is to add basic parameter functionality using a visual approach.

## Example

<img width="713" alt="VASD_EXP4_Screencap" src="https://user-images.githubusercontent.com/31696336/78155827-3c7f6e00-743e-11ea-8056-8b0cbc38af06.png">

## Instructions
Place audio files in the 'bouncelocation' folder in the same folder as the unity project/build. Set the export location of the DAW to this folder to allow for quick testing and prototyping of the non-linear audio system. The amount of loops and the amount of layers are limited to 9 files (1 to 9). Move layers to either side of a 'parameter line' to set them to the specific parameter value.

### File naming
the number and layer number have been inverted
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
| basic adaption to game parameters | requires more user input | Transition techniques |
| | | Added context, both visual and interactive |
| | | Not a lot to overview yet |
| | | Chance percentages |
