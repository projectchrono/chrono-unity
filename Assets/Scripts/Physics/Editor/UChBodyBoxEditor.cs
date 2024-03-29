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
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyBox))]
public class UChBodyBoxEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyBox body = (UChBodyBox)target;

        //// TODO: Add this dependency to the other UChBodies
        // Check if UChMaterialSurface component is attached to the object, since creation of a chbodybox requires it
        UChMaterialSurface matSurface = body.GetComponent<UChMaterialSurface>();
        if (matSurface == null)
        {
            // If not, add the UChMaterialSurface component to the object
            matSurface = body.gameObject.AddComponent<UChMaterialSurface>();
        }

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
