using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;

    void Start()
    {
        ChangeState(PlayerState.Idle);
    }
    public void ChangeState(PlayerState newPlayerState)
    {
        if (newPlayerState == _currentPlayerState) { return; }
        _currentPlayerState = newPlayerState;
    }

    public PlayerState GetPlayerState()
    {
        return _currentPlayerState;
    }
    public PlayerState GetCurrentState()
    {
        return _currentPlayerState;
     }
}
