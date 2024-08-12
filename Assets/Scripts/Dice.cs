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
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private float duration;
    [SerializeField] private bool isRolling = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(RollDice);
        targetRotation = new Vector3(0, 0, 720);
    }

    public void RollDice()
    {
        if (!isRolling) 
        { 
            isRolling = true;

            int rndValue = Random.Range(1, 7);

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
    }

    private void AnimateDice()
    {
        Vector2 originalPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = originalPosition + targetOffset; 

        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.InOutQuad).OnComplete(() => 
                                     rectTransform.DOAnchorPos(originalPosition, duration).SetEase(Ease.InOutQuad)));

        sequence.Join(rectTransform.DORotate(targetRotation, duration * 2 - 0.05f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));

        sequence.Append(rectTransform.DORotate(Vector3.zero, 0.2f, RotateMode.FastBeyond360) .SetEase(Ease.InOutQuad).OnComplete(() =>
                                    isRolling = false ));
    }
}
