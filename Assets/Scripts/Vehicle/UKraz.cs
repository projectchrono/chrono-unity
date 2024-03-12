using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UKraz : UChVehicle 
{
    public Kraz kraz;

    public enum UTireModelType
    {
        TMEASY = TireModelType.TMEASY,
        RIGID = TireModelType.RIGID,
        RIGID_MESH = TireModelType.RIGID_MESH
    }

    public enum UBrakeType
    {
        SIMPLE = BrakeType.SIMPLE,
        SHAFTS = BrakeType.SHAFTS
    }

    public bool chassisFixed;
    public ChTire.CollisionType tireCollisionType;

    public UBrakeType brakeType;
    public bool brakeLocking;

    public double initForwardVel;
    public double initWheelAngSpeed;

    public Material chassisMaterial;

    private GameObject chassis;       // This will refer to the main body of the tractor
    private GameObject trailer;       // This will refer to the main body of the trailer


    private GameObject wheel_FL;      // Tractor Front Left Wheel: "Wheel_FL"
    private GameObject wheel_FR;      // Tractor Front Right Wheel: "Wheel_FR"
    private GameObject wheel_RL1i;    // Tractor Rear Left Inner Wheel on First Axle: "Wheel_RL1i"
    private GameObject wheel_RR1i;    // Tractor Rear Right Inner Wheel on First Axle: "Wheel_RR1i"
    private GameObject wheel_RL1o;    // Tractor Rear Left Outer Wheel on First Axle: "Wheel_RL1o"
    private GameObject wheel_RR1o;    // Tractor Rear Right Outer Wheel on First Axle: "Wheel_RR1o"

    private GameObject wheel_RL2i;    // Tractor Rear Left Inner Wheel on Second Axle (if present)
    private GameObject wheel_RR2i;    // Tractor Rear Right Inner Wheel on Second Axle (if present)
    private GameObject wheel_RL2o;    // Tractor Rear Left Outer Wheel on Second Axle (if present)
    private GameObject wheel_RR2o;    // Tractor Rear Right Outer Wheel on Second Axle (if present)

    // Trailer wheels
    private GameObject wheelTrailerFL;  // Trailer Front Left Wheel: "Wheel_FL"
    private GameObject wheelTrailerFR;  // Trailer Front Right Wheel: "Wheel_FR"
    private GameObject wheelTrailerML;  // Trailer Middle Left Wheel: "Wheel_ML"
    private GameObject wheelTrailerMR;  // Trailer Middle Right Wheel: "Wheel_MR"
    private GameObject wheelTrailerRL;  // Trailer Rear Left Wheel: "Wheel_RL"
    private GameObject wheelTrailerRR;  // Trailer Rear Right Wheel: "Wheel_RR"

    private Quaternion wheelTrailerFLInitial;


    public UKraz()
    {
        chassisFixed = false;
        brakeLocking = false;
        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    protected override void OnStart()
    {
        // Create and configure the Kraz truck
        kraz = new Kraz(UChSystem.chrono_system);

        kraz.SetChassisFixed(chassisFixed);
        kraz.SetTireStepSize(Time.fixedDeltaTime);
        //kraz.GetTractor().EnableBrakeLocking(brakeLocking);


        ////Vector3 pos = transform.position;
        ////Quaternion quat = transform.rotation;
        ////Debug.Log("quat = " + quat.w + " " + quat.x + " " + quat.y + " " + quat.z);
        var csys = new ChCoordsysd(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        kraz.SetInitPosition(csys);

        kraz.SetInitFwdVel(initForwardVel);
        kraz.Initialize();

        Debug.Log("Kraz tractor mass: " + kraz.GetTractor().GetMass());
        
        foreach (Transform truck_unit in transform)
        {
            // Tractor wheels
            if (truck_unit.name == "KrazTractor")
            {
                foreach (Transform child in truck_unit)
                {
                    if (child.name == "Tractor")
                        chassis = child.gameObject;
                    else if (child.name == "Steer1Left")
                        wheel_FL = child.gameObject;  // GameObject for Front Left Wheel
                    else if (child.name == "Steer1Right")
                        wheel_FR = child.gameObject;  // GameObject for Front Right Wheel
                    else if (child.name == "Drive1LeftO")
                        wheel_RL1o = child.gameObject; // GameObject for Rear Left Inner Wheel on First Axle
                    else if (child.name == "Drive1RightO")
                        wheel_RR1o = child.gameObject; // GameObject for Rear Right Inner Wheel on First Axle
                    else if (child.name == "Drive2LeftO")
                        wheel_RL2o = child.gameObject; // GameObject for Rear Left Outer Wheel on First Axle
                    else if (child.name == "Drive2RightO")
                        wheel_RR2o = child.gameObject; // GameObject for Rear Right Outer Wheel on First Axle
                    else if (child.name == "Drive1LeftI")
                        wheel_RL1i = child.gameObject; // GameObject for Rear Left Inner Wheel on First Axle
                    else if (child.name == "Drive1RightI")
                        wheel_RR1i = child.gameObject; // GameObject for Rear Right Inner Wheel on First Axle
                    else if (child.name == "Drive2LeftI")
                        wheel_RL2i = child.gameObject; // GameObject for Rear Left Outer Wheel on First Axle
                    else if (child.name == "Drive2RightI")
                        wheel_RR2i = child.gameObject; // GameObject for Rear Right Outer Wheel on First Axle


                }
            }
            // Trailer wheels
            else if (truck_unit.name == "Trailer")
            {
                foreach (Transform child in truck_unit)
                {
                    if (child.name == "TrailerBody")
                        trailer = child.gameObject;  // GameObject for Trailer Front Left Wheel
                    else if (child.name == "Trailer1Left")
                        wheelTrailerFL = child.gameObject;  // GameObject for Trailer Front Left Wheel
                    else if (child.name == "Trailer1Right")
                        wheelTrailerFR = child.gameObject;  // GameObject for Trailer Front Right Wheel
                    else if (child.name == "Trailer2Left")
                        wheelTrailerML = child.gameObject;  // GameObject for Trailer Middle Left Wheel
                    else if (child.name == "Trailer2Right")
                        wheelTrailerMR = child.gameObject;  // GameObject for Trailer Middle Right Wheel
                    else if (child.name == "Trailer3Left")
                        wheelTrailerRL = child.gameObject;  // GameObject for Trailer Rear Left Wheel
                    else if (child.name == "Trailer3Right")
                        wheelTrailerRR = child.gameObject;  // GameObject for Trailer Rear Right Wheel
                }
            }
        }

        wheelTrailerFLInitial = wheelTrailerFL.transform.rotation;  //store initial rotation


    }

    protected override void OnAdvance(double step)
    {
        // Update tractor position and rotation
        var tractor_pos = Utils.FromChronoFlip(kraz.GetTractor().GetPos());
        var tractor_rot = Utils.FromChronoFlip(kraz.GetTractor().GetRot());

        // Position and rotation of the gameobject
        transform.position = tractor_pos;
        transform.rotation = tractor_rot;

        chassis.transform.position = tractor_pos;
        chassis.transform.rotation = tractor_rot;

        // Update trailer position and rotation
        var trailer_pos = kraz.GetTrailer().GetChassis().GetPos();
        var trailer_rot = kraz.GetTrailer().GetChassis().GetRot();
        trailer.transform.position = Utils.FromChronoFlip(trailer_pos);
        trailer.transform.rotation = Utils.FromChronoFlip(trailer_rot);

        // Update tractor wheel positions and rotations
        wheel_FL.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(0).m_wheels[0].GetPos());
        wheel_FL.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(0).m_wheels[0].GetSpindle().GetRot());
        wheel_FR.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(0).m_wheels[1].GetPos());
        wheel_FR.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(0).m_wheels[1].GetSpindle().GetRot());

        wheel_RL1i.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[0].GetPos());
        wheel_RL1i.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[0].GetSpindle().GetRot());
        wheel_RR1i.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[1].GetPos());
        wheel_RR1i.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[1].GetSpindle().GetRot());
        wheel_RL2i.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[0].GetPos());
        wheel_RL2i.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[0].GetSpindle().GetRot());
        wheel_RR2i.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[1].GetPos());
        wheel_RR2i.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[1].GetSpindle().GetRot());

        wheel_RL1o.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[2].GetPos());
        wheel_RL1o.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[2].GetSpindle().GetRot());
        wheel_RR1o.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[3].GetPos());
        wheel_RR1o.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(1).m_wheels[3].GetSpindle().GetRot());
        wheel_RL2o.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[2].GetPos());
        wheel_RL2o.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[2].GetSpindle().GetRot());
        wheel_RR2o.transform.position = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[3].GetPos());
        wheel_RR2o.transform.rotation = Utils.FromChronoFlip(kraz.GetTractor().GetAxle(2).m_wheels[3].GetSpindle().GetRot());

        // Update trailer wheel positions and rotations
        wheelTrailerFL.transform.position = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(0).GetWheel(VehicleSide.LEFT).GetPos());
        wheelTrailerFL.transform.rotation = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(0).GetWheel(VehicleSide.LEFT).GetSpindle().GetRot());

        wheelTrailerFR.transform.position = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(0).GetWheel(VehicleSide.RIGHT).GetPos());
        wheelTrailerFR.transform.rotation = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(0).GetWheel(VehicleSide.RIGHT).GetSpindle().GetRot());

        wheelTrailerML.transform.position = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(1).GetWheel(VehicleSide.LEFT).GetPos());
        wheelTrailerML.transform.rotation = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(1).GetWheel(VehicleSide.LEFT).GetSpindle().GetRot());

        wheelTrailerMR.transform.position = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(1).GetWheel(VehicleSide.RIGHT).GetPos());
        wheelTrailerMR.transform.rotation = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(1).GetWheel(VehicleSide.RIGHT).GetSpindle().GetRot());

        wheelTrailerRL.transform.position = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(2).GetWheel(VehicleSide.LEFT).GetPos());
        wheelTrailerRL.transform.rotation = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(2).GetWheel(VehicleSide.LEFT).GetSpindle().GetRot());

        wheelTrailerRR.transform.position = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(2).GetWheel(VehicleSide.RIGHT).GetPos());
        wheelTrailerRR.transform.rotation = Utils.FromChronoFlip(kraz.GetTrailer().GetAxle(2).GetWheel(VehicleSide.RIGHT).GetSpindle().GetRot());

        // Synchronize and advance the simulation
        kraz.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, chTerrain);
        kraz.Advance(step);
    }


    public override double GetMaxSpeed()
    {
        return 25.0; // 90 km/h
    }
    
    public override ChVehicle GetChVehicle()
    {
        return kraz.GetTractor();
    }
    

}