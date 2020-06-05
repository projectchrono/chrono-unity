using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeServer : MonoBehaviour {
    long physics_time = 0;
    long frame_time = 0;
    bool realtime = false;
    long physics_frame_count = 0;
    bool ready = false;

    public long GetPhysicsTicks() {
        return physics_time;
    }

    public long GetFrameTicks() {
        return frame_time;
    }

    public long GetTimeNow() {
        if (realtime) {
            return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).Ticks;
        } else {
            return Mathf.RoundToInt(Time.fixedDeltaTime * (float)1e7) * physics_frame_count;
        }
    }

    public void Init(bool realtime) {
        ready = true;
        this.realtime = realtime;
        // Time.timeScale = 0.5f;
    }

    public bool Ready() {
        return ready;
    }

    void FixedUpdate() {
        physics_time = GetTimeNow();
        physics_frame_count++;
    }

    void Update() {
        frame_time = GetTimeNow();
    }
}
