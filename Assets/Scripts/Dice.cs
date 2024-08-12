using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public int diceResult;

    [SerializeField] private Button button;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Animator animator;
    
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private Vector2 targetOffset;
    [SerializeField] private Vector2 originalPosition;

    [SerializeField] private float duration;
    [SerializeField] private bool isRolling = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(RollDice);
        originalPosition = rectTransform.anchoredPosition;
        targetRotation = new Vector3(0, 0, 1080);
    }

    public void RollDice()
    {
        if (!isRolling) 
        { 
            isRolling = true;

            diceResult = Random.Range(1, 7);
            Debug.Log(diceResult);

            switch (diceResult)
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
        Vector2 targetPosition = originalPosition + targetOffset; 

        Sequence sequence = DOTween.Sequence();

        sequence.Append(rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.InOutQuad).OnComplete(() => 
                                     rectTransform.DOAnchorPos(originalPosition, duration).SetEase(Ease.InOutQuad)));

        sequence.Join(rectTransform.DORotate(targetRotation, duration * 2 - 0.05f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));

        sequence.Append(rectTransform.DORotate(Vector3.zero, 0.2f, RotateMode.FastBeyond360) .SetEase(Ease.InOutQuad).OnComplete(() =>
                                    isRolling = false ));
    }
}
