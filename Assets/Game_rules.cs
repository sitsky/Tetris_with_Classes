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

    public void Make_new_shape(Player Current_Player)
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
            Current_Player.Lines[part.y].line.Set(part.x, true);
        }
    }

    public void remove_blocks(Player Current_Player)
    {
        Shape CurrentShape = Current_Player.Player_Current_Shape;
        foreach (Block part in CurrentShape.shape_parts)
        {
            Current_Player.Lines[part.y].line.Set(part.x, false);
        }
    }

    public void Lines_Full_check(Player Current_Player)
    {
        foreach (C_line line in Current_Player.Lines)
        {
            line.line[0] = true;
            line.line[11] = true;
            BitArray T = new BitArray(12);
            T.SetAll(true);
            T = T.And(line.line);
            //Debug.Log(T.ToString());
            bool NOTfullLine = T.Cast<bool>().Contains(false);
            //Debug.Log(NOTfullLine.ToString());
            if (!NOTfullLine)
            {
                Debug.Log("Lines_Full_check");
                Current_Player.Lines.Remove(line);
                Current_Player.Lines.Add(new C_line());
            }
        }
    }

    public void Move_Player(Player Current_Player, int move_horizontal, int move_vertical)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        foreach (Block part in Current_Shape.shape_parts)
        {
            int new_y = part.y + move_vertical;
            int new_x = part.x + move_horizontal;
            Block new_shape = new Block(new_x, new_y);
            bool in_shape = false;

            for (int new_part = 0; new_part < 4; new_part++)
            {
                if ((Current_Shape.shape_parts[new_part].x == new_x) && (Current_Shape.shape_parts[new_part].y == new_y))               
                    in_shape = true;               
            }
            if (!in_shape)
            {

                if ((!Current_Player.Lines[new_y].line[new_x].Equals(true)) && !(new_y > 15))
                {

                    remove_blocks(Current_Player);
                    foreach (Block part2move in Current_Shape.shape_parts)
                    {
                        part2move.y = part2move.y + move_vertical;
                        part2move.x += move_horizontal;
                    }
                    place_blocks(Current_Player);                   
                }
                else
                {
                    if (new_y > 15)
                    {
                        Make_new_shape(Current_Player);
                        return;
                    }
                    if (new_y > part.y)
                    {
                        Make_new_shape(Current_Player);
                        return;
                    }
                    return;
                }
            }
        }   
}
   /* public void Rotate_clock(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        foreach (Block part in Current_Shape.shape_parts)
        {
            int old_y = -part.x;
            int new_x = part.y;
            if (Current_Player.Lines[old_y].line[new_x])
                if (part.y > old_y)
                { Make_new_shape(Current_Player); }
                else { return; }
        }
        remove_blocks(Current_Player);
        foreach (Block part in Current_Shape.shape_parts)
        {
            if (part.x < 0)
                part.x += 1;
            else if (part.x > 9)
                part.x -= 1;
        }
        place_blocks(Current_Player);
    }*/
}
