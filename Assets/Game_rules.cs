using UnityEngine;
using System.Collections;
using System.Linq;

public class Game_rules
{
    public int Left_boundary = -5;
    public int Right_boundary = 6;
    public int Bottom_boundary = 40;
    public int total_columns = 11;
    public int longest_shape = 4;
    public Vector3 block_dismiss_vector = new Vector3(5, 8);
    //time interval between each down move of each piece ~ level difficulty
    public float DropSpeed = 0.5f;

    public void Make_new_process(Player Current_Player)
    {
        Current_Player.Player_Current_Shape = Current_Player.Player_Next_Shape;
        place_blocks(Current_Player);
        Current_Player.Player_Next_Shape = new Shape();
    }

    public void place_blocks(Player Current_Player)
    {
        foreach(Block current_block in Current_Player.Player_Current_Shape.shape_parts)
        {
            Current_Player.Lines[(int)current_block.position.y + 20] += (int) Mathf.Pow(2, current_block.position.x);
        }
    }
    public bool Lines_room_check(Player Current_Player)
    {
        Shape CurrentShape = Current_Player.Player_Current_Shape;
        foreach (Block check_empty_space in CurrentShape.shape_parts)
        {
            foreach (int line in Current_Player.Lines)
            {
                if (check_empty_space.position.y == Current_Player.Lines.IndexOf(line))
                {
                    int block_x_index = (int) Mathf.Pow(2, check_empty_space.position.x);
                    string linehere = System.Convert.ToString(line, 2);
                    linehere = '1' + linehere + '1';
                    if (linehere[block_x_index] == '1') return true;
                    if (check_empty_space.position.y < 1 - Bottom_boundary) return true;
                }
            }
        }
        return false;
    }
    public void Lines_Full_check(Player Current_Player)
    {
        foreach (int line in Current_Player.Lines)
        {
            if (line == 2047)
            {
                Current_Player.Lines.Remove(line);
                Current_Player.Lines.Add(0);
            }
        }
    }


    public void Move_left(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        Current_Shape.Shape_move_left();
        if (Lines_room_check(Current_Player))
        {
            Current_Shape.Shape_move_right();
        }
    }
    public void Move_right(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        Current_Shape.Shape_move_right();
        if (Lines_room_check(Current_Player))
        {
            Current_Shape.Shape_move_left();
        }
    }
    public void Move_down(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        Current_Shape.Shape_move_down();
        if (Lines_room_check(Current_Player))
        {
            Current_Shape.Shape_move_up();
        }
    }
    public void Rotate_clock(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        Current_Shape.Shape_rotate_anticlockwise();
        if (Lines_room_check(Current_Player))
        {
            Current_Shape.Shape_rotate_clockwise();
        }
    }
    public void Rotate_anti(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        Current_Shape.Shape_rotate_clockwise();
        if (Lines_room_check(Current_Player))
        {
            Current_Shape.Shape_rotate_anticlockwise();
        }
    }

}
