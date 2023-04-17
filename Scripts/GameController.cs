using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<AIenemy> enemies;
    [SerializeField] bool wonGame;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        enemies.AddRange(GameObject.FindObjectsOfType<AIenemy>());
    }

    private void Start()
    {
        
    }
    public void Update()
    {
        if (enemies.Count <= 0 && !wonGame)
        {
            print("Youn Won The Game");
            StartCoroutine(Uimanager.instance.GameWonPanel());
            wonGame = true;
        }

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
