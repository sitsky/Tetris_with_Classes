using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

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
                foreach (Block part_of_shape in active_shape.Block_positions)
                {
                    if (part_of_shape.position.y == (row - Bottom_boundary))
                    {
                        Debug.Log("Row: " + row.ToString() + " Y: " + part_of_shape.position.y.ToString());
                        part_of_shape.stay_alive = false;
                        fullrow++;
                    }
                }
                if (fullrow > 5) //FullRow space 
                {
                    for (int to_die_shape = 0; to_die_shape < Active_Shapes.Count; to_die_shape++)
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
                        for (int to_survive_block = 0; to_survive_block < Blocks_in_Shape; to_survive_block++)
                        {
                            if (!(Active_Shapes[to_survive_shape].Block_positions[to_survive_block].stay_alive))
                            {
                                Active_Shapes[to_survive_shape].Block_positions[to_survive_block].stay_alive = true;
                            }
                        }
                    }

                }
            }
        }
    }

    public bool Check_For_Boundaries()
    {
        bool hitting_walls = false;
        for (int parts_of_shape = 0; parts_of_shape < Blocks_in_Shape; parts_of_shape++)
        {
            if (Current_Shape.Block_positions[parts_of_shape].position.x < Left_boundary) hitting_walls = true;
            if (Current_Shape.Block_positions[parts_of_shape].position.x > Right_boundary) hitting_walls = true;
        }
        return hitting_walls;
    }

    public bool Check_For_Room()
    {
        if (Active_Shapes.Count > 1)
        {
            foreach (Block check_empty_space in Current_Shape.Block_positions)
            {
                foreach (Shape active_shape in Active_Shapes)
                {
                    foreach (Block occupied_space in active_shape.Block_positions)
                    {
                        if (check_empty_space.position.Equals(occupied_space.position))
                        {
                            return true;
                        }
                    }
                }

            }
        }
        return false;
    }

    public void display()
    {
        string to_text_box = "";
        int[,] the_game_view = new int[13,30];

        for (int part_of_preview = 0; part_of_preview < Blocks_in_Shape; part_of_preview++)
        {
            int col = (int)Next_Shape.Block_positions[part_of_preview].position.x +1; //rendering +1 move
            int row = (int)Next_Shape.Block_positions[part_of_preview].position.y + 29; //rendering +29 move
            the_game_view[col, row] = 1;
        }      
        foreach(Shape shape_in_game in Active_Shapes)
        {
            for(int part_of_shape = 0; part_of_shape < Blocks_in_Shape; part_of_shape++)
            {
                int column = (int)shape_in_game.Block_positions[part_of_shape].position.x - Left_boundary;
                int row = (int)shape_in_game.Block_positions[part_of_shape].position.y + Bottom_boundary +1;
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
