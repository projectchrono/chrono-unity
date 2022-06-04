//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChMaterialCompositionStrategy : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ChMaterialCompositionStrategy(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChMaterialCompositionStrategy obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ChMaterialCompositionStrategy() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          corePINVOKE.delete_ChMaterialCompositionStrategy(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual float CombineFriction(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineFriction(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineCohesion(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineCohesion(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineRestitution(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineRestitution(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineDamping(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineDamping(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineCompliance(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineCompliance(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineAdhesionMultiplier(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineAdhesionMultiplier(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineStiffnessCoefficient(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineStiffnessCoefficient(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float CombineDampingCoefficient(float a1, float a2) {
    float ret = corePINVOKE.ChMaterialCompositionStrategy_CombineDampingCoefficient(swigCPtr, a1, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChMaterialCompositionStrategy() : this(corePINVOKE.new_ChMaterialCompositionStrategy(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}