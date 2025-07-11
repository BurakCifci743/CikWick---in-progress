using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [Header("References")]
    [SerializeField] EggCounter_UI _eggCounter_UI;

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    private int _currentEggCount;
    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounter_UI.SetEggCounterText(_currentEggCount, _maxEggCount);


        if (_currentEggCount == _maxEggCount)
        {
            //WIN SITUATION
            Debug.Log("You Win!!");
            _eggCounter_UI.SetEggCompleted();
        }
        Debug.Log("Current Egg Count: " + _currentEggCount);
    }

}
