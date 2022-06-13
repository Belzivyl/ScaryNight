using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : iOnDestroy, iUpdate
{
    #region Player
    private readonly PlayerHealthUIModel _playerHealthUIModel;
    private readonly PlayerView _playerView;
    private readonly PlayerUIView _playerUIView;
    private readonly PlayerModel _playerModel;
    private Image _bloodMask;
    private GameObject _levelCompleteMenu;
    private Color _color;
    public float IncreseValueBloodMaskColorA = 2;
    public float flashDuration = 1;
    private IEnumerator _blink;
    public TextMeshProUGUI KillCounterText;
    private SceneManagerSO _sceneManagerSO;
    private TextMeshProUGUI _killCounterText;
    private TextMeshProUGUI _levelCompleteMenuText;
    #endregion

    public PlayerUIController(PlayerHealthUIModel playerHealthUIModel, PlayerView playerView, PlayerUIView playerUIView, PlayerModel playerModel)
    {
        _playerHealthUIModel = playerHealthUIModel;
        _playerView = playerView;
        _playerUIView = playerUIView;
        _bloodMask = _playerHealthUIModel.HPImage;
        _levelCompleteMenu = playerUIView.LevelCompleteMenu;
        _sceneManagerSO = SceneManageController.sceneManagerSO;
        _killCounterText = _playerUIView.KillCounterText;
        _levelCompleteMenuText = _playerUIView.LevelCompleteMenuText;
        _playerModel = playerModel;
        _blink = Blink();
    }

    public void SubscribeOnEvents()
    {
        _playerView.OnHPChangeTrack += IncreaseBloodMaskAlpha;
        _playerView.OnPlayerDeath += StopHealthUICoroutine;
        _playerUIView.OnChangeKillCounterViewText += ChangeKillCounteText;
        _playerUIView.OnActivateLevelCompleteMenu += ActivateLevelCompleteMenu;
        _playerUIView.OnReloadUI += MakeKillCountTextToDefalt;
    }

    public void OnDestroy()
    {
        _playerView.OnHPChangeTrack -= IncreaseBloodMaskAlpha;
        _playerView.OnPlayerDeath -= StopHealthUICoroutine;
        _playerUIView.OnChangeKillCounterViewText -= ChangeKillCounteText;
        _playerUIView.OnActivateLevelCompleteMenu -= ActivateLevelCompleteMenu;
        _playerUIView.OnReloadUI -= MakeKillCountTextToDefalt;
    }

    private void IncreaseBloodMaskAlpha(float damagePercentageOftwoThirdsOfPlayerHP)
    {
        _color.a += damagePercentageOftwoThirdsOfPlayerHP / IncreseValueBloodMaskColorA;
        if (_color.a <= _playerHealthUIModel.MaxValueBloodMaskColorAlpha && _playerHealthUIModel.MaxAlphaReached != true)
        {
            _bloodMask.color = _color;
            _playerUIView.HPImage.color = _color;
        }
        else if (_playerHealthUIModel.MaxAlphaReached != true)
        {
            _playerHealthUIModel.MaxAlphaIsReached(true);
            _playerUIView.ManageBlinkingCoroutine(_blink, true);
        }
    }

    public void StopHealthUICoroutine(bool value)
    {
        _playerUIView.ManageBlinkingCoroutine(_blink, value);
    }

    public void MakePlayerImageAlphaChanelToZero()
    {
        _color = Color.white;
        _color.a = 0.0f;
        _bloodMask.color = _color;
        _color = _bloodMask.color;
    }

    private IEnumerator Blink()
    {
        while (_playerHealthUIModel.MaxAlphaReached == true)
        {
            for (float t = 0; t <= flashDuration; t += Time.deltaTime)
            {
                _color = _playerUIView.HPImage.color;
                _color.a = Mathf.Lerp(_playerHealthUIModel.MaxValueBloodMaskColorAlpha, 0, t / flashDuration);
                _playerUIView.HPImage.color = _color;
                yield return null;
            }
            for (float t = 0; t <= flashDuration; t += Time.deltaTime)
            {
                _color = _playerUIView.HPImage.color;
                _color.a = Mathf.Lerp(0, _playerHealthUIModel.MaxValueBloodMaskColorAlpha, t / flashDuration);
                _playerUIView.HPImage.color = _color;
                yield return null;
            }
        }
    }

    private void ChangeKillCounteText()
    {
        _killCounterText.text = _sceneManagerSO.SceneCurrentKillCount + " / " + _sceneManagerSO.SceneMaxKillCount;
    }

    public void MakeKillCountTextToDefalt()
    {
        _killCounterText.text = _sceneManagerSO.SceneCurrentKillCount + " / " + _sceneManagerSO.SceneMaxKillCount;
    }

    private void ActivateLevelCompleteMenu()
    {
        if (_sceneManagerSO.SceneCurrentKillCount >= _sceneManagerSO.SceneMaxKillCount && _playerModel.CurrentHP > 0)
        {
            _playerUIView.StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait() 
    { 
        for(float t = 0; t <= 1.1f; t+= Time.deltaTime) 
        {
            yield return null;
        }
        Time.timeScale = 0;
        _levelCompleteMenu.SetActive(true);
        _levelCompleteMenuText.text = "Well done! Level " + _sceneManagerSO.CurrentSceneNumber + " completed";
    }

    public void Update()
    {
        ActivateLevelCompleteMenu();
    }
}

