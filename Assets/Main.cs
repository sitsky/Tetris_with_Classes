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
            Active_Blocks[Active_Blocks.Count - 1].rotate_clockwise();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Active_Blocks[Active_Blocks.Count - 1].rotate_anticlockwise();
        }

        Active_Blocks[Active_Blocks.Count - 1].move_down();

        Debug.Log(Active_Blocks[Active_Blocks.Count - 1].position[0].ToString());
        
            for (int part_of_block = 0; part_of_block < Active_Blocks[Active_Blocks.Count - 1].position.Length; part_of_block++)
        {
            if (Active_Blocks[Active_Blocks.Count - 1].position[part_of_block].y < -100)
            {
                Active_Blocks.Add(Next_Block);
                Next_Block = new Block();
            }

        }
        display();
    }

    public void display()
    {
        string the_game_view = "";
        foreach(Block block_in_game in Active_Blocks)
        {
            the_game_view = block_in_game.myShape + "\n   \n" + the_game_view;
        }
        text_box.text = the_game_view;
    }


    public void Block_inactive() { }
    public void check_for_full_row(int row) { }
    public void line_destroy_and_drop(int row_to_destroy) { }
}
