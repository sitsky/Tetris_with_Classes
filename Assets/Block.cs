using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public enum Block_Shape : int { L = 1, J, I, O, S, Z, T };
    public enum Block_Orientation : int { ZERO = 0, NINTY = 1, ONEEIGHTY = 2, TWOSEVENTY = 3 };

    int Left_boundary = -10;
    int Right_boundary = 10;
    int Bottom_boundary = 20;

    public Vector2[] position = new Vector2[4];
    public Block_Shape myshape;
    public Block_Orientation myorientation;

    public Block()
    {
        myorientation = Block_Orientation.ZERO;
        myshape = (Block_Shape)Random.Range(1, (int)Block_Shape.T);
        position = create_Block(myshape);
    }


    public Vector2[] create_Block(Block_Shape make_shape)
    {
        Vector2[] block_chosen = new Vector2[4];
        switch (make_shape)
        {
            case Block_Shape.L:
                block_chosen[2] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[0] = new Vector2(0, -2);
                block_chosen[3] = new Vector2(1, -2);
                break;
            case Block_Shape.J:
                block_chosen[2] = new Vector2(0, 0);
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[0] = new Vector2(0, -2);
                block_chosen[3] = new Vector2(-1, -2);
                break;
            case Block_Shape.I:
                block_chosen[1] = new Vector2(0, 0);
                block_chosen[0] = new Vector2(0, -1);
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
                block_chosen[1] = new Vector2(1, 0);
                block_chosen[2] = new Vector2(0, -1);
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
                block_chosen[1] = new Vector2(0, -1);
                block_chosen[2] = new Vector2(-1, 0);
                block_chosen[3] = new Vector2(1, -1);
                break;
            default:
                break;
        }
        return block_chosen;
    }

    public void rotate_clockwise()
    {
        if (myshape == Block_Shape.O) return; 
        else
        {
            Vector2 pivot = position[0];
            Debug.Log("pivot: " + pivot.ToString());
            for (int part_of_block = 1; part_of_block < position.Length; part_of_block++)
            {
                Vector2 direction_of_part = position[part_of_block] - pivot;
                Vector2 rotated;
                rotated.x = direction_of_part.y;
                rotated.y = -direction_of_part.x;
                position[part_of_block] = rotated + pivot;
                Debug.Log("originalpart: " + part_of_block.ToString());
            }
            /*if (myshape == Block_Shape.I)
            {
                if (myorientation == Block_Orientation.NINTY)
                {
                    for (int part_of_block = 0; part_of_block < 4; part_of_block++)
                    {
                        position[part_of_block] += Vector2.left;
                    }
                }
                else if (myorientation == Block_Orientation.TWOSEVENTY)
                {
                    for (int part_of_block = 0; part_of_block < 4; part_of_block++)
                    {
                        position[part_of_block] += Vector2.right;
                    }
                }
            }*/
        }
        myorientation++;
        if (myorientation > Block_Orientation.TWOSEVENTY) myorientation = Block_Orientation.ZERO;
    }

    public void rotate_anticlockwise()
    {
        if (myshape == Block_Shape.O) return;
        else
        {
            Vector2 pivot = position[0];
            for (int part_of_block = 1; part_of_block < position.Length; part_of_block++)
            {
                Vector2 direction_of_part = position[part_of_block] - pivot;
                Vector2 rotated;
                rotated.x = -direction_of_part.y;
                rotated.y = direction_of_part.x;
                position[part_of_block] = rotated + pivot;
            }
            /*if (myshape == Block_Shape.I)
            {
                if (myorientation == Block_Orientation.NINTY)
                {
                    for (int part_of_block = 0; part_of_block < 4; part_of_block++)
                    {
                        position[part_of_block] += Vector2.left;
                    }
                }
                else if (myorientation == Block_Orientation.TWOSEVENTY)
                {
                    for (int part_of_block = 0; part_of_block < 4; part_of_block++)
                    {
                        position[part_of_block] += Vector2.right;
                    }
                }
            }*/
        }

        myorientation--;
        if (myorientation > Block_Orientation.TWOSEVENTY) myorientation = Block_Orientation.ZERO;
    }

    public void move_left()
    {
        Vector2[] temp_position = new Vector2[4];
        for (int part_of_block = 0; part_of_block < position.Length; part_of_block++)
        {
            temp_position[part_of_block] = position[part_of_block] + Vector2.left;
            if ((temp_position[part_of_block].x > 10) || (temp_position[part_of_block].x < -10))
            {
                Debug.Log("Side WAlls hit! L");
                return;
            }
            if ((temp_position[part_of_block].y < -19) || (temp_position[part_of_block].y > 0))
            {
                Debug.Log("Side top or bot hit! L");
                return;
            }
        }
        position = temp_position;
    }

    public void move_right()
    {
        Vector2[] temp_position = new Vector2[4];
        for (int part_of_block = 0; part_of_block < position.Length; part_of_block++)
        {
            temp_position[part_of_block] = position[part_of_block] + Vector2.right;
            if ((temp_position[part_of_block].x > 10) || (temp_position[part_of_block].x < -10))
            {
                Debug.Log("Side WAlls hit! R");
                return;
            }
            if ((temp_position[part_of_block].y < -19) || (temp_position[part_of_block].y > 0))
            {
                Debug.Log("Side top or bot hit! R");
                return;
            }
        }
        position = temp_position;
    }

    public void move_down()
    {
        Vector2[] temp_position = new Vector2[4];
        for (int part_of_block = 0; part_of_block < 4; part_of_block++)
        {
            temp_position[part_of_block] = position[part_of_block] + Vector2.down;
            if (temp_position[part_of_block].y < -20)
            { return; }
        }
        position = temp_position;
    }

    public void move_up()
    {
        Vector2[] temp_position = new Vector2[4];
        for (int part_of_block = 0; part_of_block < 4; part_of_block++)
        {
            temp_position[part_of_block] = position[part_of_block] + Vector2.up;
            if (temp_position[part_of_block].y < -20)
            { return; }
        }
        position = temp_position;
    }
}
