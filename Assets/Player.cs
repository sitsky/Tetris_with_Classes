using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public Motion_keys[] mymotion = new Motion_keys[4];
    public List<Shape> Active_Shapes = new List<Shape>();
    public Shape Player_Current_Shape;
    public Shape Player_Next_Shape;
    public int Player_Blocks_in_Shape;
    public float last_drop;
    public TetrisNetworkManager Lan_Player;

    public Player()
    {  
    }
}
