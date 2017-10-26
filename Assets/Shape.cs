using UnityEngine;
using System.Collections;

public enum Shape_choice : int { L = 1, J, I, O, S, Z, T };

public class Shape {

    public Block[] shape_parts = new Block[4];

    public Shape_choice myshape;
    public Color mycolor;

    public enum Shape_Orientation : int { ZERO = 0, NINTY = 1, ONEEIGHTY = 2, TWOSEVENTY = 3 };
    public Shape_Orientation myorientation;

    public Shape()
    {
        myorientation = Shape_Orientation.ZERO;
        myshape = (Shape_choice)Random.Range(1, (int)Shape_choice.T);
        shape_parts = Create_choice(myshape);
        mycolor = Shape_coloring(myshape);
    }

    public Shape(Shape_choice specific_shape)
    {
        shape_parts = Create_choice(specific_shape);
        mycolor = Shape_coloring(specific_shape);
    }

    public Color Shape_coloring(Shape_choice color_shape)
    {
        Color color_chosen;
        switch(myshape)
        {
            case Shape_choice.L:
                color_chosen = Color.blue;
                break;
            case Shape_choice.J:
                color_chosen = Color.cyan;
                break;
            case Shape_choice.I:
                color_chosen = Color.green;
                break;
            case Shape_choice.O:
                color_chosen = Color.red;
                break;
            case Shape_choice.S:
                color_chosen = Color.magenta;
                break;
            case Shape_choice.Z:
                color_chosen = Color.yellow;
                break;
            case Shape_choice.T:
                color_chosen = Color.white;
                break;
            default:
                color_chosen = Color.gray;
                break;
        }
        return color_chosen;
    }


    public Block[] Create_choice(Shape_choice make_shape)
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

    public void Shape_rotate_anticlockwise()
    {
        if (myshape == Shape_choice.O) return; 
        else
        {
            myorientation++;
            if (myorientation > Shape_Orientation.TWOSEVENTY) myorientation = Shape_Orientation.ZERO;
            Vector2 pivot = shape_parts[0].position;
            for (int part_of_shape = 1; part_of_shape < shape_parts.Length; part_of_shape++)
            {
                Vector2 direction_of_block = shape_parts[part_of_shape].position - pivot;
                Vector2 rotated;
                rotated.x = direction_of_block.y;
                rotated.y = -direction_of_block.x;
                shape_parts[part_of_shape].position = rotated + pivot;

            }
        }
        if (myorientation == Shape_Orientation.TWOSEVENTY)
        {
            for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
            {
                shape_parts[part_of_shape].position += Vector2.left;
                
            }
        }
        if (myorientation == Shape_Orientation.ZERO)
        {
            for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
            {
                shape_parts[part_of_shape].position += Vector2.right;
                
            }
        }
    }

    public void Shape_rotate_clockwise()
    {
        if (myshape == Shape_choice.O) return;
        else
        {
            myorientation--;
            if (myorientation < Shape_Orientation.ZERO) myorientation = Shape_Orientation.ZERO;
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
        if (myorientation == Shape_Orientation.TWOSEVENTY)
        {
            for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
            {
                shape_parts[part_of_shape].position += Vector2.right;
                
            }
        }
        if (myorientation == Shape_Orientation.ZERO)
        {
            for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
            {
                shape_parts[part_of_shape].position += Vector2.left;
               
            }
        }
    }

    public void Shape_move_left()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.left;
        }
    }

    public void Shape_move_right()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.right;
        }
    }

    public void Shape_move_down()
    {
        
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            //Debug.Log("Move: " + Block_positions[part_of_shape].position.ToString());
            if (shape_parts[part_of_shape].position.y < 1)
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.down;
        }
    }

    public void Shape_move_up()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].position = shape_parts[part_of_shape].position + Vector2.up;
        }
    }
}
