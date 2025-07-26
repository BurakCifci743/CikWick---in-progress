using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] CatController _catController;
    [SerializeField] EggCounter_UI _eggCounter_UI;
    [SerializeField] WinLose_UI _winLoseUI;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private PlayerHealth_UI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay = 1f;

    private GameState _currentGameState;
    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
        if (_healthManager != null)
            _healthManager.OnHealthZero += OnPlayerDeath;
    }
    void Start()
    {
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void CatController_OnCatCatched()
    {
        _playerHealthUI.AnimateDamageForAll();
       StartCoroutine(OnGameOver());
    }

    private void OnDisable()
    {
        if (_healthManager != null)
            _healthManager.OnHealthZero -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        TriggerGameOver();
        
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Current Game State: " + gameState);
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounter_UI.SetEggCounterText(_currentEggCount, _maxEggCount);


        if (_currentEggCount == _maxEggCount)
        {
            //WIN SITUATION
            _eggCounter_UI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }
    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }
    public void TriggerGameOver()
    {
        if (_currentGameState == GameState.GameOver) return;
        StartCoroutine(OnGameOver());
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

}
