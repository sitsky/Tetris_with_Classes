﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;


public enum Motion_keys { left, right, up, down,  a, d, w, s};

public class Main : MonoBehaviour {

    //Play area
    int Left_boundary = -5;
    int Right_boundary = 6;
    int Bottom_boundary = 20;
    int total_columns = 11;

    int longest_shape = 4;
    //time interval between each down move of each piece ~ level difficulty
    float DropSpeed = 0.5f;

    //TXT translations to look nice
    int player_text_shift_x = 200;
    int player_text_separation = 60;
    int next_player_text_shift_x = 400;
    int player_text_shift_y = 390;

    //Object and List for the TXT based game
    public GameObject Play_Screen;
    List<Text> text_box = new List<Text>();
    Vector3 block_dismiss_vector = new Vector3(5, 8);

    //Switch on for Rendering, Object for rendering, list to hold the blocks for rendering
    public bool Render_Switch;
    public GameObject block3D;
    List<GameObject> to_render = new List<GameObject>();
    int player_render_shift_x = -30;
    int next_player_render_separation = 60;
    int next_player_render_shift = 0;
    int player_render_shift_y = 20;

    //Switch on for 2 players
    public bool two_players;
    int keys_per_player = 4;

    //List of players playing and List for their keys. 
    public List<Player> Tetris_Players = new List<Player>();
    public Motion_keys[] Current_keys = new Motion_keys[4];

    void Start() {           
        GameObject canvas = GameObject.Find("Canvas"); 
        Motion_keys last_given = Motion_keys.left;

        if (two_players)
        {
            Create_Player();
            Create_Player();
        }
        else
        {
            Create_Player();
        }

        foreach (Player Current_Player in Tetris_Players)
        {
            Give_Player_Keys(Current_Player, last_given);
            last_given += keys_per_player;
            if (last_given > (Motion_keys)(Tetris_Players.Count * keys_per_player)) last_given = Motion_keys.left;

            Current_Player.last_drop = 0;
            Current_Player.Player_Current_Shape = new Shape();
            Current_Player.Player_Blocks_in_Shape = Current_Player.Player_Current_Shape.shape_parts.Length;
            Current_Player.Active_Shapes.Add(Current_Player.Player_Current_Shape);
            Current_Player.Player_Next_Shape = new Shape();
        }
            if (Render_Switch)
            {
                Destroy(canvas);
                Render_Player_Blocks();
            }
            else
            {            
                Display();
            }               
    }

    // Update is called once per frame
    void Update() {
        next_player_render_shift = player_render_shift_x;
        foreach (Player Current_Player in Tetris_Players)
        {
            GetPlayersShapes(Current_Player);

            if (Input.GetKeyDown(Current_Player.mymotion[0].ToString()))
            {
                Move_left(Current_Player);
            }
            if (Input.GetKeyDown(Current_Player.mymotion[1].ToString()))
            {
                Move_right(Current_Player);
            }
            if (Input.GetKeyDown(Current_Player.mymotion[2].ToString()))
            {
                Rotate_clock(Current_Player);
            }
            if (Input.GetKeyDown(Current_Player.mymotion[3].ToString()))
            {
                Rotate_anti(Current_Player);
            }

            if (Time.time - Current_Player.last_drop > DropSpeed)
            {
                Current_Player.last_drop = Time.time;
                Current_Player.Player_Current_Shape.Shape_move_down();

                if (Check_For_NO_Room(Current_Player))
                {
                    Current_Player.Player_Current_Shape.Shape_move_up();
                    Check_Lines(Current_Player);
                    Make_new_process(Current_Player);
                }
            }
        }
        if (Render_Switch)
        {
            Render_Player_Blocks();
        }
        else
        {
            Display();
        }    
    }

