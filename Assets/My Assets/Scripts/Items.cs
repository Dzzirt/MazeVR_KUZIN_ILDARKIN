using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public bool has_key = false;
    public int cube;

	// Use this for initialization
	void Start () {
        cube = 0;
	}
	
	public void Got_key()
    {
        has_key = true;
    }

    public void Release_key()
    {
        has_key = false;
    }

    public void Got_cube()
    {
        cube += 1;
    }
}
