//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class SCMDeformableTerrain : ChTerrain {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal SCMDeformableTerrain(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.SCMDeformableTerrain_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SCMDeformableTerrain obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_SCMDeformableTerrain(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public SCMDeformableTerrain(ChSystem system, bool visualization_mesh) : this(vehiclePINVOKE.new_SCMDeformableTerrain__SWIG_0(ChSystem.getCPtr(system), visualization_mesh), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public SCMDeformableTerrain(ChSystem system) : this(vehiclePINVOKE.new_SCMDeformableTerrain__SWIG_1(ChSystem.getCPtr(system)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPlane(ChCoordsysD plane) {
    vehiclePINVOKE.SCMDeformableTerrain_SetPlane(swigCPtr, ChCoordsysD.getCPtr(plane));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChCoordsysD GetPlane() {
    ChCoordsysD ret = new ChCoordsysD(vehiclePINVOKE.SCMDeformableTerrain_GetPlane(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetSoilParameters(double Bekker_Kphi, double Bekker_Kc, double Bekker_n, double Mohr_cohesion, double Mohr_friction, double Janosi_shear, double elastic_K, double damping_R) {
    vehiclePINVOKE.SCMDeformableTerrain_SetSoilParameters(swigCPtr, Bekker_Kphi, Bekker_Kc, Bekker_n, Mohr_cohesion, Mohr_friction, Janosi_shear, elastic_K, damping_R);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void EnableBulldozing(bool mb) {
    vehiclePINVOKE.SCMDeformableTerrain_EnableBulldozing(swigCPtr, mb);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBulldozingParameters(double erosion_angle, double flow_factor, int erosion_iterations, int erosion_propagations) {
    vehiclePINVOKE.SCMDeformableTerrain_SetBulldozingParameters__SWIG_0(swigCPtr, erosion_angle, flow_factor, erosion_iterations, erosion_propagations);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBulldozingParameters(double erosion_angle, double flow_factor, int erosion_iterations) {
    vehiclePINVOKE.SCMDeformableTerrain_SetBulldozingParameters__SWIG_1(swigCPtr, erosion_angle, flow_factor, erosion_iterations);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBulldozingParameters(double erosion_angle, double flow_factor) {
    vehiclePINVOKE.SCMDeformableTerrain_SetBulldozingParameters__SWIG_2(swigCPtr, erosion_angle, flow_factor);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBulldozingParameters(double erosion_angle) {
    vehiclePINVOKE.SCMDeformableTerrain_SetBulldozingParameters__SWIG_3(swigCPtr, erosion_angle);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTestHeight(double offset) {
    vehiclePINVOKE.SCMDeformableTerrain_SetTestHeight(swigCPtr, offset);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetTestHeight() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTestHeight(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPlotType(SCMDeformableTerrain.DataPlotType plot_type, double min_val, double max_val) {
    vehiclePINVOKE.SCMDeformableTerrain_SetPlotType(swigCPtr, (int)plot_type, min_val, max_val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetColor(ChColor color) {
    vehiclePINVOKE.SCMDeformableTerrain_SetColor(swigCPtr, ChColor.getCPtr(color));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTexture(string tex_file, float scale_x, float scale_y) {
    vehiclePINVOKE.SCMDeformableTerrain_SetTexture__SWIG_0(swigCPtr, tex_file, scale_x, scale_y);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTexture(string tex_file, float scale_x) {
    vehiclePINVOKE.SCMDeformableTerrain_SetTexture__SWIG_1(swigCPtr, tex_file, scale_x);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTexture(string tex_file) {
    vehiclePINVOKE.SCMDeformableTerrain_SetTexture__SWIG_2(swigCPtr, tex_file);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddMovingPatch(ChBody body, ChVectorD OOBB_center, ChVectorD OOBB_dims) {
    vehiclePINVOKE.SCMDeformableTerrain_AddMovingPatch(swigCPtr, ChBody.getCPtr(body), ChVectorD.getCPtr(OOBB_center), ChVectorD.getCPtr(OOBB_dims));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RegisterSoilParametersCallback(SoilParametersCallback cb) {
    vehiclePINVOKE.SCMDeformableTerrain_RegisterSoilParametersCallback(swigCPtr, SoilParametersCallback.getCPtr(cb));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetInitHeight(ChVectorD loc) {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetInitHeight(swigCPtr, ChVectorD.getCPtr(loc));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetInitNormal(ChVectorD loc) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.SCMDeformableTerrain_GetInitNormal(swigCPtr, ChVectorD.getCPtr(loc)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetHeight(ChVectorD loc) {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetHeight(swigCPtr, ChVectorD.getCPtr(loc));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChVectorD GetNormal(ChVectorD loc) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.SCMDeformableTerrain_GetNormal(swigCPtr, ChVectorD.getCPtr(loc)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override float GetCoefficientFriction(ChVectorD loc) {
    float ret = vehiclePINVOKE.SCMDeformableTerrain_GetCoefficientFriction(swigCPtr, ChVectorD.getCPtr(loc));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public NodeInfo GetNodeInfo(ChVectorD loc) {
    NodeInfo ret = new NodeInfo(vehiclePINVOKE.SCMDeformableTerrain_GetNodeInfo(swigCPtr, ChVectorD.getCPtr(loc)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChTriangleMeshShape GetMesh() {
    global::System.IntPtr cPtr = vehiclePINVOKE.SCMDeformableTerrain_GetMesh(swigCPtr);
    ChTriangleMeshShape ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChTriangleMeshShape(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetMeshWireframe(bool val) {
    vehiclePINVOKE.SCMDeformableTerrain_SetMeshWireframe(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void WriteMesh(string filename) {
    vehiclePINVOKE.SCMDeformableTerrain_WriteMesh(swigCPtr, filename);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Initialize(double sizeX, double sizeY, double delta) {
    vehiclePINVOKE.SCMDeformableTerrain_Initialize__SWIG_0(swigCPtr, sizeX, sizeY, delta);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Initialize(string heightmap_file, double sizeX, double sizeY, double hMin, double hMax, double delta) {
    vehiclePINVOKE.SCMDeformableTerrain_Initialize__SWIG_1(swigCPtr, heightmap_file, sizeX, sizeY, hMin, hMax, delta);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Initialize(string mesh_file, double delta) {
    vehiclePINVOKE.SCMDeformableTerrain_Initialize__SWIG_2(swigCPtr, mesh_file, delta);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t GetModifiedNodes(bool all_nodes) {
    SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t ret = new SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t(vehiclePINVOKE.SCMDeformableTerrain_GetModifiedNodes__SWIG_0(swigCPtr, all_nodes), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t GetModifiedNodes() {
    SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t ret = new SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t(vehiclePINVOKE.SCMDeformableTerrain_GetModifiedNodes__SWIG_1(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetModifiedNodes(SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t nodes) {
    vehiclePINVOKE.SCMDeformableTerrain_SetModifiedNodes(swigCPtr, SWIGTYPE_p_std__vectorT_std__pairT_ChVector2T_int_t_double_t_t.getCPtr(nodes));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public TerrainForce GetContactForce(ChBody body) {
    TerrainForce ret = new TerrainForce(vehiclePINVOKE.SCMDeformableTerrain_GetContactForce(swigCPtr, ChBody.getCPtr(body)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetNumRayCasts() {
    int ret = vehiclePINVOKE.SCMDeformableTerrain_GetNumRayCasts(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetNumRayHits() {
    int ret = vehiclePINVOKE.SCMDeformableTerrain_GetNumRayHits(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetNumContactPatches() {
    int ret = vehiclePINVOKE.SCMDeformableTerrain_GetNumContactPatches(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetNumErosionNodes() {
    int ret = vehiclePINVOKE.SCMDeformableTerrain_GetNumErosionNodes(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerMovingPatches() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerMovingPatches(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerRayTesting() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerRayTesting(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerRayCasting() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerRayCasting(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerContactPatches() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerContactPatches(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerContactForces() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerContactForces(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerBulldozing() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerBulldozing(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTimerVisUpdate() {
    double ret = vehiclePINVOKE.SCMDeformableTerrain_GetTimerVisUpdate(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void PrintStepStatistics(SWIGTYPE_p_std__ostream os) {
    vehiclePINVOKE.SCMDeformableTerrain_PrintStepStatistics(swigCPtr, SWIGTYPE_p_std__ostream.getCPtr(os));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public SCMDeformableSoil GetGroundObject() {
    global::System.IntPtr cPtr = vehiclePINVOKE.SCMDeformableTerrain_GetGroundObject(swigCPtr);
    SCMDeformableSoil ret = (cPtr == global::System.IntPtr.Zero) ? null : new SCMDeformableSoil(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public enum DataPlotType {
    PLOT_NONE,
    PLOT_LEVEL,
    PLOT_LEVEL_INITIAL,
    PLOT_SINKAGE,
    PLOT_SINKAGE_ELASTIC,
    PLOT_SINKAGE_PLASTIC,
    PLOT_STEP_PLASTIC_FLOW,
    PLOT_PRESSURE,
    PLOT_PRESSURE_YELD,
    PLOT_SHEAR,
    PLOT_K_JANOSI,
    PLOT_IS_TOUCHED,
    PLOT_ISLAND_ID,
    PLOT_MASSREMAINDER
  }

}