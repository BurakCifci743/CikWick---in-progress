using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Timer_UI _timerUI;

    
    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();    
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);

    }
    private void OnOneMoreButtonClicked()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);

        // Calling From Same Scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}
