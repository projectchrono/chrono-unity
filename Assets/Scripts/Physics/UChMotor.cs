using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MotorType
{
    STATE = 0,
    SPEED = 1,
    FORCE = 2
}

public class UChMotor : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public MotorType type;
}
