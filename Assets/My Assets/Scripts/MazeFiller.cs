using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MazeFiller
{

	private int mCubeCount;
	private int mediumMonsterCount;
	private int smallEnemyCount;
	private int mTrapCount;
	
	
	public MazeFiller(int cubeCount, int basicEnemyCount, int trapCount, int smallEnemyCount)
	{
		mCubeCount = cubeCount;
		mediumMonsterCount = basicEnemyCount;
		this.smallEnemyCount = smallEnemyCount;
		mTrapCount = trapCount;
	}

	public void fill(MazeCell[,] maze)
	{
		var colCount = maze.GetLength(0);
		var rowCount = maze.Length / colCount;
		
		var random = new Random();

		var isDoorPlaced = false;

		while (!isDoorPlaced)
		{
			var randomCol = random.Next(colCount - 1);
			var randomRow = random.Next(rowCount - 1);

			var mazeCell = maze[randomRow, randomCol];

			if (randomCol == 0 && randomRow == 0)
			{
				continue;
			}
			
			if (randomCol == 0)
			{
				mazeCell.ExitSide = ExitSide.LEFT;
				isDoorPlaced = true;
			} else if (randomCol == colCount - 1)
			{
				mazeCell.ExitSide = ExitSide.RIGHT;
				isDoorPlaced = true;

			} else if (randomRow == 0)
			{
				mazeCell.ExitSide = ExitSide.BOTTOM;
				isDoorPlaced = true;


			} else if (randomRow == rowCount - 1)
			{
				mazeCell.ExitSide = ExitSide.TOP;
				isDoorPlaced = true;

			}
		}
		
		for (int k = 0; k < mCubeCount; k++)
		{
			var randomCol = random.Next(colCount - 1);
			var randomRow = random.Next(rowCount - 1);
			var mazeCell = maze[randomRow, randomCol];

			while (mazeCell.hasCube)
			{
				randomCol = random.Next(colCount - 1);
				randomRow = random.Next(rowCount - 1);
				mazeCell = maze[randomRow, randomCol];
			}
			mazeCell.hasCube = true;

		}
		
		for (int k = 0; k < mediumMonsterCount; k++)
		{
			var randomCol = random.Next(colCount - 1);
			var randomRow = random.Next(rowCount - 1);
			var mazeCell = maze[randomRow, randomCol];

			if (randomCol == 0 && randomRow == 0)
			{
				k--;
				continue;
			}
			
			while (mazeCell.enemyType != EnemyType.None)
			{
				randomCol = random.Next(colCount - 1);
				randomRow = random.Next(rowCount - 1);
				mazeCell = maze[randomRow, randomCol];
				
				if (randomCol == 0 && randomRow == 0)
				{
					k--;
					continue;
				}
			}
			mazeCell.enemyType = EnemyType.Medium;

		}
		
		for (int k = 0; k < smallEnemyCount; k++)
		{
			var randomCol = random.Next(colCount - 1);
			var randomRow = random.Next(rowCount - 1);
			var mazeCell = maze[randomRow, randomCol];

			if (randomCol == 0 && randomRow == 0)
			{
				k--;
				continue;
			}
			
			while (mazeCell.enemyType != EnemyType.None)
			{
				randomCol = random.Next(colCount - 1);
				randomRow = random.Next(rowCount - 1);
				mazeCell = maze[randomRow, randomCol];
				
				if (randomCol == 0 && randomRow == 0)
				{
					k--;
					continue;
				}
			}
			mazeCell.enemyType = EnemyType.Small;

		}
		
		for (int k = 0; k < mTrapCount; k++)
		{
			var randomCol = random.Next(colCount - 1);
			var randomRow = random.Next(rowCount - 1);
			var mazeCell = maze[randomRow, randomCol];

			if (randomCol == 0 && randomRow == 0)
			{
				k--;
				continue;
			}
			
			while (mazeCell.enemyType != EnemyType.None)
			{
				randomCol = random.Next(colCount - 1);
				randomRow = random.Next(rowCount - 1);
				mazeCell = maze[randomRow, randomCol];
				
				if (randomCol == 0 && randomRow == 0)
				{
					k--;
					continue;
				}
			}
			mazeCell.enemyType = EnemyType.Trap;

		}
		
		
	}
	
}
