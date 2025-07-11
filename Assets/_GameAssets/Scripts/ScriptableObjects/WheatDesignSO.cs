using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesingSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;
    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _inactiveWheatSprite;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
    public Sprite activeSprite => _activeSprite;
    public Sprite inactiveSprite => _inactiveSprite;
    public Sprite activeWheatSprite => _activeWheatSprite;
    public Sprite inactiveWheatSprite => _inactiveWheatSprite;

}
