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
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

//// TODO: take into account object scale!!!

[CustomEditor(typeof(UChBodyMesh))]
public class UChBodyMeshEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyMesh body = (UChBodyMesh)target;

        // Check if UChMaterialSurface component is attached to the object, assign one if not
        UChMaterialSurface matSurface = body.GetComponent<UChMaterialSurface>();
        if (matSurface == null)
        {
            // If not, add the UChMaterialSurface component to the object
            matSurface = body.gameObject.AddComponent<UChMaterialSurface>();
        }

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        body.collisionMesh = EditorGUILayout.Toggle("OBJ File", body.collisionMesh);

        if (body.collisionMesh)
            body.collisionMeshOBJFile = EditorGUILayout.TextField("File Name", body.collisionMeshOBJFile);

        body.sweptSphereRadius = EditorGUILayout.DoubleField("Swept Sphere Radius", body.sweptSphereRadius);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
