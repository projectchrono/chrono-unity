using System.Collections.Generic;
using UnityEngine;
using System;

// ==========================================================================================================

/// <summary>
/// Interface for Chrono subsystems that require FixedUpdate.
/// For any object that implements IAdvance, its Advance() function will be called at each FixedUpdate.
/// An object that implements IAdvance must register itself with the Chrono system.
/// </summary>
public interface IAdvance
{
    void Advance(double step);
}

// ==========================================================================================================

/// <summary>
/// Wrapper around a Chrono system. Allows setting the integration step size, solver and integrator type, etc.
/// Controls advancing the dynamics of the system at each call to FixedUpdate.
/// 
/// ATTENTION: The global Chrono system is set in UChSystem.Awake.
/// Since Unity does not enforce an order of calls to Awake, this global variable can be safely used by other
/// components only in their Start function.
/// </summary>
public class UChSystem : MonoBehaviour
{
    public static ChSystem chrono_system;

    public Vector3 gravity;
    public double step;

    // -----------------------------
    // Contact method

    public ChContactMethod contact_method;

    // SMC settings
    public ChSystemSMC.ContactForceModel contactModel;
    public ChSystemSMC.AdhesionForceModel adhesionModel;
    public ChSystemSMC.TangentialDisplacementModel tdisplModel;
    public bool useMatProps;

    // -----------------------------
    // Solver

    // Supported solver types
    public enum SolverType
    {
        PSOR,
        BARZILAIBORWEIN,
        APGD,
        SPARSE_LU,
        SPARSE_QR,
        GMRES,
        MINRES
    }
    public SolverType solverType;

    // Iterative solver settings
    public int solverMaxIterations;
    public double solverTolerance;
    public bool solverEnablePrecond;
    public bool solverEnableWarmStart;

    // Direct solver settings
    public bool solverUseLearner;
    public bool solverLockSparsityPattern;
    public double solverSparsityEstimate;

    // Global Collision settings
    public float contactBreakingThreshold;
    public double maxPenetrationRecoverySpeed;
    public double minBounceSpeed;
    public float contactEnvelope;
    public float contactMargin;

    // -----------------------------
    // Integrator

    // Supported integrator types
    public enum IntegratorType
    {
        EULER_IMPLICIT_LINEARIZED,
        EULER_IMPLICIT_PROJECTED,
        EULER_IMPLICIT,
        HHT
    }
    public IntegratorType integratorType;

    // Implicit integrator settings
    public int integratorMaxIters;
    public double integratorRelTol;
    public double integratorAbsTolS;
    public double integratorAbsTolL;

    // HHT settings
    public double hhtAlpha;
    public bool hhtScaling;

    // -----------------------------
    // Subsystems that require FixedUpdate
    private Dictionary<string, IAdvance> subsystems = new Dictionary<string, IAdvance>();

    // -----------------------------
    public UChSystem()
    {
        gravity = new Vector3(0, -9.8f, 0);
        step = 1e-2;
        
        useMatProps = true;

        solverType = SolverType.PSOR;

        solverMaxIterations = 50;
        solverTolerance = 0;
        solverEnablePrecond = true;
        solverEnableWarmStart = false;

        solverUseLearner = true;
        solverLockSparsityPattern = false;
        solverSparsityEstimate = 0.9;

        integratorMaxIters = 10;
        integratorRelTol = 1e-4;
        integratorAbsTolS = 1e-8;
        integratorAbsTolL = 1e-8;

        hhtScaling = true;
        hhtAlpha = -0.2;

        // Collision controls. See https://api.projectchrono.org/collision_shapes.html
        // These don't require setting, but can be tweaked if desired
        // Placed here for global control. Some can be altered mid-simulation
        // contactEnvelope = 0.001f;
        // contactBreakingThreshold = 0.001f;
        // contactMargin = 0.001f;
        // minBounceSpeed = 0.15;
        // maxPenetrationRecoverySpeed = 0.25;


        integratorType = IntegratorType.EULER_IMPLICIT_LINEARIZED;
    }

