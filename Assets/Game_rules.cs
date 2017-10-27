using UnityEngine;
using System.Collections;
using System.Linq;

public class Game_rules : Player
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
        Debug.Log("Makenew");
        Current_Player.Player_Current_Shape = Current_Player.Player_Next_Shape;
        place_blocks(Current_Player);
        Current_Player.Player_Next_Shape = new Shape();
    }

    public void place_blocks(Player Current_Player)
    {
        Shape CurrentShape = Current_Player.Player_Current_Shape;
        foreach (Block part in CurrentShape.shape_parts)
        {
            Current_Player.Lines[-part.y].line.Set(part.x, true);
        }
    }

    public void remove_blocks(Player Current_Player)
    {
        Shape CurrentShape = Current_Player.Player_Current_Shape;
        foreach (Block part in CurrentShape.shape_parts)
        {
            Current_Player.Lines[-part.y].line.Set(part.x, false);
        }
    }

    public bool Lines_NO_room_check(Player Current_Player)
    {
        Debug.Log("Lines_NO_room_check");
        Shape CurrentShape = Current_Player.Player_Current_Shape;
        foreach (Block part in CurrentShape.shape_parts)
        {
            Debug.Log(part.y.ToString());
            if (part.y == -18)
            {
                place_blocks(Current_Player);
                Make_new_process(Current_Player);
                return false;
            }
            if (Current_Player.Lines[-part.y].line[part.x]) return true;
        }
        return false;
    }

    public void Lines_Full_check(Player Current_Player)
    {
        Debug.Log("Lines_Full_check");
        foreach (C_line line in Current_Player.Lines)
        {
            BitArray T = new BitArray(10);
            T.SetAll(true);
            T = T.And(line.line);
            bool fullLine = T.Cast<bool>().Contains(true);
            if (fullLine)
            {
                Current_Player.Lines.Remove(line);
                Current_Player.Lines.Add(new C_line());
            }
        }
    }

    public bool Left_right_check(Player Current_Player)
    {
        Shape CurrentShape = Current_Player.Player_Current_Shape;
        foreach (Block part in CurrentShape.shape_parts)
        {
            if ((part.x < 1) || (part.x > 8))
            return true;
        }
        return false;
    }

    public void Move_left(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        remove_blocks(Current_Player);
        Current_Shape.Shape_move_left();
        if ((Lines_NO_room_check(Current_Player)) || (Left_right_check(Current_Player)))
        {
            Current_Shape.Shape_move_right();
        }
        place_blocks(Current_Player);
    }
    public void Move_right(Player Current_Player)
    {       
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        remove_blocks(Current_Player);
        Current_Shape.Shape_move_right();
        if ((Lines_NO_room_check(Current_Player)) || (Left_right_check(Current_Player)))
        {
            Current_Shape.Shape_move_left();
        }
        place_blocks(Current_Player);
    }
    public void Move_down(Player Current_Player)
    {
        Debug.Log("MoveDown");
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        remove_blocks(Current_Player);
        Current_Shape.Shape_move_down();
        if (Lines_NO_room_check(Current_Player))
        {
            Current_Shape.Shape_move_up();
            place_blocks(Current_Player);
            Make_new_process(Current_Player);
        }
        place_blocks(Current_Player);
    }

    public void Rotate_clock(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        remove_blocks(Current_Player);
        Current_Shape.Shape_rotate_clockwise();
        if (Lines_NO_room_check(Current_Player))
        {
            Current_Shape.Shape_rotate_anticlockwise();
        }
        place_blocks(Current_Player);
    }
}
