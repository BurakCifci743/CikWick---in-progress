using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState_UI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;

    [Header("Images")]

    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingInactiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingInactiveSprite;


    [Header("Settings")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;
    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => _holyBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => _rottenBoosterWheatImage;


    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.gameObject.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingInactiveSprite, _playerWalkingTransform, _playerSlidingTransform);

    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingInactiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;
            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetStateUserInterface(_playerWalkingInactiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;

        }
    }

    private void SetStateUserInterface(Sprite playerWalkinSprite, Sprite playerSlidingSprite, RectTransform activeTransform, RectTransform inactiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkinSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        inactiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterface(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite inactiveSprite, Sprite activeWheatSprite, Sprite inactiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = inactiveSprite;
        wheatImage.sprite = inactiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }
    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite inactiveSprite, Sprite activeWheatSprite, Sprite inactiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterface(activeTransform,boosterImage,wheatImage,activeSprite,inactiveSprite,activeWheatSprite,inactiveWheatSprite,duration));
    }
}

