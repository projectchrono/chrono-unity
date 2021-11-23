//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChFrameMovingD : ChFrameD {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChFrameMovingD(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ChronoEngine_csharpPINVOKE.ChFrameMovingD_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChFrameMovingD obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          ChronoEngine_csharpPINVOKE.delete_ChFrameMovingD(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChCoordsysD coord_dt {
    set {
      ChronoEngine_csharpPINVOKE.ChFrameMovingD_coord_dt_set(swigCPtr, ChCoordsysD.getCPtr(value));
      if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChFrameMovingD_coord_dt_get(swigCPtr);
      ChCoordsysD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChCoordsysD(cPtr, false);
      if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChCoordsysD coord_dtdt {
    set {
      ChronoEngine_csharpPINVOKE.ChFrameMovingD_coord_dtdt_set(swigCPtr, ChCoordsysD.getCPtr(value));
      if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChFrameMovingD_coord_dtdt_get(swigCPtr);
      ChCoordsysD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChCoordsysD(cPtr, false);
      if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChFrameMovingD(ChVectorD mv, ChQuaternionD mq) : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_0(ChVectorD.getCPtr(mv), ChQuaternionD.getCPtr(mq)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFrameMovingD(ChVectorD mv) : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_1(ChVectorD.getCPtr(mv)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFrameMovingD() : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_2(), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFrameMovingD(ChVectorD mv, ChMatrix33D ma) : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_3(ChVectorD.getCPtr(mv), ChMatrix33D.getCPtr(ma)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFrameMovingD(ChCoordsysD mc) : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_4(ChCoordsysD.getCPtr(mc)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFrameMovingD(ChFrameD mc) : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_5(ChFrameD.getCPtr(mc)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFrameMovingD(ChFrameMovingD other) : this(ChronoEngine_csharpPINVOKE.new_ChFrameMovingD__SWIG_6(ChFrameMovingD.getCPtr(other)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChCoordsysD GetCoord_dt() {
    ChCoordsysD ret = new ChCoordsysD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetCoord_dt__SWIG_0(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChCoordsysD GetCoord_dtdt() {
    ChCoordsysD ret = new ChCoordsysD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetCoord_dtdt__SWIG_0(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPos_dt() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetPos_dt__SWIG_0(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPos_dtdt() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetPos_dtdt__SWIG_0(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot_dt() {
    ChQuaternionD ret = new ChQuaternionD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetRot_dt__SWIG_0(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot_dtdt() {
    ChQuaternionD ret = new ChQuaternionD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetRot_dtdt__SWIG_0(swigCPtr), false);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWvel_loc() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetWvel_loc(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWvel_par() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetWvel_par(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWacc_loc() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetWacc_loc(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetWacc_par() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetWacc_par(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void SetCoord_dt(ChCoordsysD mcoord_dt) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetCoord_dt(swigCPtr, ChCoordsysD.getCPtr(mcoord_dt));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetPos_dt(ChVectorD mvel) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetPos_dt(swigCPtr, ChVectorD.getCPtr(mvel));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetRot_dt(ChQuaternionD mrot_dt) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetRot_dt(swigCPtr, ChQuaternionD.getCPtr(mrot_dt));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetWvel_loc(ChVectorD wl) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetWvel_loc(swigCPtr, ChVectorD.getCPtr(wl));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetWvel_par(ChVectorD wp) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetWvel_par(swigCPtr, ChVectorD.getCPtr(wp));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetCoord_dtdt(ChCoordsysD mcoord_dtdt) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetCoord_dtdt(swigCPtr, ChCoordsysD.getCPtr(mcoord_dtdt));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetPos_dtdt(ChVectorD macc) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetPos_dtdt(swigCPtr, ChVectorD.getCPtr(macc));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetRot_dtdt(ChQuaternionD mrot_dtdt) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetRot_dtdt(swigCPtr, ChQuaternionD.getCPtr(mrot_dtdt));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetWacc_loc(ChVectorD al) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetWacc_loc(swigCPtr, ChVectorD.getCPtr(al));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetWacc_par(ChVectorD ap) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_SetWacc_par(swigCPtr, ChVectorD.getCPtr(ap));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Compute_Adt(ChMatrix33D mA_dt) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_Compute_Adt(swigCPtr, ChMatrix33D.getCPtr(mA_dt));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Compute_Adtdt(ChMatrix33D mA_dtdt) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_Compute_Adtdt(swigCPtr, ChMatrix33D.getCPtr(mA_dtdt));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChMatrix33D GetA_dt() {
    ChMatrix33D ret = new ChMatrix33D(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetA_dt(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMatrix33D GetA_dtdt() {
    ChMatrix33D ret = new ChMatrix33D(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetA_dtdt(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void ConcatenatePreTransformation(ChFrameMovingD T) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_ConcatenatePreTransformation(swigCPtr, ChFrameMovingD.getCPtr(T));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void ConcatenatePostTransformation(ChFrameMovingD T) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_ConcatenatePostTransformation(swigCPtr, ChFrameMovingD.getCPtr(T));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD PointSpeedLocalToParent(ChVectorD localpos) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_PointSpeedLocalToParent__SWIG_0(swigCPtr, ChVectorD.getCPtr(localpos)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD PointSpeedLocalToParent(ChVectorD localpos, ChVectorD localspeed) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_PointSpeedLocalToParent__SWIG_1(swigCPtr, ChVectorD.getCPtr(localpos), ChVectorD.getCPtr(localspeed)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD PointAccelerationLocalToParent(ChVectorD localpos) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_PointAccelerationLocalToParent__SWIG_0(swigCPtr, ChVectorD.getCPtr(localpos)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD PointAccelerationLocalToParent(ChVectorD localpos, ChVectorD localspeed, ChVectorD localacc) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_PointAccelerationLocalToParent__SWIG_1(swigCPtr, ChVectorD.getCPtr(localpos), ChVectorD.getCPtr(localspeed), ChVectorD.getCPtr(localacc)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD PointSpeedParentToLocal(ChVectorD parentpos, ChVectorD parentspeed) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_PointSpeedParentToLocal(swigCPtr, ChVectorD.getCPtr(parentpos), ChVectorD.getCPtr(parentspeed)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD PointAccelerationParentToLocal(ChVectorD parentpos, ChVectorD parentspeed, ChVectorD parentacc) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_PointAccelerationParentToLocal(swigCPtr, ChVectorD.getCPtr(parentpos), ChVectorD.getCPtr(parentspeed), ChVectorD.getCPtr(parentacc)), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void TransformLocalToParent(ChFrameMovingD local, ChFrameMovingD parent) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_TransformLocalToParent(swigCPtr, ChFrameMovingD.getCPtr(local), ChFrameMovingD.getCPtr(parent));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void TransformParentToLocal(ChFrameMovingD parent, ChFrameMovingD local) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_TransformParentToLocal(swigCPtr, ChFrameMovingD.getCPtr(parent), ChFrameMovingD.getCPtr(local));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool Equals(ChFrameMovingD other) {
    bool ret = ChronoEngine_csharpPINVOKE.ChFrameMovingD_Equals__SWIG_0(swigCPtr, ChFrameMovingD.getCPtr(other));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Equals(ChFrameMovingD other, double tol) {
    bool ret = ChronoEngine_csharpPINVOKE.ChFrameMovingD_Equals__SWIG_1(swigCPtr, ChFrameMovingD.getCPtr(other), tol);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Invert() {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_Invert(swigCPtr);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public new ChFrameMovingD GetInverse() {
    ChFrameMovingD ret = new ChFrameMovingD(ChronoEngine_csharpPINVOKE.ChFrameMovingD_GetInverse(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    ChronoEngine_csharpPINVOKE.ChFrameMovingD_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

}
