using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class RestartPanelController : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    [SerializeField] private GameObject _restartButton;

    public static RestartPanelController Instance { get; private set; }


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        DefaultValues();
    }


    public void GameOver()
    {
        _restartButton.SetActive(true);
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(0.85f, 0.3f).SetDelay(0.1f);
    }


    public void RestartGameOnClick()
    {
        GameController.CurrentLevel = 0;

        _fadeImage.DOFade(1, 0.75f);
        _restartButton.SetActive(false);

        StartCoroutine(ClearGameFieldDelay());
        StartCoroutine(TurnFadeImageOffDelay());
        StartCoroutine(StartGameDelay());
    }


    private void DefaultValues()
    {
        _restartButton.SetActive(false);
        _fadeImage.DOFade(0, 0);
        _fadeImage.gameObject.SetActive(false);
    }


    private IEnumerator ClearGameFieldDelay()
    {
        yield return new WaitForSeconds(0.75f);
        GameController.Instance.ClearGameField();
    }


    private IEnumerator TurnFadeImageOffDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _fadeImage.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _fadeImage.gameObject.SetActive(false);
    }


    private IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(0.8f);
        GameController.Instance.StartGame();
    }
}
