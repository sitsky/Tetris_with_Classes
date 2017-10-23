using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TetrisNetworkManager : MonoBehaviour {


    public NetworkClient myClient;

   
    public void SetupServer()
    {
        NetworkServer.Listen(4444);       
    }

    // Create a client and connect to the server port
    public NetworkClient SetupClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);
        return myClient;
    }

    
    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
