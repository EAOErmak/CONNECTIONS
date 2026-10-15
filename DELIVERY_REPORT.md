# CONNECTIONS — playable demo delivery
Date: 15 October 2026

Project: C:\World\projects\Unity\CONNECTIONS
Unity: 6000.6.5f1 • URP 17.6.0 • Input System 1.20.0 • uGUI 2.6.0

All five requested milestones are implemented. The existing SampleScene, template assets, input asset, render-pipeline assets, package versions, and project settings were preserved. The two new scenes were placed first in the build scene list.

## Start
Open Assets/_Game/Scenes/MainMenu.unity in Unity and press Play, then click START GAME.
Gameplay can also be opened directly for development; it starts a fresh run.
A repeatable check is available at Connections > Validate Demo in the Unity menu, outside Play Mode.

## Completed milestones
1. Foundation: dedicated scenes, session states, CharacterController movement, camera look, raycast interaction, cursor and modal locking, pause, volume/sensitivity settings, credits and Exit.
2. Room 1: enclosed room, two-second warm-bulb intro, six physical switches and lamps, fixed 5 0 1 2 4 7 engravings, data-driven XOR puzzle, locked solved state, power restoration and sliding access door.
3. Room 2: connected enclosed room, transceiver and telegraph key, Morse modal, A–Z decoder, corrections, reference Image placeholder, three sequential questions, transmission audio, response delay, retry handling and exit door.
4. Atmosphere: reusable bracket-mounted CRT prefab in both rooms and menu, animated static and interference, restrained emissive glow, independent spatial sound emitters, replaceable generated WAV clips, procedural worn surfaces, pipes, cabinet and drain details.
5. Ending: total and independent puzzle timers, pause exclusion, guarded exit threshold, smooth fade, TO BE CONTINUED results, functional return to menu, fresh-run reset.

## Scenes and prefab
- Assets/_Game/Scenes/MainMenu.unity
- Assets/_Game/Scenes/Gameplay.unity
- Assets/_Game/Prefabs/CRT Television.prefab
- Assets/_Game/ScriptableObjects/Electrical Connections.asset

## Scripts created
Runtime, under Assets/_Game/Scripts:
- Core/GameSession.cs — flow, intro, pause, timers, completion and ending.
- Player/FirstPersonController.cs — CharacterController movement and camera look.
- Interaction/Interactable.cs, PlayerInteraction.cs — reusable contract and raycast focus.
- Puzzles/ElectricalConnections.cs, ElectricalPanel.cs — matrix asset and physical/modal presentation.
- Puzzles/MorseCodec.cs, MorsePuzzle.cs — message building, input, decoding and timed response.
- UI/GameUI.cs, MenuController.cs, SettingsPanel.cs, ButtonAudio.cs — all menus and interface behavior.
- Audio/SoundEmitter.cs — replaceable clips and configurable spatial attenuation.
- Environment/FacilityDoor.cs, ExitThreshold.cs, CRTStatic.cs — doors, trigger and television animation.
- Utilities/ButtonCommand.cs — disposable Input System button action bindings.

Editor: DemoBuilder.cs, RoomOneBuilder.cs, RoomTwoBuilder.cs, AtmosphereBuilder.cs and DemoValidation.cs. Builders are staged authoring utilities; do not rerun a creation stage on existing scenes. Validation is safe to repeat.

## Important scene objects
Gameplay: Game Session; Player with CharacterController, FirstPersonController and PlayerInteraction; Player Camera with AudioListener; Interface with GameUI, CanvasScaler and GraphicRaycaster; one EventSystem using InputSystemUIInputModule; Room 1/Electrical Panel; Room 2/Morse Apparatus; sliding doors 02 and EXIT; Dark Exit/Ending Threshold with trigger BoxCollider. Restored lighting and intro object arrays are serialized on Game Session.
MainMenu: Menu Controller, Menu Camera, Interface/EventSystem, Menu CRT and weak frame-fill light.

## Controls
WASD move; mouse look; E interact; Escape close a modal or pause/resume.
In Morse: LMB or G dot; RMB or J dash; Enter finish letter; Backspace correct symbols or restore the previous letter for editing. Click CLEAR to erase the current answer and TRANSMIT to send it. UI controls are excluded from symbol input.
Closing a puzzle does not reset progress or its timer. Environmental audio continues.

