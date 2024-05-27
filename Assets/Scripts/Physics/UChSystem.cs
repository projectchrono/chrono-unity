﻿// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

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
[DefaultExecutionOrder(-1000)] // This ensures that System runs before most other scripts (so there's a system to put bodies in)
public class UChSystem : MonoBehaviour
{
    public static ChSystem chrono_system;
    public static string vehicleDataLocation = "/ChronoData/vehicle/"; //used for the vehicle data
    public static string physicsDataLocation = "/ChronoData/"; //used for the chrono data


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
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;

        // Set the path to the Chrono data files so that all data is contextualised to the ChronoUnity data directory.
        chrono.SetChronoDataPath(Application.streamingAssetsPath + physicsDataLocation);
        chrono_vehicle.SetDataPath(Application.streamingAssetsPath + vehicleDataLocation);

        switch (contact_method)
        {
            case ChContactMethod.NSC:
                chrono_system = new ChSystemNSC();
                break;
            case ChContactMethod.SMC:
                chrono_system = new ChSystemSMC();
                if (chrono_system is ChSystemSMC chronoSystemSMC)
                {
                    chronoSystemSMC.UseMaterialProperties(useMatProps);
                    Debug.Log("useMatProps set for ChSystemSMC");
                }
                // (useMatProps) is defaulted as true in ChSystemSMC setup now. So explicitly
                // exposing to the ChSystem and setting it seems a bit redundant unless its necessary for the user.
                // It could be set with changing the way chrono_system is setup (i.e. as a ChSystemSMC at the outset).
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
                    integrator.SetMaxIters(integratorMaxIters);
                    integrator.SetRelTolerance(integratorRelTol);
                    integrator.SetAbsTolerances(integratorAbsTolS, integratorAbsTolL);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.HHT:
                {
                    var integrator = new ChTimestepperHHT(chrono_system);
                    integrator.SetMaxIters(integratorMaxIters);
                    integrator.SetRelTolerance(integratorRelTol);
                    integrator.SetAbsTolerances(integratorAbsTolS, integratorAbsTolL);
                    integrator.SetAlpha(hhtAlpha);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
        }
        chrono_system.SetCollisionSystemType(ChCollisionSystem.Type.BULLET);
        chrono_system.SetGravitationalAcceleration(new ChVector3d(gravity.x, gravity.y, gravity.z));
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

    // Global Cleanup
    void OnDisable()
    {
        // If the simulation world exists
        if (chrono_system != null)
        {
            // Clean up any other resources, constraints, etc., that were part of the world
            chrono_system.RemoveAllLinks();
            chrono_system.RemoveAllMeshes();
            chrono_system.RemoveAllShafts();
            chrono_system.RemoveAllOtherPhysicsItems();
            chrono_system.RemoveAllBodies();
            chrono_system = null;
        }
    }

    // TODO: - handle unloading of the dll/crashes more gracefully

}
