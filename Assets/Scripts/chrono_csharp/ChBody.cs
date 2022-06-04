//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChBody : ChPhysicsItem {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChBody(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.ChBody_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChBody obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_ChBody(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChBody(ChCollisionSystemType collision_type) : this(corePINVOKE.new_ChBody__SWIG_0((int)collision_type), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChBody() : this(corePINVOKE.new_ChBody__SWIG_1(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChBody(ChCollisionModel new_collision_model) : this(corePINVOKE.new_ChBody__SWIG_2(ChCollisionModel.getCPtr(new_collision_model)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChBody(ChBody other) : this(corePINVOKE.new_ChBody__SWIG_3(ChBody.getCPtr(other)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBodyFixed(bool state) {
    corePINVOKE.ChBody_SetBodyFixed(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetBodyFixed() {
    bool ret = corePINVOKE.ChBody_GetBodyFixed(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactCn(bool state) {
    corePINVOKE.ChBody_SetEvalContactCn(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactCn() {
    bool ret = corePINVOKE.ChBody_GetEvalContactCn(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactCt(bool state) {
    corePINVOKE.ChBody_SetEvalContactCt(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactCt() {
    bool ret = corePINVOKE.ChBody_GetEvalContactCt(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactKf(bool state) {
    corePINVOKE.ChBody_SetEvalContactKf(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactKf() {
    bool ret = corePINVOKE.ChBody_GetEvalContactKf(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactSf(bool state) {
    corePINVOKE.ChBody_SetEvalContactSf(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactSf() {
    bool ret = corePINVOKE.ChBody_GetEvalContactSf(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetCollide(bool state) {
    corePINVOKE.ChBody_SetCollide(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override bool GetCollide() {
    bool ret = corePINVOKE.ChBody_GetCollide(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetShowCollisionMesh(bool state) {
    corePINVOKE.ChBody_SetShowCollisionMesh(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetShowCollisionMesh() {
    bool ret = corePINVOKE.ChBody_GetShowCollisionMesh(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetLimitSpeed(bool state) {
    corePINVOKE.ChBody_SetLimitSpeed(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetLimitSpeed() {
    bool ret = corePINVOKE.ChBody_GetLimitSpeed(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetNoGyroTorque(bool state) {
    corePINVOKE.ChBody_SetNoGyroTorque(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetNoGyroTorque() {
    bool ret = corePINVOKE.ChBody_GetNoGyroTorque(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetUseSleeping(bool state) {
    corePINVOKE.ChBody_SetUseSleeping(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetUseSleeping() {
    bool ret = corePINVOKE.ChBody_GetUseSleeping(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSleeping(bool state) {
    corePINVOKE.ChBody_SetSleeping(swigCPtr, state);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetSleeping() {
    bool ret = corePINVOKE.ChBody_GetSleeping(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool TrySleeping() {
    bool ret = corePINVOKE.ChBody_TrySleeping(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsActive() {
    bool ret = corePINVOKE.ChBody_IsActive(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetId(int id) {
    corePINVOKE.ChBody_SetId(swigCPtr, id);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint GetId() {
    uint ret = corePINVOKE.ChBody_GetId(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetGid(uint id) {
    corePINVOKE.ChBody_SetGid(swigCPtr, id);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint GetGid() {
    uint ret = corePINVOKE.ChBody_GetGid(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetDOF() {
    int ret = corePINVOKE.ChBody_GetDOF(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetDOF_w() {
    int ret = corePINVOKE.ChBody_GetDOF_w(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_ChVariables Variables() {
    SWIGTYPE_p_ChVariables ret = new SWIGTYPE_p_ChVariables(corePINVOKE.ChBody_Variables(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void SetNoSpeedNoAcceleration() {
    corePINVOKE.ChBody_SetNoSpeedNoAcceleration(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetCollisionModel(ChCollisionModel new_collision_model) {
    corePINVOKE.ChBody_SetCollisionModel(swigCPtr, ChCollisionModel.getCPtr(new_collision_model));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChCollisionModel GetCollisionModel() {
    global::System.IntPtr cPtr = corePINVOKE.ChBody_GetCollisionModel(swigCPtr);
    ChCollisionModel ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChCollisionModel(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void SyncCollisionModels() {
    corePINVOKE.ChBody_SyncCollisionModels(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void AddCollisionModelsToSystem() {
    corePINVOKE.ChBody_AddCollisionModelsToSystem(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void RemoveCollisionModelsFromSystem() {
    corePINVOKE.ChBody_RemoveCollisionModelsFromSystem(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual ChFrameMovingD GetFrame_COG_to_abs() {
    ChFrameMovingD ret = new ChFrameMovingD(corePINVOKE.ChBody_GetFrame_COG_to_abs(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChFrameMovingD GetFrame_REF_to_abs() {
    ChFrameMovingD ret = new ChFrameMovingD(corePINVOKE.ChBody_GetFrame_REF_to_abs(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetVisualModelFrame(uint nclone) {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChBody_GetVisualModelFrame__SWIG_0(swigCPtr, nclone), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetVisualModelFrame() {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChBody_GetVisualModelFrame__SWIG_1(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void GetTotalAABB(ChVectorD bbmin, ChVectorD bbmax) {
    corePINVOKE.ChBody_GetTotalAABB(swigCPtr, ChVectorD.getCPtr(bbmin), ChVectorD.getCPtr(bbmax));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void StreamINstate(ChStreamInBinary mstream) {
    corePINVOKE.ChBody_StreamINstate(swigCPtr, ChStreamInBinary.getCPtr(mstream));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void StreamOUTstate(ChStreamOutBinary mstream) {
    corePINVOKE.ChBody_StreamOUTstate(swigCPtr, ChStreamOutBinary.getCPtr(mstream));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDensity(float mdensity) {
    corePINVOKE.ChBody_SetDensity(swigCPtr, mdensity);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddMarker(ChMarker amarker) {
    corePINVOKE.ChBody_AddMarker(swigCPtr, ChMarker.getCPtr(amarker));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddForce(ChForce aforce) {
    corePINVOKE.ChBody_AddForce(swigCPtr, ChForce.getCPtr(aforce));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveMarker(ChMarker amarker) {
    corePINVOKE.ChBody_RemoveMarker(swigCPtr, ChMarker.getCPtr(amarker));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveForce(ChForce aforce) {
    corePINVOKE.ChBody_RemoveForce(swigCPtr, ChForce.getCPtr(aforce));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAllForces() {
    corePINVOKE.ChBody_RemoveAllForces(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAllMarkers() {
    corePINVOKE.ChBody_RemoveAllMarkers(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChMarker SearchMarker(string m_name) {
    global::System.IntPtr cPtr = corePINVOKE.ChBody_SearchMarker(swigCPtr, m_name);
    ChMarker ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChMarker(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChForce SearchForce(string m_name) {
    global::System.IntPtr cPtr = corePINVOKE.ChBody_SearchForce(swigCPtr, m_name);
    ChForce ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChForce(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMarkerList GetMarkerList() {
    ChMarkerList ret = new ChMarkerList(corePINVOKE.ChBody_GetMarkerList(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChForceList GetForceList() {
    ChForceList ret = new ChForceList(corePINVOKE.ChBody_GetForceList(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Point_World2Body(ChVectorD mpoint) {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_Point_World2Body(swigCPtr, ChVectorD.getCPtr(mpoint)), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Point_Body2World(ChVectorD mpoint) {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_Point_Body2World(swigCPtr, ChVectorD.getCPtr(mpoint)), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Dir_World2Body(ChVectorD dir) {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_Dir_World2Body(swigCPtr, ChVectorD.getCPtr(dir)), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Dir_Body2World(ChVectorD dir) {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_Dir_Body2World(swigCPtr, ChVectorD.getCPtr(dir)), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD RelPoint_AbsSpeed(ChVectorD mrelpoint) {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_RelPoint_AbsSpeed(swigCPtr, ChVectorD.getCPtr(mrelpoint)), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD RelPoint_AbsAcc(ChVectorD mrelpoint) {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_RelPoint_AbsAcc(swigCPtr, ChVectorD.getCPtr(mrelpoint)), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMass(double newmass) {
    corePINVOKE.ChBody_SetMass(swigCPtr, newmass);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetMass() {
    double ret = corePINVOKE.ChBody_GetMass(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInertia(ChMatrix33D newXInertia) {
    corePINVOKE.ChBody_SetInertia(swigCPtr, ChMatrix33D.getCPtr(newXInertia));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChMatrix33D GetInertia() {
    ChMatrix33D ret = new ChMatrix33D(corePINVOKE.ChBody_GetInertia(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMatrix33D GetInvInertia() {
    ChMatrix33D ret = new ChMatrix33D(corePINVOKE.ChBody_GetInvInertia(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInertiaXX(ChVectorD iner) {
    corePINVOKE.ChBody_SetInertiaXX(swigCPtr, ChVectorD.getCPtr(iner));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetInertiaXX() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetInertiaXX(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInertiaXY(ChVectorD iner) {
    corePINVOKE.ChBody_SetInertiaXY(swigCPtr, ChVectorD.getCPtr(iner));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetInertiaXY() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetInertiaXY(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMaxSpeed(float m_max_speed) {
    corePINVOKE.ChBody_SetMaxSpeed(swigCPtr, m_max_speed);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetMaxSpeed() {
    float ret = corePINVOKE.ChBody_GetMaxSpeed(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMaxWvel(float m_max_wvel) {
    corePINVOKE.ChBody_SetMaxWvel(swigCPtr, m_max_wvel);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetMaxWvel() {
    float ret = corePINVOKE.ChBody_GetMaxWvel(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void ClampSpeed() {
    corePINVOKE.ChBody_ClampSpeed(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSleepTime(float m_t) {
    corePINVOKE.ChBody_SetSleepTime(swigCPtr, m_t);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetSleepTime() {
    float ret = corePINVOKE.ChBody_GetSleepTime(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSleepMinSpeed(float m_t) {
    corePINVOKE.ChBody_SetSleepMinSpeed(swigCPtr, m_t);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetSleepMinSpeed() {
    float ret = corePINVOKE.ChBody_GetSleepMinSpeed(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSleepMinWvel(float m_t) {
    corePINVOKE.ChBody_SetSleepMinWvel(swigCPtr, m_t);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetSleepMinWvel() {
    float ret = corePINVOKE.ChBody_GetSleepMinWvel(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void ComputeQInertia(SWIGTYPE_p_ChMatrix44T_t mQInertia) {
    corePINVOKE.ChBody_ComputeQInertia(swigCPtr, SWIGTYPE_p_ChMatrix44T_t.getCPtr(mQInertia));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void ComputeGyro() {
    corePINVOKE.ChBody_ComputeGyro(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Accumulate_force(ChVectorD force, ChVectorD appl_point, bool local) {
    corePINVOKE.ChBody_Accumulate_force(swigCPtr, ChVectorD.getCPtr(force), ChVectorD.getCPtr(appl_point), local);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Accumulate_torque(ChVectorD torque, bool local) {
    corePINVOKE.ChBody_Accumulate_torque(swigCPtr, ChVectorD.getCPtr(torque), local);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Empty_forces_accumulators() {
    corePINVOKE.ChBody_Empty_forces_accumulators(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD Get_accumulated_force() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_Get_accumulated_force(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Get_accumulated_torque() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_Get_accumulated_torque(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void UpdateMarkers(double mytime) {
    corePINVOKE.ChBody_UpdateMarkers(swigCPtr, mytime);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void UpdateForces(double mytime) {
    corePINVOKE.ChBody_UpdateForces(swigCPtr, mytime);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void UpdateTime(double mytime) {
    corePINVOKE.ChBody_UpdateTime(swigCPtr, mytime);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(double mytime, bool update_assets) {
    corePINVOKE.ChBody_Update__SWIG_0(swigCPtr, mytime, update_assets);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(double mytime) {
    corePINVOKE.ChBody_Update__SWIG_1(swigCPtr, mytime);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(bool update_assets) {
    corePINVOKE.ChBody_Update__SWIG_2(swigCPtr, update_assets);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update() {
    corePINVOKE.ChBody_Update__SWIG_3(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetAppliedForce() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetAppliedForce(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetAppliedTorque() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetAppliedTorque(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetContactForce() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetContactForce(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetContactTorque() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetContactTorque(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChPhysicsItem GetPhysicsItem() {
    global::System.IntPtr cPtr = corePINVOKE.ChBody_GetPhysicsItem(swigCPtr);
    ChPhysicsItem ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPhysicsItem(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    corePINVOKE.ChBody_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    corePINVOKE.ChBody_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetVariables(SWIGTYPE_p_std__vectorT_ChVariables_p_t mvars) {
    corePINVOKE.ChBody_LoadableGetVariables(swigCPtr, SWIGTYPE_p_std__vectorT_ChVariables_p_t.getCPtr(mvars));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableStateIncrement(uint off_x, ChState x_new, ChState x, uint off_v, ChStateDelta Dv) {
    corePINVOKE.ChBody_LoadableStateIncrement(swigCPtr, off_x, ChState.getCPtr(x_new), ChState.getCPtr(x), off_v, ChStateDelta.getCPtr(Dv));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetStateBlock_x(int block_offset, ChState mD) {
    corePINVOKE.ChBody_LoadableGetStateBlock_x(swigCPtr, block_offset, ChState.getCPtr(mD));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetStateBlock_w(int block_offset, ChStateDelta mD) {
    corePINVOKE.ChBody_LoadableGetStateBlock_w(swigCPtr, block_offset, ChStateDelta.getCPtr(mD));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void ComputeNF(double U, double V, double W, ChVectorDynamicD Qi, SWIGTYPE_p_double detJ, ChVectorDynamicD F, ChVectorDynamicD state_x, ChVectorDynamicD state_w) {
    corePINVOKE.ChBody_ComputeNF(swigCPtr, U, V, W, ChVectorDynamicD.getCPtr(Qi), SWIGTYPE_p_double.getCPtr(detJ), ChVectorDynamicD.getCPtr(F), ChVectorDynamicD.getCPtr(state_x), ChVectorDynamicD.getCPtr(state_w));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPos(ChVectorD p) {
    corePINVOKE.ChBody_SetPos(swigCPtr, ChVectorD.getCPtr(p));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRot(ChQuaternionD q) {
    corePINVOKE.ChBody_SetRot__SWIG_0(swigCPtr, ChQuaternionD.getCPtr(q));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRot(ChMatrix33D A) {
    corePINVOKE.ChBody_SetRot__SWIG_1(swigCPtr, ChMatrix33D.getCPtr(A));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetPos() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetPos(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot() {
    ChQuaternionD ret = new ChQuaternionD(corePINVOKE.ChBody_GetRot(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMatrix33D GetA() {
    ChMatrix33D ret = new ChMatrix33D(corePINVOKE.ChBody_GetA(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPos_dt(ChVectorD pd) {
    corePINVOKE.ChBody_SetPos_dt(swigCPtr, ChVectorD.getCPtr(pd));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRot_dt(ChQuaternionD qd) {
    corePINVOKE.ChBody_SetRot_dt(swigCPtr, ChQuaternionD.getCPtr(qd));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWvel_loc(ChVectorD wl) {
    corePINVOKE.ChBody_SetWvel_loc(swigCPtr, ChVectorD.getCPtr(wl));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWvel_par(ChVectorD wp) {
    corePINVOKE.ChBody_SetWvel_par(swigCPtr, ChVectorD.getCPtr(wp));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetPos_dt() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetPos_dt(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot_dt() {
    ChQuaternionD ret = new ChQuaternionD(corePINVOKE.ChBody_GetRot_dt(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWvel_loc() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetWvel_loc(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWvel_par() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetWvel_par(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPos_dtdt() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetPos_dtdt(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot_dtdt() {
    ChQuaternionD ret = new ChQuaternionD(corePINVOKE.ChBody_GetRot_dtdt(swigCPtr), false);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWacc_loc() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetWacc_loc(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWacc_par() {
    ChVectorD ret = new ChVectorD(corePINVOKE.ChBody_GetWacc_par(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
