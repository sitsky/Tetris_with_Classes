using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {


    public List<Block> Active_Blocks;
	// Use this for initialization
	void Start () {
        Active_Blocks.Add(new Block());       
	}

	// Update is called once per frame
	void Update () {

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
    }
}
