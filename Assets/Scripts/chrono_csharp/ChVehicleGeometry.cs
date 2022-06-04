//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChVehicleGeometry : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ChVehicleGeometry(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChVehicleGeometry obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ChVehicleGeometry() {
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
          vehiclePINVOKE.delete_ChVehicleGeometry(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ChVehicleGeometry() : this(vehiclePINVOKE.new_ChVehicleGeometry(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool m_has_collision {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_has_collision_set(swigCPtr, value);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = vehiclePINVOKE.ChVehicleGeometry_m_has_collision_get(swigCPtr);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMaterialSurface_t_t m_materials {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_materials_set(swigCPtr, SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMaterialSurface_t_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_materials_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMaterialSurface_t_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMaterialSurface_t_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t m_coll_boxes {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_coll_boxes_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_coll_boxes_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t m_coll_spheres {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_coll_spheres_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_coll_spheres_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t m_coll_cylinders {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_coll_cylinders_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_coll_cylinders_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__ConvexHullsShape_t m_coll_hulls {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_coll_hulls_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__ConvexHullsShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_coll_hulls_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__ConvexHullsShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__ConvexHullsShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__TrimeshShape_t m_coll_meshes {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_coll_meshes_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__TrimeshShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_coll_meshes_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__TrimeshShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__TrimeshShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool m_has_primitives {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_has_primitives_set(swigCPtr, value);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = vehiclePINVOKE.ChVehicleGeometry_m_has_primitives_get(swigCPtr);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t m_vis_boxes {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_vis_boxes_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_vis_boxes_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__BoxShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t m_vis_spheres {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_vis_spheres_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_vis_spheres_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__SphereShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t m_vis_cylinders {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_vis_cylinders_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_vis_cylinders_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__CylinderShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__LineShape_t m_vis_lines {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_vis_lines_set(swigCPtr, SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__LineShape_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_vis_lines_get(swigCPtr);
      SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__LineShape_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__vectorT_chrono__vehicle__ChVehicleGeometry__LineShape_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool m_has_colors {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_has_colors_set(swigCPtr, value);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = vehiclePINVOKE.ChVehicleGeometry_m_has_colors_get(swigCPtr);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChColor m_color_boxes {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_color_boxes_set(swigCPtr, ChColor.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_color_boxes_get(swigCPtr);
      ChColor ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChColor(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChColor m_color_spheres {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_color_spheres_set(swigCPtr, ChColor.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_color_spheres_get(swigCPtr);
      ChColor ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChColor(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChColor m_color_cylinders {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_color_cylinders_set(swigCPtr, ChColor.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.ChVehicleGeometry_m_color_cylinders_get(swigCPtr);
      ChColor ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChColor(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool m_has_mesh {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_has_mesh_set(swigCPtr, value);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = vehiclePINVOKE.ChVehicleGeometry_m_has_mesh_get(swigCPtr);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string m_vis_mesh_file {
    set {
      vehiclePINVOKE.ChVehicleGeometry_m_vis_mesh_file_set(swigCPtr, value);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = vehiclePINVOKE.ChVehicleGeometry_m_vis_mesh_file_get(swigCPtr);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public void AddVisualizationAssets(ChBody body, VisualizationType vis) {
    vehiclePINVOKE.ChVehicleGeometry_AddVisualizationAssets(swigCPtr, ChBody.getCPtr(body), (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddCollisionShapes(ChBody body, int collision_family) {
    vehiclePINVOKE.ChVehicleGeometry_AddCollisionShapes(swigCPtr, ChBody.getCPtr(body), collision_family);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
