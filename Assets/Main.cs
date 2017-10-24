using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;


public enum Motion_keys { left, right, up, down,  a, d, w, s};


public class Main : MonoBehaviour {

    //TXT translations to look nice
    int player_text_shift_x = 200;
    int next_player_text_shift_x = 400;
    int player_text_shift_y = 390;

    //Object and List for the TXT based game
    public GameObject Play_Screen;
    List<Text> text_box = new List<Text>();
    
    //Switch on for Rendering, Object for rendering, list to hold the blocks for rendering
    public bool Render_Switch;
    public GameObject PlayArea3D;
    public GameObject block3D;
    List<GameObject> to_render = new List<GameObject>();
    GameObject[] areas_to_render = new GameObject[2];
    int first_player_render_shift_x = 30;
    int next_player_render_separation = -60;
    int next_player_render_shift = 30;
    int player_render_shift_y = 20;
    int next_piece_render_shift = 5;

    //Switch on for 2 players
    public bool two_players;
    int keys_per_player = 4;

    //List of players playing and List for their keys. 
    public List<Player> Tetris_Players = new List<Player>();
    public Motion_keys[] Current_keys = new Motion_keys[4];
    Game_rules Tetris_Rules = new Game_rules();

    //Networking
    public bool Network_Game = false; 
    public bool Set_As_Server;
    public int Port_of_server;
    public bool Set_As_Remote;
    public string Server_IP;
    public int Server_Port;


    void Start() {

        GameObject canvas = GameObject.Find("Canvas");
        Motion_keys last_given = Motion_keys.left;

        if (Network_Game) //call network setup functions
        {
            Network_Game = false; //Till implement
            if (Set_As_Server) 
            { }
            if (Set_As_Remote)
            { }
            //create players based on clients
            //server full Tetris_Player list
            //Clients 1 player list
        }
        else
        {
            if (two_players)
            {
                Create_Player();
                Create_Player();
            }
            else
            {
                Create_Player();
            }
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
            areas_to_render[0] = (Instantiate(PlayArea3D));
            areas_to_render[1] = (Instantiate(PlayArea3D));
            areas_to_render[1].transform.position = areas_to_render[0].transform.position + new Vector3(next_player_render_separation, 0, 0);
            Render_Player_Blocks();
        }
        else
        {
            TXT_Display();
        }               
    }

    // Update is called once per frame
    void Update() {
        foreach (Player Current_Player in Tetris_Players)
        {
            GetPlayersKeys(Current_Player);
            Keypressing(Current_Player); //apply appropriate KeysReceived to the client in Tetris_Players

            if (Time.time - Current_Player.last_drop > Tetris_Rules.DropSpeed)
            {
                //Send to Clients their data
                Current_Player.last_drop = Time.time;
                Current_Player.Player_Current_Shape.Shape_move_down();
                if (Tetris_Rules.Check_For_NO_Room(Current_Player))
                {
                    Current_Player.Player_Current_Shape.Shape_move_up();
                    Tetris_Rules.Check_Lines(Current_Player);
                    Tetris_Rules.Make_new_process(Current_Player);
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

    void Keypressing(Player Current_Player)
    {
        if (Network_Game)
        {
            Network_Game = false; //Till implement
            if (Set_As_Server)
            { }
            if (Set_As_Remote) //Send to Server Key presses. Server Tetris_Players List is the valid copy.
            { }
        }
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
        if (Input.GetKeyDown(Current_Player.mymotion[3].ToString()))
        {
            Tetris_Rules.Rotate_anti(Current_Player);
        }
    }

    public void GetPlayersKeys(Player Current_Player)
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
    public void Create_Player() //TODO: Move some TXT setup to TXT_Display()
    {
        int shift_text_boxes = player_text_shift_x - (text_box.Count - 1) * next_player_text_shift_x;
        GameObject canvas = GameObject.Find("Canvas");
        Tetris_Players.Add(new Player());
        GameObject temp_play_screen = (GameObject)Instantiate(Play_Screen);
        temp_play_screen.transform.SetParent(canvas.transform);
        temp_play_screen.transform.position = new Vector3(shift_text_boxes, player_text_shift_y, 0);
        text_box.Add(temp_play_screen.GetComponent<Text>());
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
            }
        }     
    }
    public void TXT_Display()
    {

        int y_shift_for_TXT = 29;
        int x_shift_for_TXT = 1;
        foreach (Player Current_Player in Tetris_Players)
        {
            GetPlayersKeys(Current_Player);
            string to_text_box = "";
            int[,] the_game_view = new int[13, 30];

            for (int part_of_preview = 0; part_of_preview < Current_Player.Player_Blocks_in_Shape; part_of_preview++)
            {
                int col = (int)Current_Player.Player_Next_Shape.shape_parts[part_of_preview].position.x + x_shift_for_TXT; //rendering +1 move
                int row = (int)Current_Player.Player_Next_Shape.shape_parts[part_of_preview].position.y + y_shift_for_TXT; //rendering +29 move
                the_game_view[col, row] = 1;
            }

            foreach (Shape shape_in_game in Current_Player.Active_Shapes)
            {
                for (int part_of_shape = 0; part_of_shape < Current_Player.Player_Blocks_in_Shape; part_of_shape++)
                {
                    int column = (int)shape_in_game.shape_parts[part_of_shape].position.x - Tetris_Rules.Left_boundary;
                    int row = (int)shape_in_game.shape_parts[part_of_shape].position.y + Tetris_Rules.Bottom_boundary + 1;
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
}
