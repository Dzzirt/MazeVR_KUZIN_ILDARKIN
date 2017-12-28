using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	public Player player;

	public MazeCell Cell;

	public bool needToHit = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var component1 = GetComponent<Animation>();
		component1.Play();

		if (Cell.colPos == player.cell.colPos && Cell.rowPos == player.cell.rowPos)
		{
			if (needToHit)
			{
				needToHit = false;
				var component = GetComponent<Animation>();
				var audio = GetComponent<AudioSource>();
				component.Play();
				player.health -= 1;
				audio.Play();
			}	
		}
		else
		{
			needToHit = true;
		}
		
	}
}
