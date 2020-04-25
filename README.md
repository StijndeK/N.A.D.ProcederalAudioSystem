# N.A.D. Procedural Audio System
PAS (working title) is a framework/tool designed to improve procedural (game)music systems prototyping and implementing. Even though procedural sound design has become standard in gameaudio, procedural music is still relatively underrepresented. It can be difficult to prototype, test and implement. PAS allows for quick testing and implementing procedural audio, using a sample based sequencer. 

PAS is a general framework/tool for procedural non-linear audio, but is also simultaneously being used for and tested on 'Procedural Imagination'. A game that is currently being developed, which generates envirnonments while exploring.
![08 concept art 1](https://user-images.githubusercontent.com/31696336/80291854-857ec580-8751-11ea-884a-7a34bae40979.png)
![08 concept art 2b](https://user-images.githubusercontent.com/31696336/80292132-1a82be00-8754-11ea-904e-b270132c07cd.png)

A (adapted) functionality caried over from [N.A.D Automatic File Loading in Python](https://github.com/StijndeK/N.A.D.AutomaticSoundloader) automatically loads files from specific folders and obtains certain parameters based on the filenames. This reduces the amount of time and disturbance of the workflow, caused by having to implement the audio before being able to test it.

## N.A.D.
Procedural Audio System (working title) is the third experiment for my thesis about: Tools for Designing Non-Linear Audio for Games. As a game audio designer I am hindered during my creative process, due to the discrepancy between the nonlinearity of my work and the linear character of standard audio production software. Standard linear audio production tools offer little to none non-linear sequencing, transitioning, parameter adaption and probability functionalities. Furthermore, linear sequencers are not optimised to provide a visual representation of non-linear systems. Existing standard middleware solutions focus less on tackling the obstructions in the design stage of a project. These issues cause testing and prototyping to take unnecessary amounts of time and obscures communication with collaborators. This obstructs the workflow and discourages innovation. 

#### Other N.A.D. tools & experiments
- [N.A.D Automatic File Loading in Python](https://github.com/StijndeK/N.A.D.AutomaticSoundloader)
- [N.A.D Visual Parameter Adaption in Unity](https://github.com/StijndeK/N.A.D.VisualParameterAdaption)

## Unity and C#
To allow for quick testing and implementing, PAD is written in C# for Unity. However, because PAD should serve as a general framework, PAS moves away from a component based Unity hierarchy and uses little unity specific elements. A dynamic approach allows for easy being adding and changing functionalities, which keep new possibilities open.

## Systems Design
Parameters received from the game are used to trigger and generate data cycles, which set audio parameters for layers, that are triggered in the sequencer. 

### Generating musical values
Musical values for layers are divided into a 'rythm' and a 'tonal' layer. The rythm decides on which tick audio needs to be player. The tonal layer decides what notes need to be played based on the layertype (melody, countermelody, chord, percussion, etc) and the available samples within the layer. This data is then parsed to a cycle which will set layer data when triggered.
![PI_Concept-Copy of Dataflow green](https://user-images.githubusercontent.com/31696336/80291982-a98ed680-8752-11ea-956d-5cdb794d3c7d.png)

### Cycles
 A cycle holds the parameters (rythm, On/Off toggle, etc) it needs to adapt and for what layers they need to adapt. Cycles are triggerd by external data, or by the sequencer (timed cycles). An important future functionality is to have cycles hold data on how the parameters need to adapt.
![PI_Concept-Copy of Copy of Dataflow green](https://user-images.githubusercontent.com/31696336/80292024-1c984d00-8753-11ea-92d1-9880b7cc3c60.png)

### Layers
The following diagram depicts the working of the layer classes. A layer can be a vertical looping layer or sound effect. The adaptable data that layers currently hold can be added to in the future, for a more dynamic procedural system.
![PI_Concept-Copy of Copy of Dataflow green (1)](https://user-images.githubusercontent.com/31696336/80292056-684af680-8753-11ea-9617-b626d93414bb.png)

### General Dataflow
![PI_Concept-Dataflow green](https://user-images.githubusercontent.com/31696336/80291955-6fbdd000-8752-11ea-9429-48b1b7a9ca80.png)

## Current status
The most important goals for this project are to improve the audio designing process by removing the obstructions in the workflow and allow for more experimentation, by making procedural systems easier to test.

## Improvements
The current MVP for N.A.D. has been reached. However, there still are a lot of features I want to add, such as:
- DSP
- priority system with ducking
- template for receiving game parameters
- how parameters need to be changed in cycles
- dynamics system
- randomisation
- more info from input files
- synthesis
- less randomness and more algorythmic composition

