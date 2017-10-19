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
        Debug.Log("Next: " + Next_Block.myShape);
        Debug.Log(Active_Blocks[Active_Blocks.Count - 1].myShape);

        text_box.text = Active_Blocks[Active_Blocks.Count - 1].myShape;
        Next_Block = new Block();
        
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

        Active_Blocks[Active_Blocks.Count-1].position.y = Active_Blocks[Active_Blocks.Count - 1].position.y - 1f;

        if (Active_Blocks[Active_Blocks.Count - 1].position.y < -100)
        {
            Debug.Log(Active_Blocks[Active_Blocks.Count - 1].position.y.ToString());
            Active_Blocks.Add(Next_Block);
            Next_Block = new Block();
            Debug.Log(Active_Blocks[Active_Blocks.Count - 1].position.y.ToString());
        }
        text_box.text = Active_Blocks[Active_Blocks.Count - 1].myShape;

        //this needs to change
        text_box.transform.position = Active_Blocks[Active_Blocks.Count - 1].position; 
    }

    public void Block_inactive() { }
    public void check_for_full_row(int row) { }
    public void line_destroy_and_drop(int row_to_destroy) { }
}
