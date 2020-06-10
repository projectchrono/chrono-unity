using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class ICommandable : MonoBehaviour {

    protected TCPServer server;
    protected TimeServer time_server;

    protected string full_name;

    public void SendHeader(uint type, string name, long ticks) {
        SendData(BitConverter.GetBytes(0xDEADC0DE));
        SendData(BitConverter.GetBytes(type));
        SendData(BitConverter.GetBytes(ticks));
        SendData(name);
    }

    public void SendData(Quaternion data) {
        SendData(BitConverter.GetBytes(data.x));
        SendData(BitConverter.GetBytes(data.y));
        SendData(BitConverter.GetBytes(data.z));
        SendData(BitConverter.GetBytes(data.w));
    }

    public void SendData(Vector3 data) {
        SendData(BitConverter.GetBytes(data.x));
        SendData(BitConverter.GetBytes(data.y));
        SendData(BitConverter.GetBytes(data.z));
    }

    public void SendData(string data) {
        SendData(Encoding.ASCII.GetBytes(data + char.MinValue));
    }

    public void SendData(float data) {
        SendData(BitConverter.GetBytes(data));
    }

    public void SendData(int data) {
        SendData(BitConverter.GetBytes(data));
    }

    public void SendData(byte[] data, int length = -1) {
        server.SendData(data, length);
    }

    private static string GetGameObjectPath(Transform transform) {
        string path = transform.name;
        while (transform.parent != null) {
            transform = transform.parent;
            path = transform.name + "/" + path;
        }
        return path;
    }

    public void Start() {
        time_server = (TimeServer)GameObject.FindObjectOfType(typeof(TimeServer));
        full_name = GetGameObjectPath(transform);
        RegisterWithServer();
        OnStart();
    }

    public virtual void OnStart() { }

    void RegisterWithServer() {
        server = (TCPServer)GameObject.FindObjectOfType(typeof(TCPServer));
        if (server != null) {
            Debug.Log(full_name + " found TCPServer");
            server.Register(full_name, this);
        } else {
            Debug.Log(full_name + " failed to find TCPServer");
        }
    }

    public virtual bool OnCommand(string[] command) {
        return false;
    }

    public bool Configure(string[] command) {
        for (int i = 2; i < command.Length; ++i) {
            // For each of these commands expect input to be in the form
            // "key:value"
            string[] attr = command[i].Split(':');
            if (attr.Length == 2) {

                // Use reflection to check if object has a field of the
                // right name
                var field = this.GetType().GetField(attr[0]);

                if (field != null) {
                    Debug.Log(full_name + ": Setting " + attr[0] + ": " + attr[1]);
                    // Explicitly convert the type to match the
                    // destination field
                    field.SetValue(this, System.Convert.ChangeType(attr[1], field.FieldType));
                } else {
                    Debug.Log(full_name + ": Unable to find field: " + attr[0]);
                }
            } else {
                Debug.Log(full_name + ": Could not parse additional attribute: " + command[11]);
            }
        }

        OnConfigure();

        return true;
    }

    public virtual void OnConfigure() { }
}

