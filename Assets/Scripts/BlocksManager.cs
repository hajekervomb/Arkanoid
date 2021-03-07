using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//TODO: how autogeneration works?

public class BlocksManager : MonoBehaviour
{
    #region Singleton


    public static BlocksManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private int maxRows = 6;
    private int maxCols = 12;
    private GameObject bricksContainer;
    private float initialBrickSpawnPositionX = -93f;
    private float initialBrickSpawnPositionY = 95.5f;
    public float shiftXAmount = 17f;
    public float shiftYamount = 9f;
    
    public Block brickPrefab;

    public Sprite[] sprites;
    public Color[] brickColors;

    public List<Block> RemainingBricks { get; set; }

    public List<int[,]> LevelsData { get; set; }

    public int CurrentLevel;

<<<<<<< Updated upstream
    public event Action BlockDestroyed;

    private void OnBlockDestroyed()
    {
        BlockDestroyed?.Invoke();
    }
    
=======
    private GameManager gm;
    private BallsManager bm;    
>>>>>>> Stashed changes

    private void Start()
    {
        gm = GameManager.Instance;     // get references
        bm = BallsManager.Instance;

        this.bricksContainer = new GameObject("BricksContainer");
        
        LevelsData = LoadLevelsData();
        GenerateBricks();
    }

    public void LoadLevel(int level)
    {
        CurrentLevel = level;
        ClearRemainingBricks(); // optional - reset all brick on current level
        GenerateBricks();
    }

    public void LoadNextLevel()
    {       

        CurrentLevel++; // increment the level value

        if (CurrentLevel >= LevelsData.Count) //we completed all levels
        {
            //show victory screen
            Debug.Log("FLAWLESS VICTORY!");
        }
        else
        {
            LoadLevel(CurrentLevel);
        }
    }

    private void ClearRemainingBricks()
    {
        foreach (var brick in RemainingBricks.ToList())
        {
            Destroy(brick.gameObject);
        }
    }

    private void GenerateBricks()
    {
        RemainingBricks = new List<Block>();    // initizialization of collection
        int[,] currentLevelData = LevelsData[CurrentLevel];
        float currentSpawnX = initialBrickSpawnPositionX;
        float currentSpawnY = initialBrickSpawnPositionY;
        //little trick with Z shift
        float zShift = 0;

        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                int brickType = currentLevelData[row, col];

                if (brickType > 0 )
                {
                    Block newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0f - zShift), Quaternion.identity) as Block;
                    newBrick.Init(bricksContainer.transform, this.sprites[brickType - 1], this.brickColors[brickType - 1], brickType);

                    RemainingBricks.Add(newBrick);
                    // z-shift is a trick for not overlapping
                    zShift += 0.0001f;
                }

                //once we spawned a brick, we need a shift for X value
                currentSpawnX += shiftXAmount;
                if (col + 1 == this.maxCols)
                {
                    currentSpawnX = initialBrickSpawnPositionX;
                }
            }

            currentSpawnY -= shiftYamount;

        }

    }

    // this whole method basically
    // reads the file
    // parses the file information
    // create List of matrices
    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;
        // split our text in rows
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        // create temp variable to store all levels data
        List<int[,]> levelsData = new List<int[,]>();
        // another variable to store current level data
        int[,] currentLevel = new int[maxRows, maxCols];
        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }

                currentRow++;
            }
            else
            {
                // end of current level
                // add the matrix to the last and continue the loop
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[maxRows, maxCols];
            }
        }

        return levelsData;
    }
}
