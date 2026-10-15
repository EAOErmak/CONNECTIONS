
You are the lead Unity developer and technical game designer for this project.

You have access to the Unity project and Unity Editor through Unity MCP / Unity CLI.

Your task is to build a complete playable 3D first-person psychological suspense puzzle game demo based on the specification below.

The current placeholder title is "CONNECTIONS".

IMPORTANT: "Connections" is the game jam theme and a temporary project title, not necessarily the final game name.

Do not add subtitles such as "Continuity Test" to the game title.

==================================================
1. DEVELOPMENT RULES
==================================================

You must work directly in the Unity project using the connected tools.

Do not simply provide code snippets or instructions.

You are expected to:
- Inspect the existing Unity project.
- Identify Unity version, render pipeline, packages, input system and existing content.
- Use the available Unity MCP tools to create and modify scenes, GameObjects, components, prefabs, scripts, materials and UI.
- Preserve existing useful project content.
- Avoid destructive project changes.
- Create a clean, modular project architecture.
- Implement features incrementally.
- Validate each completed stage.
- Check Unity Console errors after changes.
- Fix compilation and runtime issues introduced by your work.
- Actually inspect scenes and GameObject references.
- Use reasonable gameplay defaults for unspecified details.
- Expose adjustable values in the Unity Inspector.
- Use ScriptableObjects when they provide a clear benefit.
- Avoid unnecessary overengineering.
- Never claim a feature was tested unless it was actually tested.

When using Unity MCP, inspect the available tools before selecting the implementation method.

If a tool is unavailable, use another supported method without unnecessarily changing the project.

Do not overwrite existing project settings unless necessary.

Maintain a readable folder structure.

Recommended structure:

Assets/
  _Game/
    Scenes/
    Scripts/
      Core/
      Player/
      Interaction/
      Puzzles/
      UI/
      Audio/
      Environment/
      Utilities/
    Prefabs/
    Materials/
    Textures/
    Audio/
    ScriptableObjects/

You may adjust this structure according to the actual project.

Create GAME_DESIGN.md containing the confirmed game specification and IMPLEMENTATION_LOG.md tracking completed functionality.

If AGENTS.md already exists, preserve and respect it.

==================================================
2. GAME OVERVIEW
==================================================

Genre:
3D First-Person Puzzle / Psychological Suspense / Thriller.

Age rating target:
PG-13.

No graphic violence, gore, combat or mandatory jump scares.

The game takes place inside a mysterious facility consisting of connected, enclosed rooms.

The atmosphere should feel unsettling, cold, isolated and oppressive.

Visual inspiration:
- Abandoned industrial bathrooms.
- Dirty ceramic tiles.
- Old concrete walls.
- Rusty pipes.
- Worn metal doors.
- Electrical equipment.
- Damp-looking surfaces.
- Weak warm light bulbs.
- Old CRT televisions.
- Dark corners and heavy shadows.

The setting is inspired by the atmosphere of classic psychological thrillers such as the original Saw, but must be an original environment and not a recreation of copyrighted scenes or characters.

The story is intentionally mysterious.

The player is apparently participating in an unknown experiment.

A mysterious, calm and unsettling person will eventually communicate with the player through CRT televisions.

Do NOT implement dialogue, voice acting or subtitle sequences yet.

Those systems will be developed later.

==================================================
3. CURRENT PROJECT SCOPE
==================================================

The complete game is planned to contain five rooms.

However, the CURRENT DEMO includes ONLY:

1. Main Menu.
2. Room 1 — Electrical Panel Puzzle.
3. Room 2 — Morse Communication Puzzle.
4. Dark exit transition.
5. "TO BE CONTINUED" results screen.

DO NOT implement:
- Safe puzzle.
- Wire connection puzzle.
- Bridge puzzle.
- Radio frequency puzzle.
- Rooms 3, 4 or 5.
- Final story reveal.
- Television dialogue sequences.
- Voice acting.
- Subtitle timing system.

You may prepare clean integration points for future content, but do not build unnecessary placeholder systems.

