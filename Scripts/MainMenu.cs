using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] GameObject _optionsMenu;

    private void Start()
    {
        _optionsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(level);
    }

    public void OptionMenuOpen()
    {
        _optionsMenu.SetActive(true);
    }

    public void OptionMenuClose()
    {
        _optionsMenu.SetActive(false);
    }

    public void SelectFPP()
    {
        level = 1;
    }

    public void SelectTPP()
    {
        level = 2;
    }

    public void QiutGame()
    {
        print("Qiut");
        Application.Quit();
    }




}
