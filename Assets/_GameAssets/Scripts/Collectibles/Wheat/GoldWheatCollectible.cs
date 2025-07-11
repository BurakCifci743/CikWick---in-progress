using UnityEngine;
using UnityEngine.UI;


public class GoldWheatCollectible : MonoBehaviour, ICollectible
{

    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerState_UI _playerStateUI;
    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;
    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }


    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetGoldBoosterWheatImage,_wheatDesignSO.activeSprite,_wheatDesignSO.inactiveSprite,_wheatDesignSO.activeWheatSprite,_wheatDesignSO.inactiveWheatSprite,_wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
