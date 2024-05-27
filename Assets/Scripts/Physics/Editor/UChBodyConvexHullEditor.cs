// =============================================================================
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

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyConvexHull))]
public class UChBodyConvexHullEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {


        UChBodyConvexHull body = (UChBodyConvexHull)target;

        // Check if UChMaterialSurface component is attached to the object, assign one if not
        UChMaterialSurface matSurface = body.GetComponent<UChMaterialSurface>();
        if (matSurface == null)
        {
            // If not, add the UChMaterialSurface component to the object
            matSurface = body.gameObject.AddComponent<UChMaterialSurface>();
        }


        // standard GUI
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Create a dropdown for selecting the point source
        body.pointSource = (UChBodyConvexHull.PointSource)EditorGUILayout.EnumPopup("Point Source", body.pointSource);

        // If the manual points option is selected, show the existing point input fields
        if (body.pointSource == UChBodyConvexHull.PointSource.ManualPoints)
        {

            int size = 0;
            size = EditorGUILayout.DelayedIntField("Number of Points", body.points.Count);
            while (size < body.points.Count)
                body.points.RemoveAt(body.points.Count - 1);
            while (size > body.points.Count)
                body.points.Add(new Vector3());

            for (int i = 0; i < size; i++)
            {
                body.points[i] = EditorGUILayout.Vector3Field("Point " + (i + 1), body.points[i]);
            }

        }
        else if (body.pointSource == UChBodyConvexHull.PointSource.MeshFilter)
        {
            // Fetch the MeshFilter component from the GameObject
            MeshFilter meshFilter = body.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                // Display the number of vertices
                int vertexCount = meshFilter.sharedMesh.vertexCount;
                EditorGUILayout.LabelField("Number of Vertices: " + vertexCount);
            }
            else
            {
                EditorGUILayout.HelpBox("No MeshFilter with a mesh found on the GameObject.", MessageType.Warning);
            }
        }

        // display settings
        body.pointGizmoRadius = EditorGUILayout.FloatField("Point Radius Gizmo", body.pointGizmoRadius);
        body.showCollisionShape = EditorGUILayout.Toggle("Show Collision Shape", body.showCollisionShape);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
