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
        mycolor = Color.blue;
        myshape = (Shape_choice)Random.Range(1, (int)Shape_choice.T);
        shape_parts = Create_choice(myshape);
        myorientation = Shape_Orientation.ZERO;
    }

    public Shape(Shape_choice specific_shape)
    {
        Create_choice(specific_shape);
    }
    public Block[] Create_choice(Shape_choice make_shape)
    {
        Block[] shape_parts = new Block[4];
        switch (make_shape)
        {
            case Shape_choice.L:
                shape_parts[2] = new Block(5, 0);
                shape_parts[0] = new Block(5, -1);
                shape_parts[1] = new Block(5, -2);
                shape_parts[3] = new Block(6, -2);
                mycolor = Color.blue;
                myorientation = Shape_Orientation.ZERO;
                break;
            case Shape_choice.J:
                shape_parts[2] = new Block(5, 0);
                shape_parts[0] = new Block(5, -1);
                shape_parts[1] = new Block(5, -2);
                shape_parts[3] = new Block(6, -2);
                mycolor = Color.cyan;
                myorientation = Shape_Orientation.TWOSEVENTY;
                break;
            case Shape_choice.I:
                shape_parts[1] = new Block(5, -0);
                shape_parts[0] = new Block(5, -1);
                shape_parts[2] = new Block(5, -2);
                shape_parts[3] = new Block(5, -3);
                mycolor = Color.green;
                myorientation = Shape_Orientation.TWOSEVENTY;
                break;
            case Shape_choice.O:
                shape_parts[0] = new Block(5, 0);
                shape_parts[1] = new Block(5, -1);
                shape_parts[2] = new Block(6, 0);
                shape_parts[3] = new Block(6, -1);
                mycolor = Color.gray;
                myorientation = Shape_Orientation.ZERO;
                break;
            case Shape_choice.S:
                shape_parts[0] = new Block(5, 0);
                shape_parts[1] = new Block(6, 0);
                shape_parts[2] = new Block(5, -1);
                shape_parts[3] = new Block(6, -1);
                mycolor = Color.red;
                myorientation = Shape_Orientation.ZERO;
                break;
            case Shape_choice.T:
                shape_parts[0] = new Block(5, 0);
                shape_parts[1] = new Block(6, 0);
                shape_parts[2] = new Block(6, 0);
                shape_parts[3] = new Block(5, -1);
                mycolor = Color.magenta;
                myorientation = Shape_Orientation.ZERO;
                break;
            case Shape_choice.Z:
                shape_parts[0] = new Block(5, 0);
                shape_parts[1] = new Block(5, -1);
                shape_parts[2] = new Block(6, 0);
                shape_parts[3] = new Block(6, -1);
                mycolor = Color.yellow;
                myorientation = Shape_Orientation.ZERO;
                break;
            default:
                break;             
        }
        return shape_parts;
    }

    public void Shape_rotate_clockwise()
    {
        if (myshape == Shape_choice.O) return;
        else  
        {
            myorientation++;
            if (myorientation > Shape_Orientation.TWOSEVENTY) myorientation = Shape_Orientation.ZERO;
            Block pivot = shape_parts[0];
            for (int part = 1; part < shape_parts.Length; part++)
            {
                Block direction = new Block(0,0);
                direction.x = shape_parts[part].x - pivot.x;
                direction.y = shape_parts[part].y - pivot.y;
                Block rotated = new Block(0, 0);
                rotated.x = direction.y;
                rotated.y = -direction.x;
                shape_parts[part].x = rotated.x + pivot.x;
                shape_parts[part].y = rotated.y + pivot.y;
            }
        }
        if (myshape == Shape_choice.I)
        {
            if (myorientation == Shape_Orientation.NINTY)
            {
                for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
                {
                    shape_parts[part_of_shape].x += -1;

                }
            }
            if (myorientation == Shape_Orientation.ONEEIGHTY)
            {
                for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
                {
                    shape_parts[part_of_shape].x += 1;
                }
            }
        }
    }
    public void Shape_rotate_anticlockwise()
    {
        if (myshape == Shape_choice.O) return;
        else
        {
            myorientation--;
            if (myorientation < Shape_Orientation.ZERO) myorientation = Shape_Orientation.ZERO;

            if (myshape == Shape_choice.I)
            {
                if (myorientation == Shape_Orientation.NINTY)
                {
                    for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
                    {
                        shape_parts[part_of_shape].x += 1;
                    }
                }
                if (myorientation == Shape_Orientation.ONEEIGHTY)
                {
                    for (int part_of_shape = 0; part_of_shape < 4; part_of_shape++)
                    {
                        shape_parts[part_of_shape].x += -1;
                    }
                }
            }
            Block pivot = shape_parts[0];
            for (int part = 1; part < shape_parts.Length; part++)
            {
                Block direction = new Block(0, 0);
                direction.x = shape_parts[part].x - pivot.x;
                direction.y = shape_parts[part].y - pivot.y;
                Block rotated = new Block(0, 0);
                rotated.x = direction.y;
                rotated.y = -direction.x;
                shape_parts[part].x = rotated.x + pivot.x;
                shape_parts[part].y = rotated.y + pivot.y;
            }
        }
    }

    public void Shape_move_left()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].x += -1;
        }
    }
    public void Shape_move_right()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].x += 1;
        }
    }

    public void Shape_move_down()
    {
        
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].y += -1;
        }
    }
    public void Shape_move_up()
    {
        for (int part_of_shape = 0; part_of_shape < shape_parts.Length; part_of_shape++)
        {
            shape_parts[part_of_shape].y += 1;
        }
    }
}
