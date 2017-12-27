using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int health;

	public int maxHealth;

	public Text cubesCountText;

	public Items items;

	public MazeCell cell;

	public Slider Slider;

	public int winCount;

	public float CellWidth;
	public float CellHeight;

	public GameObject wallSpawner;
	public MazeSpawner mazeSpawner;

	public int CubesCount;
	
	public void onCubeCollect()
	{
		CubesCount += 1;
	}

	// Use this for initialization
	void Start ()
	{
		health = maxHealth;
		Slider.maxValue = maxHealth;
	}
	
	// Update is called once per frame
	
	IEnumerator DestroyLevel() {
		foreach (GameObject t in mazeSpawner.levelObjects)
		{
			Destroy(t);
			yield return null;
		}
		CubesCount = 0;
		mazeSpawner.maxCubeCount = 0;
		health = maxHealth;
		Slider.value = health;
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
		

		mazeSpawner = wallSpawner.GetComponent<MazeSpawner>();
		
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
