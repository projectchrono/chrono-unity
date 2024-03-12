using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

// Physics items will cause Unity to crash if trying to add bodies to the system before any of those
// have intialised first. Therefore, force the execution to be after these (default execution order is '0')
[DefaultExecutionOrder(100)]    
public class UChMotorLinear : UChMotor
{    
    private ChLinkMotorLinear motor;

    private ChLinkMotorLinear Create(MotorType type)
    {
        switch (type)
        {
            case MotorType.STATE:
            default:
                var motorP = new ChLinkMotorLinearPosition();
                return motorP;
            case MotorType.SPEED:
                var motorS = new ChLinkMotorLinearSpeed();
                return motorS;
            case MotorType.FORCE:
                var motorF = new ChLinkMotorLinearForce();
                return motorF;
        }
    }

    void Start()
    {
        motor = Create(type);
        ChFramed frame = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        motor.Initialize(body1.GetChBody(), body2.GetChBody(), frame);

        // Get a handle to the associated function component and set the motor's function
        var fun_component = this.GetComponent<UChFunction>();
        motor.SetMotorFunction(fun_component.GetChFunction());

        UChSystem.chrono_system.AddLink(motor);
    }

    void Update()
    {
        var csys = motor.GetFrameAbs();
        transform.position = Utils.FromChrono(csys.GetPos()); // overhaul changes from pos and rot
        transform.rotation = Utils.FromChrono(csys.GetRot());
    }
}
