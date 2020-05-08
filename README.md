# N.A.D. Procedural Audio System
PAS is a framework/tool designed to improve procedural music systems prototyping and implementing. Even though procedural sound design has become somewhat standard in gameaudio, I find procedural music is still relatively underrepresented. It can be difficult to prototype, test and implement. PAS allows for quick testing and implementing procedural audio, using a sample-based sequencer. 

PAS is made as a general framework/tool, but is also used for and tested on 'Procedural Imagination'. A procedurally generated game world that dynamically responds to the player's actions. Currently being developed by [Josien Vos](http://josienvos.nl/).
![08 concept art 1](https://user-images.githubusercontent.com/31696336/80291854-857ec580-8751-11ea-884a-7a34bae40979.png)
 [This video](https://streamable.com/y8uoq6) shows the current system applied to Procedural Imagination

An (adapted) functionality caried over from [N.A.D Automatic File Loading in Python](https://github.com/StijndeK/N.A.D.AutomaticSoundloader) automatically loads files from specific folders and obtains certain parameters based on the file's names. This reduces the amount of time and disturbance of the workflow caused by having to implement the audio before being able to test it.

## NADT
Procedural Audio System (working title) is the third experiment in a range of experiments meant to adress obstructions in the process of designing and prototyping nonlinear audiosystems. [Click here](http://sdkoning.com/PF/N.A.D.T..html) for more information on the project.

### Other N.A.D. tools & experiments
- [N.A.D Automatic File Loading in Python](https://github.com/StijndeK/N.A.D.AutomaticSoundloader)
- [N.A.D Visual Parameter Adaption in Unity](https://github.com/StijndeK/N.A.D.VisualParameterAdaption)

## Unity and C#
To allow for quick testing and implementing, PAS is written in C# for Unity. However, because PAS should serve as a general framework, PAS moves away from a component based Unity hierarchy and uses little to none unity specific elements. Only the ProceduralAudio component needs to be placed in a scene. Everything else is done within the code.

## Systems Design
External gamedata is used to trigger and generate `Cycles`. These `Cycles` set audio parameters for `Layers`. `Layers` hold audiofiles and data on when and how to play them. The `Sequencer` receives ticks from the `Clock` and checks the information of layers to trigger audiofiles. For a more intricate audiosystem, checking what to play and calling the sounds should be split to reduce the risk of delays.

![PI_Diagram](https://user-images.githubusercontent.com/31696336/80965632-2d138c00-8e13-11ea-9b8a-95dc09f23286.png)

A modular approach makes it possible to easily add and/or edit functionalities, which keeps future possibilities open and helps to not condition the user into using certain systems. Because this tool is meant as a framework and is not specific to any one game, it needs to be able to adapt to different projects and general changes in the industry.

 [Click here](https://user-images.githubusercontent.com/31696336/80933589-b354b180-8dc4-11ea-9f22-79c06825a77c.png) for a more indepth dataflow overview.

### Layers
The loaded audio is divided into vertical layers, to be played independently or over each other. A `Layer` can be a vertical looping layer or sound effect. A `Layer` holds data on how, when and what variation to play. 

### Generating notes
Layers contain musical values, among which a *rythm* and a *tonal layer*. These rythms and melodys are used in the sequencer to check what and when to play. The rythm calls on what ticks audio needs to be played. The tonal layer decides what notes need to be played based on the `Layertype` (melody, countermelody, chord, percussion, etc) and the available samples within the layer. The notes are generated at initialisation and when a `Cycle` calls on them.

### Cycles
 A `Cycle` holds `Parameters` to adjust, that hold values to adjust, values to adjust to and what layers to adjust. Using a `Cycle` is the only way to adapt audiodata. `Cycles` can be triggered with game events using a `AdaptionMoment` or from the `Sequencer`, using a `CycleTimer`. A `DynamicCycle` only sets the on/off value of layers, using a `CycleTimer` the `DynamicCycle` can create a natural sounding buildup. For now this buildup is mostly random, in the future specific dynamic structures could be given and/or influenced by external parameters. This system is build so that any one parameter or game event can easily be linked to multiple aspects of audiodata. This prevents conditioning the user into using certain systems.

 In this example 2 cycles are created and given a Parameter. `Cycle1` is set to adapt the rythm and melody of layer 1. `Cycle2` is set to adapt the bpm based on data from the game. An adaption moment is then created and given the first cycle. The second cycle is set to adapt using a cycletimer from the sequencer.
```C#
    public enum AdaptableParametersCycle { chordsAndScale, layerdata, rythmAndMelody, melody, bpm, onOff, dynamicCycleLayerAmount };

    PCycle cycle = new PCycle(new List<PParameter> { new PParameter(AdaptableParametersCycle.rythmAndMelody, null, new List<int> { 1 })}));
    PCycle cycle2 = new PCycle(new List<PParameter> { new PParameter(AdaptableParametersCycle.bpm, gamedata)});

    PAdaptionMoment adaptionMoment = new PAdaptionMoment(new List<PCycle> {cycle});

    PCycleTime timer = new PCycleTimer(cycle2);
```

## Current status & improvements
The most important goal for this experiment is to research how to improve the audio designing process and encouraging experimentation by removing the obstructions in the workflow. As this approach to nonlinear systems design has been tested extensively, the current MVP for this project have been achieved. However, the project could be further developed indefenitly. The most important features that I'd like to adress in the near future are:
- [ ] easily being able to adapt every single parameter. Probably by converting to C++ to use pointers.
- [ ] (adapative) DSP
- [ ] generative variations
- [ ] AI to check if the audio is adapting well
- [ ] a priority system with ducking
- [ ] more eleborate dynamics system
- [ ] probability
- [ ] more info from input files
- [ ] more intricate algorythms to generate data
- [ ] a visual respresentation such as in [N.A.D Visual Parameter Adaption in Unity](https://github.com/StijndeK/N.A.D.VisualParameterAdaption)


