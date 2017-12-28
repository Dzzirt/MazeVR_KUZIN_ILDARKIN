using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int health;

	public int maxHealth;

	public Text cubesCountText;
	
	public Text winText;


	public Items items;

	public MazeCell cell;

	public Slider Slider;

	public int winCount;

	public Text winCountText;

	public float CellWidth;
	public float CellHeight;
	
	WinData winData = new WinData();

	public GameObject wallSpawner;
	public MazeSpawner mazeSpawner;

	public int CubesCount;

	public bool isCollectedAllCubes;
	
	public void handleWin()
	{
		winData.winCount++;
		Save();
	}
	[Serializable]
	class WinData
	{
		public int winCount;
	}
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		var file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);
		bf.Serialize(file, winData);
		file.Close();
	}
	
	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			var file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			winData = (WinData) bf.Deserialize(file);
			file.Close();
		}	
	}
    
	public void onCubeCollect()
	{
		CubesCount += 1;
	}

	// Use this for initialization
	void Start ()
	{
		Load();
		health = maxHealth;
		Slider.maxValue = maxHealth;
	}
	
	// Update is called once per frame
	
	IEnumerator DestroyLevel()
	{
		cell = null;
		CubesCount = 0;
		mazeSpawner.maxCubeCount = 0;
		health = maxHealth;
		Slider.value = health;
		foreach (GameObject t in mazeSpawner.levelObjects)
		{
			Destroy(t);
			yield return null;
		}

		Camera.main.transform.parent.position = new Vector3(-27.5f, 0, 2.5f);
	}
	
	public void Restart()
	{
		Camera.main.transform.parent.position = new Vector3(-104.95f, 0, 6.332f);
		StartCoroutine(DestroyLevel());
	}
	
	void Update ()
	{


		Slider.value = health;
		winCountText.text = "Побед: " + winData.winCount;

		mazeSpawner = wallSpawner.GetComponent<MazeSpawner>();

		isCollectedAllCubes = CubesCount == mazeSpawner.maxCubeCount;

		var audioSources = GetComponents<AudioSource>();

		if (isCollectedAllCubes && mazeSpawner.maxCubeCount  != 0)
		{
			winText.text = "Вы победили.\nСчетчик побед увеличился.\nПоиграем еще?.";
		}
		else
		{
			winText.text = "Вы умерли.\nПроисходит реанимация.";
		}


		if (mazeSpawner.maxCubeCount == 0)
		{
			if (!audioSources[1].isPlaying)
			{
				audioSources[1].Play();
				audioSources[2].Stop();
			}
	
		}
		else
		{
			if (!audioSources[2].isPlaying)
			{
				audioSources[2].Play();
				audioSources[1].Stop();
			}
			
		}
	
		
		if (health <= 0)
		{		
			Restart();
		}
		if (mazeSpawner.maxCubeCount == 0)
		{
			cubesCountText.text = "Выберите уровень";
		}
		else
		{
			cubesCountText.text = CubesCount + "/" + mazeSpawner.maxCubeCount;
		}
	}
}
