﻿using System;
 using System.Collections;
 using System.IO;
 using UnityEngine;
 using Object = UnityEngine.Object;

public enum Direction{
	Start,
	Right,
	Front,
	Left,
	Back,
};

public enum ExitSide
{
	LEFT,
	RIGHT,
	TOP,
	BOTTOM,
	NONE
}


public enum EnemyType{
	Small,
	Medium,
	Trap,
	None
};
//<summary>
//Class for representing concrete maze cell.
//</summary>
[Serializable]
public class MazeCell
{
	public bool IsVisited = false;
	public bool WallRight = false;
	public bool WallFront = false;
	public bool WallLeft = false;
	public bool WallBack = false;
	public bool hasPlayer = false;
	public bool hasCube = false;
	public EnemyType enemyType = EnemyType.None;
	public bool hasKey = false;

	public ExitSide ExitSide = ExitSide.NONE;

	public int rowPos;
	public int colPos;

	public int waveCounter = 0;
	public bool isWaveVisited = false;
	
	public MazeCell clone()
	{
		var mazeCell = new MazeCell();
		mazeCell.IsVisited = IsVisited;
		mazeCell.WallRight = WallRight;
		mazeCell.WallFront = WallFront;
		mazeCell.WallBack = WallBack;
		mazeCell.WallLeft = WallLeft;
		mazeCell.hasPlayer = hasPlayer;
		mazeCell.hasCube = hasCube;
		mazeCell.enemyType = enemyType;
		mazeCell.hasKey = hasKey;
		mazeCell.rowPos = rowPos;
		mazeCell.colPos = colPos;
		mazeCell.waveCounter = waveCounter;
		mazeCell.isWaveVisited = isWaveVisited;
		

		return mazeCell;
	}
	
	public static T Clone<T>(T source)
	{
		if (!typeof(T).IsSerializable)
		{
			throw new ArgumentException("The type must be serializable.", "source");
		}

		if (Object.ReferenceEquals(source, null))
		{
			return default(T);
		}

		System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		Stream stream = new MemoryStream();
		using (stream)
		{
			formatter.Serialize(stream, source);
			stream.Seek(0, SeekOrigin.Begin);
			return (T)formatter.Deserialize(stream);
		}
	}
}