==================================================
4. MAIN MENU
==================================================

Create a dedicated main menu scene.

The menu must appear almost completely black.

There should be no clearly visible walls, floor, ceiling or traditional decorated background.

On the LEFT side of the screen:

CONNECTIONS

START GAME
SETTINGS
CREDITS
EXIT

The buttons should be text, not physical buttons.

They should feel like they are located in the same dark space as the television rather than being placed on a visible menu panel.

On the RIGHT side:

One old CRT television.

The television should be a physical 3D object.

The television screen continuously displays animated analog static.

Use a procedural static effect if no suitable assets exist.

The television is the primary visible environmental object.

Its screen emits weak light, revealing only a little of its frame.

The rest of the scene remains almost pitch black.

Menu button hover:
Subtle brightness change.

Menu button click:
Short mechanical click.

Television:
- Animated black/white static.
- Slight brightness instability.
- Occasional horizontal interference.
- Continuous low-volume static audio.

Avoid excessive visual effects or strong flashing.

START GAME:
Transition to the first room.

SETTINGS:
Open a functional minimal settings panel.
At minimum support overall audio volume and mouse sensitivity if applicable.

CREDITS:
Open a simple credits panel with a Back button.
Text should be easy to edit later.

EXIT:
Exit the application in a standalone build.

Ensure there are no decorative buttons that do nothing.

==================================================
5. PLAYER CONTROLLER
==================================================

Create a functional first-person player controller.

Controls:

WASD — movement.
Mouse — camera look.
E — interact.
Escape — close puzzle modal or pause when appropriate.

Use a CharacterController or another appropriate Unity solution.

Implement:
- Smooth first-person movement.
- Mouse sensitivity.
- Mouse cursor locking during gameplay.
- Cursor unlocking in menus and puzzle interfaces.
- Simple collision with the environment.
- Appropriate player height.
- Interaction raycasting.

Do not add:
- Weapons.
- Combat.
- Sprint mechanics unless genuinely needed.
- Complex parkour.
- Player inventory.

The player should feel like an ordinary person exploring a confined facility.

==================================================
6. INTERACTION SYSTEM
==================================================

Create a reusable interaction system.

Interactive objects should implement a clean interaction contract, such as an interface or component-based design.

When the player looks at an interactable object within range, display a subtle prompt:

[E] INTERACT

Pressing E opens the corresponding interaction.

For puzzle objects:
- Open a modal puzzle interface.
- Unlock the mouse cursor.
- Disable player movement.
- Disable camera rotation.
- Keep environmental audio playing.
- Allow Escape to close the modal.
- Restore normal gameplay controls when closed.

Puzzle state must persist when the modal is closed.

Do not reset puzzle progress merely because the player stops interacting.

Ensure input events do not leak between gameplay and modal interfaces.

==================================================
7. DEMO LEVEL LAYOUT
==================================================

Create two connected physical rooms.

Room 1:
- Initial player spawn.
- Electrical switch panel near the starting area.
- One dim warm light source for the opening sequence.
- Other ceiling or wall lights activated after puzzle completion.
- One elevated CRT television positioned in a corner away from the puzzle.
- Door leading to Room 2.

Room 2:
- Morse communication apparatus.
- Industrial equipment and environmental details.
- One elevated CRT television positioned away from the Morse apparatus.
- Lighting activated after Room 1 is solved.
- Dark exit doorway leading to the demo ending.

Both rooms should have:
- Dirty industrial or bathroom-style surfaces.
- Concrete and/or worn tile.
- Rusted pipes.
- Worn metal equipment.
- Shadows and dark corners.
- Sufficient visual clarity to navigate.

Televisions must be mounted on brackets above player head height.

They should not stand on desks.

The player must be able to return from Room 2 to Room 1.

Completed puzzles should remain completed.

The two rooms should be part of one navigable world, whether implemented as one scene or an appropriate multi-scene setup.

Prefer simple and reliable scene organization.

If external art assets are unavailable, use Unity primitives, procedural geometry and materials to create a coherent prototype.

