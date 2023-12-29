using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(50)] // After ChBody, prior to links, motors, etc.
public class BallGenerator : MonoBehaviour
{
    public Material ballMaterial;
    public bool drawBallFrames = false;

    void CreateBall(Object prefab,
                    Vector3 pos,
                    float radius,
                    float density)
    {
        // Create a new clone of the BodyConvexHull prefab
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Get the UChBodyConvexHull component and set its properties...
        var body = clone.GetComponent<UChBodySphere>();
        body.radius = radius;
        body.density = density;
        body.showFrameGizmo = drawBallFrames;

        // Set the material for the underlying MeshRenderer
        if (ballMaterial != null)
        {
            clone.GetComponent<MeshRenderer>().material = ballMaterial;
        }

        // Set the position and scale of the new clone...
        clone.transform.position = pos;
        clone.transform.localScale = new Vector3(2 * radius, 2 * radius, 2 * radius);

        body.InstanceCreation();
    }

    void Start()
    {
        Object prefab = Resources.Load("BodySphere", typeof(GameObject));

        float radius = 0.8f;
        float density = 1000.0f;

        for (int i = 0; i < 250; i++)
        {
            // Set random range with diameter as a minimum so as not to generate with pre-existing penetrations (slows the start of the sim down)
            Vector3 pos = new Vector3(-5 + Random.Range(2 * radius, 10), 4 + i * 0.05f, -5 + Random.Range(2 * radius, 10));
            CreateBall(prefab, pos, radius, density);
        }
    }
}
