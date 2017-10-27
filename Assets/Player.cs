using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Shape Player_Current_Shape;
    public Shape Player_Next_Shape;
    public float last_drop;
    public Motion_keys[] mymotion = new Motion_keys[Tetris_consts.keys_per_player];
    public List<C_line> Lines = new List<C_line>();

    public Player()
    {
        for (int i = 0; i < 20; i++)
        {
            Lines.Add(new C_line());
        }
    }

    public class C_line
    {
        public BitArray line = new BitArray(10);
        public C_line()
        {
            line = new BitArray(10);
        }
    }
}

