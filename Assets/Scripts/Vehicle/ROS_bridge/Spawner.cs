using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : ICommandable {

    // public Rect windowRect = new Rect(20, 20, 500, 500);

    private List<string> scenes = new List<string>();
    private bool need_to_activate_new_scene;

    public override void OnStart() {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i) {
            var scene = SceneUtility.GetScenePathByBuildIndex(i);
            if (scene != "Assets/Simulation.unity")
                scenes.Add(scene);
        }
    }

    public int scene_index = -1;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        if (need_to_activate_new_scene) {
            Debug.Log("Setting " + scene.name + " active");
            SceneManager.SetActiveScene(scene);
            need_to_activate_new_scene = false;
        }
    }

    bool LoadScene(string scene_name) {
        // Only allow one scene to be loaded at a time
        //
        // Keep Simulation (always) and the just-added scene loaded
        //
        // Note: this is the current convention and might change in the
        // future if we need especially large environments where regions
        // of the environment are brough in and out of memory
        // dynamically.
        bool already_loaded = false;
        for (int i = 0; i < SceneManager.sceneCount; ++i) {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "Simulation" &&
               scene.name != scene_name) {
                SceneManager.UnloadSceneAsync(scene.name);
            }
            if (scene.name == scene_name)
                already_loaded = true;
        }

        if (!already_loaded) {
            SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
            need_to_activate_new_scene = true;
            SceneManager.sceneLoaded += OnSceneLoaded;

            scene_index = 0; // to remove scene selection gui
        }
        
        Resources.UnloadUnusedAssets(); // Is this necessary?

        return true;
    }

    bool LoadAssetBundle(string asset_bundle_path) {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(asset_bundle_path);
        if (myLoadedAssetBundle == null) {
            Debug.LogError("Failed to load AssetBundle from: " + asset_bundle_path);
            return false;
        }

        Debug.Log("Loaded asset bundle " + asset_bundle_path + ", contains:");

        string[] scenePaths = myLoadedAssetBundle.GetAllScenePaths();

        for (int i = 0; i < scenePaths.Length; ++i) {
            Debug.Log("  - Scene: " + scenePaths[i]);
            scenes.Add(scenePaths[i]);
        }

        return true;
    }

    bool Spawn(string[] command, string prefab_name, string object_name) {
        bool success = false;
        string temp = "";
        for (int i = 0; i < command.Length; i++) {
            temp += command[i] + ", ";
        }
        Debug.Log("SPAWN: [" + temp + "]");
        // If this is not an object with a "parent"
        if (command.Length < 11) {
            // Check if object_name already exists
            if (GameObject.Find(object_name)) {
                Debug.Log(object_name + " already exists, skipping spawn");
                return false;
            }
        }

        if (command.Length == 4) {
            GameObject new_obj = Instantiate(Resources.Load(prefab_name, typeof(GameObject))) as GameObject;
            new_obj.name = object_name;
            new_obj.tag = "Player";
            success = true;
        } else if (command.Length == 7) {
            float x = float.Parse(command[4]);
            float y = float.Parse(command[5]);
            float z = float.Parse(command[6]);

            GameObject new_obj = Instantiate(Resources.Load(prefab_name, typeof(GameObject)),
                                             new Vector3(x, z, y),
                                             Quaternion.identity) as GameObject;
            new_obj.name = object_name;
            new_obj.tag = "Player";
            success = true;
        } else if (command.Length == 10) {
            float x = float.Parse(command[4]);
            float y = float.Parse(command[5]);
            float z = float.Parse(command[6]);
            float rot_x = float.Parse(command[7]);
            float rot_y = float.Parse(command[8]);
            float rot_z = float.Parse(command[9]);
            GameObject new_obj = Instantiate(Resources.Load(prefab_name, typeof(GameObject)),
                                             new Vector3(x, z, y),
                                             Quaternion.Euler(rot_x, rot_y, rot_z)) as GameObject;
            new_obj.name = object_name;
            new_obj.tag = "Player";
            success = true;
        } else if (command.Length == 11) {
            float x = float.Parse(command[4]);
            float y = float.Parse(command[5]);
            float z = float.Parse(command[6]);
            float rot_x = float.Parse(command[7]);
            float rot_y = float.Parse(command[8]);
            float rot_z = float.Parse(command[9]);
            string parent_name = command[10];

            Transform parent_obj = GameObject.Find(parent_name).transform;

            // Check if object_name already exists
            if (GameObject.Find(parent_name + "/" + object_name)) {
                Debug.Log(parent_name + "/" + object_name + " already exists, skipping spawn");
                return false;
            }
            Debug.Log("Rotation y: " + rot_y);
            GameObject new_obj = Instantiate(Resources.Load(prefab_name, typeof(GameObject)),
                                             parent_obj.TransformPoint(new Vector3(x, z, y)),
                                             parent_obj.rotation * Quaternion.Euler(rot_x, rot_y, rot_z),
                                             parent_obj) as GameObject;

            new_obj.name = object_name;

            success = true;
        } else {
            Debug.Log("Got spawn command with wrong number of arguments (" + command.Length + ")");
        }
        return success;
    }

    public override bool OnCommand(string[] command)
    {
        string command_name = command[1];
        Debug.Log("Spawner got command: " + command_name);
        if (command_name == "spawn")
        {
            string prefab_name = command[2];
            GameObject prefab_obj = Resources.Load(prefab_name, typeof(GameObject)) as GameObject;
            if (prefab_obj == null)
            {
                Debug.Log("Could not find: [" + prefab_name + "]");
                return false;
            }
            string object_name = command[3];
            return Spawn(command, prefab_name, object_name);
        }
        else if (command_name == "environment")
        {
            string environment_name = command[2];
            Debug.Log("Spawner looking for " + environment_name);

            Object[] game_objects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in game_objects)
            {
                if (obj.name == environment_name)
                {
                    Debug.Log("Spawner enabling " + obj.name);
                    obj.SetActive(true);
                    Thread.Sleep(1000); //give the environment some time to spawn
                }
                else
                {
                    if (obj.tag == "Environment")
                    {
                        Debug.Log("Spawner disabling " + obj.name);
                        obj.SetActive(false);
                    }
                }
            }
        }
        else if (command_name == "scene")
        {
            string scene_name = command[2];
            Debug.Log("Spawner looking for scene: " + scene_name);

            return LoadScene(scene_name);
        }
        else if (command_name == "load")
        {
            if (command.Length != 3)
            {
                Debug.LogError("Invalid usage! Should be: Spawner load <asset bundle path>");
                return false;
            }
            string asset_bundle_path = command[2];

            LoadAssetBundle(asset_bundle_path);
        }
        else
        {
            Debug.Log("Spawner didn't understand: [" + command_name + "]");
        }

        return true;
    }
}
