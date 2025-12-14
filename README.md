# Unity Learning

A personal Unity project where I'm learning through trial and error, experimenting with 3D character movement mechanics and camera controls.

## Overview

This is my experimental Unity project where I'm trying out different game development concepts. Currently implements a basic 3D character controller with physics-based movement, jumping mechanics, and a smooth follow camera as I learn Unity through hands-on experimentation.

## Features

- **Character Movement**: WASD keyboard controls for directional movement
- **Running**: Hold Shift to increase movement speed
- **Jumping**: Space bar to jump (only when grounded)
- **Air Control**: Reduced movement control while in the air
- **Follow Camera**: Smooth camera that follows the player with mouse-controlled rotation
- **Performance Monitoring**: Real-time FPS and speed display

## Requirements

- **Unity Version**: 6000.3.1f1 or compatible
- **Input System**: Unity's new Input System package
- **TextMesh Pro**: For UI text rendering

## Project Structure

```
unity-learning/
├── Assets/
│   ├── InputSystem_Actions.inputactions
│   ├── Scenes/
│   │   └── MainScene.unity
│   ├── Scripts/
│   │   ├── Movement.cs          # Player movement controller
│   │   └── FollowCamera.cs      # Camera follow system
│   └── Settings/
├── ProjectSettings/
└── Packages/
```

## Setup Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/xWalfie-SMR/unity-learning.git
   ```

2. **Open in Unity**:
   - Open Unity Hub
   - Click "Add" and select the cloned project folder
   - Unity will automatically import all required packages

3. **Open the Main Scene**:
   - Navigate to `Assets/Scenes/MainScene.unity`
   - Double-click to open the scene

4. **Play the scene**:
   - Press the Play button in the Unity Editor to test the game

## Controls

| Input | Action |
|-------|--------|
| **W** | Move Forward |
| **A** | Move Left |
| **S** | Move Backward |
| **D** | Move Right |
| **Space** | Jump |
| **Shift** | Run (hold) |
| **Mouse** | Look Around / Rotate Camera |

## Scripts Overview

### Movement.cs
Handles all player movement mechanics including:
- Keyboard input processing (WASD)
- Physics-based movement using Rigidbody
- Ground detection using raycasts
- Jump mechanics with ground check
- Sprint functionality
- Speed and FPS display

### FollowCamera.cs
Implements the camera system with:
- Smooth position interpolation
- Mouse-based rotation control
- Configurable camera offset and distance
- Vertical angle clamping to prevent over-rotation

## Customization

You can adjust various parameters in the Unity Inspector:

**Movement Component**:
- `Base Speed`: Normal walking speed (default: 7)
- `Base Accel Speed`: Acceleration rate (default: 7)
- `Run Multiplier`: Sprint speed multiplier (default: 2)
- `Jump Speed`: Jump force (default: 12)
- `Air Control Mult`: Movement control while airborne (default: 0.2)

**Follow Camera Component**:
- `Speed`: Camera follow speed (default: 90)
- `Y Offset`: Vertical camera offset (default: 1)
- `Z Offset`: Distance behind player (default: -7)
- `Mouse Sensitivity`: Mouse look sensitivity (default: 12)
- `Clamp`: Vertical rotation limits (default: 0-85 degrees)

## What I'm Learning

Through trial and error in this project, I'm experimenting with:
- Unity's new Input System
- Rigidbody physics and movement
- Raycast-based ground detection
- Vector mathematics for movement and rotation
- Quaternion rotations for camera control
- UI text updates with TextMesh Pro
- Fixed vs regular Update loops
- Camera follow algorithms

## License

This is my personal trial-and-error project for learning Unity. Feel free to check it out if you're interested.
