using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    public static Block Next_Block;
    public static List<Block> Active_Blocks;

    float time_of_last_down;
    Text grid_text;

    // Use this for initialization
    void Start () {
        grid_text = GetComponent<Text>();
        Next_Block = new Block();

        Active_Blocks.Add(Next_Block);
        grid_text.text = Active_Blocks.Count.ToString();
        Next_Block = new Block();
        time_of_last_down = 0;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Active_Blocks[0].move_left();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Active_Blocks[0].move_right();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Active_Blocks[0].rotate_clockwise();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Active_Blocks[0].rotate_anticlockwise();
        }

        Active_Blocks[0].position.y = Active_Blocks[0].position.y - 1;

        if (Active_Blocks[0].position.y < -20)
        {
            Active_Blocks.Add(Next_Block);
            Next_Block = new Block();
        }
    }
}
