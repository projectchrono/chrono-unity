using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using UnityEngine;

public class TCPServer : MonoBehaviour
{
    private object syncLock = new object();

    Dictionary<string, ICommandable> commandables = new Dictionary<string, ICommandable>();
    TcpListener server;

    public string host = "localhost";
    public int sensorPort = 9998;
    public int commandPort = 9999;

    private Thread tcpCommandListenerThread;
    private Thread tcpSensorListenerThread;

    private TcpClient connectedTcpClient;
    private TcpListener tcpListener;
    private TcpClient client;
    private Stream stream;

    private Queue<string> messageQueue = new Queue<string>();
    private bool shuttingDown = false;

    //TODO: split command and sensor servers into two classes
    void StartCommandThread()
    {
        tcpCommandListenerThread = new Thread(new ThreadStart(ListenForCommandConnections));
        tcpCommandListenerThread.IsBackground = true;
        tcpCommandListenerThread.Start();
    }


    void OnDestroy() {
        Debug.Log("Shutting down listener thread.");
        shuttingDown = true;
        if (tcpListener != null) {
            tcpListener.Stop();
        }

        if (connectedTcpClient != null)
        {
            connectedTcpClient.Close();
        }
        Debug.Log("Joining TCP thread...");
        tcpCommandListenerThread.Join();
        Debug.Log("TCP thread joined");
    }

    void Connect()
    {
        try
        {
            client = new TcpClient();
            Debug.Log("trying to connect to " + host + " on port " + sensorPort);
            client.Connect(host, sensorPort);
            stream = client.GetStream();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void Start()
    {
        StartCommandThread();
        ////Connect();
    }

    public void SendData(byte[] data, int length = -1)
    {
        if (client.Connected)
        {
            stream.Write(data, 0, length > 0 ? length : data.Length);
        }
    }

    public void Register(string client_name, ICommandable commandable)
    {
        Debug.Log("Commandable: " + client_name + " registered");
        commandables.Add(client_name, commandable);
    }

    private void ListenForCommandConnections()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), commandPort);
            tcpListener.Start();
            Debug.Log("Command server is listening");
            Byte[] bytes = new Byte[1024];
            while (!shuttingDown)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    Debug.Log("Command client connected");
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            byte[] incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            string clientMessages = Encoding.ASCII.GetString(incomingData);

                            lock (syncLock)
                            {
                                foreach(string clientMessage in clientMessages.Split(',')) {
                                    if (clientMessage.Length > 0) {
                                        messageQueue.Enqueue(clientMessage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    void ProcessMessages()
    {
        lock (syncLock)
        {
            while (messageQueue.Count > 0)
            {

                string message = messageQueue.Dequeue();
                Debug.Log("Got command: " + message);
                string[] message_words = message.Split(' ');

                if (message_words.Length > 1)
                {
                    string object_name = message_words[0];
                    if (commandables.ContainsKey(object_name))
                    {
                      if(message_words.Length > 2 && message_words[1] == "configure") {
                        commandables[object_name].Configure(message_words);
                      }
                      else {
                        commandables[object_name].OnCommand(message_words);
                      }
                    }
                    else {
                        Debug.Log("Can't find commandable: " + object_name);
                    }
                }
                

            }
        }
    }
    
    void Update()
    {
        ////if (!client.Connected)
        ////{
        ////    Connect();
        ////} 
        ProcessMessages();
    }
}
