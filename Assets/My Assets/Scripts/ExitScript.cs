using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{

	public Player Player;

	public GameObject doorText;

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
			Player.handleWin();
			Player.Restart();
		}
	}
}
