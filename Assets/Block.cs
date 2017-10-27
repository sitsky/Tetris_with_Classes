using UnityEngine;
using System.Collections;

public class Block {

    public int x;
    public int y;
    public bool stay_alive;
    
    public Block(int x_coord, int y_coord)
    {
        stay_alive = true;
        x = x_coord;
        y = y_coord;
       
    }
}
