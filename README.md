# ChronoUnity
[![BSD License](http://www.projectchrono.org/assets/logos/chrono-bsd.svg)](https://projectchrono.org/license-chrono.txt)

ChronoUnity is a CSharp wrapping to integrate the powerful Project Chrono C++ physics engine within Unity, allowing for advanced physics simulations and visualisation within the Unity environment. This integration leverages SWIG (Simplified Wrapper and Interface Generator) to wrap Chrono's C++ functionalities into C# scripts, for the purpose of making them accessible within Unity projects.

## Overview

ChronoUnity brings together the high-performance physics capabilities of Project Chrono with the versatile and intuitive Unity game engine. This combination enables users to create complex physical simulations, using Chrono's vehicle dynamics, rigid body system, collision detection, and more, with the visual and interactive strengths of Unity.

## Key Features
  The included demos highlight a number of the following key features:
- **Chrono Core Module:** Utilise Project Chrono's capabilities for NSC and SMC contact systems. Multiple solvers and approaches supported for accurate physics simulation within the Unity environment.
- **Chrono Vehicle Module:** Currently supported vehicles are the HMMWV, Gator, Kraz, UAZBus, MAN and Generic Vehicle determined by JSON files. Unity Terrain interaction is supported through Chrono's RigidTerrain.
- **Unity and Chrono Physics System Interactions:** ChronoUnity allows for one-way interactions between Unity and Chrono physics by adding Unity colliders onto Chrono objects. While Chrono can easily influence Unity's physics world, the reverse has not been developed.
- **Joints, Links and ChBody Support:** A number of ChLink, ChLinkMates and ChBodies are supported and demonstrated in the provided physics scenes.

## Limitations

### Limited C++ Functionality

Not all C++ functions from Project Chrono are available in ChronoUnity. The current SWIG wrapping primarily supports core and essential vehicle functions, with more advanced or less commonly used features potentially unavailable. Users should refer to the Unity demos and generated C# classes to see which methods and properties are accessible.

## Installation using the supplied pre-built Chrono library and .cs files

Download the current release into your selected directory.
Download the Windows dll and Csharp chrono library zip file (Unix to come)
Unzip the chrono library package to the Chrono Unity Assets/Plugin folder
Launch UnityHub and add a project from disk and navigate to the location of your ChronoUnity directory.
Launch Unity, wait for compilation, and open the physics or vehicle demo scenes. Run in Editor.

### Prerequisites

- ChronoUnity has been developed and tested in Unity 2022.3.13f1. Other versions work.

## Installation using a custom Chrono library build

If you wish to build the module yourself, you will require Swig 4.x along with the other pre-requisites for the Chrono library. See the official Project Chrono documentation for more detail.

### Self-build steps

1. **Clone and Build Project Chrono:**
   Clone the Chrono repository, and follow the instructions provided in the Project Chrono documentation to build the Chrono engine from source.
   Ensure you select the Shared libs, CSharp and Vehicle modules. (OpenMP module can optionally be included)

3. **Generate Chrono Dlls and SWIG Wrappers:**
   Build the Chrono library with Swig C# wrappers.
   The Csharp scripts can be found in your Chrono build/chrono_csharp/ directory. They will be 'core' and 'vehicle'.
   Navigate to your Chrono build/bin/release/ directory. These are your build dll's.

5. **Set Up Unity Project:**
   Navigate to your clone of the Chrono Unity project directory.
   Copy the built Chrono libraries (`.dll` files), along with the csharp scripts 'core' and 'vehicle' into the `Assets/Plugins` directory of your Unity project.

   NOTE: The SWIG wrapping process generates duplicates of some types. Combine all the .cs scripts from the 'core' and 'vehicle' directories into a single directory, overwrite the duplicate .cs files.
    
   Open Unity Hub and add a new project from disk. Navigate to your Chrono Unity directory. Allow Unity to compile scripts and open a demo scene to test.

## Documentation

For detailed information on how to use ChronoUnity, please refer to the following resources:

- **Project Chrono Documentation:** [Project Chrono](https://projectchrono.org/)
- **SWIG Documentation:** [SWIG](http://www.swig.org/)
- **Unity Documentation:** [Unity](https://docs.unity3d.com/)

## License

See the main Chrono license for details [Chrono BSD Clause](https://github.com/projectchrono/chrono?tab=BSD-3-Clause-1-ov-file)
