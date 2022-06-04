//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChNodeXYZ : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal ChNodeXYZ(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChNodeXYZ obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ChNodeXYZ() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          vehiclePINVOKE.delete_ChNodeXYZ(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SWIGTYPE_p_ChVariablesNode Variables() {
    SWIGTYPE_p_ChVariablesNode ret = new SWIGTYPE_p_ChVariablesNode(vehiclePINVOKE.ChNodeXYZ_Variables(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPos() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChNodeXYZ_GetPos(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPos(ChVectorD mpos) {
    vehiclePINVOKE.ChNodeXYZ_SetPos(swigCPtr, ChVectorD.getCPtr(mpos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetPos_dt() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChNodeXYZ_GetPos_dt(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPos_dt(ChVectorD mposdt) {
    vehiclePINVOKE.ChNodeXYZ_SetPos_dt(swigCPtr, ChVectorD.getCPtr(mposdt));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetPos_dtdt() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChNodeXYZ_GetPos_dtdt(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPos_dtdt(ChVectorD mposdtdt) {
    vehiclePINVOKE.ChNodeXYZ_SetPos_dtdt(swigCPtr, ChVectorD.getCPtr(mposdtdt));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual double GetMass() {
    double ret = vehiclePINVOKE.ChNodeXYZ_GetMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void SetMass(double mm) {
    vehiclePINVOKE.ChNodeXYZ_SetMass(swigCPtr, mm);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual int Get_ndof_x() {
    int ret = vehiclePINVOKE.ChNodeXYZ_Get_ndof_x(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int LoadableGet_ndof_x() {
    int ret = vehiclePINVOKE.ChNodeXYZ_LoadableGet_ndof_x(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int LoadableGet_ndof_w() {
    int ret = vehiclePINVOKE.ChNodeXYZ_LoadableGet_ndof_w(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void LoadableGetStateBlock_x(int block_offset, ChState mD) {
    vehiclePINVOKE.ChNodeXYZ_LoadableGetStateBlock_x(swigCPtr, block_offset, ChState.getCPtr(mD));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableGetStateBlock_w(int block_offset, ChStateDelta mD) {
    vehiclePINVOKE.ChNodeXYZ_LoadableGetStateBlock_w(swigCPtr, block_offset, ChStateDelta.getCPtr(mD));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LoadableStateIncrement(uint off_x, ChState x_new, ChState x, uint off_v, ChStateDelta Dv) {
    vehiclePINVOKE.ChNodeXYZ_LoadableStateIncrement(swigCPtr, off_x, ChState.getCPtr(x_new), ChState.getCPtr(x), off_v, ChStateDelta.getCPtr(Dv));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public int Get_field_ncoords() {
    int ret = vehiclePINVOKE.ChNodeXYZ_Get_field_ncoords(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual int GetSubBlocks() {
    int ret = vehiclePINVOKE.ChNodeXYZ_GetSubBlocks(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetSubBlockOffset(int nblock) {
    uint ret = vehiclePINVOKE.ChNodeXYZ_GetSubBlockOffset(swigCPtr, nblock);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetSubBlockSize(int nblock) {
    uint ret = vehiclePINVOKE.ChNodeXYZ_GetSubBlockSize(swigCPtr, nblock);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsSubBlockActive(int nblock) {
    bool ret = vehiclePINVOKE.ChNodeXYZ_IsSubBlockActive(swigCPtr, nblock);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void LoadableGetVariables(SWIGTYPE_p_std__vectorT_ChVariables_p_t mvars) {
    vehiclePINVOKE.ChNodeXYZ_LoadableGetVariables(swigCPtr, SWIGTYPE_p_std__vectorT_ChVariables_p_t.getCPtr(mvars));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void ComputeNF(double U, double V, double W, ChVectorDynamicD Qi, SWIGTYPE_p_double detJ, ChVectorDynamicD F, ChVectorDynamicD state_x, ChVectorDynamicD state_w) {
    vehiclePINVOKE.ChNodeXYZ_ComputeNF(swigCPtr, U, V, W, ChVectorDynamicD.getCPtr(Qi), SWIGTYPE_p_double.getCPtr(detJ), ChVectorDynamicD.getCPtr(F), ChVectorDynamicD.getCPtr(state_x), ChVectorDynamicD.getCPtr(state_w));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetDensity() {
    double ret = vehiclePINVOKE.ChNodeXYZ_GetDensity(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    vehiclePINVOKE.ChNodeXYZ_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    vehiclePINVOKE.ChNodeXYZ_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD pos {
    set {
      vehiclePINVOKE.ChNodeXYZ_pos_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChNodeXYZ_pos_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChVectorD pos_dt {
    set {
      vehiclePINVOKE.ChNodeXYZ_pos_dt_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChNodeXYZ_pos_dt_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChVectorD pos_dtdt {
    set {
      vehiclePINVOKE.ChNodeXYZ_pos_dtdt_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChNodeXYZ_pos_dtdt_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}