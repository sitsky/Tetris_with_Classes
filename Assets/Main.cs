using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    int Left_boundary = -5;
    int Right_boundary = 6;
    int Bottom_boundary = 20;

    public Block Next_Block;
    List<Block> Active_Blocks = new List<Block>();
    int number_parts_in_a_block;

    float last_drop;
    float DropSpeed = 0.5f;
    Text text_box;

    public bool Render_Switch;

    // Use this for initialization
    void Start () {
        last_drop = 0;
        Render_Switch = false;

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
            if (Check_For_Boundaries())
            {
                Active_Blocks[Active_Blocks.Count - 1].move_right();
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
            if (Check_For_Boundaries())
            {
                Active_Blocks[Active_Blocks.Count - 1].move_left();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
            Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }
            if (Check_For_Boundaries())
            {
                Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
      
            Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
            no_room = Check_For_Room();
            if (no_room)
            {
                Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }
            if (Check_For_Boundaries())
            {
                Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
            }

        }
        if (Time.time - last_drop > DropSpeed)
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
                    if (Active_Blocks[Active_Blocks.Count - 1].position[part_of_block].y < -Bottom_boundary)
                    {
                        Active_Blocks.Add(Next_Block);
                        Next_Block = new Block();
                    }
                }
            }
        }
        Check_Lines();
        if (!Render_Switch)
        {
            display();
        }
        else { }//TODO;
    }
    

    public void Check_Lines()
    {
        List<Block> RowList =  new List<Block>();
        for (int row = -Bottom_boundary; row < 0; row++)
        {
            foreach (Block active_block in Active_Blocks)
            {
                for (int parts_of_bock = 0; parts_of_bock < number_parts_in_a_block; parts_of_bock++)
                {
                    if (active_block.position[parts_of_bock].y == row)
                    {
                        RowList.Add(active_block);
                    }
                }

                if (RowList.Count > 5)//Right_boundary - Left_boundary)
                {
                    foreach (Block block_to_die in RowList)
                    {
                        for (int parts_of_block = 0; parts_of_block < number_parts_in_a_block; parts_of_block++)
                        {
                            if (block_to_die.position[parts_of_block].y == row)
                            {
                                block_to_die.position[parts_of_block] = new Vector2(0, -4);
                            }
                        }
                    }
                    foreach(Block active in Active_Blocks)
                    {
                        active.move_down();
                    }
                }
                else { RowList.Clear(); }
            }
        }
    }

    public bool Check_For_Boundaries()
    {
        bool hitting_walls = false;
        for (int parts_of_block = 0; parts_of_block < number_parts_in_a_block; parts_of_block++)
        {
            if (Active_Blocks[Active_Blocks.Count - 1].position[parts_of_block].x < Left_boundary) hitting_walls = true;
            if (Active_Blocks[Active_Blocks.Count - 1].position[parts_of_block].x > Right_boundary) hitting_walls = true;
        }
        return hitting_walls;
    }

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
        int[,] the_game_view = new int[13,30];

        for (int part_of_preview = 0; part_of_preview < number_parts_in_a_block; part_of_preview++)
        {
            int col = (int)Next_Block.position[part_of_preview].x +1;
            int row = (int)Next_Block.position[part_of_preview].y + 29;
            Debug.Log("col: " + col + "row: " + row);
            the_game_view[col, row] = 1;
        }

        
            foreach(Block block_in_game in Active_Blocks)
        {
            for(int part_of_block = 0; part_of_block < number_parts_in_a_block; part_of_block++)
            {
                //Debug.Log(position_part_of_block.x.ToString() + "  " + position_part_of_block.y.ToString());
                Debug.Log("Step1");
                int column = (int)block_in_game.position[part_of_block].x - Left_boundary;
                int row = (int)block_in_game.position[part_of_block].y + Bottom_boundary +1;
                if (row < 0)
                {
                    row = 29;
                    column = 12;
                }
                Debug.Log("Step2");

                Debug.Log("col: " + column + "row: " + row);

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
