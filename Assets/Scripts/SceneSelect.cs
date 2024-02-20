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
