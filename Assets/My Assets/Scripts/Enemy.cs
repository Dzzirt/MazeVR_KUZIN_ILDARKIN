using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        PURSUE,
        ATTACK,
        DEAD
    }

    public enum Side
    {
        BACK,
        FRONT,
        RIGHT,
        LEFT
    }

    public Animator anim;

    public MazeCell[,] maze;

    public MazeCell cell;

    public Player player;

    public float CellWidth;
    public float CellHeight;

    public EnemyState enemyState = EnemyState.IDLE;

    public float MAX_ATTACK_DELAY = 2000;

    public float waitingTime = 1f;

    public float timer = 0f;

    public int range = 5;

    public MazeCell[,] waveMaze;

    public int lastPlayerCol;
    public int lastPlayerRow;


    public int health = 3;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            timer = 0f;
            updateState();
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Stone(Clone)")
        {
            health -= 1;
            if (health <= 0)
            {
                anim.Play("dead");
                enemyState = EnemyState.DEAD;
                gameObject.GetComponent<MeshCollider>().enabled = false;
//                Destroy(gameObject.GetComponent<Rigidbody>());
//                Destroy(gameObject.GetComponent<MeshCollider>());
            }
           
        }
        
        
    }

    private void updateRangeMazeIfNeeds()
    {
        if (player.cell != null)
        {
            if (player.cell.colPos > cell.colPos - range &&
                player.cell.colPos < cell.colPos + range &&
                player.cell.rowPos > cell.rowPos - range &&
                player.cell.rowPos < cell.rowPos + range)
            {
                var waveMaze1 = MazeCell.Clone<MazeCell[,]>(maze);
                var current = MazeCell.Clone(cell);
                var end = MazeCell.Clone(player.cell);

                var endCell = waveMaze1[end.rowPos, end.colPos];
                var mazeCells = createMap(waveMaze1, waveMaze1[current.rowPos, current.colPos], endCell, 1);
                var start = "";

                for (int i = 0; i < mazeCells.Length / mazeCells.GetLength(0); i++)
                {
                    start += "{ ";
                    for (int j = 0; j < mazeCells.GetLength(0); j++)
                    {
                        if (mazeCells[i, j].waveCounter < 10)
                        {
                            start += "0";
                        }
                        start += (mazeCells[i, j].waveCounter + " ");
                    }
                    start += " }\n";
                }

//                print(start);

                var way = new MazeCell[endCell.waveCounter - 1];

                findWay(waveMaze1, endCell, way);

                if (way.Length > 1)
                {
                    var newPos = new Vector3(way[1].colPos * CellWidth + CellWidth / 2f, - 2, way[1].rowPos * CellHeight + CellHeight / 2f);
                    var dif = newPos - transform.position;

                    if (dif.x < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f)); ;

                    }

                    if (dif.x > 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f)); ;

                    }
                    
                    

                    if (dif.z < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f)); ;

                    }
                    
                    if (dif.z > 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f)); ;
                    }
                  
                    
                    transform.position = newPos;
                    cell = way[1];
                }
                else
                {
                    enemyState = EnemyState.ATTACK;
                }

               
                
                var str = "";

                for (int i = 0; i < way.Length; i++)
                {
                    str += "{ " + way[i].rowPos + " " + way[i].colPos + " } ";
                }


