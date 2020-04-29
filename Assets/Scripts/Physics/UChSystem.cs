using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChSystem : MonoBehaviour
{
    // ATTENTION: The global Chrono system is set in UChSystem.Awake.
    // Since Unity does not enforce an order of calls to Awake,
    // this can be safely used by other components only in their Start function.

    public static ChSystem chrono_system;

    public ContactMethod contact_method;

    public Vector3 gravity;
    public double step;

    public UChSystem()
    {
        gravity = new Vector3(0, -9.8f, 0);
        step = 1e-2;
    }

    void Awake()
    {
        switch (contact_method)
        {
            case ContactMethod.NSC:
                chrono_system = new ChSystemNSC();
                break;
            case ContactMethod.SMC:
                chrono_system = new ChSystemSMC();
                break;
        }

        chrono_system.Set_G_acc(new ChVectorD(gravity.x, gravity.y, gravity.z));
        chrono_system.SetStep(step);
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Time.fixedDeltaTime = (float)step;

        ////Debug.Log("sys Time = " + chrono_system.GetChTime() + "     num bodies: " + chrono_system.GetNbodies());
        chrono_system.DoStepDynamics(step);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}
