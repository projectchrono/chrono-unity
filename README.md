# ChronoUnity
[![BSD License](http://www.projectchrono.org/assets/logos/chrono-bsd.svg)](https://projectchrono.org/license-chrono.txt)

ChronoUnity is a CSharp wrapping to integrate the powerful Project Chrono C++ physics engine within Unity, allowing for advanced physics simulations and visualisation within the Unity environment. This integration leverages SWIG (Simplified Wrapper and Interface Generator) to wrap Chrono's C++ functionalities into C# scripts, for the purpose of making them accessible within Unity projects.

## Overview

ChronoUnity brings together the high-performance physics capabilities of Project Chrono with the versatile and intuitive Unity game engine. This combination enables users to create complex physical simulations, using Chrono's vehicle dynamics, rigid body system, collision detection, and more, with the visual and interactive strengths of Unity.

<img src="https://github.com/user-attachments/assets/049b9d70-5131-43a0-b71b-396a477c0f92" width="800">


## Key Features
  The included demos highlight a number of the following key features:
- **Chrono Core Module:** Utilise Project Chrono's capabilities for NSC and SMC contact systems. Multiple solvers and approaches supported for accurate physics simulation within the Unity environment.
- **Chrono Vehicle Module:** Currently supported vehicles are the HMMWV, Gator, Kraz, UAZBus, MAN and Generic Vehicle determined by JSON files. Unity Terrain interaction is supported through Chrono's RigidTerrain.
- **Unity and Chrono Physics System Interactions:** ChronoUnity allows for one-way interactions between Unity and Chrono physics by adding Unity colliders onto Chrono objects. While Chrono can easily influence Unity's physics world, the reverse has not been developed.
- **Joints, Links and ChBody Support:** A number of ChLink, ChLinkMates and ChBodies are supported and demonstrated in the provided physics scenes.

## Limitations

### Limited C++ Functionality

Not all C++ functions from Project Chrono are available in ChronoUnity. The current SWIG wrapping primarily supports core and essential vehicle functions, with more advanced or less commonly used features potentially unavailable. Users should refer to the Unity demos and generated C# classes to see which methods and properties are accessible. Not all vehicle models are available. Track vehicle and robot models are currently not available.

## Installation using the supplied pre-built Chrono library and .cs files

Clone the current release from github (NOTE: This repo uses LFS - simply downloading a zip clone will not work).
Download the Windows Chrono Relase 9.0.1 library zip file (Unix to come).
Unzip the chrono package to the Chrono Unity Assets/Plugin folder.
Launch UnityHub and add a project from disk and navigate to the location of your ChronoUnity directory.
Launch the correct Unity version, wait for compilation, and open the physics or vehicle demo scenes.

### Prerequisites

- ChronoUnity has been developed and tested in Unity 2022.3.41.f1.
- Chrono Release 9.0.1 (Dll build and C# scripts provided for Windows x64 under releases)

## Installation using a custom Chrono library build

If you wish to build the module yourself, you will require Swig 4.x along with the other pre-requisites for the Chrono library. See the official Project Chrono documentation for more detail.

### Self-build steps

1. **Clone and Build Project Chrono:**
   Clone the Chrono repository - Release 9.0.1 and follow the instructions provided in the Project Chrono documentation to build the Chrono engine from source.
   Ensure you select the Shared libs, CSharp and Vehicle modules. (OpenMP module can optionally be included)
   The minimum build required for ChronoUnity should look like this: (note: be sure to provide your Eigen include path)
   
   <img src="https://github.com/user-attachments/assets/5ca00563-4c31-4e22-842b-42a34e18dbb4" width="500">

3. **Generate Chrono Dlls and SWIG Wrappers:**
   Open the solution and build the Chrono library with Swig C# wrappers.
   The C# scripts can be found in your Chrono build/chrono_csharp/ directory. They will be 'core' and 'vehicle'.
   Navigate to your Chrono build/bin/release/ directory. These are your build dll's.

5. **Set Up Unity Project:**
   Navigate to your clone of the Chrono Unity project directory.
   Copy the built Chrono libraries (`.dll` files), along with the csharp scripts 'core' and 'vehicle' into the `Assets/Plugins` directory of your Unity project.

   **IMPORTANT**: The SWIG wrapping process generates duplicates of some types. Unity will raise errors on acount of duplicates causing ambiguity. Combine all the .cs scripts from the 'core' and 'vehicle' directories into a single directory, overwrite the duplicate .cs files.
    
   Open Unity Hub and add a new project from disk. Navigate to your Chrono Unity directory. Allow Unity to compile scripts and open a demo scene to test.

## Documentation

For detailed information on how to use ChronoUnity, please refer to the following resources:

- **Project Chrono Documentation:** [Project Chrono](https://projectchrono.org/)
- **SWIG Documentation:** [SWIG](http://www.swig.org/)
- **Unity Documentation:** [Unity](https://docs.unity3d.com/)

## License

See the main Chrono license for details [Chrono BSD Clause](https://github.com/projectchrono/chrono?tab=BSD-3-Clause-1-ov-file)
