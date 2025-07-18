using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState _currentCatState = CatState.Walking;

    public void ChangeState(CatState newState)
    {
        if (newState == _currentCatState) { return; }
        _currentCatState = newState;
    }
    void Start()
    {
        _currentCatState = CatState.Walking;
    }

    public CatState GetCurrentState()
    {
        return _currentCatState;
    } 
    
}
