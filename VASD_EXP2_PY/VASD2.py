import os
import simpleaudio as sa
import soundfile
import random
import sys, select
import time

print ("Press return to stop audio playing")

# initialise 
nameList = [] # to contain all wav files
newNameList = [] # to contain all new wav files
entryList = [] # all different audio files
currentTrack = []
loopDuration = 0

# set functions
def playTrack(name):
    wave_obj = sa.WaveObject.from_wave_file(name)
    print("playing: " + name)
    play_obj = wave_obj.play()

    # TODO: change this into wait a certain time (this does require a bpm input and)
    time.sleep(loopDuration)
    # play_obj.wait_done()

def calculateTime():
    while True:
        bpm = float(input("set bpm: "))
        if bpm > 0 and bpm < 300:
            break
    while True:
        beats = float(input("set loop length in beats: "))
        if beats > 0:
            break
    newDuration = (60/bpm) * beats
    return newDuration

def play():
    print ("Now playing")
    # start by playing first track
    for entry in entryList:
        if int(entry[1]) == 1:
            currentTrack = entry
            break
    playTrack(currentTrack[0])
    currentTransitionOptions = currentTrack[2]
    # play loop
    while True:
        # choose which track to play
        transitionValue = currentTransitionOptions[random.randint(1, len(currentTransitionOptions)) - 1]
        # find track
        for entry in entryList:
            # check if first track
            if entry[1] == transitionValue:
                currentTrack = entry
                break
        # play track
        playTrack(currentTrack[0])
        currentTransitionOptions = currentTrack[2]
        # check if return is pressed        
        i,o,e = select.select([sys.stdin],[],[],0.0001)
        if i == [sys.stdin]: 
            break

# get al wav files in a list
print("Loaded audio loops:")
for root, dirs, files in os.walk("."):
    for filename in files:
        if filename.endswith('.wav'): 
            print(" - " + filename)
            nameList.append(filename)

# convert tot 16 bit to make sure audio is playable and copy the audio so the source files are safe
for name in nameList:
    data, samplerate = soundfile.read(name)
    newName = "".join(("new_", name))
    soundfile.write(newName, data, samplerate, subtype='PCM_16')
    newNameList.append(newName)

# set all entrys
for name in newNameList:
    # set possible transitions
    possibleTransitions = []
    tempName = name[:-4] # remove .wav
    while True:
        possibleTransition = tempName[-1]
        if possibleTransition != "_":
            possibleTransitions.append(possibleTransition)
            tempName = tempName[:-1]
        else:
            break
    
    # add entry
    entryList.append([name, name[4], possibleTransitions])

# set duration of loop
loopDuration = calculateTime()

while True:
    while True:
        i = input("Type 'p' to play or 'q' to quit: ")
        if i == "p" or i == "q":
            break
    if i == "p":
        "Playing"
        play()
    else: 
        print("bye!")
        break

# remove created files
for name in newNameList:
    os.remove(name)

