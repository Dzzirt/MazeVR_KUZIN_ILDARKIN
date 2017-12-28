using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{

	public MazeSpawner MazeSpawner;
	
	public int dif;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void onClick()
	{
		switch (dif)
		{
				case 1:
					MazeSpawner.maxCubeCount = 4;
					MazeSpawner.basicEnemyCount = 1;
					MazeSpawner.smallEnemyCount = 2;
					MazeSpawner.trapCount = 1;
					MazeSpawner.Rows = 5;
					MazeSpawner.Columns = 5;
					break;
				case 2:
					MazeSpawner.maxCubeCount = 6;
					MazeSpawner.basicEnemyCount = 2;
					MazeSpawner.smallEnemyCount = 2;
					MazeSpawner.trapCount = 2;
					MazeSpawner.Rows = 7;
					MazeSpawner.Columns = 7;
					break;
				case 3:
					MazeSpawner.maxCubeCount = 8;
					MazeSpawner.basicEnemyCount = 3;
					MazeSpawner.smallEnemyCount = 3;
					MazeSpawner.trapCount = 3;
					MazeSpawner.Rows = 10;
					MazeSpawner.Columns = 10;
					break;
					
		}
		MazeSpawner.init();
		
		Camera.main.transform.parent.position = new Vector3(2.5f, 0, 2.5f);

	}
}
