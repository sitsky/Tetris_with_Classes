using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    public Block Next_Block;
    List<Block> Active_Blocks = new List<Block>();

    Text text_box;

    // Use this for initialization
    void Start () {

        text_box = GetComponent<Text>();

        Next_Block = new Block();
        Active_Blocks.Add(Next_Block);
        Next_Block = new Block();
        display();
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Active_Blocks[Active_Blocks.Count - 1].move_left();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Active_Blocks[Active_Blocks.Count - 1].move_right();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Main.UP");
            Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
        }

        if (Active_Blocks.Count < 2)
        {
            Active_Blocks[Active_Blocks.Count - 1].move_down();
        }

        //Debug.Log(Active_Blocks[Active_Blocks.Count - 1].position[0].ToString());
        
        for (int part_of_block = 0; part_of_block < Active_Blocks[Active_Blocks.Count - 1].position.Length; part_of_block++)
        {
            if (Active_Blocks[Active_Blocks.Count - 1].position[part_of_block].y < -8)
            {
                Active_Blocks.Add(Next_Block);
                //Next_Block = new Block();
            }

        }
        display();
    }

    public void display()
    {
        string to_text_box = "";

        int[,] the_game_view = new int[20,40];
        foreach(Block block_in_game in Active_Blocks)
        {
            for(int part_of_block = 0; part_of_block < 4; part_of_block++)
            {
                //Debug.Log(position_part_of_block.x.ToString() + "  " + position_part_of_block.y.ToString());

                int column = 5 + (int)block_in_game.position[part_of_block].x;
                int row = 32 + (int)block_in_game.position[part_of_block].y;
                the_game_view[column, row] = 1;
                }
        }
        for (int line = 39; line >= 0; line--)
        {
            for (int column = 0; column < 20; column++)
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
            to_text_box = to_text_box + "\n";
        }
        text_box.text = to_text_box;
    }


    public void Block_inactive() { }
    public void check_for_full_row(int row) { }
    public void line_destroy_and_drop(int row_to_destroy) { }
}
