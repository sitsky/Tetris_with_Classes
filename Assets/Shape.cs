using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

    public Block[] shape_parts = new Block[4];
    public GameObject[] block3D = new GameObject[4];

    public enum Shape_choice : int { L = 1, J, I, O, S, Z, T };   
    public Shape_choice myshape;

    //public enum Shape_Orientation : int { ZERO = 0, NINTY = 1, ONEEIGHTY = 2, TWOSEVENTY = 3 };
    //public Shape_Orientation myorientation;

    void Start()
    {
        myshape = (Shape_choice)Random.Range(1, (int)Shape_choice.T);
    }
/*
        shape_parts = create_choice(myshape);
        for (int i = 0; i < 4; i++)
        {
            Instantiate(block3D[i]);
        }
        for (int i = 0; i < 4; i++)
        {
            block3D[i].transform.position = shape_parts[i].position + new Vector2(-200, 0);
        }
        
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            block3D[i].transform.position = shape_parts[i].position + new Vector2(-200, 0);
        }
    }*/


    public Shape()
    {
        //myorientation = Shape_Orientation.ZERO;
        
        shape_parts = create_choice(myshape);
    }


    public Block[] create_choice(Shape_choice make_shape)
    {
        Block[] block_chosen = new Block[4];
        switch (make_shape)
        {
            case Shape_choice.L:
                block_chosen[2] = new Block(0, 0);
                block_chosen[1] = new Block(0, -1);
                block_chosen[0] = new Block(0, -2);
                block_chosen[3] = new Block(1, -2);
                break;
            case Shape_choice.J:
                block_chosen[2] = new Block(0, 0);
                block_chosen[1] = new Block(0, -1);
                block_chosen[0] = new Block(0, -2);
                block_chosen[3] = new Block(-1, -2);
                break;
            case Shape_choice.I:
                block_chosen[1] = new Block(0, 0);
                block_chosen[0] = new Block(0, -1);
                block_chosen[2] = new Block(0, -2);
                block_chosen[3] = new Block(0, -3);
                break;
            case Shape_choice.O:
                block_chosen[0] = new Block(0, 0);
                block_chosen[1] = new Block(0, -1);
                block_chosen[2] = new Block(1, 0);
                block_chosen[3] = new Block(1, -1);
                break;
            case Shape_choice.S:
                block_chosen[0] = new Block(0, 0);
                block_chosen[1] = new Block(1, 0);
                block_chosen[2] = new Block(0, -1);
                block_chosen[3] = new Block(-1, -1);
                break;
            case Shape_choice.T:
                block_chosen[0] = new Block(0, 0);
                block_chosen[1] = new Block(-1,0);
                block_chosen[2] = new Block(1, 0);
                block_chosen[3] = new Block(0, -1);
                break;
            case Shape_choice.Z:
                block_chosen[0] = new Block(0, 0);
                block_chosen[1] = new Block(0, -1);
                block_chosen[2] = new Block(-1, 0);
                block_chosen[3] = new Block(1, -1);
                break;
            default:
                break;
        }
        return block_chosen;
    }

    public void rotate_clockwise()
    {
        if (myshape == Shape_choice.O) return; 
        else
        {
            Vector2 pivot = shape_parts[0].position;
            //Debug.Log("pivot: " + pivot.ToString());
            for (int part_of_shape = 1; part_of_shape < shape_parts.Length; part_of_shape++)
            {
                Vector2 direction_of_block = shape_parts[part_of_shape].position - pivot;
                Vector2 rotated;
                rotated.x = direction_of_block.y;
                rotated.y = -direction_of_block.x;
                shape_parts[part_of_shape].position = rotated + pivot;
               // Debug.Log("originalpart: " + part_of_shape.ToString());
            }
            //Sample Code for Rotations if we need to adjust to look better.
            /*if (myshape == Shape_choice.I)
            {
                if (myorientation == Shape_Orientation.NINTY)
                {
                    for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
                    {
                        position[part_of_shape] += Vector2.left;
                    }
                }
                else if (myorientation == Shape_Orientation.TWOSEVENTY)
                {
                    for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
                    {
                        position[part_of_shape] += Vector2.right;
                    }
                }
            }*/
        }
        //myorientation++;
        //if (myorientation > Shape_Orientation.TWOSEVENTY) myorientation = Shape_Orientation.ZERO;
    }

    public void rotate_anticlockwise()
    {
        if (myshape == Shape_choice.O) return;
        else
        {
            Vector2 pivot = shape_parts[0].position;
            for (int part_of_shape = 1; part_of_shape < shape_parts.Length; part_of_shape++)
            {
                Vector2 direction_of_part = shape_parts[part_of_shape].position - pivot;
                Vector2 rotated;
                rotated.x = -direction_of_part.y;
                rotated.y = direction_of_part.x;
                shape_parts[part_of_shape].position = rotated + pivot;
            }           
        }
    }

    public void move_left()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.left;
        }
    }

    public void move_right()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.right;
        }
    }

    public void move_down()
    {
        
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            //Debug.Log("Move: " + Block_positions[part_of_shape].position.ToString());
            if (shape_parts[part_of_shape].position.y < 1)
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.down;
        }
    }

    public void move_up()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.up;
        }
    }
}
