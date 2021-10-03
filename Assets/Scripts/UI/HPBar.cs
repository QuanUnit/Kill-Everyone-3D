using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class HPBar : MonoBehaviour
{
    [SerializeField] private Entity target;

    private Image barImage;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        barImage = GetComponent<Image>();
        target.OnHPChanged.AddListener(ChangeValue);
    }

    public void ChangeValue(int value, int maxValue)
    {
        animator.SetTrigger("Change");
        barImage.fillAmount = (float)value / maxValue;
    }
}
