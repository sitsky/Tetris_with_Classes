using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    enum Block_Shape : int { L = 1, J, I, O, S, Z, T };
    Vector2 position;
    Vector2 orientation;
    Block_Shape myShape;

    public Block()
    {
        position = new Vector2(0, 0);
        orientation = new Vector2(0, 1);
        myShape = (Block_Shape)Random.Range(1, 7);
    }
    public void move_left()
    {
        position.x = position.x - 1;
    }
    public void move_right()
    {
        position.x = position.x + 1;
    }
    public void rotate_clockwise()
    {
        Vector2 temp;
        temp.x = orientation.x * 0 + orientation.y * 1;
        temp.y = orientation.x * -1 + orientation.y * 0;
        orientation = temp;
    }
    public void rotate_anticlockwise()
    {
        Vector2 temp;
        temp.x = orientation.x * 0 + orientation.y * -1;
        temp.y = orientation.x * 1 + orientation.y * 0;
        orientation = temp;
    }

}
