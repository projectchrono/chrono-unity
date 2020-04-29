using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        ChFrameD frame = new ChFrameD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        motor.Initialize(body1.GetChBody(), body2.GetChBody(), frame);

        // Get a handle to the associated function component and set the motor's function
        var fun_component = this.GetComponent<UChFunction>();
        motor.SetMotorFunction(fun_component.GetChFunction());

        UChSystem.chrono_system.AddLink(motor);
    }

    void FixedUpdate()
    {
        var csys = motor.GetLinkAbsoluteCoords();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);
    }
}
