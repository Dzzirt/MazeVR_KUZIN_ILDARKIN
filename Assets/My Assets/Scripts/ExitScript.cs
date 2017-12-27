using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{

	public Player Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void onClick()
	{
		if (Player.isCollectedAllCubes)
		{
			Player.winCount++;
			Player.Restart();
		}
	}
}
