// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Josh Diyn
// =============================================================================

using UnityEngine;
using UnityEngine.Events;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject enableOnEnter; // Set this to the GameObject to show when a vehicle enters
    public GameObject disableOnEnter; // Set this to the GameObject to hide when a vehicle enters
    public UnityEvent onTriggerEnterEvent; // Optional Unity event trigger to have go off when a vehicle enters

    private void Start()
    {
        // Set the initial checkpoints to red
        if (enableOnEnter != null) enableOnEnter.SetActive(false);
        if (disableOnEnter != null) disableOnEnter.SetActive(true);
    }

    private void OnTriggerEnter(Collider vehicle)
    {
        // Likely not required to explicitly require only UChvehicles. However, if the user wishes
        // to have a different type of object to set off the checkpoint, simply remove the 'if' encapsulation
        if (vehicle.gameObject.GetComponent<UChVehicle>() != null)
        {
            if (enableOnEnter != null) enableOnEnter.SetActive(true);
            if (disableOnEnter != null) disableOnEnter.SetActive(false);    
            onTriggerEnterEvent?.Invoke(); // set off the event if there's one selected
        }
    }

}
