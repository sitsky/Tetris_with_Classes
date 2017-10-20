﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Main : MonoBehaviour {

    //Play area
    int Left_boundary = -5;
    int Right_boundary = 6;
    int Bottom_boundary = 20;

    public Shape Next_Shape;
    public Shape Current_Shape;
    List<Shape> Active_Shapes = new List<Shape>();
    int Blocks_in_Shape;

    float last_drop;
    float DropSpeed = 0.5f;
    Text text_box;

    public bool Render_Switch;

    // Use this for initialization
    void Start () {
        last_drop = 0;
        Render_Switch = false;

        text_box = GetComponent<Text>();

<<<<<<< HEAD
        Current_Shape = new Shape();
        Blocks_in_Shape = Current_Shape.shape_parts.Length;
        Active_Shapes.Add(Current_Shape);
=======
>>>>>>> parent of 3f7420f... Logic fixed :)
        Next_Shape = new Shape();
        Active_Shapes.Add(Next_Shape);
        Next_Shape = new Shape();
        display();
        
    }

    // Update is called once per frame
    void Update() {
        Current_Shape = Active_Shapes[Active_Shapes.Count -1];
        Blocks_in_Shape = Current_Shape.Block_positions.Length;
        bool no_room = false;



        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Current_Shape.move_left();
            no_room = Check_For_Room();
            if (no_room)
            {
                Current_Shape.move_right();
                Active_Shapes.Add(Next_Shape);
                Next_Shape = new Shape();
            }
            if (Check_For_Boundaries())
            {
                Current_Shape.move_right();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Current_Shape.move_right();
            no_room = Check_For_Room();
            if (no_room)
            {
                Current_Shape.move_left();
                Active_Shapes.Add(Next_Shape);
                Next_Shape = new Shape();
            }
            if (Check_For_Boundaries())
            {
                Current_Shape.move_left();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
            Current_Shape.rotate_clockwise();
            no_room = Check_For_Room();
            if (no_room)
            {
                Current_Shape.rotate_anticlockwise();
                Active_Shapes.Add(Next_Shape);
                Next_Shape = new Shape();
            }
            if (Check_For_Boundaries())
            {
                Current_Shape.rotate_anticlockwise();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
      
            Current_Shape.rotate_anticlockwise();
            no_room = Check_For_Room();
            if (no_room)
            {
                Current_Shape.rotate_clockwise();
                Active_Shapes.Add(Next_Shape);
                Next_Shape = new Shape();
            }
            if (Check_For_Boundaries())
            {
                Current_Shape.rotate_clockwise();
            }

        }
        if (Time.time - last_drop > DropSpeed)
        {
            last_drop = Time.time;
            Current_Shape.move_down();
            no_room = Check_For_Room();
            if (no_room)
            {
                Current_Shape.move_up();
                Active_Shapes.Add(Next_Shape);
                Next_Shape = new Shape();
            }
            else
            {
                for (int part_of_block = 0; part_of_block < Blocks_in_Shape; part_of_block++)
                {
                    if (Current_Shape.Block_positions[part_of_block].position.y < -Bottom_boundary)
                    {
                        Active_Shapes.Add(Next_Shape);
                        Next_Shape = new Shape();
                    }
                }
            }
            Check_Lines();
        }

        if (!Render_Switch)
        {
            display();
        }
        else { }//TODO;
    }


    public void Check_Lines()
    {
        int fullrow;
        for (int row = 0; row < Bottom_boundary; row++)
        {
            fullrow = 0;
            foreach (Shape active_shape in Active_Shapes)
            {
                foreach (Block part_of_shape in active_shape.shape_parts)
                {
                    if (part_of_shape.position.y == (row - Bottom_boundary))
                    {
                        Debug.Log("Row: " + row.ToString() + " Y: " + part_of_shape.position.y.ToString());
                        part_of_shape.stay_alive = false;
                        fullrow++;
                    }
                }
<<<<<<< HEAD
            }
            if (fullrow > 11) //FullRow space 
            {
                row--;
            for (int to_die_shape = 0; to_die_shape < Active_Shapes.Count; to_die_shape++)
                {
                    for (int to_die_block = 0; to_die_block < Blocks_in_Shape; to_die_block++)
                    {
                        if (!(Active_Shapes[to_die_shape].shape_parts[to_die_block].stay_alive))
                        {
                            Active_Shapes[to_die_shape].shape_parts[to_die_block].position = new Vector2(5, 8);
                        }
                    }
                }
                foreach (Shape active_shape in Active_Shapes)
                {
                    bool to_move = false;
                    foreach (Block part_of_shape in active_shape.shape_parts)
=======
                if (fullrow > 5) //FullRow space 
                {
                    for (int to_die_shape = 0; to_die_shape < Active_Shapes.Count; to_die_shape++)
>>>>>>> parent of 3f7420f... Logic fixed :)
                    {
                        for (int to_die_block =0; to_die_block < Blocks_in_Shape; to_die_block++)
                        {
                            if(!(Active_Shapes[to_die_shape].Block_positions[to_die_block].stay_alive))
                            {
                                Debug.Log("Pieces dying: ");
                                Active_Shapes[to_die_shape].Block_positions[to_die_block].position = new Vector2(5,5);
                            }
                        }
                    }
                }
                else
                {
                    for (int to_survive_shape = 0; to_survive_shape < Active_Shapes.Count; to_survive_shape++)
                    {
<<<<<<< HEAD
                        if (!(Active_Shapes[to_survive_shape].shape_parts[to_survive_block].stay_alive))
                        {
                            Active_Shapes[to_survive_shape].shape_parts[to_survive_block].stay_alive = true;
=======
                        for (int to_survive_block = 0; to_survive_block < Blocks_in_Shape; to_survive_block++)
                        {
                            if (!(Active_Shapes[to_survive_shape].Block_positions[to_survive_block].stay_alive))
                            {
                                Active_Shapes[to_survive_shape].Block_positions[to_survive_block].stay_alive = true;
                            }
>>>>>>> parent of 3f7420f... Logic fixed :)
                        }
                    }

                }
            }
        }
    }

<<<<<<< HEAD
    public void make_new_process()
    {
        Active_Shapes.Add(Next_Shape);
        Current_Shape = Next_Shape;
        Blocks_in_Shape = Current_Shape.shape_parts.Length;
        Next_Shape = new Shape();
    }
    public bool Check_For_NO_Room()
=======
    public bool Check_For_Boundaries()
>>>>>>> parent of 3f7420f... Logic fixed :)
    {
        bool hitting_walls = false;
        for (int parts_of_shape = 0; parts_of_shape < Blocks_in_Shape; parts_of_shape++)
        {
<<<<<<< HEAD
            foreach (Block in_first_Shape in Current_Shape.shape_parts)
            {
                if (in_first_Shape.position.y < -19) return true;
            }
=======
            if (Current_Shape.Block_positions[parts_of_shape].position.x < Left_boundary) hitting_walls = true;
            if (Current_Shape.Block_positions[parts_of_shape].position.x > Right_boundary) hitting_walls = true;
>>>>>>> parent of 3f7420f... Logic fixed :)
        }
        return hitting_walls;
    }

    public bool Check_For_Room()
    {
        if (Active_Shapes.Count > 1)
        {
            foreach (Block check_empty_space in Current_Shape.shape_parts)
            {
                foreach (Shape active_shape in Active_Shapes)
                {
                    foreach (Block occupied_space in active_shape.Block_positions)
                    {
<<<<<<< HEAD
                        foreach (Block occupied_space in active_shape.shape_parts)
=======
                        if (check_empty_space.position.Equals(occupied_space.position))
>>>>>>> parent of 3f7420f... Logic fixed :)
                        {
                            return true;
                        }
                    }
                }

<<<<<<< HEAD
        }
        if (Check_For_Boundaries())
        {
            Current_Shape.move_right();
        }
    }
    public void move_right()
    {
        Current_Shape.move_right();
        if (Check_For_NO_Room())
        {
            Current_Shape.move_left();
            make_new_process();
        }
        if (Check_For_Boundaries())
        {
            Current_Shape.move_left();
        }
    }
    public void rotate_clock()
    {
        Current_Shape.rotate_clockwise();
        if (Check_For_NO_Room())
        {
            Current_Shape.rotate_anticlockwise();
            make_new_process();
        }
        if (Check_For_Boundaries())
        {
            Current_Shape.rotate_anticlockwise();
        }
    }
    public void rotate_anti()
    {
        Current_Shape.rotate_anticlockwise();
        if (Check_For_NO_Room())
        {
            Current_Shape.rotate_clockwise();
            make_new_process();
        }
        if (Check_For_Boundaries())
        {
            Current_Shape.rotate_clockwise();
        }
    }
    public bool Check_For_Boundaries()
    {
        for (int parts_of_shape = 0; parts_of_shape < Blocks_in_Shape; parts_of_shape++)
        {
            if (Current_Shape.shape_parts[parts_of_shape].position.x < Left_boundary) return true;
            if (Current_Shape.shape_parts[parts_of_shape].position.x > Right_boundary) return true;
=======
            }
>>>>>>> parent of 3f7420f... Logic fixed :)
        }
        return false;
    }

    public void display()
    {
        string to_text_box = "";
        int[,] the_game_view = new int[13,30];

        for (int part_of_preview = 0; part_of_preview < Blocks_in_Shape; part_of_preview++)
        {
            int col = (int)Next_Shape.shape_parts[part_of_preview].position.x +1; //rendering +1 move
            int row = (int)Next_Shape.shape_parts[part_of_preview].position.y + 29; //rendering +29 move
            the_game_view[col, row] = 1;
        }      
        foreach(Shape shape_in_game in Active_Shapes)
        {
            for(int part_of_shape = 0; part_of_shape < Blocks_in_Shape; part_of_shape++)
            {
                int column = (int)shape_in_game.shape_parts[part_of_shape].position.x - Left_boundary;
                int row = (int)shape_in_game.shape_parts[part_of_shape].position.y + Bottom_boundary +1;
                if (row < 0)
                {
                    row = 29;
                    column = 12;
                }
                the_game_view[column, row] = 1;
             }
        }
        for (int line = 29; line >= 0; line--)
        {
            to_text_box = to_text_box + "|";
            for (int column = 0; column < 12; column++)
            {
                if (the_game_view[column, line] == 1)
                {
                    to_text_box = to_text_box + "X";
                }
                else
                {
                    to_text_box = to_text_box + " ";
                }
            }
            to_text_box = to_text_box + "|\n";
        }
        text_box.text = to_text_box;
    }
}
