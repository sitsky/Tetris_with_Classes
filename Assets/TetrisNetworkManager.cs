using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TetrisNetworkManager : MonoBehaviour {
    public enum Lan_Status { Server, Local_Client, Remote_Client };

    public NetworkClient myClient;
    public Lan_Status my_status;
   
    public void SetupServer()
    {
        NetworkServer.Listen(4444);       
    }

    // Create a client and connect to the server port
    public void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);
        my_status = Lan_Status.Remote_Client;
    }

    
    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        my_status = Lan_Status.Local_Client;
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
