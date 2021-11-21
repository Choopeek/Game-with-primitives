using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public Button restartButton;
    private SpawnManager spawnManager;
    private Enemy enemy;
    public GameObject gameOverPlane;
    private Vector3 gameOverPos;
    public GameObject gameOverPlanePrefab;
    public GameObject victoryText;

    public bool enemyGotTheBase;
    public bool enemyKilledPlayer;
    public bool victory;
    public bool victoryTrigger;

    private MusicController musicController;

    public Animator transition;
    public float sceneTransitionTime = 1f;

    [SerializeField] GameObject menu;

    private void Awake()
    {
        musicController = GameObject.Find("Music").GetComponent<MusicController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPos = gameOverPlane.transform.position;
        Destroy(gameOverPlane.gameObject);
        //linking spawnmanager to gamemanager
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        StartGame();
        musicController.MusicChangeToCalm();

        victory = false;
        victoryTrigger = true;
        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnManager.enemiesCount == 0 && victory && victoryTrigger)
        {
            victoryTrigger = false;
            StartCoroutine(LoadNextLevel());

        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!IsGamePaused())
            {
                PauseMenu();
            }
            else
            {
                ResumeGame();
            }
            
        }
    }

    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        spawnManager.gameIsActive = true;
        StartCoroutine(spawnManager.SpaceshipsArmadaFlying());
        
    }

    public void GameOver()
    {
        if (!enemyKilledPlayer)
        {
            Instantiate(gameOverPlanePrefab, new Vector3(gameOverPos.x, gameOverPos.y, gameOverPos.z), gameOverPlanePrefab.transform.rotation);
        }
            if (enemyKilledPlayer)
        {
            
            restartButton.gameObject.SetActive(true);
        }
    }

    public void MusicIntensifies()
    {
        musicController.MusicChangeToDynamic();
    }

    public void Victory()
    {
        victory = true;
        spawnManager.gameIsActive = false;

    }

    IEnumerator LoadNextLevel()
    {        
        victoryText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        StartCoroutine(LevelFadeAndLoad());

    }

    IEnumerator LevelFadeAndLoad()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(sceneTransitionTime);
        SceneManager.LoadScene("Level 2");
    }


    bool IsGamePaused()
    {
        if (Time.timeScale == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void PauseMenu()
    {
        Time.timeScale = 0;
        menu.SetActive(true);

    }

    public void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