    // -----------------------------
    void Awake()
    {
        ////  TODO -- EXPERIMENTAL
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;


        switch (contact_method)
        {
            case ChContactMethod.NSC:
                chrono_system = new ChSystemNSC();
                break;
            case ChContactMethod.SMC:
                chrono_system = new ChSystemSMC(useMatProps);
                break;
        }

        ////Debug.Log("SOLVER: " + solverType);
        ////Debug.Log("INTEGRATOR: " + integratorType);

        // Set solver
        switch (solverType)
        {
            case SolverType.PSOR:
                {
                    var solver = new ChSolverPSOR();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.BARZILAIBORWEIN:
                {
                    var solver = new ChSolverBB();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.APGD:
                {
                    var solver = new ChSolverAPGD();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.SPARSE_LU:
                {
                    var solver = new ChSolverSparseLU();
                    solver.UseSparsityPatternLearner(solverUseLearner);
                    solver.LockSparsityPattern(solverLockSparsityPattern);
                    solver.SetSparsityEstimate(solverSparsityEstimate);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.SPARSE_QR:
                {
                    var solver = new ChSolverSparseQR();
                    solver.UseSparsityPatternLearner(solverUseLearner);
                    solver.LockSparsityPattern(solverLockSparsityPattern);
                    solver.SetSparsityEstimate(solverSparsityEstimate);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.GMRES:
                {
                    var solver = new ChSolverGMRES();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.MINRES:
                {
                    var solver = new ChSolverMINRES();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
        }

        // Set integrator
        switch (integratorType)
        {
            case IntegratorType.EULER_IMPLICIT_LINEARIZED:
                {
                    var integrator = new ChTimestepperEulerImplicitLinearized(chrono_system);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.EULER_IMPLICIT_PROJECTED:
                {
                    var integrator = new ChTimestepperEulerImplicitProjected(chrono_system);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.EULER_IMPLICIT:
                {
                    var integrator = new ChTimestepperEulerImplicit(chrono_system);
                    integrator.SetMaxiters(integratorMaxIters);
                    integrator.SetRelTolerance(integratorRelTol);
                    integrator.SetAbsTolerances(integratorAbsTolS, integratorAbsTolL);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.HHT:
                { // Modifications to the HHT integrator implemented here to bring up to date. no Mode, no scaling.
                    var integrator = new ChTimestepperHHT(chrono_system);
                    integrator.SetMaxiters(integratorMaxIters);
                    integrator.SetRelTolerance(integratorRelTol);
                    integrator.SetAbsTolerances(integratorAbsTolS, integratorAbsTolL);
                    integrator.SetAlpha(hhtAlpha);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
        }
        
        chrono_system.Set_G_acc(new ChVectorD(gravity.x, gravity.y, gravity.z));
        chrono_system.SetStep(step);
    }

    // -----------------------------
    void FixedUpdate()
    {
        float unity_step = Time.fixedDeltaTime;
        ////Debug.Log("Fixed step:  " + unity_step + " =====================================");

        // Take as many steps as necessary to cover the base FixedUpdate step
        double t = 0;
        while (t < unity_step)
        {
            double h = Math.Min(step, unity_step - t);
            ////Debug.Log("Substep: " + h + " -------------------------------------");

            foreach (var subsys in subsystems.Values)
            {
                subsys.Advance(h);
            }
            chrono_system.DoStepDynamics(h);
            t += h;
        }

        ////Time.fixedDeltaTime = (float)step;
        ////foreach (var subsys in subsystems.Values)
        ////{
        ////    subsys.Advance(step);
        ////}
        ////chrono_system.DoStepDynamics(step);

    }

    // -----------------------------
    public void Register(string name, IAdvance subsystem)
    {
        Debug.Log("[ChSystem] registered subsystem " + name);
        subsystems.Add(name, subsystem);
    }

    // -----------------------------
    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

    // Cleanup everything here
    // TODO: double check this isn't duplicated elsewhere
    void OnDisable()
    {
        // If the simulation world exists
        if (chrono_system != null)
        {
            // Retrieve and iterate over all bodies in the world
            List<ChBody> allBodies = new List<ChBody>();
            chrono_system.Get_bodylist();

            foreach (ChBody body in allBodies)
            {
                // If you're done with the body, here you can dispose of any resources it's using
                // If the body has any other specific cleanup or destruction methods you want, call it here
                // Remove the body from the world
                chrono_system.Remove(body);
            }

            // Clean up any other resources, constraints, etc., that were part of the world

            // Finally, if there's a way to shut down the system, call it here
            // This might involve setting the mySystem to null, or calling a Dispose method if one exists
            // mySystem.Dispose(); // Call this if the system has a Dispose method
            chrono_system = null;
        }
    }


}
