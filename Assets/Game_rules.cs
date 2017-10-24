using UnityEngine;
using System.Collections;

public class Game_rules
{
    public int Left_boundary = -5;
    public int Right_boundary = 6;
    public int Bottom_boundary = 20;
    public int total_columns = 11;
    public int longest_shape = 4;
    public Vector3 block_dismiss_vector = new Vector3(5, 8);
    //time interval between each down move of each piece ~ level difficulty
    public float DropSpeed = 0.5f;

    public void Make_new_process(Player Current_Player)
    {
        Current_Player.Active_Shapes.Add(Current_Player.Player_Next_Shape);
        Current_Player.Player_Current_Shape = Current_Player.Player_Next_Shape;
        Current_Player.Player_Next_Shape = new Shape();
    }
    public void Check_Lines(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            int fullrow;
            for (int row = 0; row < Bottom_boundary; row++)
            {
                fullrow = 0;
                foreach (Shape active_shape in Current_Player.Active_Shapes)
                {
                    foreach (Block part_of_shape in active_shape.shape_parts)
                    {
                        if (part_of_shape.position.y == (row - Bottom_boundary))
                        {
                            part_of_shape.stay_alive = false;
                            fullrow++;
                        }
                    }
                }
                if (fullrow > total_columns)
                {
                    row--;
                    for (int to_die_shape = 0; to_die_shape < Current_Player.Active_Shapes.Count; to_die_shape++)
                    {
                        for (int to_die_block = 0; to_die_block < Current_Player.Player_Blocks_in_Shape; to_die_block++)
                        {
                            if (!(Current_Player.Active_Shapes[to_die_shape].shape_parts[to_die_block].stay_alive))
                            {
                                Current_Player.Active_Shapes[to_die_shape].shape_parts[to_die_block].position = block_dismiss_vector;
                            }
                        }
                    }
                    foreach (Shape active_shape in Current_Player.Active_Shapes)
                    {
                        bool to_move = false;
                        foreach (Block part_of_shape in active_shape.shape_parts)
                        {
                            if ((part_of_shape.position.y > (row - Bottom_boundary)) && (part_of_shape.position.y < 0))
                            {
                                to_move = true;
                            }
                        }
                        if (to_move) active_shape.Shape_move_down();
                    }
                }
                else
                {
                    for (int to_survive_shape = 0; to_survive_shape < Current_Player.Active_Shapes.Count; to_survive_shape++)
                    {
                        for (int to_survive_block = 0; to_survive_block < Current_Player.Player_Blocks_in_Shape; to_survive_block++)
                        {
                            if (!(Current_Player.Active_Shapes[to_survive_shape].shape_parts[to_survive_block].stay_alive))
                            {
                                Current_Player.Active_Shapes[to_survive_shape].shape_parts[to_survive_block].stay_alive = true;
                            }
                        }
                    }
                }
            }
        }
    }
    public bool Check_For_NO_Room(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            Shape Current_Shape = Current_Player.Player_Current_Shape;

            if (Current_Player.Active_Shapes.Count == 1)
            {
                foreach (Block in_first_Shape in Current_Shape.shape_parts)
                {
                    if (in_first_Shape.position.y < 1 - Bottom_boundary) return true;
                }
            }
            else
            {
                foreach (Block check_empty_space in Current_Shape.shape_parts)
                {
                    if (check_empty_space.position.y < -longest_shape)
                    {
                        foreach (Shape active_shape in Current_Player.Active_Shapes.GetRange(0, Current_Player.Active_Shapes.Count - 1))
                        {
                            foreach (Block occupied_space in active_shape.shape_parts)
                            {

                                if (check_empty_space.position.Equals(occupied_space.position))
                                    return true;
                                else if (check_empty_space.position.y < 1 - Bottom_boundary)
                                    return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    public bool Check_For_Boundaries(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            for (int parts_of_shape = 0; parts_of_shape < Current_Player.Player_Blocks_in_Shape; parts_of_shape++)
            {
                if (Current_Player.Player_Current_Shape.shape_parts[parts_of_shape].position.x < Left_boundary) return true;
                if (Current_Player.Player_Current_Shape.shape_parts[parts_of_shape].position.x > Right_boundary) return true;
            }
        }
        return false;       
    }
    public void Move_left(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            Shape Current_Shape = Current_Player.Player_Current_Shape;
            Current_Shape.Shape_move_left();
            if (Check_For_NO_Room(Current_Player))
            {
                Current_Shape.Shape_move_right();

            }
            if (Check_For_Boundaries(Current_Player))
            {
                Current_Shape.Shape_move_right();
            }
            Current_Player.Player_Current_Shape = Current_Shape;
        }
    }
    public void Move_right(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            Shape Current_Shape = Current_Player.Player_Current_Shape;
            Current_Shape.Shape_move_right();
            if (Check_For_NO_Room(Current_Player))
            {
                Current_Shape.Shape_move_left();
            }
            if (Check_For_Boundaries(Current_Player))
            {
                Current_Shape.Shape_move_left();
            }
            Current_Player.Player_Current_Shape = Current_Shape;
        }
    }

    public void Move_down(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            Shape Current_Shape = Current_Player.Player_Current_Shape;
            Current_Shape.Shape_move_down();
            if (Check_For_NO_Room(Current_Player))
            {
                Current_Shape.Shape_move_up();
            }
            if (Check_For_Boundaries(Current_Player))
            {
                Current_Shape.Shape_move_up();
            }
            Current_Player.Player_Current_Shape = Current_Shape;
        }
    }

    public void Rotate_clock(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            Shape Current_Shape = Current_Player.Player_Current_Shape;
            Current_Shape.Shape_rotate_anticlockwise();
            if (Check_For_NO_Room(Current_Player))
            {
                Current_Shape.Shape_rotate_clockwise();
            }
            if (Check_For_Boundaries(Current_Player))
            {
                Current_Shape.Shape_rotate_clockwise();
            }
            Current_Player.Player_Current_Shape = Current_Shape;
        }
    }
    public void Rotate_anti(Player Current_Player)
    {
        if (check_out_spawn_area(Current_Player))
        {
            Shape Current_Shape = Current_Player.Player_Current_Shape;
            Current_Shape.Shape_rotate_clockwise();
            if (Check_For_NO_Room(Current_Player))
            {
                Current_Shape.Shape_rotate_anticlockwise();
            }
            if (Check_For_Boundaries(Current_Player))
            {
                Current_Shape.Shape_rotate_anticlockwise();
            }
            Current_Player.Player_Current_Shape = Current_Shape;
        }
    }
    public bool check_out_spawn_area(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        if (Current_Shape.shape_parts[0].position.y < -longest_shape)
        {
            return true;
        }
        return false;
    }
}
