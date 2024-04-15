// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution and at
// http://projectchrono.org/license-chrono.txt.
//
// =============================================================================
// Authors: Radu Serban, Josh Diyn
// =============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public Material ballMaterial;
    public bool drawBallFrames = false;

    void CreateBall(Object prefab,
                    Vector3 pos,
                    float radius,
                    float density)
    {
        // Create a new clone of the BodyConvexHull prefab, setting the position and scale
        GameObject clone = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        clone.transform.localScale = new Vector3(2 * radius, 2 * radius, 2 * radius);
        
        // Get the UChBodyConvexHull component and set its properties...
        var body = clone.GetComponent<UChBodySphere>();
        body.Destroy(); // remove existing body from chrono system, and avoid body duplication
        body.radius = radius;
        body.density = density;
        body.showFrameGizmo = drawBallFrames;

        // Set the material for the underlying MeshRenderer
        if (ballMaterial != null)
        {
            clone.GetComponent<MeshRenderer>().material = ballMaterial;
        }
        body.InstanceCreation(); // generate resized ball for chrono system
    }

    void Start()
    {
        Object prefab = Resources.Load("BodySphere", typeof(GameObject));

        // Randomly generate balls, but use a list with a check to ensure no overlaps
        List<Vector3> existingPositions = new List<Vector3>();
        float radius = 0.8f;
        float density = 1000.0f;
        float generatorSize = 12f;
        float spacing = 0.01f;
        float effectiveDiameter = 2 * radius + spacing;
        int maximumHeightBalls = (int)(generatorSize / effectiveDiameter) + 5; // Maximum number of balls that can be stacked vertically (add 5m buffer)

        for (int i = 0; i < 250; i++)
        {
            Vector3 pos;
            bool validPosition;
            do
            {
                pos = new Vector3(
                    Random.Range(-generatorSize / 2 + radius, generatorSize / 2 - radius),
                    2 + Random.Range(0, maximumHeightBalls * effectiveDiameter), // 2 is the starting height
                    Random.Range(-generatorSize / 2 + radius, generatorSize / 2 - radius)
                );
                validPosition = true;
                foreach (Vector3 existing in existingPositions)
                {
                    if (Vector3.Distance(pos, existing) < effectiveDiameter)
                    {
                        validPosition = false;
                        break;
                    }
                }
            } while (!validPosition);
            // add to the list and create the ball at that position
            existingPositions.Add(pos);
            CreateBall(prefab, pos, radius, density);
        }
        var count = UChSystem.chrono_system.GetBodies().Count;
        Debug.Log("Chrono body count "  + count);
    }
}
