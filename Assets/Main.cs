using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    int number_parts_in_a_block;

    public Block Next_Block;
    List<Block> Active_Blocks = new List<Block>();


    float last_drop;
    Text text_box;

    // Use this for initialization
    void Start () {
        last_drop = 0;

        text_box = GetComponent<Text>();

        Next_Block = new Block();
        Active_Blocks.Add(Next_Block);
        Next_Block = new Block();
        display();
        number_parts_in_a_block = Next_Block.position.Length;
    }

    // Update is called once per frame
    void Update() {

        bool no_room = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Active_Blocks[Active_Blocks.Count - 1].move_left();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].move_right();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Active_Blocks[Active_Blocks.Count - 1].move_right();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].move_left();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Main.UP");
            Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Main.DOWN");
            Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }

        }

        if (Time.time - last_drop > 0.1)

        {
            last_drop = Time.time;
            Active_Blocks[Active_Blocks.Count - 1].move_down();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].move_up();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }
            else
            {
                for (int part_of_block = 0; part_of_block < Active_Blocks[Active_Blocks.Count - 1].position.Length; part_of_block++)
                {
                    if (Active_Blocks[Active_Blocks.Count - 1].position[part_of_block].y < -19)
                    {
                        Active_Blocks.Add(Next_Block);
                        Next_Block = new Block();
                    }
                }
            }
        }
        display();

    }
    /*
    public void Check_Lines()
    {

        for(int row = -20; row < 0; row++)
        {
            foreach (Block active_block in Active_Blocks)
            {
                    if (active_block.position.y == row)

            }
        }
    }*/

    public bool Check_For_Room()
    {
        bool occupado = false;
        if (Active_Blocks.Count > 1)
        {
            for (int active_block = 0; active_block < (Active_Blocks.Count - 1); active_block++)
            {
                for (int each_part = 0; each_part < number_parts_in_a_block; each_part++)
                {
                    for (int parts_of_active_blocks = 0; parts_of_active_blocks < number_parts_in_a_block; parts_of_active_blocks++)
                    {
                        if (Active_Blocks[Active_Blocks.Count - 1].position[each_part].Equals(Active_Blocks[active_block].position[parts_of_active_blocks]))
                        {
                            occupado = true;
                        }
                    }
                }
            }
        }
        return occupado;
    }

    public void display()
    {
        string to_text_box = "";

        int[,] the_game_view = new int[40,40];
        foreach(Block block_in_game in Active_Blocks)
        {
            for(int part_of_block = 0; part_of_block < 4; part_of_block++)
            {
                //Debug.Log(position_part_of_block.x.ToString() + "  " + position_part_of_block.y.ToString());

                int column = 10 + (int)block_in_game.position[part_of_block].x;
                int row = 32 + (int)block_in_game.position[part_of_block].y;
                the_game_view[column, row] = 1;
                }
        }
        for (int line = 39; line >= 0; line--)
        {
            to_text_box = to_text_box + "|";
            for (int column = 0; column < 21; column++)
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


    //public void Block_inactive() { }
    public void check_for_full_row(int row) { }
    public void line_destroy_and_drop(int row_to_destroy) { }
}
