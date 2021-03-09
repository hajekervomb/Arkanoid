using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

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

    public bool isGameStarted { get; set; }

    private static int _score;
    public static int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    [SerializeField] private int startLives = 2;
    [SerializeField] private int maxLives = 5;

    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
        set { _remainingLives = value; }
    }

    private BallsManager bm;
    private BlocksManager bricksManager;

    [SerializeField] private Transform player;

    [SerializeField] private GameObject gameOverUI;

    private void Start()
    {
        bm = BallsManager.Instance;
        bricksManager = BlocksManager.Instance;
        // this allow to set user's screen resolution
        // Screen.SetResolution()
        _remainingLives = startLives;

        Ball.ballDeath += ResetPosition;
        Block.onBrickDestroy += EndLevelContainer;
    }

    public void ChangeScore(Object sender, MyEventArgs args)
    {
        Score += args.scorePoints;
    }

    public void ResetPosition() // for racket
    {
        // reset reacket position when all balls destroyed
        isGameStarted = false;
        player.position = new Vector3(0, -95f, 0);        

    }

    public void KillPlayer()
    {
        if (bm.Balls.Count <= 0)
        {
            _remainingLives -= 1;
            Debug.Log("MINUS LIFE!");

            if (_remainingLives < 1)
            {
                // show game over screen
                //Ball.ballDeath -= ResetPosition;
                EndGame();
            }
            else
            {
                bm.InitBall();
                BlocksManager.Instance.LoadLevel(BlocksManager.Instance.CurrentLevel);
            }
        }

    }

    public void EndLevelContainer(Block obj)    // it's just a container for coroutine "End Level"
    {
        StartCoroutine(EndLevel());
    }

    public IEnumerator EndLevel()   // check for remaining bricks when a brick destroyed
    {
        if (bricksManager.RemainingBricks.Count <= 0)
        {
            // wait for sec
            
            isGameStarted = false;
            ResetPosition();
            Debug.Log("RESET PLAYER POS!");
            
            bm.InitBall();

            yield return new WaitForSeconds(5f);

            bricksManager.LoadNextLevel();
        }
        
    }

    public void EndGame()
    {
        Debug.Log("No Lives Remaining! END GAME!");
        gameOverUI.SetActive(true);
    }

    private void OnDisable()
    {
        Ball.ballDeath -= ResetPosition;
        Block.onBrickDestroy -= EndLevelContainer;
    }
}
