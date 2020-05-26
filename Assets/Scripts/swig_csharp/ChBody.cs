//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChBody : ChPhysicsItem {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChBody(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ChronoEngine_csharpPINVOKE.ChBody_SWIGSmartPtrUpcast(cPtr), true) {
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
          ChronoEngine_csharpPINVOKE.delete_ChBody(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChBody() : this(ChronoEngine_csharpPINVOKE.new_ChBody__SWIG_0(), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChBody(ChCollisionModel new_collision_model) : this(ChronoEngine_csharpPINVOKE.new_ChBody__SWIG_1(ChCollisionModel.getCPtr(new_collision_model)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChBody(ChBody other) : this(ChronoEngine_csharpPINVOKE.new_ChBody__SWIG_2(ChBody.getCPtr(other)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChObj Clone() {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChBody_Clone(swigCPtr);
    ChBody ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBody(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetBodyFixed(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetBodyFixed(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetBodyFixed() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetBodyFixed(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactCn(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetEvalContactCn(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactCn() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetEvalContactCn(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactCt(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetEvalContactCt(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactCt() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetEvalContactCt(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactKf(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetEvalContactKf(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactKf() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetEvalContactKf(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetEvalContactSf(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetEvalContactSf(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetEvalContactSf() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetEvalContactSf(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetCollide(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetCollide(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override bool GetCollide() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetCollide(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetShowCollisionMesh(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetShowCollisionMesh(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetShowCollisionMesh() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetShowCollisionMesh(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetLimitSpeed(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetLimitSpeed(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetLimitSpeed() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetLimitSpeed(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetNoGyroTorque(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetNoGyroTorque(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetNoGyroTorque() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetNoGyroTorque(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetUseSleeping(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetUseSleeping(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetUseSleeping() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetUseSleeping(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSleeping(bool state) {
    ChronoEngine_csharpPINVOKE.ChBody_SetSleeping(swigCPtr, state);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool GetSleeping() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_GetSleeping(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool TrySleeping() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_TrySleeping(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsActive() {
    bool ret = ChronoEngine_csharpPINVOKE.ChBody_IsActive(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetId(int id) {
    ChronoEngine_csharpPINVOKE.ChBody_SetId(swigCPtr, id);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public uint GetId() {
    uint ret = ChronoEngine_csharpPINVOKE.ChBody_GetId(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetGid(uint id) {
    ChronoEngine_csharpPINVOKE.ChBody_SetGid(swigCPtr, id);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public uint GetGid() {
    uint ret = ChronoEngine_csharpPINVOKE.ChBody_GetGid(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetDOF() {
    int ret = ChronoEngine_csharpPINVOKE.ChBody_GetDOF(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetDOF_w() {
    int ret = ChronoEngine_csharpPINVOKE.ChBody_GetDOF_w(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_ChVariables Variables() {
    SWIGTYPE_p_ChVariables ret = new SWIGTYPE_p_ChVariables(ChronoEngine_csharpPINVOKE.ChBody_Variables(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void SetNoSpeedNoAcceleration() {
    ChronoEngine_csharpPINVOKE.ChBody_SetNoSpeedNoAcceleration(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetCollisionModel(ChCollisionModel new_collision_model) {
    ChronoEngine_csharpPINVOKE.ChBody_SetCollisionModel(swigCPtr, ChCollisionModel.getCPtr(new_collision_model));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChCollisionModel GetCollisionModel() {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChBody_GetCollisionModel(swigCPtr);
    ChCollisionModel ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChCollisionModel(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void SyncCollisionModels() {
    ChronoEngine_csharpPINVOKE.ChBody_SyncCollisionModels(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void AddCollisionModelsToSystem() {
    ChronoEngine_csharpPINVOKE.ChBody_AddCollisionModelsToSystem(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void RemoveCollisionModelsFromSystem() {
    ChronoEngine_csharpPINVOKE.ChBody_RemoveCollisionModelsFromSystem(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual ChFrameMovingD GetFrame_COG_to_abs() {
    ChFrameMovingD ret = new ChFrameMovingD(ChronoEngine_csharpPINVOKE.ChBody_GetFrame_COG_to_abs(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChFrameMovingD GetFrame_REF_to_abs() {
    ChFrameMovingD ret = new ChFrameMovingD(ChronoEngine_csharpPINVOKE.ChBody_GetFrame_REF_to_abs(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetAssetsFrame(uint nclone) {
    ChFrameD ret = new ChFrameD(ChronoEngine_csharpPINVOKE.ChBody_GetAssetsFrame__SWIG_0(swigCPtr, nclone), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetAssetsFrame() {
    ChFrameD ret = new ChFrameD(ChronoEngine_csharpPINVOKE.ChBody_GetAssetsFrame__SWIG_1(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void GetTotalAABB(ChVectorD bbmin, ChVectorD bbmax) {
    ChronoEngine_csharpPINVOKE.ChBody_GetTotalAABB(swigCPtr, ChVectorD.getCPtr(bbmin), ChVectorD.getCPtr(bbmax));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void StreamINstate(ChStreamInBinary mstream) {
    ChronoEngine_csharpPINVOKE.ChBody_StreamINstate(swigCPtr, ChStreamInBinary.getCPtr(mstream));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void StreamOUTstate(ChStreamOutBinary mstream) {
    ChronoEngine_csharpPINVOKE.ChBody_StreamOUTstate(swigCPtr, ChStreamOutBinary.getCPtr(mstream));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDensity(float mdensity) {
    ChronoEngine_csharpPINVOKE.ChBody_SetDensity(swigCPtr, mdensity);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddMarker(ChMarker amarker) {
    ChronoEngine_csharpPINVOKE.ChBody_AddMarker(swigCPtr, ChMarker.getCPtr(amarker));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddForce(ChForce aforce) {
    ChronoEngine_csharpPINVOKE.ChBody_AddForce(swigCPtr, ChForce.getCPtr(aforce));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveMarker(ChMarker amarker) {
    ChronoEngine_csharpPINVOKE.ChBody_RemoveMarker(swigCPtr, ChMarker.getCPtr(amarker));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveForce(ChForce aforce) {
    ChronoEngine_csharpPINVOKE.ChBody_RemoveForce(swigCPtr, ChForce.getCPtr(aforce));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAllForces() {
    ChronoEngine_csharpPINVOKE.ChBody_RemoveAllForces(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAllMarkers() {
    ChronoEngine_csharpPINVOKE.ChBody_RemoveAllMarkers(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChMarker SearchMarker(string m_name) {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChBody_SearchMarker(swigCPtr, m_name);
    ChMarker ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChMarker(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChForce SearchForce(string m_name) {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChBody_SearchForce(swigCPtr, m_name);
    ChForce ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChForce(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMarker_t_t GetMarkerList() {
    SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMarker_t_t ret = new SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMarker_t_t(ChronoEngine_csharpPINVOKE.ChBody_GetMarkerList(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChForce_t_t GetForceList() {
    SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChForce_t_t ret = new SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChForce_t_t(ChronoEngine_csharpPINVOKE.ChBody_GetForceList(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Point_World2Body(ChVectorD mpoint) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_Point_World2Body(swigCPtr, ChVectorD.getCPtr(mpoint)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Point_Body2World(ChVectorD mpoint) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_Point_Body2World(swigCPtr, ChVectorD.getCPtr(mpoint)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Dir_World2Body(ChVectorD dir) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_Dir_World2Body(swigCPtr, ChVectorD.getCPtr(dir)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Dir_Body2World(ChVectorD dir) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_Dir_Body2World(swigCPtr, ChVectorD.getCPtr(dir)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD RelPoint_AbsSpeed(ChVectorD mrelpoint) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_RelPoint_AbsSpeed(swigCPtr, ChVectorD.getCPtr(mrelpoint)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD RelPoint_AbsAcc(ChVectorD mrelpoint) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_RelPoint_AbsAcc(swigCPtr, ChVectorD.getCPtr(mrelpoint)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMass(double newmass) {
    ChronoEngine_csharpPINVOKE.ChBody_SetMass(swigCPtr, newmass);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetMass() {
    double ret = ChronoEngine_csharpPINVOKE.ChBody_GetMass(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInertia(ChMatrix33D newXInertia) {
    ChronoEngine_csharpPINVOKE.ChBody_SetInertia(swigCPtr, ChMatrix33D.getCPtr(newXInertia));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChMatrix33D GetInertia() {
    ChMatrix33D ret = new ChMatrix33D(ChronoEngine_csharpPINVOKE.ChBody_GetInertia(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMatrix33D GetInvInertia() {
    ChMatrix33D ret = new ChMatrix33D(ChronoEngine_csharpPINVOKE.ChBody_GetInvInertia(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInertiaXX(ChVectorD iner) {
    ChronoEngine_csharpPINVOKE.ChBody_SetInertiaXX(swigCPtr, ChVectorD.getCPtr(iner));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetInertiaXX() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetInertiaXX(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInertiaXY(ChVectorD iner) {
    ChronoEngine_csharpPINVOKE.ChBody_SetInertiaXY(swigCPtr, ChVectorD.getCPtr(iner));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetInertiaXY() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetInertiaXY(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMaxSpeed(float m_max_speed) {
    ChronoEngine_csharpPINVOKE.ChBody_SetMaxSpeed(swigCPtr, m_max_speed);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetMaxSpeed() {
    float ret = ChronoEngine_csharpPINVOKE.ChBody_GetMaxSpeed(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMaxWvel(float m_max_wvel) {
    ChronoEngine_csharpPINVOKE.ChBody_SetMaxWvel(swigCPtr, m_max_wvel);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetMaxWvel() {
    float ret = ChronoEngine_csharpPINVOKE.ChBody_GetMaxWvel(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void ClampSpeed() {
    ChronoEngine_csharpPINVOKE.ChBody_ClampSpeed(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSleepTime(float m_t) {
    ChronoEngine_csharpPINVOKE.ChBody_SetSleepTime(swigCPtr, m_t);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetSleepTime() {
    float ret = ChronoEngine_csharpPINVOKE.ChBody_GetSleepTime(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSleepMinSpeed(float m_t) {
    ChronoEngine_csharpPINVOKE.ChBody_SetSleepMinSpeed(swigCPtr, m_t);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetSleepMinSpeed() {
    float ret = ChronoEngine_csharpPINVOKE.ChBody_GetSleepMinSpeed(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSleepMinWvel(float m_t) {
    ChronoEngine_csharpPINVOKE.ChBody_SetSleepMinWvel(swigCPtr, m_t);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public float GetSleepMinWvel() {
    float ret = ChronoEngine_csharpPINVOKE.ChBody_GetSleepMinWvel(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void ComputeQInertia(SWIGTYPE_p_ChMatrix44T_t mQInertia) {
    ChronoEngine_csharpPINVOKE.ChBody_ComputeQInertia(swigCPtr, SWIGTYPE_p_ChMatrix44T_t.getCPtr(mQInertia));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void ComputeGyro() {
    ChronoEngine_csharpPINVOKE.ChBody_ComputeGyro(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Accumulate_force(ChVectorD force, ChVectorD appl_point, bool local) {
    ChronoEngine_csharpPINVOKE.ChBody_Accumulate_force(swigCPtr, ChVectorD.getCPtr(force), ChVectorD.getCPtr(appl_point), local);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Accumulate_torque(ChVectorD torque, bool local) {
    ChronoEngine_csharpPINVOKE.ChBody_Accumulate_torque(swigCPtr, ChVectorD.getCPtr(torque), local);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Empty_forces_accumulators() {
    ChronoEngine_csharpPINVOKE.ChBody_Empty_forces_accumulators(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD Get_accumulated_force() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_Get_accumulated_force(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD Get_accumulated_torque() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_Get_accumulated_torque(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void UpdateMarkers(double mytime) {
    ChronoEngine_csharpPINVOKE.ChBody_UpdateMarkers(swigCPtr, mytime);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void UpdateForces(double mytime) {
    ChronoEngine_csharpPINVOKE.ChBody_UpdateForces(swigCPtr, mytime);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void UpdateTime(double mytime) {
    ChronoEngine_csharpPINVOKE.ChBody_UpdateTime(swigCPtr, mytime);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(double mytime, bool update_assets) {
    ChronoEngine_csharpPINVOKE.ChBody_Update__SWIG_0(swigCPtr, mytime, update_assets);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(double mytime) {
    ChronoEngine_csharpPINVOKE.ChBody_Update__SWIG_1(swigCPtr, mytime);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(bool update_assets) {
    ChronoEngine_csharpPINVOKE.ChBody_Update__SWIG_2(swigCPtr, update_assets);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update() {
    ChronoEngine_csharpPINVOKE.ChBody_Update__SWIG_3(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetAppliedForce() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetAppliedForce(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetAppliedTorque() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetAppliedTorque(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetContactForce() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetContactForce(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetContactTorque() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetContactTorque(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChPhysicsItem GetPhysicsItem() {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChBody_GetPhysicsItem(swigCPtr);
    ChPhysicsItem ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPhysicsItem(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    ChronoEngine_csharpPINVOKE.ChBody_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    ChronoEngine_csharpPINVOKE.ChBody_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetVariables(SWIGTYPE_p_std__vectorT_ChVariables_p_t mvars) {
    ChronoEngine_csharpPINVOKE.ChBody_LoadableGetVariables(swigCPtr, SWIGTYPE_p_std__vectorT_ChVariables_p_t.getCPtr(mvars));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableStateIncrement(uint off_x, ChState x_new, ChState x, uint off_v, ChStateDelta Dv) {
    ChronoEngine_csharpPINVOKE.ChBody_LoadableStateIncrement(swigCPtr, off_x, ChState.getCPtr(x_new), ChState.getCPtr(x), off_v, ChStateDelta.getCPtr(Dv));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetStateBlock_x(int block_offset, ChState mD) {
    ChronoEngine_csharpPINVOKE.ChBody_LoadableGetStateBlock_x(swigCPtr, block_offset, ChState.getCPtr(mD));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetStateBlock_w(int block_offset, ChStateDelta mD) {
    ChronoEngine_csharpPINVOKE.ChBody_LoadableGetStateBlock_w(swigCPtr, block_offset, ChStateDelta.getCPtr(mD));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void ComputeNF(double U, double V, double W, ChVectorDynamicD Qi, SWIGTYPE_p_double detJ, ChVectorDynamicD F, ChVectorDynamicD state_x, ChVectorDynamicD state_w) {
    ChronoEngine_csharpPINVOKE.ChBody_ComputeNF(swigCPtr, U, V, W, ChVectorDynamicD.getCPtr(Qi), SWIGTYPE_p_double.getCPtr(detJ), ChVectorDynamicD.getCPtr(F), ChVectorDynamicD.getCPtr(state_x), ChVectorDynamicD.getCPtr(state_w));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPos(ChVectorD p) {
    ChronoEngine_csharpPINVOKE.ChBody_SetPos(swigCPtr, ChVectorD.getCPtr(p));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRot(ChQuaternionD q) {
    ChronoEngine_csharpPINVOKE.ChBody_SetRot__SWIG_0(swigCPtr, ChQuaternionD.getCPtr(q));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRot(ChMatrix33D A) {
    ChronoEngine_csharpPINVOKE.ChBody_SetRot__SWIG_1(swigCPtr, ChMatrix33D.getCPtr(A));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetPos() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetPos(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot() {
    ChQuaternionD ret = new ChQuaternionD(ChronoEngine_csharpPINVOKE.ChBody_GetRot(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMatrix33D GetA() {
    ChMatrix33D ret = new ChMatrix33D(ChronoEngine_csharpPINVOKE.ChBody_GetA(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPos_dt(ChVectorD pd) {
    ChronoEngine_csharpPINVOKE.ChBody_SetPos_dt(swigCPtr, ChVectorD.getCPtr(pd));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRot_dt(ChQuaternionD qd) {
    ChronoEngine_csharpPINVOKE.ChBody_SetRot_dt(swigCPtr, ChQuaternionD.getCPtr(qd));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWvel_loc(ChVectorD wl) {
    ChronoEngine_csharpPINVOKE.ChBody_SetWvel_loc(swigCPtr, ChVectorD.getCPtr(wl));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWvel_par(ChVectorD wp) {
    ChronoEngine_csharpPINVOKE.ChBody_SetWvel_par(swigCPtr, ChVectorD.getCPtr(wp));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetPos_dt() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetPos_dt(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot_dt() {
    ChQuaternionD ret = new ChQuaternionD(ChronoEngine_csharpPINVOKE.ChBody_GetRot_dt(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWvel_loc() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetWvel_loc(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWvel_par() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetWvel_par(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPos_dtdt() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetPos_dtdt(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot_dtdt() {
    ChQuaternionD ret = new ChQuaternionD(ChronoEngine_csharpPINVOKE.ChBody_GetRot_dtdt(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWacc_loc() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetWacc_loc(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWacc_par() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChBody_GetWacc_par(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
