//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChTerrain : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal ChTerrain(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChTerrain obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ChTerrain() {
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
          vehiclePINVOKE.delete_ChTerrain(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ChTerrain() : this(vehiclePINVOKE.new_ChTerrain(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    SwigDirectorConnect();
  }

  public virtual void Synchronize(double time) {
    if (SwigDerivedClassHasMethod("Synchronize", swigMethodTypes0)) vehiclePINVOKE.ChTerrain_SynchronizeSwigExplicitChTerrain(swigCPtr, time); else vehiclePINVOKE.ChTerrain_Synchronize(swigCPtr, time);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Advance(double step) {
    if (SwigDerivedClassHasMethod("Advance", swigMethodTypes1)) vehiclePINVOKE.ChTerrain_AdvanceSwigExplicitChTerrain(swigCPtr, step); else vehiclePINVOKE.ChTerrain_Advance(swigCPtr, step);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual double GetHeight(ChVectorD loc) {
    double ret = (SwigDerivedClassHasMethod("GetHeight", swigMethodTypes2) ? vehiclePINVOKE.ChTerrain_GetHeightSwigExplicitChTerrain(swigCPtr, ChVectorD.getCPtr(loc)) : vehiclePINVOKE.ChTerrain_GetHeight(swigCPtr, ChVectorD.getCPtr(loc)));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetNormal(ChVectorD loc) {
    ChVectorD ret = new ChVectorD((SwigDerivedClassHasMethod("GetNormal", swigMethodTypes3) ? vehiclePINVOKE.ChTerrain_GetNormalSwigExplicitChTerrain(swigCPtr, ChVectorD.getCPtr(loc)) : vehiclePINVOKE.ChTerrain_GetNormal(swigCPtr, ChVectorD.getCPtr(loc))), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual float GetCoefficientFriction(ChVectorD loc) {
    float ret = (SwigDerivedClassHasMethod("GetCoefficientFriction", swigMethodTypes4) ? vehiclePINVOKE.ChTerrain_GetCoefficientFrictionSwigExplicitChTerrain(swigCPtr, ChVectorD.getCPtr(loc)) : vehiclePINVOKE.ChTerrain_GetCoefficientFriction(swigCPtr, ChVectorD.getCPtr(loc)));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void RegisterFrictionFunctor(FrictionFunctor functor) {
    vehiclePINVOKE.ChTerrain_RegisterFrictionFunctor(swigCPtr, FrictionFunctor.getCPtr(functor));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("Synchronize", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateChTerrain_0(SwigDirectorMethodSynchronize);
    if (SwigDerivedClassHasMethod("Advance", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateChTerrain_1(SwigDirectorMethodAdvance);
    if (SwigDerivedClassHasMethod("GetHeight", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateChTerrain_2(SwigDirectorMethodGetHeight);
    if (SwigDerivedClassHasMethod("GetNormal", swigMethodTypes3))
      swigDelegate3 = new SwigDelegateChTerrain_3(SwigDirectorMethodGetNormal);
    if (SwigDerivedClassHasMethod("GetCoefficientFriction", swigMethodTypes4))
      swigDelegate4 = new SwigDelegateChTerrain_4(SwigDirectorMethodGetCoefficientFriction);
    vehiclePINVOKE.ChTerrain_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3, swigDelegate4);
  }

  private bool SwigDerivedClassHasMethod(string methodName, global::System.Type[] methodTypes) {
    global::System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(ChTerrain));
    return hasDerivedMethod;
  }

  private void SwigDirectorMethodSynchronize(double time) {
    Synchronize(time);
  }

  private void SwigDirectorMethodAdvance(double step) {
    Advance(step);
  }

  private double SwigDirectorMethodGetHeight(global::System.IntPtr loc) {
    return GetHeight(new ChVectorD(loc, false));
  }

  private global::System.IntPtr SwigDirectorMethodGetNormal(global::System.IntPtr loc) {
    return ChVectorD.getCPtr(GetNormal(new ChVectorD(loc, false))).Handle;
  }

  private float SwigDirectorMethodGetCoefficientFriction(global::System.IntPtr loc) {
    return GetCoefficientFriction(new ChVectorD(loc, false));
  }

  public delegate void SwigDelegateChTerrain_0(double time);
  public delegate void SwigDelegateChTerrain_1(double step);
  public delegate double SwigDelegateChTerrain_2(global::System.IntPtr loc);
  public delegate global::System.IntPtr SwigDelegateChTerrain_3(global::System.IntPtr loc);
  public delegate float SwigDelegateChTerrain_4(global::System.IntPtr loc);

  private SwigDelegateChTerrain_0 swigDelegate0;
  private SwigDelegateChTerrain_1 swigDelegate1;
  private SwigDelegateChTerrain_2 swigDelegate2;
  private SwigDelegateChTerrain_3 swigDelegate3;
  private SwigDelegateChTerrain_4 swigDelegate4;

  private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] { typeof(double) };
  private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] { typeof(double) };
  private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] { typeof(ChVectorD) };
  private static global::System.Type[] swigMethodTypes3 = new global::System.Type[] { typeof(ChVectorD) };
  private static global::System.Type[] swigMethodTypes4 = new global::System.Type[] { typeof(ChVectorD) };
}
