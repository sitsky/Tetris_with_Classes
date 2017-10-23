using UnityEngine;
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

<<<<<<< HEAD
    //time interval between each down move of each piece ~ level difficulty
    float DropSpeed = 0.5f;

    //Rendering translations to look nice
    int player_render_shift_x = -30;
    int player_render_shift_y = 0;
=======
    //public Shape Next_Shape;
    //public Shape Current_Shape;
    //int Blocks_in_Shape;

    public List<Player> Tetris_Players = new List<Player>();
    bool two_players = true;
    public Motion_keys[] Current_keys = new Motion_keys[4];

    //float last_drop;
    float DropSpeed = 0.5f;
    int player_render_shift_x = -50;
    int player_render_shift_y = 30;
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.

    //Object and List for the TXT based game
    public GameObject Play_Screen;
//    Text[] text_box = new Text[2];
    List<Text> text_box = new List<Text>();

    //Switch on for Rendering, Object for rendering, list to hold the blocks for rendering
    public bool Render_Switch;
    public GameObject block3D;
    public GameObject[] shape;

<<<<<<< HEAD
    //Switch on for 2 players
    public bool two_players;

    //Switch for Lan
    public bool Go_Lan = false;
    public bool me_server = false; //button to assign self server and local client
    public bool me_remote = false; //button to assign self remote client
    
    TetrisNetworkManager Lan_Manager;

    //List of players playing and List for their keys. 
    public List<Player> Tetris_Players = new List<Player>();
    public Motion_keys[] Current_keys = new Motion_keys[4];

    void Start() {           
=======
    // Use this for initialization
    void Start() {
        
        Render_Switch = true;
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
        GameObject canvas = GameObject.Find("Canvas"); 

        Motion_keys last_given = Motion_keys.left;

        if (Go_Lan) //lan 2 player 
        {
<<<<<<< HEAD
            Create_Player();
            Create_Player();
            if (me_server)
            {
                Lan_Manager.SetupServer();
                Tetris_Players[0].Lan_Player.SetupLocalClient();
                me_remote = false;
            }
            if (me_remote)
            {
                Tetris_Players[1].Lan_Player.SetupClient();
                me_server = false;
            }
=======
            Tetris_Players.Add(new Player());
            Tetris_Players.Add(new Player());

            GameObject ps1 = (GameObject)Instantiate(Play_Screen);
            ps1.transform.SetParent(canvas.transform);
            ps1.transform.position = new Vector3(500, 390, 0);
            text_box.Add(ps1.GetComponent<Text>());

            GameObject ps2 = (GameObject)Instantiate(Play_Screen);
            ps2.transform.SetParent(canvas.transform);
            ps2.transform.position = new Vector3(100, 390, 0);
            text_box.Add(ps2.GetComponent<Text>());
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
        }
        else //local 2 player
        {
<<<<<<< HEAD
            if (two_players)
            {
                Create_Player();
                Create_Player();
            }
            else
            {
                Create_Player();
            }
=======
            Tetris_Players.Add(new Player());
            GameObject ps1 = (GameObject)Instantiate(Play_Screen);
            ps1.transform.SetParent(canvas.transform);
            ps1.transform.position = new Vector3(500, 390, 0);
            text_box.Add(ps1.GetComponent<Text>());
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
        }

        int each_player_boxes = 0;
        foreach (Player Current_Player in Tetris_Players)
        {
            Current_Player.last_drop = 0;
            Give_Player_Keys(Current_Player, last_given);
            last_given += 4;
            if (last_given > (Motion_keys)(Tetris_Players.Count * 4)) last_given = Motion_keys.left;
           
            Current_Player.Player_Current_Shape = new Shape();
            Current_Player.Player_Blocks_in_Shape = Current_Player.Player_Current_Shape.shape_parts.Length;
            Current_Player.Active_Shapes.Add(Current_Player.Player_Current_Shape);
            Current_Player.Player_Next_Shape = new Shape(); ;

            if (Render_Switch)
            {
                Destroy(canvas);
                shape = new GameObject[Current_Player.Player_Current_Shape.shape_parts.Length];
                for (int i = 0; i < Current_Player.Player_Current_Shape.shape_parts.Length; i++)
                {
                    shape[i] = Instantiate(block3D);
                }
                for (int i = 0; i < Current_Player.Player_Current_Shape.shape_parts.Length; i++)
                {                    
                    shape[i].transform.position = new Vector3(Current_Player.Player_Current_Shape.shape_parts[i].position.x + player_render_shift_x, 
                        Current_Player.Player_Current_Shape.shape_parts[i].position.y + player_render_shift_y, 0);
                }
            }
            else
            {
               
                Display();
            }
            each_player_boxes += 4;
            player_render_shift_x += 100;
        }
    }
<<<<<<< HEAD
=======

    // Update is called once per frame
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
    void Update() {
        player_render_shift_x = -50;
        int each_player_boxes = 0;

        foreach (Player Current_Player in Tetris_Players)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            Get_Player_Keys(Current_Player); //TODO flip keys for local arrows?...
=======
=======
            each_player_boxes = Tetris_Players.IndexOf(Current_Player);
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
            GetPlayersShapes(Current_Player);
>>>>>>> parent of 57ed951... No more magic numbers.

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
                    if (me_server)
                    {
                        Make_new_shape(Current_Player);
                        //TODO: Update_PlayerLists_Clients()
                    }
                    if (me_remote)
                    {
                        //callback on receive Tetris_Players List.
                    }
                }
            }
