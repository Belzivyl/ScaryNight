using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{
    public Image HPImage;
    public TextMeshProUGUI KillCounterText;
    public Action OnChangeKillCounterViewText;
    public Action OnActivateLevelCompleteMenu;
    [SerializeField] private Button _nextLevelLevelComplete;
    [SerializeField] private Button _mainMenuLevelComplete;
    [SerializeField] private Button _newGameDefeat;
    [SerializeField] private Button _mainMenuDefeat;
    [SerializeField] private Button _startGameStart;
    public TextMeshProUGUI LevelCompleteMenuText;
    public GameObject LevelCompleteMenu;
    public GameObject DefeatMenu;
    [SerializeField] private GameObject StartMenu;
    public Action OnReloadUI;

    private void Awake() 
    {
        if (SceneManageController.sceneManagerSO.CurrentSceneNumber == 0) 
        {
            StartMenu.SetActive(true);
            Time.timeScale = 0;
        }
        _nextLevelLevelComplete.onClick.AddListener(OnClickNextLevel);
        _mainMenuLevelComplete.onClick.AddListener(OnClickMainMenu);
        _startGameStart.onClick.AddListener(OnClickStart);
        _mainMenuDefeat.onClick.AddListener(OnClickMainMenu);
        _newGameDefeat.onClick.AddListener(OnClickNewGame);
    }

    public void ManageBlinkingCoroutine(IEnumerator coroutine, bool shouldBlink) 
    {
        if (shouldBlink == true)
        {
            StartCoroutine(coroutine);
        }
        else { StopCoroutine(coroutine); }
    }

    public void ChangeKillCounterText() 
    {
        OnChangeKillCounterViewText?.Invoke();
    }

    public void ActivateLevelCompleteMenu()
    {
        OnActivateLevelCompleteMenu?.Invoke();
    }

    public void OnClickNextLevel()
    {
        SceneManager.LoadScene(0);
        SceneManageController.IncreaseLevelDifficulty();
        SceneManageController.IncreaseLevelNumber();
        SceneManageController.sceneManagerSO.SceneCurrentKillCount = 0;
        Time.timeScale = 1;
    }

    public void OnClickMainMenu()
    {
        //SceneManagerSO.SceneCurrentKillCount = 0;
        SceneManageController.RestoreToDefaults();
        SceneManageController.sceneManagerSO.CurrentSceneNumber = 0;
        ReloadUI();
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
    }

    public void OnClickStart()
    {
        SceneManageController.RestoreToDefaults();
        ReloadUI();
        StartMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene(0);
        SceneManageController.RestoreToDefaults();
        Time.timeScale = 1;
    }

    private void ReloadUI() 
    { 
        OnReloadUI?.Invoke();
    }
}

