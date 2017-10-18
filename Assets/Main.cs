using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Block
{

    public enum Block_Shape : int { L = 1, J, I, O, S, Z, T };

    public Vector2 position;
    public Vector2 orientation;
    public Block_Shape myShape;

    public Block()
    {
        position = new Vector2(0, 0);
        orientation = new Vector2(0, 1);
        myShape = (Block_Shape)Random.Range(1, 7);
    }


    public void move_left()
    {
        position.x = position.x - 1;
    }
    public void move_right()
    {
        position.x = position.x + 1;
    }
    public void rotate_clockwise()
    {
        Vector2 temp;
        temp.x = orientation.x * 0 + orientation.y * 1;
        temp.y = orientation.x * -1 + orientation.y * 0;
        orientation = temp;
    }
    public void rotate_anticlockwise()
    {
        Vector2 temp;
        temp.x = orientation.x * 0 + orientation.y * -1;
        temp.y = orientation.x * 1 + orientation.y * 0;
        orientation = temp;
    }

}

public class Main : MonoBehaviour {

    Block Next_Block;
    List<Block> Active_Blocks = new List<Block>();

    float time_of_last_down;
    Text grid_text;

    // Use this for initialization
    void Start () {

        grid_text = GetComponent<Text>();

        Next_Block = new Block();
        Active_Blocks.Add(Next_Block);

        grid_text.text = "Active_Blocks.Count: " + Active_Blocks.Count.ToString();
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

        Active_Blocks[0].position.y = Active_Blocks[0].position.y - 0.1f;

        if (Active_Blocks[0].position.y < -20)
        {
            Active_Blocks.Add(Next_Block);
            Next_Block = new Block();
        }
        grid_text.text = "Active_Blocks.Count: " + Active_Blocks.Count.ToString();
    } 
}
