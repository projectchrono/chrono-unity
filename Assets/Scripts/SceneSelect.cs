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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public static class SceneSelectData
{
    public static bool launchedFromManager = false;
    public static string mainScene;
}

public class SceneSelect : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneSelectData.mainScene = SceneManager.GetActiveScene().name;
        SceneSelectData.launchedFromManager = true;
        SceneManager.LoadScene(sceneName);
        DynamicGI.UpdateEnvironment(); // try to force a lighting update
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == SceneSelectData.mainScene)
            {
                Application.Quit();
                #if UNITY_EDITOR
                    // If using the editor, use a fake quit
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }

            if (SceneSelectData.launchedFromManager)
            {
                SceneSelectData.launchedFromManager = false;
                SceneManager.LoadScene(SceneSelectData.mainScene);
            }



        }
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

}
