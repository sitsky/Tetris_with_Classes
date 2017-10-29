using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Linq;

public enum Motion_keys : int { left, right, up, down, a, d, w, s};

public static class Tetris_consts
{
    public const int keys_per_player = 4;
}
public class Main : MonoBehaviour {
 

    public GameObject PlayArea3D;
    public GameObject block3D;
    List<GameObject> to_render = new List<GameObject>();
    GameObject[] areas_to_render = new GameObject[2];

    //int next_player_render_separation = -60;


   


    //Switch on for Rendering, Object for rendering, list to hold the blocks for rendering
    public bool Render_Switch;
    //Switch on for 2 players
    public bool two_players = false;
    
    //List of players playing and List for their keys. 
    public List<Player> Tetris_Players = new List<Player>();
    Motion_keys[] Current_keys = new Motion_keys[5];
    Motion_keys last_Key_given = Motion_keys.left;
    Game_rules Tetris_Rules = new Game_rules();


    void Start() {
        if (two_players)
            {
                Tetris_Players.Add(Create_Player());
                Tetris_Players.Add(Create_Player());
                foreach (Player Current_Player in Tetris_Players)
                {
                SetupPlayerControls(Current_Player, last_Key_given);
                    last_Key_given += Tetris_consts.keys_per_player;
                    if (last_Key_given > (Motion_keys)(Tetris_Players.Count * Tetris_consts.keys_per_player)) last_Key_given = Motion_keys.left;
                }
            }
            else
            {
                Tetris_Players.Add(Create_Player());
            }
        

        if (Render_Switch)
        {
            GameObject canvas = GameObject.Find("Canvas");
            Destroy(canvas);

            areas_to_render[0] = (Instantiate(PlayArea3D));
            //areas_to_render[1] = (Instantiate(PlayArea3D));
            //areas_to_render[1].transform.position = areas_to_render[0].transform.position + new Vector3(next_player_render_separation, 0, 0);
            //Render_Player_Blocks();
        }

    }

    void Update()
    {
        foreach (Player Current_Player in Tetris_Players)
        {
            RetrievePlayerControls(Current_Player);
            PlayerControls(Current_Player);
            if (Time.time - Current_Player.last_drop > Tetris_Rules.DropSpeed)
            {
                Current_Player.last_drop = Time.time;
                //Debug.Log("Move time");
                Tetris_Rules.Move_Player(Current_Player, 0, 1);
            }
            //Tetris_Rules.Lines_Full_check(Current_Player);
            if (Render_Switch)
            {
                Render_Player_Blocks();
            }
        }
    }
    
    void PlayerControls(Player Current_Player)
    {
        if (Input.GetKeyDown(Current_Player.mymotion[0].ToString()))
        {
            Tetris_Rules.Move_Player(Current_Player, -1, 0);
        }
        if (Input.GetKeyDown(Current_Player.mymotion[1].ToString()))
        {
            Tetris_Rules.Move_Player(Current_Player, 1, 0);
        }
        if (Input.GetKeyDown(Current_Player.mymotion[2].ToString()))
        {
            //Tetris_Rules.Rotate_clock(Current_Player);
        }
        if(Input.GetKey(Current_Player.mymotion[3].ToString()))
        {
            Tetris_Rules.Move_Player(Current_Player, 0, 1);
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
    public Player Create_Player()
    {
        Player new_player = new Player();
        new_player.last_drop = 0;
        new_player.Player_Current_Shape = new Shape();
        Tetris_Rules.place_blocks(new_player);
        new_player.Player_Next_Shape = new Shape();
        SetupPlayerControls(new_player, last_Key_given);
        return new_player;
    }

    public void Render_Player_Blocks() //TODO:spawn area
    {
        GameObject[] to_die = GameObject.FindGameObjectsWithTag("rendered_block");
        foreach (GameObject created_block in to_die) { Destroy(created_block); }

        //next_player_render_shift = first_player_render_shift_x;
        foreach (Player Current_Player in Tetris_Players)
        {
            /*
            next_player_render_shift += Tetris_Players.IndexOf(Current_Player) * next_player_render_separation;
            foreach (Block block_to_render in Current_Player.Player_Next_Shape.shape_parts)
            {
                float temp_x = block_to_render.position.x + next_player_render_shift - next_piece_render_shift;
                float temp_y = block_to_render.position.y + player_render_shift_y + next_piece_render_shift;
                to_render.Add(Instantiate(block3D));
                to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, temp_y, 0);
                to_render[to_render.Count - 1].GetComponent<MeshRenderer>().material.color = Current_Player.Player_Next_Shape.mycolor;
            }
            */
            foreach(Player.C_line line in Current_Player.Lines)
            {
                int temp_x;
                int temp_y;
                for (int i = 1; i < 11; i++)
                {
                    if (line.line[i] == true)
                    {
                        temp_x = i;
                        temp_y = Current_Player.Lines.IndexOf(line);
                        //Debug.Log("R: " + temp_y.ToString());
                        to_render.Add(Instantiate(block3D));
                        to_render[to_render.Count - 1].transform.position = new Vector3(temp_x, -temp_y, 0);
                        to_render[to_render.Count - 1].GetComponent<MeshRenderer>().material.color = Current_Player.Player_Current_Shape.mycolor;
                    }
                }
            }
        }
    }
}
