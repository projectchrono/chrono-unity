//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChTrackAssembly : ChPart {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChTrackAssembly(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChTrackAssembly_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChTrackAssembly obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChTrackAssembly(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public VehicleSide GetVehicleSide() {
    VehicleSide ret = (VehicleSide)vehiclePINVOKE.ChTrackAssembly_GetVehicleSide(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetNumRoadWheelAssemblies() {
    uint ret = vehiclePINVOKE.ChTrackAssembly_GetNumRoadWheelAssemblies(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetNumRollers() {
    uint ret = vehiclePINVOKE.ChTrackAssembly_GetNumRollers(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual uint GetNumTrackShoes() {
    uint ret = vehiclePINVOKE.ChTrackAssembly_GetNumTrackShoes(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChSprocket GetSprocket() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackAssembly_GetSprocket(swigCPtr);
    ChSprocket ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSprocket(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChIdler GetIdler() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackAssembly_GetIdler(swigCPtr);
    ChIdler ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChIdler(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChTrackBrake GetBrake() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackAssembly_GetBrake(swigCPtr);
    ChTrackBrake ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChTrackBrake(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__vehicle__ChRoadWheelAssembly_t_t GetRoadWheelAssemblies() {
    SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__vehicle__ChRoadWheelAssembly_t_t ret = new SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__vehicle__ChRoadWheelAssembly_t_t(vehiclePINVOKE.ChTrackAssembly_GetRoadWheelAssemblies(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChRoadWheelAssembly GetRoadWheelAssembly(uint id) {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackAssembly_GetRoadWheelAssembly(swigCPtr, id);
    ChRoadWheelAssembly ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRoadWheelAssembly(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__shared_ptrT_ChRoller_t GetRoller(uint id) {
    SWIGTYPE_p_std__shared_ptrT_ChRoller_t ret = new SWIGTYPE_p_std__shared_ptrT_ChRoller_t(vehiclePINVOKE.ChTrackAssembly_GetRoller(swigCPtr, id), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChRoadWheel GetRoadWheel(uint id) {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackAssembly_GetRoadWheel(swigCPtr, id);
    ChRoadWheel ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRoadWheel(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChTrackShoe GetTrackShoe(uint id) {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackAssembly_GetTrackShoe(swigCPtr, id);
    ChTrackShoe ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChTrackShoe(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetTrackShoePos(uint id) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetTrackShoePos(swigCPtr, id), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetTrackShoeRot(uint id) {
    ChQuaternionD ret = new ChQuaternionD(vehiclePINVOKE.ChTrackAssembly_GetTrackShoeRot(swigCPtr, id), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetTrackShoeLinVel(uint id) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetTrackShoeLinVel(swigCPtr, id), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetTrackShoeAngVel(uint id) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetTrackShoeAngVel(swigCPtr, id), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public BodyState GetTrackShoeState(uint id) {
    BodyState ret = new BodyState(vehiclePINVOKE.ChTrackAssembly_GetTrackShoeState(swigCPtr, id), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void GetTrackShoeStates(SWIGTYPE_p_std__vectorT_chrono__vehicle__BodyState_t states) {
    vehiclePINVOKE.ChTrackAssembly_GetTrackShoeStates(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__BodyState_t.getCPtr(states));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetMass() {
    double ret = vehiclePINVOKE.ChTrackAssembly_GetMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetSprocketLocation() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetSprocketLocation(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetIdlerLocation() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetIdlerLocation(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetRoadWhelAssemblyLocation(int which) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetRoadWhelAssemblyLocation(swigCPtr, which), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetRollerLocation(int which) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackAssembly_GetRollerLocation(swigCPtr, which), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Initialize(ChChassis chassis, ChVectorD location, bool create_shoes) {
    vehiclePINVOKE.ChTrackAssembly_Initialize__SWIG_0(swigCPtr, ChChassis.getCPtr(chassis), ChVectorD.getCPtr(location), create_shoes);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Initialize(ChChassis chassis, ChVectorD location) {
    vehiclePINVOKE.ChTrackAssembly_Initialize__SWIG_1(swigCPtr, ChChassis.getCPtr(chassis), ChVectorD.getCPtr(location));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSprocketVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.ChTrackAssembly_SetSprocketVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetIdlerVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.ChTrackAssembly_SetIdlerVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRoadWheelAssemblyVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.ChTrackAssembly_SetRoadWheelAssemblyVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRoadWheelVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.ChTrackAssembly_SetRoadWheelVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRollerVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.ChTrackAssembly_SetRollerVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTrackShoeVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.ChTrackAssembly_SetTrackShoeVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWheelCollisionType(bool roadwheel_as_cylinder, bool idler_as_cylinder, bool roller_as_cylinder) {
    vehiclePINVOKE.ChTrackAssembly_SetWheelCollisionType(swigCPtr, roadwheel_as_cylinder, idler_as_cylinder, roller_as_cylinder);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Synchronize(double time, double braking, TerrainForces shoe_forces) {
    vehiclePINVOKE.ChTrackAssembly_Synchronize(swigCPtr, time, braking, TerrainForces.getCPtr(shoe_forces));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void SetOutput(bool state) {
    vehiclePINVOKE.ChTrackAssembly_SetOutput(swigCPtr, state);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LogConstraintViolations() {
    vehiclePINVOKE.ChTrackAssembly_LogConstraintViolations(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool IsRoadwheelCylinder() {
    bool ret = vehiclePINVOKE.ChTrackAssembly_IsRoadwheelCylinder(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsIdlerCylinder() {
    bool ret = vehiclePINVOKE.ChTrackAssembly_IsIdlerCylinder(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsRolerCylinder() {
    bool ret = vehiclePINVOKE.ChTrackAssembly_IsRolerCylinder(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
