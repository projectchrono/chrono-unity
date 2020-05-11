using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UHMMWV : MonoBehaviour
{
    public HMMWV_Full hmmwv;

    public bool chassisFixed;
    public PowertrainModelType powertrainModel;
    public DrivelineType drivelineModel;
    public TireModelType tireModel;
    public ChTire.CollisionType tireCollisionType;

    public double initForwardVel;
    public double initWheelAngSpeed;

    public Material chassisMaterial;

    private GameObject chassis;
    private GameObject wheelFL;
    private GameObject wheelFR;
    private GameObject wheelRL;
    private GameObject wheelRR;

    private ChTerrain terrain;

    public UHMMWV()
    {
        ChWorldFrame.SetYUP();
        //ChWorldFrame.Set(new ChMatrix33D(1));

        chassisFixed = false;
        powertrainModel = PowertrainModelType.SIMPLE;
        drivelineModel = DrivelineType.RWD;
        tireModel = TireModelType.TMEASY;
        tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    // Transform the given frame, expressed in the current *right-handed* Chrono World Frame,
    // into a frame expressed in the *left-handed* Unity World Frame.
    // 'vehicle_frame' is the frame of the vehicle (chassis) expressed in the Chrono World Frame.
    private void ConvertToUnityFrame(ChFrameMovingD vehicle_frame, ChFrameD frame)
    {
        var frame_local = new ChFrameD();
        vehicle_frame.TransformParentToLocal(frame, frame_local);
        frame_local.GetPos().y *= -1;
        frame_local.SetRot(Utils.ISOtoLHF(frame_local.GetRot()));
        vehicle_frame.TransformLocalToParent(frame_local, frame);
    }

    private void ConvertToUnityFrame(ChFrameMovingD vehicle_frame, ref ChVectorD pos, ref ChQuaternionD rot)
    {
        var frame = new ChFrameD(pos, rot);
        var frame_local = new ChFrameD();
        vehicle_frame.TransformParentToLocal(frame, frame_local);
        frame_local.GetPos().y *= -1;
        frame_local.SetRot(Utils.ISOtoLHF(frame_local.GetRot()));
        vehicle_frame.TransformLocalToParent(frame_local, frame);
        pos = frame.GetPos();
        rot = frame.GetRot();
    }

    void Start()
    {
        hmmwv = new HMMWV_Full(UChSystem.chrono_system);

        hmmwv.SetChassisFixed(chassisFixed);
        hmmwv.SetPowertrainType(powertrainModel);
        hmmwv.SetDriveType(drivelineModel);
        hmmwv.SetTireType(tireModel);
        hmmwv.SetTireCollisionType(tireCollisionType);
        
        hmmwv.SetInitPosition(new ChCoordsysD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation)));
        hmmwv.SetInitFwdVel(initForwardVel);
        vector_double omega = new vector_double();
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        hmmwv.SetInitWheelAngVel(omega);

        hmmwv.Initialize();

        // Hide the editing child object and enable the run-time components
        ////var listOfChildren = GetComponentsInChildren<Renderer>();
        ////listOfChildren[0].enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;

        Object chassis_prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Vehicle/HMMWV/Chassis.prefab", typeof(GameObject));
        chassis = Instantiate(chassis_prefab, transform) as GameObject;
        chassis.transform.parent = gameObject.transform;
        if (chassisMaterial != null)
            chassis.GetComponentInChildren<Renderer>().sharedMaterial = chassisMaterial;

        Object wheelL_prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Vehicle/HMMWV/WheelLeft.prefab", typeof(GameObject));
        wheelFL = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelFL.transform.parent = gameObject.transform;
        wheelRL = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelRL.transform.parent = gameObject.transform;

        Object wheelR_prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Vehicle/HMMWV/WheelRight.prefab", typeof(GameObject));
        wheelFR = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelFR.transform.parent = gameObject.transform;
        wheelRR = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelRR.transform.parent = gameObject.transform;
    }

    void FixedUpdate()
    {
        var vehicle_frame = hmmwv.GetChassisBody().GetFrame_REF_to_abs();
        var vehicle_pos = vehicle_frame.GetPos();
        var vehicle_rot = vehicle_frame.GetRot();

        var spindleFL_pos = hmmwv.GetVehicle().GetSpindlePos(0, VehicleSide.LEFT);
        var spindleFL_rot = hmmwv.GetVehicle().GetSpindleRot(0, VehicleSide.LEFT);
        //var spindleFL_frame = new ChFrameD(spindleFL_pos, spindleFL_rot);
        //ConvertToUnityFrame(vehicle_frame, spindleFL_frame);
        //spindleFL_pos = spindleFL_frame.GetPos();
        //spindleFL_rot = spindleFL_frame.GetRot();
        ConvertToUnityFrame(vehicle_frame, ref spindleFL_pos, ref spindleFL_rot);

        var spindleFR_pos = hmmwv.GetVehicle().GetSpindlePos(0, VehicleSide.RIGHT);
        var spindleFR_rot = hmmwv.GetVehicle().GetSpindleRot(0, VehicleSide.RIGHT);
        ConvertToUnityFrame(vehicle_frame, ref spindleFR_pos, ref spindleFR_rot);

        var spindleRL_pos = hmmwv.GetVehicle().GetSpindlePos(1, VehicleSide.LEFT);
        var spindleRL_rot = hmmwv.GetVehicle().GetSpindleRot(1, VehicleSide.LEFT);
        ConvertToUnityFrame(vehicle_frame, ref spindleRL_pos, ref spindleRL_rot);

        var spindleRR_pos = hmmwv.GetVehicle().GetSpindlePos(1, VehicleSide.RIGHT);
        var spindleRR_rot = hmmwv.GetVehicle().GetSpindleRot(1, VehicleSide.RIGHT);
        ConvertToUnityFrame(vehicle_frame, ref spindleRR_pos, ref spindleRR_rot);

        float horiz = Input.GetAxis("Horizontal");

        //Debug.Log("terrain height = " + UTerrain.chrono_terrain.GetHeight(new ChVectorD(0,0,0)));

        DriverInputs bar = new DriverInputs();
        bar.m_braking = 0;
        bar.m_steering = horiz;
        bar.m_throttle = 0.1;
        hmmwv.Synchronize(UChSystem.chrono_system.GetChTime(), bar, UTerrain.chrono_terrain);
        hmmwv.Advance(0.03);


        // Set position of the vehicle GameObject and of all moving parts

        var veh_pos = Utils.FromChrono(vehicle_pos);
        var veh_rot = Utils.FromChrono(vehicle_rot);

        //Debug.Log("vehicle pos:  " + veh_pos.ToString("F5"));

        transform.position = veh_pos;
        transform.rotation = veh_rot;

        chassis.transform.position = veh_pos;
        chassis.transform.rotation = veh_rot;

        wheelFL.transform.position = Utils.FromChrono(spindleFL_pos);
        wheelFL.transform.rotation = Utils.FromChrono(spindleFL_rot);

        wheelFR.transform.position = Utils.FromChrono(spindleFR_pos);
        wheelFR.transform.rotation = Utils.FromChrono(spindleFR_rot);

        wheelRL.transform.position = Utils.FromChrono(spindleRL_pos);
        wheelRL.transform.rotation = Utils.FromChrono(spindleRL_rot);

        wheelRR.transform.position = Utils.FromChrono(spindleRR_pos);
        wheelRR.transform.rotation = Utils.FromChrono(spindleRR_rot);
    }
}