//                print(str);
            }
            else
            {
                enemyState = EnemyState.IDLE;
            }
        }
    }

    private void findWay(MazeCell[,] waveMaze1, MazeCell end, MazeCell[] path)
    {
        if (end.waveCounter == 1)
        {
            return;
        }

        if (!end.WallBack && end.rowPos - 1 >= 0 &&
            waveMaze1[end.rowPos - 1, end.colPos].waveCounter + 1 == end.waveCounter)
        {
            var mazeCell = waveMaze1[end.rowPos - 1, end.colPos];
            path[mazeCell.waveCounter - 1] = mazeCell;
            findWay(waveMaze1, mazeCell, path);
        }
        else if (!end.WallFront && end.rowPos + 1 < waveMaze1.Length / waveMaze1.GetLength(0) &&
                 waveMaze1[end.rowPos + 1, end.colPos].waveCounter + 1 == end.waveCounter)
        {
            var mazeCell = waveMaze1[end.rowPos + 1, end.colPos];
            path[mazeCell.waveCounter - 1] = mazeCell;
            findWay(waveMaze1, mazeCell, path);
        }

        else if (!end.WallLeft && end.colPos - 1 >= 0 &&
                 waveMaze1[end.rowPos, end.colPos - 1].waveCounter + 1 == end.waveCounter)
        {
            var mazeCell = waveMaze1[end.rowPos, end.colPos - 1];
            path[mazeCell.waveCounter - 1] = mazeCell;
            findWay(waveMaze1, mazeCell, path);
        }

        else if (!end.WallRight && end.colPos + 1 < waveMaze1.GetLength(0) &&
                 waveMaze1[end.rowPos, end.colPos + 1].waveCounter + 1 == end.waveCounter)
        {
            var mazeCell = waveMaze1[end.rowPos, end.colPos + 1];
            path[mazeCell.waveCounter - 1] = mazeCell;
            findWay(waveMaze1, mazeCell, path);
        }
    }

    private MazeCell[,] createMap(MazeCell[,] waveMaze1, MazeCell current, MazeCell end, int counter)
    {
        if (current.isWaveVisited || end.waveCounter != 0)
        {
            return waveMaze1;
        }

        if ((current.colPos == end.colPos && current.rowPos == end.rowPos))
        {
            end.waveCounter = counter;
            return waveMaze1;
        }


        current.waveCounter = counter;
        current.isWaveVisited = true;


        if (!current.WallBack && current.rowPos - 1 >= 0)
        {
            createMap(waveMaze1, waveMaze1[current.rowPos - 1, current.colPos], end, counter + 1);
        }

        if (!current.WallFront && current.rowPos + 1 < waveMaze1.Length / waveMaze1.GetLength(0))
        {
            createMap(waveMaze1, waveMaze1[current.rowPos + 1, current.colPos], end, counter + 1);
        }

        if (!current.WallLeft && current.colPos - 1 >= 0)
        {
            createMap(waveMaze1, waveMaze1[current.rowPos, current.colPos - 1], end, counter + 1);
        }

        if (!current.WallRight && current.colPos + 1 < waveMaze1.GetLength(0))
        {
            createMap(waveMaze1, waveMaze1[current.rowPos, current.colPos + 1], end, counter + 1);
        }

        return waveMaze1;
    }

    private void updateState()
    {
        switch (enemyState)
        {
            case EnemyState.IDLE:
//                updateRangeMazeIfNeeds();

                checkZone();
                break;
            case EnemyState.ATTACK:
                
                var newPos = Camera.main.transform.position;
                var dif = newPos - transform.position;

                if (dif.x < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f)); ;

                }

                if (dif.x > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f)); ;

                }
                    
                    

                if (dif.z < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f)); ;

                }
                    
                if (dif.z > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f)); ;
                }
                
                anim.Play("attack");
                player.health -= 1;
                enemyState = EnemyState.PURSUE;
                break;
            case EnemyState.PURSUE:
                updateRangeMazeIfNeeds();
                break;
        }
    }
    

    void checkZone()
    {
        if (cell != null && player.cell != null)
        {
            checkSide(Side.BACK);
            checkSide(Side.FRONT);
            checkSide(Side.LEFT);
            checkSide(Side.RIGHT);
        }
    }

    private void checkSide(Side side)
    {
        var currentCell = cell;
        for (int i = 1; i < range; i++)
        {
            if (player.cell.rowPos == currentCell.rowPos && player.cell.colPos == currentCell.colPos)
            {
                enemyState = EnemyState.PURSUE;
            }

            var nextRow = cell.rowPos;
            var nextCol = cell.colPos;

            switch (side)
            {
                case Side.BACK:
                    nextRow -= i;

                    if (currentCell.WallBack || nextRow < 0)
                    {
                        i = range;
                    }
                    break;
                case Side.FRONT:
                    nextRow += i;

                    if (currentCell.WallFront || nextRow >= maze.Length / maze.GetLength(0))
                    {
                        i = range;
                    }
                    break;
                case Side.LEFT:
                    nextCol -= i;

                    if (currentCell.WallLeft || nextCol < 0)
                    {
                        i = range;
                    }
                    break;
                case Side.RIGHT:
                    nextCol += i;

                    if (currentCell.WallRight || nextCol >= maze.GetLength(0))
                    {
                        i = range;
                    }
                    break;
            }

            if (i == range)
            {
                break;
            }

            currentCell = maze[nextRow, nextCol];
        }
    }
}