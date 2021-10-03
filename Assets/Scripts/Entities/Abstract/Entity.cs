using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, IDamageReceived
{
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    public bool IsImmortal => isImmortal;

    public UnityEvent<int, int> OnHPChanged;
    public UnityEvent OnDeath;

    [SerializeField] protected Animator animator;
    [SerializeField] protected int maxHP;
    [SerializeField] protected bool isImmortal;


    private int currentHP;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (isImmortal == true || currentHP <= 0)
            return;

        currentHP -= damage;
        OnHPChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
            Die();
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
    }
}
