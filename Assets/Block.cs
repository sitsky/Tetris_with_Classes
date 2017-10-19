using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public enum Block_Shape : int { L = 1, J, I, O, S, Z, T };
    public Vector2 position;
    public string myShape;

    public Block()
    {
        position = new Vector2(0, 0);
        myShape = create_Block((Block_Shape)Random.Range(1, 7));
    }


    public string create_Block(Block_Shape make_shape)
    {
        string block_chosen = "nothing";
        switch(make_shape)
        {
            case Block_Shape.L:
                block_chosen = " X \n X \n XX ";
                break;
            case Block_Shape.J:
                block_chosen = " X \n X \n XX ";
                break;
            case Block_Shape.I:
                block_chosen = " X \n X \n X \n X ";
                break;
            case Block_Shape.O:
                block_chosen = " XX\n XX";
                break;
            case Block_Shape.S:
                block_chosen = " XX\nXX ";
                break;
            case Block_Shape.T:
                block_chosen = "XXX\n X ";
                break;
            case Block_Shape.Z:
                block_chosen = "XX \n XX";
                break;
            default: block_chosen = "Fail!";
                break;
        }
        return block_chosen;
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
    }
    public void rotate_anticlockwise()
    {
    }
}
