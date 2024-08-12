using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    int diceResult;

    [SerializeField] private Button button;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private WaitForSeconds waitForDice;
    [SerializeField] private Vector2 targetOffset;
    [SerializeField] private float duration;
    [SerializeField] private Vector2 diceMove;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(RollDice);
        waitForDice = new WaitForSeconds(0.05f);
        diceMove = new Vector2(0, 40);
    }

    public void RollDice()
    {
        int rndValue = Random.Range(1, 7);
        Debug.Log(rndValue);

        switch (rndValue)
        {
            case 1:
                animator.SetTrigger("Dice1");
                break;
            case 2:
                animator.SetTrigger("Dice2");
                break;
            case 3:
                animator.SetTrigger("Dice3");
                break;
            case 4:
                animator.SetTrigger("Dice4");
                break;
            case 5:
                animator.SetTrigger("Dice5");
                break;
            case 6:
                animator.SetTrigger("Dice6");
                break;
            default:
                animator.SetTrigger("Dice1"); 
                break;
        }

        AnimateDice();
    }

    private void AnimateDice()
    {
        Vector2 originalPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = originalPosition + targetOffset; 

        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOAnchorPos(targetPosition, duration)
                                     .SetEase(Ease.InOutQuad));

        sequence.Join(rectTransform.DORotate(new Vector3(0, 0, 720), duration * 2, RotateMode.LocalAxisAdd)
                                 .SetEase(Ease.InOutQuad));

        sequence.Append(rectTransform.DOAnchorPos(originalPosition, duration)
                                     .SetEase(Ease.InOutQuad));

        sequence.Join(rectTransform.DORotate(Vector3.zero, 0.01f, RotateMode.FastBeyond360)
                             .SetEase(Ease.InOutQuad));
    }

    IEnumerator RollAnimation()
    {
        Vector3 originalPosition = rectTransform.anchoredPosition;

        Quaternion newRotation = Quaternion.identity;

        yield return waitForDice;

        rectTransform.anchoredPosition += diceMove;
        newRotation = Quaternion.Euler(0f, 0f, 120f);
        transform.rotation = newRotation;

        yield return waitForDice;

        rectTransform.anchoredPosition += diceMove;
        newRotation = Quaternion.Euler(0f, 0f, 240f);
        transform.rotation = newRotation;

        yield return waitForDice;

        rectTransform.anchoredPosition += diceMove;
        newRotation = Quaternion.Euler(0f, 0f, 360f);
        transform.rotation = newRotation;

        yield return waitForDice;

        rectTransform.anchoredPosition -= diceMove;
        newRotation = Quaternion.Euler(0f, 0f, 120f);
        transform.rotation = newRotation;

        yield return waitForDice;

        rectTransform.anchoredPosition -= diceMove;
        newRotation = Quaternion.Euler(0f, 0f, 240f);
        transform.rotation = newRotation;

        yield return waitForDice;

        rectTransform.anchoredPosition -= diceMove;
        newRotation = Quaternion.Euler(0f, 0f, 360f);
        transform.rotation = newRotation;

    }
}
