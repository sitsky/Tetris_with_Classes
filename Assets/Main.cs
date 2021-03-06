﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public enum Motion_keys : int { left, right, up, down, a, d, w, s};

public static class Tetris_consts
{
    public const int keys_per_player = 4;
}
public class Main : MonoBehaviour {

    //TXT translations to look nice
    int player_text_shift_x = 200;
    int next_player_text_shift_x = 400;
    int player_text_shift_y = 390;

    //Object and List for the TXT based game
    public GameObject Play_Screen;
    List<Text> text_box = new List<Text>();

    public GameObject PlayArea3D;
    public GameObject block3D;
    List<GameObject> to_render = new List<GameObject>();
    GameObject[] areas_to_render = new GameObject[2];
    int first_player_render_shift_x = 30;
    int next_player_render_separation = -60;
    int next_player_render_shift = 30;
    int player_render_shift_y = 20;
    int next_piece_render_shift = 5;


    //Switch on for Rendering, Object for rendering, list to hold the blocks for rendering
    public bool Render_Switch;
    //Switch on for 2 players
    public bool two_players;
    
    //List of players playing and List for their keys. 
    public List<Player> Tetris_Players = new List<Player>();
    Motion_keys[] Current_keys = new Motion_keys[5];
    Motion_keys last_given = Motion_keys.left;
    Game_rules Tetris_Rules = new Game_rules();

    void Start() {
        Debug.Log("Local run");
        if (two_players)
        {
            Tetris_Players.Add(Create_Player(0));
            Tetris_Players.Add(Create_Player(0));
            foreach (Player Current_Player in Tetris_Players)
            {
                SetupPlayerControls(Current_Player, last_given);
                last_given += Tetris_consts.keys_per_player;
                if (last_given > (Motion_keys)(Tetris_Players.Count * Tetris_consts.keys_per_player)) last_given = Motion_keys.left;
            }
        }
        else
        {
            Tetris_Players.Add(Create_Player(0));
        }
 
        GameObject canvas = GameObject.Find("Canvas");
        Destroy(canvas);
        areas_to_render[0] = (Instantiate(PlayArea3D));
        areas_to_render[1] = (Instantiate(PlayArea3D));
        areas_to_render[1].transform.position = areas_to_render[0].transform.position + new Vector3(next_player_render_separation, 0, 0);
    }
/*
Client first piece slow.
Line checking and block removal not perfect.
Code looks shit on Main.

*/
    void Update() {

        if (Tetris_Players.Count > 0)
        {

            foreach (Player Current_Player in Tetris_Players)
            {
                Shape Current_Shape = Current_Player.Player_Current_Shape;
                RetrievePlayerControls(Current_Player);
                PlayerControls(Current_Player);
                if (Time.time - Current_Player.last_drop > Tetris_Rules.DropSpeed)
                {
                    Current_Player.last_drop = Time.time;
                    Current_Player.Player_Current_Shape.Shape_move_down();
                    Tetris_Rules.Move_down(Current_Player);
                    if (Tetris_Rules.Check_For_NO_Room(Current_Player))
                    {
                        Current_Player.Player_Current_Shape.Shape_move_up();                       
                        Tetris_Rules.Check_Lines(Current_Player);
                        Tetris_Rules.SpawnNewShape(Current_Player);
                    }
                }
            }
            if (Render_Switch)
            {
                Render_Player_Blocks();
            }
            else
            {
                TXT_Display();
            }
        }
    }


    void PlayerControls(Player Current_Player)
    { 
        if (Input.GetKeyDown(Current_Player.mymotion[0].ToString()))
        {
            Tetris_Rules.Move_left(Current_Player);
        }
        if (Input.GetKeyDown(Current_Player.mymotion[1].ToString()))
        {
            Tetris_Rules.Move_right(Current_Player);
        }
        if (Input.GetKeyDown(Current_Player.mymotion[2].ToString()))
        {
            Tetris_Rules.Rotate_clock(Current_Player);
        }
        if (Input.GetKey(Current_Player.mymotion[3].ToString()))
        {
            Tetris_Rules.Move_down(Current_Player);
        }
    }

    public void RetrievePlayerControls(Player Current_Player)
    {
        for (int keys = 0; keys < Tetris_consts.keys_per_player; keys++)
        {
            Current_keys[keys] =  Current_Player.mymotion[keys];
        }
    }
    public void SetupPlayerControls(Player Current_Player, Motion_keys last_given)
    {
        for (int keys = 0; keys < Tetris_consts.keys_per_player; keys++)
        {
            Current_Player.mymotion[keys] = last_given + keys;
        }
    }
    public Player Create_Player(int ID) //TODO: Move some TXT setup to TXT_Display()
    {
        Player new_player = new Player(ID);
        new_player.last_drop = 0;
        new_player.Player_Current_Shape = new Shape();
        new_player.Active_Shapes.Add(new_player.Player_Current_Shape);
        new_player.Player_Blocks_in_Shape = new_player.Player_Current_Shape.shape_parts.Count;
        new_player.Player_Next_Shape = new Shape();
        SetupPlayerControls(new_player, 0);
        return new_player;
    }