Do not require paid asset packs.

==================================================
8. INTRO SEQUENCE
==================================================

After pressing START GAME:

Load the gameplay environment.

The player spawns inside Room 1.

Initially:
Complete darkness or near-total darkness.

Player control may be briefly restricted during the initial two-second sequence.

After approximately 2 seconds:
A single weak warm incandescent-style bulb turns on.

Play an electrical click or activation sound.

The bulb starts producing a quiet electrical hum.

The nearby electrical puzzle panel becomes visible.

CRT televisions begin displaying and emitting static after the initial two seconds.

The other main room lights remain off or inactive until the Electrical Panel puzzle is solved.

The initial sequence should feel atmospheric, not like a jump scare.

==================================================
9. ROOM 1 — ELECTRICAL PANEL
==================================================

Create a physical electrical panel near the player.

The panel should contain:
- Six switches.
- Six indicator lamps.
- Six engraved numbers above the switches.

The switch labels are:

5 0 1 2 4 7

These engraved numbers must remain in their fixed order.

Before room lighting is restored, the engravings should be barely visible in weak light.

After the puzzle is solved and room lighting activates, they should become significantly easier to read.

These numbers will be used in a future safe puzzle.

Do not implement the safe yet.

Interaction:
Player approaches the panel and presses E.

Open a modal puzzle UI resembling a mechanical electrical control panel.

The interface has exactly six interactive switches and six indicator lamps.

All switches and indicator lamps start OFF.

Each switch TOGGLES the state of certain indicator lamps.

This means:
If a lamp is OFF, it becomes ON.
If a lamp is ON, it becomes OFF.

Use the following logic:

Switch 1 toggles lamps 2, 3 and 6.
Switch 2 toggles lamps 1 and 4.
Switch 3 toggles lamp 3.
Switch 4 toggles lamps 2 and 5.
Switch 5 toggles lamp 4.
Switch 6 toggles lamp 6.

The final state of the system depends on which switches are enabled.

The initial state is all switches OFF.

The required solution is:

Switch 1 OFF
Switch 2 ON
Switch 3 ON
Switch 4 ON
Switch 5 OFF
Switch 6 ON

This configuration must make ALL SIX indicator lamps ON.

The combination must be the unique solution.

Implement the switch behavior using maintainable logic, preferably a connection matrix or equivalent data-driven representation.

Do not use a long collection of special-case conditions.

Every switch click should:
- Change its visual state.
- Update connected indicator lamps.
- Play a mechanical switch sound.
- Immediately update the puzzle UI.

Puzzle completion occurs when all six indicator lamps are ON.

When completed:
1. Lock the solved state.
2. Play a power restoration sound.
3. Turn on additional lights in Room 1.
4. Activate the prepared lighting in Room 2.
5. Unlock and open the door to Room 2.
6. Allow the player to continue exploring.

Do not automatically teleport the player.

Make the solved panel readable when revisited.

Avoid allowing subsequent interaction to undo the global solved state.

==================================================
10. ROOM 2 — MORSE COMMUNICATION
==================================================

Create an old communication apparatus.

It should look like an industrial Morse or telegraph device, not a modern smartphone.

Player approaches and presses E.

Open a modal Morse communication interface.

The modal should include:

- Current question.
- Morse symbol input.
- Decoded text display.
- Morse reference image placeholder.
- Control hints.
- TRANSMIT button.
- CLEAR button.

IMPORTANT:
The Morse reference chart must initially be a placeholder.

Do not download or generate a final Morse alphabet image.

Provide an easily replaceable Unity UI Image reference so the developers can assign their own Sprite in the Inspector.

The placeholder should be visibly labeled:
MORSE CHART PLACEHOLDER

==================================================
11. MORSE INPUT
==================================================

Controls while the Morse modal is open:

LMB or G:
Enter dot (.)

RMB or J:
Enter dash (-)

Enter:
Finish the current Morse letter.

Backspace:
Remove the most recent input symbol, or allow correction of the previous completed letter when appropriate.

CLEAR:
Clear the current message.

