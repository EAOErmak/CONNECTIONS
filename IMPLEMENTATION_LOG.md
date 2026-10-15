# IMPLEMENTATION LOG — CONNECTIONS
Date: 15 October 2026
Project: C:\World\projects\Unity\CONNECTIONS

## Initial inspection
Unity 6000.6.5f1; active URP asset PC_RPAsset; URP package 17.6.0; Input System 1.20.0, new-input-only setting; uGUI 2.6.0; Pipeline 0.8.0-exp.1 and Test Framework 1.8.0. Existing content was the starter SampleScene and tutorial/editor assets. No applicable AGENTS.md was found. Existing template assets and project settings were preserved.
One initial unrelated Editor console entry was present: "Maximized serialized file backup not found." TMP Essentials were imported non-interactively. An old imported font metadata-version warning appeared during that import; it did not remain in the final Console ground-truth check.

## Milestone 1 — complete
Added the modular folder structure and foundation scripts. Created and saved MainMenu and Gameplay through live Editor APIs. Added both scenes ahead of the preserved SampleScene in build settings. Established intro/playing/modal/paused/ending/results states, controls, settings, credits, cursor ownership, interaction contract, UI and pause-aware timers. Inspected the scene references and one EventSystem per scene.
Play Mode validated WASD movement and modal locking. Later input tests exposed a tooling/player-loop frame-edge weakness in raw key polling; button actions now use Input System callbacks, and actual simulated E/Escape/G/J/Enter/Backspace inputs passed. Camera look consumes accumulated mouse-delta action events. Intro uses real-time elapsed delay and background simulation is enabled for reliable Editor operation.

## Milestone 2 — complete
Constructed Room 1, warm intro practical, physical electrical panel, six switches/lamps/engravings and mechanical modal. Added Electrical Connections ScriptableObject with XOR masks 38,9,4,18,8,32. Exhaustive evaluation of 64 configurations found unique solution 46 (OFF,ON,ON,ON,OFF,ON). Play Mode confirmed all lamps on, restored lighting, door opening and solved-state lock. Final mouse-click validation solved the actual UI switches. No automatic player teleport exists.

## Milestone 3 — complete
Constructed Room 2, transceiver, telegraph key and dark exit. Added Morse message/codec, correction, modal, replaceable chart Image, symbol inputs, mouse-control exclusion, transmission lock/audio spacing and configured 2.5-second response delay. Questions/answers match the specification.
Verified A-Z roundtrips and malformed/correctable input. Play Mode rejected an incorrect answer without clearing it, accepted POWER/KEY/READY sequentially and opened the exit. Actual input and UI click checks passed. Environmental audio and an in-progress transmission are not reset by modal closure.

## Milestone 4 — complete
Created CRT Television prefab and placed one in the dark menu and two above head height, away from the puzzles. Added restrained procedural static/interference/glow and independent spatial static audio. Added procedural WAV assets and lamp/machinery hum with configurable attenuation. Added aged ceramic/concrete/rust textures, pipework, cabinets and drains.
Inspected menu and restored room captures and confirmed active loop AudioSources, spatial blend and distance settings. No human listening test was claimed.

## Milestone 5 — complete
Implemented guarded exit threshold, fade, TO BE CONTINUED, three formatted times and menu return. Tested walking across the threshold, result visibility, return and new-run reset. Confirmed closed-door collision and two-way travel through the unlocked doorway. Pause excluded elapsed time; solved timers stopped.
Created Connections > Validate Demo, which passed 415 deterministic and serialized-scene checks. Final compilation succeeded. Final actual Unity Console ground truth had zero errors and zero warnings.

## Deliverables and limits
GAME_DESIGN.md contains the user's confirmed specification. DELIVERY_REPORT.md describes scenes, scripts, prefab, controls, customization, replacement workflows, tests and next steps. Automated validation output is Assets/_Game/Documentation/VALIDATION_REPORT.txt.
Current demo contains only Main Menu, two puzzle rooms, dark exit and results. No future safe, wire, bridge or radio puzzles; no Rooms 3–5; no dialogue/voice/subtitle timing.
Chart, art and audio remain intentionally replaceable prototype content. No standalone build or human uninterrupted playthrough was performed. Tool-driven checks sometimes repositioned the player or fed answer data through runtime methods; actual keyboard, mouse and collision paths were separately exercised. URP can automatically reduce point-light shadow resolution to fit its unchanged shadow atlas.
