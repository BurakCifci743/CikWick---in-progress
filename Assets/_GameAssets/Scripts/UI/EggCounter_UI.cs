using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounter_UI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;
    [Header("Settings")]
    [SerializeField] private Color _eggCounterColor;
    [SerializeField] private float _eggColorChangeDuration;
    [SerializeField] private float _eggScaleChangeDuration;

    private RectTransform _eggCounterRectTransform;


    void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }

    public void SetEggCounterText(int counter, int max)
    {
        _eggCounterText.text = counter.ToString() + " / " + max.ToString();
    }
    public void SetEggCompleted()
    {
        _eggCounterText.DOColor(_eggCounterColor, _eggColorChangeDuration);
        _eggCounterRectTransform.DOScale(1.2f,_eggScaleChangeDuration).SetEase(Ease.OutBack);
    }
    
}
