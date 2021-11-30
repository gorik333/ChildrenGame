using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image _cellSprite;
    [SerializeField] private Animator _cellAnimator;
    [SerializeField] private Button _cellButton;

    private string _cellName;


    public void SetCell(QuestionData questionData)
    {
        _cellSprite.sprite = questionData.CellImage;
        _cellName = questionData.Answer;
    }


    public void FirstLevelBounce()
    {
        _cellAnimator.enabled = true;
        _cellButton.interactable = false;
        _cellAnimator.SetTrigger("FirstLevel");

        StartCoroutine(TurnAnimatorOffDelay(0.8f));
        StartCoroutine(TurnButtonOnDelay(0.8f)); // waiting for animator Bounce effect anim duration
    }


    public void CheckAswerOnClick()
    {
        if (_cellName.Equals(CellGenerator.Instance.CorrectAnswer))
        {
            _cellAnimator.enabled = true;
            _cellAnimator.SetTrigger("Win");
            _cellButton.interactable = false;

            GameController.Instance.SpawnWinParticles(transform.position);

            StartCoroutine(NewLevelDelay(0.85f)); // waiting for win bounce effect
            StartCoroutine(TurnAnimatorOffDelay(0.75f)); 
        }
        else
        {
            _cellButton.interactable = false;
            _cellSprite.transform.DOScale(0.75f, 0.5f).SetEase(Ease.InBounce);
            _cellSprite.transform.DOScale(1f, 0.5f).SetDelay(0.5f);

            StartCoroutine(TurnButtonOnDelay(1.25f)); // wrong answer anim duration
        }
    }


    private IEnumerator NewLevelDelay(float duration)
    {
        yield return new WaitForSeconds(duration); 
        GameController.Instance.GenerateNewLevel();
    }


    private IEnumerator TurnButtonOnDelay(float duration)
    {
        yield return new WaitForSeconds(duration); 
        _cellButton.interactable = true;
    }


    private IEnumerator TurnAnimatorOffDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        _cellAnimator.enabled = false;
    }
}