    public void Render_Player_Blocks()
    {
        GameObject[] to_die = GameObject.FindGameObjectsWithTag("rendered_block");
        foreach (GameObject created_block in to_die) { Destroy(created_block); }

        foreach (Player Current_Player in Tetris_Players)
        {
            next_player_render_shift += Tetris_Players.IndexOf(Current_Player) * next_player_render_separation;
            foreach (Shape shape_to_render in Current_Player.Active_Shapes)
            {
                foreach (Block block_to_render in shape_to_render.shape_parts)
                {
                    float temp_x = block_to_render.position.x + next_player_render_shift;
                    float temp_y = block_to_render.position.y + player_render_shift_y;
                    to_render.Add(Instantiate(block3D));
                    to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, temp_y, 0);
                }
            }
        }
    }

    public void Display()
    {
        foreach (Player Current_Player in Tetris_Players)
        {
            GetPlayersShapes(Current_Player);
            string to_text_box = "";
            int[,] the_game_view = new int[13, 30];

            for (int part_of_preview = 0; part_of_preview < Current_Player.Player_Blocks_in_Shape; part_of_preview++)
            {
                int col = (int)Current_Player.Player_Next_Shape.shape_parts[part_of_preview].position.x + 1; //rendering +1 move
                int row = (int)Current_Player.Player_Next_Shape.shape_parts[part_of_preview].position.y + 29; //rendering +29 move
                the_game_view[col, row] = 1;
            }

            foreach (Shape shape_in_game in Current_Player.Active_Shapes)
            {
                for (int part_of_shape = 0; part_of_shape < Current_Player.Player_Blocks_in_Shape; part_of_shape++)
                {
                    int column = (int)shape_in_game.shape_parts[part_of_shape].position.x - Left_boundary;
                    int row = (int)shape_in_game.shape_parts[part_of_shape].position.y + Bottom_boundary + 1;
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
            text_box[Tetris_Players.IndexOf(Current_Player)].text = to_text_box;
        }
    }


    public void Make_new_process(Player Current_Player)
    { 
        Current_Player.Active_Shapes.Add(Current_Player.Player_Next_Shape);
        Current_Player.Player_Current_Shape = Current_Player.Player_Next_Shape;
        Current_Player.Player_Next_Shape = new Shape();
    }

    public void Create_Player()
    {
        int shift_text_boxes = player_text_shift_x - (text_box.Count - 1) * next_player_text_shift_x;
        GameObject canvas = GameObject.Find("Canvas");
        Tetris_Players.Add(new Player());
        GameObject temp_play_screen = (GameObject)Instantiate(Play_Screen);
        temp_play_screen.transform.SetParent(canvas.transform);
        temp_play_screen.transform.position = new Vector3(shift_text_boxes, player_text_shift_y, 0);
        text_box.Add(temp_play_screen.GetComponent<Text>());
    }
    public void GetPlayersShapes(Player Current_Player)
    {
        for (int keys = 0; keys < keys_per_player; keys++)
        {
            Current_keys[keys] = Current_Player.mymotion[keys];

        }
    }
    public void Give_Player_Keys(Player Assign_Keys, Motion_keys last_given)
    {
        for (int keys = 0; keys < keys_per_player; keys++)
        {
            Assign_Keys.mymotion[keys] = last_given + keys;

        }
    }
    public void Check_Lines(Player Current_Player)
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
                        //Debug.Log("Row: " + row.ToString() + " Y: " + part_of_shape.position.y.ToString());
                        part_of_shape.stay_alive = false;
                        fullrow++;
                    }
                }
            }
            if (fullrow > total_columns) //FullRow space 
            {
                row--;
                for (int to_die_shape = 0; to_die_shape < Current_Player.Active_Shapes.Count; to_die_shape++)
                {
                    for (int to_die_block = 0; to_die_block < Current_Player.Player_Blocks_in_Shape; to_die_block++)
                    {
                        if (!(Current_Player.Active_Shapes[to_die_shape].shape_parts[to_die_block].stay_alive))
                        {
                            Current_Player.Active_Shapes[to_die_shape].shape_parts[to_die_block].position = new Vector2(5, 8);
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
    public bool Check_For_NO_Room(Player Current_Player)
    {
        Shape Current_Shape = Current_Player.Player_Current_Shape;
        if (Current_Player.Active_Shapes.Count == 1)
        {
            foreach (Block in_first_Shape in Current_Shape.shape_parts)
            {
                if (in_first_Shape.position.y < -19) return true;
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
                            else if (check_empty_space.position.y < -19)
                                return true;
                        }
                    }
                }
                //else{} //TODO: GAMEOVER!
            }
        }
        return false;
    }
    public bool Check_For_Boundaries(Player Current_Player)
    {
        for (int parts_of_shape = 0; parts_of_shape < Current_Player.Player_Blocks_in_Shape; parts_of_shape++)
        {
            if (Current_Player.Player_Current_Shape.shape_parts[parts_of_shape].position.x < Left_boundary) return true;
            if (Current_Player.Player_Current_Shape.shape_parts[parts_of_shape].position.x > Right_boundary) return true;
        }
        return false;
    }
    public void Move_left(Player Current_Player)
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
    public void Move_right(Player Current_Player)
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
    public void Rotate_clock(Player Current_Player)
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
    public void Rotate_anti(Player Current_Player)
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
