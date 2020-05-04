# N.A.D. Procedural Audio System
PAS is a framework/tool designed to improve procedural music systems prototyping and implementing. Even though procedural sound design has become somewhat standard in gameaudio, I find procedural music is still relatively underrepresented. It can be difficult to prototype, test and implement. PAS allows for quick testing and implementing procedural audio, using a sample-based sequencer. 

PAS is made as a general framework/tool, but is also used for and tested on 'Procedural Imagination'.A procedurally generated game world that dynamically responds to the player's actions.
Currently being developed by [Josien Vos](http://josienvos.nl/).
 [This video](https://streamable.com/y8uoq6) shows the current system applied to Procedural Imagination
![08 concept art 1](https://user-images.githubusercontent.com/31696336/80291854-857ec580-8751-11ea-884a-7a34bae40979.png)

An (adapted) functionality caried over from [N.A.D Automatic File Loading in Python](https://github.com/StijndeK/N.A.D.AutomaticSoundloader) automatically loads files from specific folders and obtains certain parameters based on the file's names. This reduces the amount of time and disturbance of the workflow caused by having to implement the audio before being able to test it.

## N.A.D.
Procedural Audio System (working title) is the third experiment for my thesis about: Tools for Designing Nonlinear Audio for Games. As a game audio designer I am hindered during my creative process, due to the discrepancy between the nonlinearity of my work and the linear character of standard audio production software. Standard linear audio production tools offer little to none nonlinear sequencing, transitioning, parameter adaption and probability functionalities. Furthermore, linear sequencers are not optimised to provide a visual representation of nonlinear systems. Existing standard middleware solutions focus less on tackling the obstructions in the design stage of a project. These issues cause testing and prototyping to take unnecessary amounts of time and obscures communication with collaborators. This obstructs the workflow and discourages innovation. 

#### Other N.A.D. tools & experiments
- [N.A.D Automatic File Loading in Python](https://github.com/StijndeK/N.A.D.AutomaticSoundloader)
- [N.A.D Visual Parameter Adaption in Unity](https://github.com/StijndeK/N.A.D.VisualParameterAdaption)

## Unity and C#
To allow for quick testing and implementing, PAS is written in C# for Unity. However, because PAS should serve as a general framework, PAS moves away from a component based Unity hierarchy and uses little to none unity specific elements. 

## Systems Design
External gamedata is used to trigger and generate datacycles that change audiodata. These cycles set audio parameters for layers. Layers hold audiofiles and data on when and how to play. The `Sequencer` receives ticks from the `Clock` and checks the information of layers to trigger audiofiles. For a more intricate audiosystem, checking what to play and calling the sounds should be split to reduce the risk of delays.

![PI_Diagram](https://user-images.githubusercontent.com/31696336/80965632-2d138c00-8e13-11ea-9b8a-95dc09f23286.png)

A modular approach makes it possible to easily add and/or edit functionalities, which keeps future possibilities open and helps to not condition the user into using certain systems. Because this tool is meant as a framework and is not specific to one game, it needs to be able to adapt to different projects and general changes in the industry/technology. For example, an infinit amount of cycles can be created to change any parameter for any layer based on any value. 

![PI_Concept-DataflowComplete (1)](https://user-images.githubusercontent.com/31696336/80933589-b354b180-8dc4-11ea-9f22-79c06825a77c.png)

### Layers
The loaded audio is divided into vertical layers, to be played independently or over each other. A layer can be a vertical looping layer or sound effect. A layer holds data on how, when and what variation to play. 

### Generating notes
Layers contain musical values, among which a *rythm* and a *tonal layer*. These rythms and melodys are used in the sequencer to check what and when to play. The rythm calls on what ticks audio needs to be played. The tonal layer decides what notes need to be played based on the `Layertype` (melody, countermelody, chord, percussion, etc) and the available samples within the layer. The notes are generated at initialisation and when a `Cycle` calls on them.

### Cycles
 A `Cycle` holds audiovalues to adapt, values to adapt to and what layers to adapt. The only way to adapt audio data is by using a cycle. Cycles can be triggered with game events using a `AdaptionMoment` or from the sequencer, using a `CycleTimer`. A `DynamicCycle` only sets the on/off value of layers, using a `CycleTimer` the `DynamicCycle` can create a natural sounding buildup. For now this buildup is mostly random, in the future specific dynamic structures could be given and/or influenced by external parameters.

## Current status & improvements
The main goal for this experiment is to research how to improve the audio designing process and encouraging experimentation by removing the obstructions in the workflow. 

As this approach to nonlinear systems design has been tested extensively, the current MVP for this project have been achieved. However, the project could be further developed indefenitly. The most important features that I'd like to adress in the near future are:
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