<<<<<<< HEAD
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
  
    //Player parameters
    public void Create_Player()
    {
        int shift_text_boxes = 200 - (text_box.Count - 1) * 400;
        GameObject canvas = GameObject.Find("Canvas");
        Tetris_Players.Add(new Player());
        GameObject temp_play_screen = (GameObject)Instantiate(Play_Screen);
        temp_play_screen.transform.SetParent(canvas.transform);
        temp_play_screen.transform.position = new Vector3(shift_text_boxes, 390, 0);
        text_box.Add(temp_play_screen.GetComponent<Text>());
    }
=======
            if (Render_Switch)
            {
                GameObject[] todie = GameObject.FindGameObjectsWithTag("shape");
                foreach (GameObject i in todie) Destroy(i);

                int total_shapes_to3d = 0;
                foreach (Shape i in Current_Player.Active_Shapes)
                    foreach (Block j in i.shape_parts) total_shapes_to3d++;

                shape = new GameObject[total_shapes_to3d];
                int k = 0;
                foreach (Shape i in Current_Player.Active_Shapes)
                {
                    foreach (Block j in i.shape_parts)
                    {
                        shape[k] = Instantiate(block3D);
                        float temp_x = j.position.x + player_render_shift_x;
                        float temp_y = j.position.y + player_render_shift_y;
                        shape[k].transform.position = new Vector3(temp_x, temp_y, 0);
                        k++;
                    }
                }
            }
            else
            {
                Display();
            }
            player_render_shift_x += 100;
        }
        //else { }//TODO;
    }

>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
    public void GetPlayersShapes(Player Current_Player)
    {
        //Current_Shape = Current_Player.Player_Current_Shape;
       // Blocks_in_Shape = Current_Player.Player_Blocks_in_Shape;
       // Next_Shape = Current_Player.Player_Next_Shape;
        for (int keys = 0; keys < 4; keys++)
        {
            Current_keys[keys] = Current_Player.mymotion[keys];

        }
    }

    public void Give_Player_Keys(Player Assign_Keys, Motion_keys last_given)
    {
        for (int keys = 0; keys < 4; keys++)
        {
            Assign_Keys.mymotion[keys] = last_given + keys;

        }

    }

<<<<<<< HEAD
    //Game rules
=======
>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
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
            if (fullrow > 11) //FullRow space 
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

    public void Make_new_process(Player Current_Player)
    {
        
        //foreach (Player Current_Player in Tetris_Players)
        {
            //Debug.Log(Current_Shape.shape_parts[0].position.ToString());
            //Debug.Log(Next_Shape.shape_parts[0].position.ToString());

            Current_Player.Active_Shapes.Add(Current_Player.Player_Next_Shape);
            //Current_Shape = Current_Player.Player_Next_Shape;
            //Blocks_in_Shape = Current_Shape.shape_parts.Length;

            //Next_Shape = new Shape();
            Current_Player.Player_Current_Shape = Current_Player.Player_Next_Shape;
            Current_Player.Player_Next_Shape = new Shape();

            if (Render_Switch)
            {
                for (int i = 0; i < 4; i++)
                {
                    //Instantiate(block3D[i]);
                }
                for (int i = 0; i < 4; i++)
                {
                    //block3D[i].transform.position = new Vector3(Current_Player.Player_Current_Shape.shape_parts[i].position.x, 
                    //    Current_Player.Player_Current_Shape.shape_parts[i].position.y, 0);
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
                if (check_empty_space.position.y < -4)
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
<<<<<<< HEAD
    public void Make_new_shape(Player Current_Player)
    {
        Current_Player.Active_Shapes.Add(Current_Player.Player_Next_Shape);
        Current_Player.Player_Current_Shape = Current_Player.Player_Next_Shape;
        Current_Player.Player_Next_Shape = new Shape();
        if (Render_Switch)
        {
            for (int i = 0; i < 4; i++)
            {
                //Instantiate(block3D[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                //block3D[i].transform.position = new Vector3(Current_Player.Player_Current_Shape.shape_parts[i].position.x, 
                //    Current_Player.Player_Current_Shape.shape_parts[i].position.y, 0);
            }
        }
    }

    //Motion functions
=======

>>>>>>> parent of f1b4b02... All seem good. On to Networking Manager.
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

    //Visuals TXT vs Rendered
    public void Render_Player_Blocks()
    {
        GameObject[] to_die = GameObject.FindGameObjectsWithTag("rendered_block");
        foreach (GameObject created_block in to_die) { Destroy(created_block); }

        foreach (Player Current_Player in Tetris_Players)
        {
            player_render_shift_x += Tetris_Players.IndexOf(Current_Player) * 60;
            foreach (Shape shape_to_render in Current_Player.Active_Shapes)
            {
                foreach (Block block_to_render in shape_to_render.shape_parts)
                {
                    float temp_x = block_to_render.position.x + player_render_shift_x;
                    float temp_y = block_to_render.position.y + player_render_shift_y;
                    to_render.Add(Instantiate(block3D));
                    to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, temp_y, 0);
                }
            }
        }
    }
    public void Display()
    {
        int screen_number = 0;
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
            text_box[screen_number].text = to_text_box;
            screen_number++;
        }
    }

    public void Display()
    {
        int screen_number = 0;
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
            text_box[screen_number].text = to_text_box;
            screen_number++;
        }
    }
}
