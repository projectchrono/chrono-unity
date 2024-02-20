using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:: Script to add this to any terrainmanager parent object automatically

[DefaultExecutionOrder(900)]
public class TerrainWireframe : MonoBehaviour
{
    private RigidTerrain chronoRigidTerrain; // Reference to the RigidTerrain component

    public float gridSize = 25f; // The size of the grid (25m x 25m)
    public float interval = 0.5f; // The interval between points (0.5m)
    public ChVectorD vectorLoc;

    void Start()
    {
        // Get the RigidTerrain component from the same GameObject
        chronoRigidTerrain = UChRigidTerrainManager.chronoRigidTerrain;

        // Ensure the RigidTerrain component is attached
        if (chronoRigidTerrain == null)
        {
            Debug.LogError("RigidTerrain component not found on the GameObject.");
            return;
        }
    }
    
    void OnDrawGizmos()
    {
        // Ensure the RigidTerrain component is available
        if (chronoRigidTerrain == null)
            return;

        // Calculate the start and end positions based on the grid size
        float halfGridSize = gridSize / 2;
        Vector3 vehicleCentre = Utils.FromChrono(FindObjectOfType<UChVehicle>().GetChVehicle().GetPos()); // Center of the grid at the vehicle location
        Vector3 gridCentre = new Vector3(vehicleCentre.x, 0, -vehicleCentre.z);

        if (interval <= 0.0025f)
        {
            interval = 1.0f; // Do not permit a division by zero or too small a grid.
        }

        // Set the color of the Gizmos
        Gizmos.color = Color.green;

        // Create a variable to hold the height at each point
        float[,] heights = new float[(int)(gridSize / interval) + 1, (int)(gridSize / interval) + 1];

        // First pass: Query all heights
        for (float x = -halfGridSize; x <= halfGridSize; x += interval)
        {
            for (float z = -halfGridSize; z <= halfGridSize; z += interval)
            {
                int xi = (int)((x + halfGridSize) / interval);
                int zi = (int)((z + halfGridSize) / interval);

                // Query the terrain height at this point from the RigidTerrain object
                ChVectorD vectorLoc = new ChVectorD(x + gridCentre.x, 100, -(z + gridCentre.z)); // Note the -ve z to mirror to the RHF/LHF
                heights[xi, zi] = (float)chronoRigidTerrain.GetHeight(vectorLoc);
            }
        }

        // Second pass: Draw the wireframe
        for (int xi = 0; xi < (gridSize / interval); xi++)
        {
            for (int zi = 0; zi < (gridSize / interval); zi++)
            {
                // Current point
                Vector3 currentPos = new Vector3((xi * interval - halfGridSize), heights[xi, zi], 1*(zi * interval - halfGridSize)) + gridCentre; // flip the z by -1.. 

                // Draw line to the right neighbor if it exists
                if (xi < (gridSize / interval) - 1)
                {
                    Vector3 rightNeighborPos = new Vector3(((xi + 1) * interval - halfGridSize), heights[xi + 1, zi], 1*(zi * interval - halfGridSize)) + gridCentre;
                    Gizmos.DrawLine(currentPos, rightNeighborPos);
                }

                // Draw line to the top neighbor if it exists
                if (zi < (gridSize / interval) - 1)
                {
                    Vector3 topNeighborPos = new Vector3((xi * interval - halfGridSize), heights[xi, zi + 1], 1*((zi + 1) * interval - halfGridSize)) + gridCentre;
                    Gizmos.DrawLine(currentPos, topNeighborPos);
                }
            }
        }
    }

    





}
