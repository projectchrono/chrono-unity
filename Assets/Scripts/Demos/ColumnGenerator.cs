﻿// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Radu Serban, Josh Diyn
// =============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnGenerator : MonoBehaviour
{
    public Material columnMaterial;

    void CreateColumn(Object prefab,
                              Vector3 pos,
                              int num_edges,
                              float radius_hi,
                              float radius_lo,
                              float height,
                              float density)
    {

        // Create a new clone of the BodyConvexHull prefab
        // Likely to generate an error as no points are passed to it prior to the body Awake()
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Get the UChBodyConvexHull component
        var body = clone.GetComponent<UChBodyConvexHull>();
        if (body.GetChBody() != null)
            body.Destroy(); // remove existing body from chrono system, and avoid body duplication
                            
        body.density = density;
        body.pointSource = UChBodyConvexHull.PointSource.ManualPoints;
        
        // Set the convex hull points...
        for (int i = 0; i < num_edges; ++i)
        {
            float alpha = 2 * Mathf.PI * ((float)i / num_edges);
            float x = radius_hi * Mathf.Cos(alpha);
            float z = radius_hi * Mathf.Sin(alpha);
            float y = height;
            body.points.Add(new Vector3(x,y,z));
        }
        for (int i = 0; i < num_edges; ++i)
        {
            float alpha = 2 * Mathf.PI * ((float)i / num_edges);
            float x = radius_lo * Mathf.Cos(alpha);
            float z = radius_lo * Mathf.Sin(alpha);
            float y = 0;
            body.points.Add(new Vector3(x, y, z));
        }

        // Check number of points
        // Debug.Log("Number of points added to the new body " + body.points.Count);

        // Set the material for the underlying MeshRenderer
        if (columnMaterial != null)
        {
            clone.GetComponent<MeshRenderer>().material = columnMaterial;
        }
        
        // Set the position of the new clone
        clone.transform.position = pos;

        // generate the body
        body.InstanceCreation();
    }

    void Start()
    {
        Object prefab = Resources.Load("BodyConvexHull", typeof(GameObject));
        float spacing = 1.6f;
        float density = 3000.0f;

        for (int i = 0; i < 5; i++)
        {
            CreateColumn(prefab, new Vector3(i * spacing, 0.0f, 0), 10, 0.45f, 0.50f, 1.5f, density);
            CreateColumn(prefab, new Vector3(i * spacing, 1.5f, 0), 10, 0.40f, 0.45f, 1.5f, density);
            CreateColumn(prefab, new Vector3(i * spacing, 3.0f, 0), 10, 0.35f, 0.40f, 1.5f, density);
        }
    }
}