TRANSMIT:
Must be clicked using the mouse cursor and LMB.

Show all input hints clearly in the UI.

Prevent mouse clicks on UI buttons from being interpreted as Morse symbols.

This is important:
Clicking TRANSMIT must NOT add a dot.

Clicking CLEAR must NOT add a dot.

Use a standard international Morse alphabet mapping for A-Z.

The system should display:
1. Raw Morse symbols.
2. Decoded letters/text.

The player should be able to see the message being built.

Invalid Morse input should produce understandable feedback and remain correctable.

Do not silently accept malformed input.

==================================================
12. MORSE QUESTIONS
==================================================

There are three questions in this exact order.

QUESTION 1:
"What did the first panel restore?"

Correct answer:
POWER

QUESTION 2:
"What can open a lock?"

Correct answer:
KEY

QUESTION 3:
"Confirm that you are ready to continue."

Correct answer:
READY

These are ordinary English questions.

The player must encode the answers in Morse using the reference chart.

Responses are checked after transmission.

Correct response:
- Play a confirmation sound.
- Display MESSAGE ACCEPTED.
- Advance to the next question.

Incorrect response:
- Display INVALID MESSAGE or equivalent.
- Allow unlimited retries.
- Do not reset previously completed questions.
- Allow the current message to be corrected and retransmitted.

The puzzle should not punish experimentation.

==================================================
13. MORSE TRANSMISSION SEQUENCE
==================================================

When TRANSMIT is clicked:

1. Prevent editing during active transmission.
2. Play the corresponding Morse sound sequence.
3. Keep the decoded text visible.
4. After the outgoing sequence finishes, wait approximately 2–3 seconds.
5. Simulate a response from the receiving side.
6. Display the result.
7. Play a confirmation or rejection signal.
8. Re-enable input if a retry is needed.

The response delay should be configurable in the Inspector.

Support actual audio feedback for dot and dash signals.

Use different dot and dash durations.

Use proper spacing between Morse elements and letters.

For now, the responses are text and non-verbal audio signals.

Do not implement human voice dialogue yet.

After all three answers are accepted:
- Mark the puzzle solved.
- Play communication-established feedback.
- Unlock and open the exit doorway.
- Allow the player to walk toward it.

==================================================
14. ENVIRONMENTAL AUDIO
==================================================

Spatial environmental audio is extremely important.

Create an audio architecture supporting independent 3D sound emitters.

LIGHT BULBS:
Each active light bulb may produce a quiet continuous electrical hum.

The hum must be spatial.

When the player approaches a bulb:
The hum becomes louder.

When the player moves away:
The hum fades naturally.

Avoid making the hum equally loud everywhere.

CRT TELEVISIONS:
Each television has its own static audio emitter.

The static should be audible nearby but much quieter at a distance.

Televisions should be located away from the main puzzle apparatus to avoid overwhelming the player with overlapping sounds.

Use configurable:
- Minimum distance.
- Maximum distance.
- Volume.
- Spatial blend.
- Rolloff curve.

Appropriate low-pass or occlusion effects may be added if practical.

Other sound categories:
- Switch clicks.
- Electrical relay activation.
- Door unlocking.
- Door opening.
- Morse dot/dash.
- Morse response.
- Puzzle completion.
- General ambient machinery.

Keep the soundscape restrained and unsettling.

If final audio assets are unavailable, create simple procedural or replaceable placeholder audio.

Do not introduce dependencies on missing external audio files.

Ensure all placeholder sounds can later be replaced in the Inspector.

==================================================
15. CRT TELEVISION SYSTEM
==================================================

Create a reusable CRT television prefab.

It should have:
- 3D television model or convincing primitive prototype.
- Screen material.
- Procedural static animation.
- Very subtle emissive glow.
- Spatial static audio.
- Mounting bracket geometry.

Televisions in gameplay rooms are mounted high on walls or in corners, above the player's head.

They should not be directly beside puzzle interaction points.

The menu television can use the same reusable television components with different scene placement and audio settings.