    public void Render_Player_Blocks() //TODO:spawn area
    {
        GameObject[] to_die = GameObject.FindGameObjectsWithTag("rendered_block");
        foreach (GameObject created_block in to_die) { Destroy(created_block); }

        next_player_render_shift = first_player_render_shift_x;
        foreach (Player Current_Player in Tetris_Players)
        {
            next_player_render_shift += Tetris_Players.IndexOf(Current_Player) * next_player_render_separation;
            foreach (Block block_to_render in Current_Player.Player_Next_Shape.shape_parts)
            {
                float temp_x = block_to_render.position.x + next_player_render_shift - next_piece_render_shift;
                float temp_y = block_to_render.position.y + player_render_shift_y + next_piece_render_shift;
                to_render.Add(Instantiate(block3D));
                to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, temp_y, 0);
                to_render[to_render.Count - 1].GetComponent<MeshRenderer>().material.color = Current_Player.Player_Next_Shape.mycolor;
            }

            foreach (Shape shape_to_render in Current_Player.Active_Shapes)
            {
                foreach (Block block_to_render in shape_to_render.shape_parts)
                {
                    float temp_x = block_to_render.position.x + next_player_render_shift;
                    float temp_y = block_to_render.position.y + player_render_shift_y;
                    to_render.Add(Instantiate(block3D));
                    to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, temp_y, 0);
                    to_render[to_render.Count - 1].GetComponent<MeshRenderer>().material.color = shape_to_render.mycolor;
                }

                /*Make landed pieces one color
                if(Current_Player.Active_Shapes.Count >2 )
                {
                    if(Current_Player.Active_Shapes.IndexOf(shape_to_render)< Current_Player.Active_Shapes.Count - 1)
                    {
                        foreach (Block block_to_render in shape_to_render.shape_parts)
                        {
                            float temp_x = block_to_render.position.x + next_player_render_shift;
                            float temp_y = block_to_render.position.y + player_render_shift_y;
                            to_render.Add(Instantiate(block3D));
                            to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, temp_y, 0);
                            to_render[to_render.Count - 1].GetComponent<MeshRenderer>().material.color = Color.gray;
                        }
                    }

                }*/
            }
        }
    }
    public void TXT_Display()
    {
        int y_shift_for_TXT = 29;
        int x_shift_for_TXT = 1;
        foreach (Player Current_Player in Tetris_Players)
        {
            string to_text_box = "";
            int[,] the_game_view = new int[13, 50];

            for (int part_of_preview = 0; part_of_preview < Current_Player.Player_Blocks_in_Shape; part_of_preview++)
            {
                int col = (int)Current_Player.Player_Next_Shape.shape_parts[part_of_preview].position.x + x_shift_for_TXT; //rendering +1 move
                int row = (int)Current_Player.Player_Next_Shape.shape_parts[part_of_preview].position.y + y_shift_for_TXT; //rendering +29 move
                the_game_view[col, row] = 1;
            }

            foreach (Shape shape_in_game in Current_Player.Active_Shapes)
                foreach(Block part_of_shape in shape_in_game.shape_parts)
                {
                    int column = (int)part_of_shape.position.x - Tetris_Rules.Left_boundary;
                    int row = (int)part_of_shape.position.y + Tetris_Rules.Bottom_boundary + 1;
                    the_game_view[column, row] = 1;
                }

                /*for (int part_of_shape = 0; part_of_shape < Current_Player.Player_Blocks_in_Shape; part_of_shape++)
                {
                    Debug.Log("ps: " + part_of_shape.ToString());
                    int column = (int)shape_in_game.shape_parts[part_of_shape].position.x - Tetris_Rules.Left_boundary;
                    int row = (int)shape_in_game.shape_parts[part_of_shape].position.y + Tetris_Rules.Bottom_boundary + 1;
                    the_game_view[column, row] = 1;
                }*/
            
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
    public void TXT_Setup()
    {
        for (int twoplayers = 0; twoplayers < 2; twoplayers++)
        {
            int shift_text_boxes = player_text_shift_x - (text_box.Count - 1) * next_player_text_shift_x;
            GameObject canvas = GameObject.Find("Canvas");
            GameObject temp_play_screen = (GameObject)Instantiate(Play_Screen);
            temp_play_screen.transform.SetParent(canvas.transform);
            temp_play_screen.transform.position = new Vector3(shift_text_boxes, player_text_shift_y, 0);
            text_box.Add(temp_play_screen.GetComponent<Text>());
        }
    }
    
}
