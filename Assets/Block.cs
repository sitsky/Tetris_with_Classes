using UnityEngine;
using System.Collections;

public class Block {

    //position of each block
    public Vector2 position;
    
    //Flip block state for potential destroy if line is full
    public bool stay_alive;
    
    public Block(int x_coord, int y_coord)
    {
        stay_alive = true;
        position = new Vector2(x_coord, y_coord);
       
    }
}