Do not create human dialogue or voice playback sequences yet.

Keep the television system simple enough to extend later with voice clips and subtitles.

==================================================
16. LIGHTING AND VISUAL ATMOSPHERE
==================================================

The environment should communicate PG-13 psychological suspense.

Not extreme horror.

Use:
- Cold desaturated environmental tones.
- Warm isolated practical lights.
- Dirty surfaces.
- Aged materials.
- Rusty metal.
- Deep but readable shadows.
- Minimal atmospheric fog where appropriate.

Avoid:
- Bright clean laboratory aesthetics.
- Cheerful colors.
- Excessive gore.
- Graphic injuries.
- Monster combat.
- Cheap jump scares.

When the first puzzle is solved, lighting should be visibly restored, but the environment must remain moody.

Do not turn the rooms into fully bright, comfortable spaces.

The main menu should remain much darker than gameplay.

==================================================
17. PUZZLE TIMING AND RUN STATISTICS
==================================================

Create a simple run timing system.

Track:

TOTAL TIME
ELECTRICAL PANEL TIME
MORSE COMMUNICATION TIME

Total time:
Starts when gameplay begins after selecting START GAME.
Stops when the player enters the final dark exit.

Puzzle time:
Starts when the player FIRST interacts with the puzzle.
Stops when the puzzle is solved.

If the player closes the modal, moves around or thinks about the answer, the puzzle timer continues.

Reopening the puzzle must not restart its timer.

If a pause system exists, paused time should not count.

Use reliable time accounting and do not depend on display frame rate.

Format results as MM:SS or HH:MM:SS when necessary.

No score, lives or penalties are needed in this demo.

==================================================
18. DEMO ENDING
==================================================

After the Morse puzzle is completed:
Unlock and open the exit door.

Behind the doorway should be absolute or near-absolute darkness.

When the player walks across the exit threshold:

1. Detect the player entering the ending trigger.
2. Prevent the ending from being triggered repeatedly.
3. Fade the screen smoothly to full black.
4. Disable gameplay input.
5. Stop the run timer.
6. Display:

TO BE CONTINUED

Underneath, show:

TOTAL TIME
[formatted time]

ELECTRICAL PANEL
[formatted time]

MORSE COMMUNICATION
[formatted time]

Add a functional:

RETURN TO MENU

button.

The result screen must remain minimalist, dark and atmospheric.

Do not display "Continuity Test".

Do not reveal the identity or motivation of the experiment's organizer.

When the player returns to the main menu, ensure the next new run starts with correctly reset puzzle and timer states.

==================================================
19. THEME: CONNECTIONS
==================================================

The game jam theme is CONNECTIONS.

It should be integrated into gameplay, not only visual decoration.

Room 1:
Physical and causal connections.

Switches are logically connected to indicator lamps.

The player discovers how actions affect multiple connected components.

Room 2:
Communication connections.

The player establishes communication through Morse signals.

Across the game:
Rooms are physically connected.
Actions open access to new spaces.
Previously observed information may become useful later.
Televisions suggest a connection between the player and an unknown observer.

Do not overexplain the theme to the player.

The mystery should remain intact.

==================================================
20. DEVELOPMENT MILESTONES
==================================================

Implement the CURRENT DEMO through the following milestones.

MILESTONE 1 — PROJECT FOUNDATION

- Inspect the Unity project.
- Establish folder structure.
- Configure input.
- Create basic GameState / Session flow.
- Create Main Menu scene.
- Create Gameplay scene.
- Implement first-person controller.
- Implement interaction system.
- Implement modal input locking.
- Confirm compilation.

MILESTONE 2 — ROOM 1

- Build Room 1 geometry.
- Build warm lamp intro.
- Build electrical panel.
- Implement six-switch logic.
- Implement six indicator lamps.
- Add the six engraved digits.
- Implement completion event.
- Restore lighting.
- Open Room 2 door.
- Confirm puzzle can be solved.

MILESTONE 3 — ROOM 2

