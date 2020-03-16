# EXP3
The third VASD prototype. EXP3 builds further on the system created in [EXP2](https://github.com/StijndeK/VASD/tree/master/VASD_EXP2_PY). 
This versions main focus will be on adding external parameters for audio to adapt to and adding play percentage functionalities.

```diff
- In progress. EXP3 now has the same functionality as EXP2 and is ported into unity.
```

### Unity + FMOD API
Because of the need for more complex features and the eventual application of VASD on games, EXP3 is build in Unity using the [FMOD Core API](https://fmod.com/resources/documentation-api?version=2.0&page=core-guide.html). The FMOD API allows for the ability to keep prototyping relatively quickly with certain game-specific functionalities easily available. Unity allows for quicker prototyping, especially for the visual 2d interface. I do want to eventually port the program to C++ for maximal optimisation and to for example apply it to UE4.

## Example

## Instructions
Place audio files in the 'bouncelocation' folder in the same folder as the unity project/build. Set the export location of the DAW to this folder to allow for quick testing and prototyping of the non-linear audio system. The amount of loops and the amount of layers are limited to 9 files (1 to 9). 

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
