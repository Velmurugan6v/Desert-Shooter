using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Uimanager : MonoBehaviour
{
    public static Uimanager instance;

    //Ammo UI Text
    [SerializeField] TextMeshProUGUI _currentAmmoText;
    [SerializeField] TextMeshProUGUI _remainAmmoText;

    //Soldier UI Tect
    private int _totalSoldierCount;
    [SerializeField] TextMeshProUGUI _remainSoldierText;
    [SerializeField] TextMeshProUGUI _totalSoldierText;

    //Player Health Slider/Bar
    [SerializeField] Slider _playerHealthBar;

    //Game Over Panel
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameWonPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _gameOverPanel.SetActive(false);
        _gameWonPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerHealthBar = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        _currentAmmoText = GameObject.Find("CurrentAmmoText").GetComponent<TextMeshProUGUI>();
        _remainAmmoText = GameObject.Find("RemainAmmoText").GetComponent<TextMeshProUGUI>();
        _remainSoldierText = GameObject.Find("RemainSoldierText").GetComponent<TextMeshProUGUI>();
        _totalSoldierText = GameObject.Find("TotalSoldierText").GetComponent<TextMeshProUGUI>();
        _totalSoldierCount= GameController.instance.enemies.Count;
        _playerHealthBar.maxValue = PlayerMovement.instance.Health;
        UpdateSoldierCount();
    }

    //Update Ammo Text
    public void UpdateAmmo(int currentAmmoValue, int ammoInReserve)
    {
        _currentAmmoText.text = currentAmmoValue.ToString();
        _remainAmmoText.text = "/ " +ammoInReserve.ToString();
    }

    public void UpdatePlayerHealth(int damageAmount)
    {
        //int currentPlayerHealth = PlayerMovement.instance.Health;
        //currentPlayerHealth -= damageAmount;
        //_playerHealthBar.value = currentPlayerHealth;
        PlayerMovement.instance.Health -= damageAmount;
        _playerHealthBar.value = PlayerMovement.instance.Health;

        if (PlayerMovement.instance.Health == 0)
        {
            PlayerMovement.instance.PlayerDeath();
            StartCoroutine(RestartGamePanel());
        }
    }

    IEnumerator  RestartGamePanel()
    {
        yield return new WaitForSeconds(1);
        _gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<Gun>().enabled = false;
        FindObjectOfType<CameraControll>().enabled = false;
        Cursor.visible = true;
    }

    public IEnumerator GameWonPanel()
    {
        yield return new WaitForSeconds(1f);
        _gameWonPanel.SetActive(true);
        FindObjectOfType<Gun>().enabled = false;
        FindObjectOfType<CameraControll>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UpdateSoldierCount()
    {
        _totalSoldierText.text = "/" + _totalSoldierCount;
        _remainSoldierText.text= ""+GameController.instance.enemies.Count;
    }

}
