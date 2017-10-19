using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public enum Block_Shape : int { L = 1, J, I, O, S, Z, T };
    public enum Block_Orientation : int { ZERO = 0, NINTY = 1, ONEEIGHTY = 2, TWOSEVENTY = 3 };
    public Vector2[] position = new Vector2[4];
    public Block_Shape myshape;
    public Block_Orientation myorientation;

    //public string myShape;

    public Block()
    {
        myorientation = Block_Orientation.ZERO;
        myshape = (Block_Shape)Random.Range(1, 7);
        position = create_Block(Block_Shape.L);

        //myShape = create_Block_string(myshape);
    }


    public Vector2[] create_Block(Block_Shape make_shape)
    {
        Vector2[] block_chosen = new Vector2[4];
        switch (make_shape)
        {
            case Block_Shape.L:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[2] = new Vector2(0, -2);
                block_chosen[3] = new Vector2(1, -2);
                break;
            case Block_Shape.J:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[2] = new Vector2(0, -2);
                block_chosen[3] = new Vector2(-1, -2);
                break;
            case Block_Shape.I:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[2] = new Vector2(0, -2);
                block_chosen[3] = new Vector2(0, -3);
                break;
            case Block_Shape.O:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[2] = new Vector2(1, 0);
                block_chosen[3] = new Vector2(1, -1);
                break;
            case Block_Shape.S:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, 1);
                block_chosen[2] = new Vector2(1, 0);
                block_chosen[3] = new Vector2(-1, -1);
                break;
            case Block_Shape.T:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(-1,0);
                block_chosen[2] = new Vector2(1, 0);
                block_chosen[3] = new Vector2(0, -1);
                break;
            case Block_Shape.Z:
                block_chosen[0] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, 1);
                block_chosen[2] = new Vector2(0, -1);
                block_chosen[3] = new Vector2(-1, -1);
                break;
            default:
                break;
        }
        return block_chosen;
    }
    /*public string create_Block_string(Block_Shape make_shape)
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
    }*/

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
            position[part_of_block] += (Vector2.down) * 0.5f;
        }
    }

    public void rotate_clockwise()
    {
        Debug.Log("Block| myorient: " + myorientation.ToString());
        myorientation++;
        if (myorientation > Block_Orientation.TWOSEVENTY) myorientation = Block_Orientation.ZERO;

        if (myshape == Block_Shape.O) return;
        else
        {
            Vector2 pivot = position[0];
            Debug.Log("pivot: " + pivot.ToString());
            for (int part_of_block = 0; part_of_block < 4; part_of_block++)
            {
                Vector2 direction_of_part = position[part_of_block] - pivot;
                direction_of_part = Quaternion.Euler(0, 0, -90) * direction_of_part;
                position[part_of_block] = direction_of_part + pivot;

                Debug.Log("originalpart: " + part_of_block.ToString());
            }
        }
    }
    public void rotate_anticlockwise()
    {
        myorientation--;
        if (myorientation > Block_Orientation.TWOSEVENTY) myorientation = Block_Orientation.ZERO;

        if (myshape == Block_Shape.O) return;
        else
        {
            Vector2 pivot = position[0];
            for (int part_of_block = 1; part_of_block < position.Length; part_of_block++)
            {
                Vector2 direction_of_part = position[part_of_block] - pivot;
                direction_of_part = Quaternion.Euler(0, 0, 90) * direction_of_part;
                position[part_of_block] = direction_of_part + pivot;
            }
        }
    }
}
