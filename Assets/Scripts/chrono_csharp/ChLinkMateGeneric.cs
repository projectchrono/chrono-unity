//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChLinkMateGeneric : ChLinkMate {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChLinkMateGeneric(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.ChLinkMateGeneric_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChLinkMateGeneric obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_ChLinkMateGeneric(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChLinkMateGeneric(bool mc_x, bool mc_y, bool mc_z, bool mc_rx, bool mc_ry, bool mc_rz) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_0(mc_x, mc_y, mc_z, mc_rx, mc_ry, mc_rz), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric(bool mc_x, bool mc_y, bool mc_z, bool mc_rx, bool mc_ry) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_1(mc_x, mc_y, mc_z, mc_rx, mc_ry), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric(bool mc_x, bool mc_y, bool mc_z, bool mc_rx) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_2(mc_x, mc_y, mc_z, mc_rx), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric(bool mc_x, bool mc_y, bool mc_z) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_3(mc_x, mc_y, mc_z), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric(bool mc_x, bool mc_y) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_4(mc_x, mc_y), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric(bool mc_x) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_5(mc_x), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric() : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_6(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkMateGeneric(ChLinkMateGeneric other) : this(corePINVOKE.new_ChLinkMateGeneric__SWIG_7(ChLinkMateGeneric.getCPtr(other)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChCoordsysD GetLinkRelativeCoords() {
    ChCoordsysD ret = new ChCoordsysD(corePINVOKE.ChLinkMateGeneric_GetLinkRelativeCoords(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetVisualModelFrame(uint nclone) {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChLinkMateGeneric_GetVisualModelFrame__SWIG_0(swigCPtr, nclone), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetVisualModelFrame() {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChLinkMateGeneric_GetVisualModelFrame__SWIG_1(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChFrameD GetFrame1() {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChLinkMateGeneric_GetFrame1(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChFrameD GetFrame2() {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChLinkMateGeneric_GetFrame2(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsConstrainedX() {
    bool ret = corePINVOKE.ChLinkMateGeneric_IsConstrainedX(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsConstrainedY() {
    bool ret = corePINVOKE.ChLinkMateGeneric_IsConstrainedY(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsConstrainedZ() {
    bool ret = corePINVOKE.ChLinkMateGeneric_IsConstrainedZ(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsConstrainedRx() {
    bool ret = corePINVOKE.ChLinkMateGeneric_IsConstrainedRx(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsConstrainedRy() {
    bool ret = corePINVOKE.ChLinkMateGeneric_IsConstrainedRy(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsConstrainedRz() {
    bool ret = corePINVOKE.ChLinkMateGeneric_IsConstrainedRz(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetConstrainedCoords(bool mc_x, bool mc_y, bool mc_z, bool mc_rx, bool mc_ry, bool mc_rz) {
    corePINVOKE.ChLinkMateGeneric_SetConstrainedCoords(swigCPtr, mc_x, mc_y, mc_z, mc_rx, mc_ry, mc_rz);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Initialize(ChBodyFrame mbody1, ChBodyFrame mbody2, ChFrameD mabsframe) {
    corePINVOKE.ChLinkMateGeneric_Initialize__SWIG_0(swigCPtr, ChBodyFrame.getCPtr(mbody1), ChBodyFrame.getCPtr(mbody2), ChFrameD.getCPtr(mabsframe));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Initialize(ChBodyFrame mbody1, ChBodyFrame mbody2, bool pos_are_relative, ChFrameD mframe1, ChFrameD mframe2) {
    corePINVOKE.ChLinkMateGeneric_Initialize__SWIG_1(swigCPtr, ChBodyFrame.getCPtr(mbody1), ChBodyFrame.getCPtr(mbody2), pos_are_relative, ChFrameD.getCPtr(mframe1), ChFrameD.getCPtr(mframe2));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Initialize(ChBodyFrame mbody1, ChBodyFrame mbody2, bool pos_are_relative, ChVectorD mpt1, ChVectorD mpt2, ChVectorD mnorm1, ChVectorD mnorm2) {
    corePINVOKE.ChLinkMateGeneric_Initialize__SWIG_2(swigCPtr, ChBodyFrame.getCPtr(mbody1), ChBodyFrame.getCPtr(mbody2), pos_are_relative, ChVectorD.getCPtr(mpt1), ChVectorD.getCPtr(mpt2), ChVectorD.getCPtr(mnorm1), ChVectorD.getCPtr(mnorm2));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(double mtime, bool update_assets) {
    corePINVOKE.ChLinkMateGeneric_Update__SWIG_0(swigCPtr, mtime, update_assets);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Update(double mtime) {
    corePINVOKE.ChLinkMateGeneric_Update__SWIG_1(swigCPtr, mtime);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override int RestoreRedundant() {
    int ret = corePINVOKE.ChLinkMateGeneric_RestoreRedundant(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void SetDisabled(bool mdis) {
    corePINVOKE.ChLinkMateGeneric_SetDisabled(swigCPtr, mdis);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void SetBroken(bool mon) {
    corePINVOKE.ChLinkMateGeneric_SetBroken(swigCPtr, mon);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override int GetDOC() {
    int ret = corePINVOKE.ChLinkMateGeneric_GetDOC(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetDOC_c() {
    int ret = corePINVOKE.ChLinkMateGeneric_GetDOC_c(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetDOC_d() {
    int ret = corePINVOKE.ChLinkMateGeneric_GetDOC_d(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChVectorDynamicD GetConstraintViolation() {
    ChVectorDynamicD ret = new ChVectorDynamicD(corePINVOKE.ChLinkMateGeneric_GetConstraintViolation(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void IntStateGatherReactions(uint off_L, ChVectorDynamicD L) {
    corePINVOKE.ChLinkMateGeneric_IntStateGatherReactions(swigCPtr, off_L, ChVectorDynamicD.getCPtr(L));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void IntStateScatterReactions(uint off_L, ChVectorDynamicD L) {
    corePINVOKE.ChLinkMateGeneric_IntStateScatterReactions(swigCPtr, off_L, ChVectorDynamicD.getCPtr(L));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void IntLoadResidual_CqL(uint off_L, ChVectorDynamicD R, ChVectorDynamicD L, double c) {
    corePINVOKE.ChLinkMateGeneric_IntLoadResidual_CqL(swigCPtr, off_L, ChVectorDynamicD.getCPtr(R), ChVectorDynamicD.getCPtr(L), c);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void IntLoadConstraint_C(uint off, ChVectorDynamicD Qc, double c, bool do_clamp, double recovery_clamp) {
    corePINVOKE.ChLinkMateGeneric_IntLoadConstraint_C(swigCPtr, off, ChVectorDynamicD.getCPtr(Qc), c, do_clamp, recovery_clamp);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void IntLoadConstraint_Ct(uint off, ChVectorDynamicD Qc, double c) {
    corePINVOKE.ChLinkMateGeneric_IntLoadConstraint_Ct(swigCPtr, off, ChVectorDynamicD.getCPtr(Qc), c);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void IntToDescriptor(uint off_v, ChStateDelta v, ChVectorDynamicD R, uint off_L, ChVectorDynamicD L, ChVectorDynamicD Qc) {
    corePINVOKE.ChLinkMateGeneric_IntToDescriptor(swigCPtr, off_v, ChStateDelta.getCPtr(v), ChVectorDynamicD.getCPtr(R), off_L, ChVectorDynamicD.getCPtr(L), ChVectorDynamicD.getCPtr(Qc));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void IntFromDescriptor(uint off_v, ChStateDelta v, uint off_L, ChVectorDynamicD L) {
    corePINVOKE.ChLinkMateGeneric_IntFromDescriptor(swigCPtr, off_v, ChStateDelta.getCPtr(v), off_L, ChVectorDynamicD.getCPtr(L));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void InjectConstraints(SWIGTYPE_p_ChSystemDescriptor mdescriptor) {
    corePINVOKE.ChLinkMateGeneric_InjectConstraints(swigCPtr, SWIGTYPE_p_ChSystemDescriptor.getCPtr(mdescriptor));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiReset() {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiReset(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiLoad_C(double factor, double recovery_clamp, bool do_clamp) {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiLoad_C__SWIG_0(swigCPtr, factor, recovery_clamp, do_clamp);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiLoad_C(double factor, double recovery_clamp) {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiLoad_C__SWIG_1(swigCPtr, factor, recovery_clamp);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiLoad_C(double factor) {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiLoad_C__SWIG_2(swigCPtr, factor);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiLoad_C() {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiLoad_C__SWIG_3(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiLoad_Ct(double factor) {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiLoad_Ct__SWIG_0(swigCPtr, factor);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsBiLoad_Ct() {
    corePINVOKE.ChLinkMateGeneric_ConstraintsBiLoad_Ct__SWIG_1(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsLoadJacobians() {
    corePINVOKE.ChLinkMateGeneric_ConstraintsLoadJacobians(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsFetch_react(double factor) {
    corePINVOKE.ChLinkMateGeneric_ConstraintsFetch_react__SWIG_0(swigCPtr, factor);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ConstraintsFetch_react() {
    corePINVOKE.ChLinkMateGeneric_ConstraintsFetch_react__SWIG_1(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    corePINVOKE.ChLinkMateGeneric_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    corePINVOKE.ChLinkMateGeneric_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}