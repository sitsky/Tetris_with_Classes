﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public Motion_keys[] mymotion = new Motion_keys[Tetris_consts.keys_per_player];
    public List<int> Lines = new List<int>(20);
    public Shape Player_Current_Shape;
    public Shape Player_Next_Shape;
    public int Player_Blocks_in_Shape;
    public float last_drop;
    public int netID;

    public bool[] line = new bool[10];

    public Player(int NETID)
    { 
        netID = NETID;
    }
}
