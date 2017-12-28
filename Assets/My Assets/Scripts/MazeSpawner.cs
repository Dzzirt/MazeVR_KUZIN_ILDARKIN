﻿using UnityEngine;
using System.Collections;
 using System.Collections.Generic;
 using UnityEngine.UI;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public GameObject WayPoint = null;
    public GameObject Cube = null;
    public GameObject MeduimEnemy = null;
    public GameObject SmallEnemy = null;
    public GameObject ExitDoor = null;
    public GameObject Trap = null;

    public Text winText;
    public GameObject doorText;

    public List<GameObject> levelObjects = new List<GameObject>();
    
    public int maxCubeCount;
    public int basicEnemyCount;
    public int smallEnemyCount;
    public int trapCount;
    public int Rows = 10;
    public int Columns = 10;
    public float CellWidth;
    public float CellHeight;
    public Player player;
    public GameObject playerItems;

    public Random Random = new Random();

    private BasicMazeGenerator mMazeGenerator = null;
    private MazeFiller mMazeFiller;

    public void init()
    {
        Cursor.visible = false;

        player.CellWidth = CellWidth;
        player.CellHeight = CellHeight;
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        var maze = mMazeGenerator.GenerateMaze();

        player.cell = maze[0, 0];
        

        mMazeFiller = new MazeFiller(maxCubeCount, basicEnemyCount, trapCount, smallEnemyCount);

        mMazeFiller.fill(maze);

        float y = 0f;

        if (Pillar != null)
        {
            for (int row = Rows; row >= 0; row--)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth);
                    float z = row * (CellHeight);
                    GameObject tmp =
                        Instantiate(Pillar, new Vector3(x, y, z),
                            Quaternion.identity);
                    tmp.transform.parent = transform;
                    levelObjects.Add(tmp);
                }
            }
        }


        for (int row = Rows - 1; row >= 0; row--)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth);
                float z = row * (CellHeight);

                if (Wall != null)
                {
                    MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                    if (cell.WallRight)
                    {
                        var tmp = Instantiate(Wall, new Vector3(x + CellWidth, y, z + CellHeight / 2f),
                            Quaternion.Euler(0, 0, 0));
                        levelObjects.Add(tmp);

                    }
                    if (cell.WallFront)
                    {
                        var tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2f, y, z + CellHeight),
                            Quaternion.Euler(0, 90, 0));
                        levelObjects.Add(tmp);

                    }
                    if (cell.WallLeft)
                    {
                        var tmp = Instantiate(Wall, new Vector3(x, y, z + CellHeight / 2f),
                            Quaternion.Euler(0, 0, 0));
                        levelObjects.Add(tmp);
                

                    }
                    if (cell.WallBack)
                    {
                        var tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2f, y, z),
                            Quaternion.Euler(0, 90, 0));
                        levelObjects.Add(tmp);

                    }
                }

                if (Floor != null)
                {
                    var tmp = Instantiate(Floor, new Vector3(x + CellWidth / 2f, y - 2.5f, z + CellHeight / 2f),
                        Quaternion.Euler(0, 0, 90));
                    levelObjects.Add(tmp);

                }

                if (WayPoint != null)
                {
                    var instantiate = Instantiate(WayPoint,
                        new Vector3(x + CellWidth / 2f, y, z + CellHeight / 2f),
                        Quaternion.identity);
                    var waypointModified = instantiate.GetComponent<Waypoint_Modified>();
                    waypointModified.Cell = maze[row, column];
                    levelObjects.Add(instantiate);

                }

                if (ExitDoor != null)
                {
                    MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                    Vector3 position = new Vector3();
                    var rotation = Quaternion.identity;
                    var wallDiff = 0.5f;
                    var doorDiff = 0.2f;
                    switch (cell.ExitSide)
                    {
                        case ExitSide.LEFT:
                            position = new Vector3(x + wallDiff, y, z + CellHeight / 2f);
                            rotation = Quaternion.Euler(0, 90f, 0);
                            doorText.transform.position = new Vector3(position.x + doorDiff, position.y, position.z);
                            break;
                        case ExitSide.RIGHT:
                            position = new Vector3(x + CellWidth - wallDiff, y, z + CellHeight / 2f);
                            rotation = Quaternion.Euler(0, -90f, 0);
                            doorText.transform.position = new Vector3(position.x - doorDiff, position.y, position.z);

                            break;
                        case ExitSide.BOTTOM:
                            position = new Vector3(x + CellWidth / 2f, y, z + wallDiff);
                            doorText.transform.position = new Vector3(position.x, position.y, position.z + doorDiff);

                            break;
                        case ExitSide.TOP:
                            position = new Vector3(x + CellWidth / 2f, y, z + CellHeight - wallDiff);
                            rotation = Quaternion.Euler(0, 180f, 0);
                            doorText.transform.position = new Vector3(position.x, position.y, position.z - doorDiff);

                            break;
                           
                    }

                    

                    if (cell.ExitSide != ExitSide.NONE)
                    {
                        doorText.transform.rotation = rotation;
                        var instantiate = Instantiate(ExitDoor,
                            position,
                            rotation);
                        var exitScript = instantiate.GetComponent<ExitScript>();
                        exitScript.Player = player;
                        exitScript.doorText = doorText;
                        levelObjects.Add(instantiate);
                    }

                   
                }

                if (Cube != null && maze[row, column].hasCube)
                {
                    var randomX = Random.Range(CellWidth / 2f - 1, CellWidth / 2f + 1);
                    var randomY = Random.Range(CellHeight / 2f - 1, CellHeight / 2f + 1);
                    
                    var instantiate = Instantiate(Cube,
                        new Vector3(x + randomX, y - 2, z + randomY),
                        Quaternion.identity);

                    var component = instantiate.GetComponent<Cube>();
                    component.player = player;
                    levelObjects.Add(instantiate);

                }
                
                if (MeduimEnemy != null && maze[row, column].enemyType == EnemyType.Medium)
                {
                    var instantiate = Instantiate(MeduimEnemy,
                        new Vector3(x + CellWidth / 2f, y - 2, z + CellHeight / 2f),
                        Quaternion.identity);
                    var component = instantiate.GetComponent<Enemy>();
                    component.cell = maze[row, column];
                    component.maze = maze;
                    component.player = player;
                    component.CellWidth = CellWidth;
                    component.CellHeight = CellHeight;
                    levelObjects.Add(instantiate);

                    
                }
                
                if (SmallEnemy != null && maze[row, column].enemyType == EnemyType.Small)
                {
                    var instantiate = Instantiate(SmallEnemy,
                        new Vector3(x + CellWidth / 2f, y - 2, z + CellHeight / 2f),
                        Quaternion.identity);
                    var component = instantiate.GetComponent<Enemy>();
                    component.cell = maze[row, column];
                    component.maze = maze;
                    component.player = player;
                    component.CellWidth = CellWidth;
                    component.CellHeight = CellHeight;
                    levelObjects.Add(instantiate);

                    
                }
                
                if (Trap != null && maze[row, column].enemyType == EnemyType.Trap)
                {
                    var instantiate = Instantiate(Trap,
                        new Vector3(x + CellWidth / 2f, y - 2, z + CellHeight / 2f + 0.5f),
                        Quaternion.identity);
                    var component = instantiate.GetComponent<Trap>();
                    component.Cell = maze[row, column];
                    component.player = player;
                    levelObjects.Add(instantiate);
                }
    
            }
        }
    }
    
    

    void Start()
    {
        player.winText = winText;
        
    }

    private void Update()
    {
    }
}