- Build Room 2 geometry.
- Create Morse apparatus.
- Implement Morse UI.
- Add replaceable reference image placeholder.
- Implement LMB/G and RMB/J input.
- Implement Enter/Backspace correction.
- Implement proper Morse decoding.
- Implement three questions.
- Implement TRANSMIT.
- Implement transmission sound.
- Implement response delay.
- Implement success/retry flow.
- Open exit door after completion.

MILESTONE 4 — ATMOSPHERE

- Create reusable CRT television prefab.
- Add televisions to menu and both rooms.
- Implement animated static.
- Implement localized television audio.
- Add spatial lamp hum.
- Add appropriate sound feedback.
- Improve lighting and materials.
- Add simple environmental detail.

MILESTONE 5 — ENDING AND VALIDATION

- Implement total timer.
- Implement separate puzzle timers.
- Implement dark exit trigger.
- Implement fade to black.
- Implement TO BE CONTINUED screen.
- Display run statistics.
- Add Return to Menu.
- Test restart/reset behavior.
- Validate complete playthrough.

Finish and validate each milestone before proceeding to the next.

Avoid changing completed working systems unnecessarily.

If execution limits prevent completing everything in one session, finish a coherent milestone and clearly document what remains.

==================================================
21. VALIDATION AND ACCEPTANCE TESTS
==================================================

The demo is complete only when the following behaviors work.

MAIN MENU:
- Menu is almost pitch black.
- CONNECTIONS text appears on the left.
- CRT television appears on the right.
- Static animation works.
- Buttons respond correctly.
- START GAME loads gameplay.

PLAYER:
- WASD movement works.
- Mouse look works.
- E interaction works.
- Modal windows stop movement and mouse look.
- Environmental audio continues during modal interactions.
- Closing a modal restores gameplay.

INTRO:
- The player initially sees darkness.
- A dim warm bulb activates after approximately 2 seconds.
- Audio and television behavior activates correctly.

ELECTRICAL PUZZLE:
- Six switches work.
- Six lamps respond to their connections.
- All lamps initially OFF.
- The unique required solution works.
- The room lights activate.
- Door to Room 2 opens.
- Engraved numbers are present.
- Puzzle completion persists when revisited.

MORSE PUZZLE:
- Dot and dash inputs work.
- Both mouse and keyboard alternatives work.
- Enter completes a letter.
- Backspace allows correction.
- Decoded text is displayed.
- Morse placeholder image is replaceable.
- TRANSMIT does not accidentally add a dot.
- Morse signal audio is played.
- A configurable 2–3 second response delay works.
- Wrong responses can be retried.
- POWER, KEY and READY are accepted.
- Exit door opens after all three answers.

ENDING:
- Exit trigger works.
- Screen fades to black.
- TO BE CONTINUED appears.
- Total runtime is displayed.
- Individual puzzle times are displayed.
- RETURN TO MENU works.
- Starting a new game resets the run correctly.

TECHNICAL:
- No C# compilation errors.
- No broken serialized scene references.
- No missing required components.
- No critical runtime exceptions.
- No unresponsive UI controls.
- No permanently stuck player input state.
- No duplicate puzzle completion events.

Where practical, create automated checks for deterministic puzzle logic, especially the switch matrix and Morse decoding.

After changes, inspect the actual Unity Console and scenes through available MCP tools.

Do not falsely report successful Play Mode testing if the tools cannot perform it.

==================================================
22. FINAL DELIVERY REPORT
==================================================

After implementation, give a concise report containing:

1. Completed milestones.
2. Scenes created.
3. Scripts created or modified.
4. Prefabs created.
5. GameObjects and important components.
6. How to start the game in Unity.
7. Player controls.
8. How the two puzzles work.
9. How to replace Morse reference image.
10. How to replace placeholder audio later.
11. Current Inspector customization options.
12. Tests performed.
13. Remaining known issues.
14. Recommended next improvements.

The result must be an actual playable Unity project, not merely a written implementation proposal.

Begin now by inspecting the connected Unity project and implementing Milestone 1.

Proceed incrementally through the milestones, validating work as you go.
