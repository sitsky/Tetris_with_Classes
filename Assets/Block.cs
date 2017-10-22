using UnityEngine;
using System.Collections;

public class Block {

    public Vector2 position;
    public bool stay_alive;
    
    public Block(int x_coord, int y_coord)
    {
        stay_alive = true;
        position = new Vector2(x_coord, y_coord);
       
    }
}