## Puzzle behavior
The electrical asset contains lamp masks 38, 9, 4, 18, 8, 32, with bit 0 representing lamp 1. Enabled switch masks combine with XOR. The unique all-on solution is OFF / ON / ON / ON / OFF / ON. Completion locks switches, restores both rooms' lights and opens door 02 without teleporting the player.
Morse answers are POWER, KEY and READY, in order. Letters must be finished with Enter. Invalid letters remain editable. Each transmission locks edits, plays dots/dashes and inter-letter spacing, waits 2.5 seconds by default, and accepts or rejects the answer. Wrong answers can be corrected without losing previous question progress.

## Replace the chart
In Gameplay, select Room 2/Morse Apparatus. MorsePuzzle.referenceImage points to Interface/Morse Modal/Morse Reference Image. Assign your imported Sprite to that Image's Source Image field. Preserve Aspect is enabled; the placeholder label hides when a Sprite is assigned. No final chart was downloaded or generated.

## Replace audio
Generated clips are under Assets/_Game/Audio. Every SoundEmitter exposes click, relay, power, door, dot, dash, accept, reject and loop AudioClip fields. Assign replacements directly on emitters; for televisions, edit the CRT Television prefab for reusable defaults.
Spatial settings expose volume, spatial blend, minimum and maximum distance, rolloff mode and a custom rolloff curve. Lamp hum uses 0.6–6 m; CRT static uses 0.7–7 m. Menu static uses a very quiet 2D override. Global volume multiplies all sources.
Default Morse clips last 0.10 and 0.30 seconds. If changing dotSeconds, replace clips with matching durations to retain sound/scheduling alignment.

## Inspector customization
Game Session: intro delay, fade duration, intro/restored object arrays, door/UI/audio references.
FirstPersonController: speed, acceleration, gravity, base mouse sensitivity and view.
Electrical Connections: six lamp masks. ElectricalPanel: visual colors, switch/lamp/engraving and audio references.
MorsePuzzle: questions/answers, response delay, dot timing, chart Image and UI/audio references.
FacilityDoor: leaf, open offset, opening duration and sound.
CRTStatic: frame rate, brightness, subtle instability, screen Renderer and glow Light.
MenuController: credits text and start transition delay.
SoundEmitter: every clip and spatial setting. Ordinary Light components and material assets remain editable.

## Validation performed
- Final C# compilation completed successfully; final Unity Console ground truth reported zero errors and zero warnings.
- Connections > Validate Demo passed 415 checks: all 64 electrical states and exact unique solution, A–Z Morse round-trips, malformed input rejection, correction/clear behavior, all three answer encodings, time formatting, missing-script checks and required scene/audio/UI/prefab references.
- Tool-driven Play Mode: menu settings and credits, START GAME, intro progression, WASD movement, mouse delta look, E raycast interaction, Escape modal closing/cursor locking and pause/resume.
- Four actual simulated mouse clicks solved the panel; a subsequent switch attempt could not undo completion.
- Actual simulated G/J and LMB/RMB produced dot/dash; Enter decoded A; Backspace restored the completed letter; CLEAR and TRANSMIT clicks added no unintended dot.
- Wrong Morse answer remained editable; POWER, KEY and READY were accepted in order. Mouse TRANSMIT started sequences; edits were blocked while busy. Dot/dash clip durations and spatial emitter playback/settings were inspected.
- Pause total time remained identical across successive checks. Puzzle timers persisted outside their modals and stopped on completion.
- CharacterController movement was blocked by the closed Room 2 door; it passed through the opened doorway in both directions. Power remained restored on return.
- Walking across the exit threshold triggered the fade and results. RETURN TO MENU and a subsequent START GAME worked by mouse. Both puzzles, puzzle timers and doors reset on the new run.
- Menu, both rooms, both modals and results were visually captured/inspected through Unity MCP. Some test setup repositioned the player and entered answer data through runtime methods; this was a tool-driven validation, not a human uninterrupted playthrough.

## Known limitations and next improvements
No critical gameplay defects were observed in these checks. Art and sound are intentionally procedural prototype assets; no dialogue, voice or subtitle sequence exists. The requested chart is still a placeholder, so assign a chart before testing with players unfamiliar with Morse. A standalone build and Application.Quit in a standalone executable were not tested. A human listening/balance pass was not performed.
URP may log automatic point-light shadow-atlas resolution reductions when all room lights are active. Newly encountered Editor shader variants can temporarily use Unity's cyan compilation placeholder; inspected later captures show the finished materials. See [Unity's asynchronous shader compilation documentation](https://docs.unity.com/en-us/engine/6000.0/manual/materials-and-shaders/shaders/shader-troubleshooting/shader-reduce-stalling/asynchronous-shader-compilation/introduction).

Next: assign the team's Morse chart Sprite; replace prototype art/audio; playtest clue clarity and audio balance; then create and test a target-platform build. The demo stops at the requested two-room ending.
