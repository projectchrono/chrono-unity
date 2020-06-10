using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : ICommandable {	
	const uint type = 9;
	
	void FixedUpdate () {
       long timestamp = time_server.GetPhysicsTicks(); 
       ////SendHeader(type, full_name, timestamp);
	}


	public override bool OnCommand(string[] command) {
		string command_name = command[1];
		bool success = false;
		if(command_name == "realtime") {
			bool realtime = bool.Parse(command[2]);
			time_server.Init(realtime);
			success = true;
		}
		return success;
	}
}
