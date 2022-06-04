//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChFunctionRotation_ABCfunctions : ChFunctionRotation {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChFunctionRotation_ABCfunctions(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.ChFunctionRotation_ABCfunctions_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChFunctionRotation_ABCfunctions obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_ChFunctionRotation_ABCfunctions(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChFunctionRotation_ABCfunctions() : this(corePINVOKE.new_ChFunctionRotation_ABCfunctions__SWIG_0(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFunctionRotation_ABCfunctions(ChFunctionRotation_ABCfunctions other) : this(corePINVOKE.new_ChFunctionRotation_ABCfunctions__SWIG_1(ChFunctionRotation_ABCfunctions.getCPtr(other)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetFunctionAngleA(ChFunction mx) {
    corePINVOKE.ChFunctionRotation_ABCfunctions_SetFunctionAngleA(swigCPtr, ChFunction.getCPtr(mx));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFunction GetFunctionAngleA() {
    global::System.IntPtr cPtr = corePINVOKE.ChFunctionRotation_ABCfunctions_GetFunctionAngleA(swigCPtr);
    ChFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChFunction(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetFunctionAngleB(ChFunction mx) {
    corePINVOKE.ChFunctionRotation_ABCfunctions_SetFunctionAngleB(swigCPtr, ChFunction.getCPtr(mx));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFunction GetFunctionAngleB() {
    global::System.IntPtr cPtr = corePINVOKE.ChFunctionRotation_ABCfunctions_GetFunctionAngleB(swigCPtr);
    ChFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChFunction(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetFunctionAngleC(ChFunction mx) {
    corePINVOKE.ChFunctionRotation_ABCfunctions_SetFunctionAngleC(swigCPtr, ChFunction.getCPtr(mx));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChFunction GetFunctionAngleC() {
    global::System.IntPtr cPtr = corePINVOKE.ChFunctionRotation_ABCfunctions_GetFunctionAngleC(swigCPtr);
    ChFunction ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChFunction(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetAngleset(AngleSet mset) {
    corePINVOKE.ChFunctionRotation_ABCfunctions_SetAngleset(swigCPtr, (int)mset);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public AngleSet GetAngleset() {
    AngleSet ret = (AngleSet)corePINVOKE.ChFunctionRotation_ABCfunctions_GetAngleset(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChQuaternionD Get_q(double s) {
    ChQuaternionD ret = new ChQuaternionD(corePINVOKE.ChFunctionRotation_ABCfunctions_Get_q(swigCPtr, s), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    corePINVOKE.ChFunctionRotation_ABCfunctions_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    corePINVOKE.ChFunctionRotation_ABCfunctions_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}
