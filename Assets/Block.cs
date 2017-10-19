using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public enum Block_Shape : int { L = 1, J, I, O, S, Z, T };
    public Vector2[] position = new Vector2[4];
    public string myShape;

    public Block()
    {
        position = create_Block(Block_Shape.L);
        myShape = create_Block_string((Block_Shape)Random.Range(1, 7));
    }


    public Vector2[] create_Block(Block_Shape make_shape)
    {
        Vector2[] block_chosen = new Vector2[4];
        switch (make_shape)
        {
            case Block_Shape.L:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(1, 1);
                block_chosen[2] = new Vector2(2, 1);
                block_chosen[3] = new Vector2(3, 1);
                break;
            case Block_Shape.J:
                break;
            case Block_Shape.I:
                break;
            case Block_Shape.O:
                break;
            case Block_Shape.S:
                break;
            case Block_Shape.T:
                break;
            case Block_Shape.Z:
                break;
            default:
                break;
        }
        return block_chosen;
    }
    public string create_Block_string(Block_Shape make_shape)
    {
        string block_chosen = "";
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
        for (int part_of_block = 0; part_of_block < position.Length; part_of_block++)
        {
            position[part_of_block] += Vector2.left;
        }
    }
    public void move_right()
    {
        for (int part_of_block = 0; part_of_block < position.Length; part_of_block++)
        {
            position[part_of_block] += Vector2.right;
        }
    }

    public void move_down()
    {
        for (int part_of_block = 0; part_of_block < position.Length; part_of_block++)
        {
            position[part_of_block] += Vector2.down;
        }
    }

    public void rotate_clockwise()
    {
        Vector2 pivot = position[0];
        for (int part_of_block = 1; part_of_block > position.Length; part_of_block++)
        {
            Vector2 direction_of_part = position[part_of_block] - pivot;
            direction_of_part = Quaternion.Euler(0, 0, 90) * direction_of_part;
            position[part_of_block] = direction_of_part + pivot;
        }
    }
    public void rotate_anticlockwise()
    {
        Vector2 pivot = position[0];
        for (int part_of_block = 1; part_of_block > position.Length; part_of_block++)
        {
            Vector2 direction_of_part = position[part_of_block] - pivot;
            direction_of_part = Quaternion.Euler(0, 0, 90) * direction_of_part;
            position[part_of_block] = direction_of_part + pivot;
        }
    }
}
