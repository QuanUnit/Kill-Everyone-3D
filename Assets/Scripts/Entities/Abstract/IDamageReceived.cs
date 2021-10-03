using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceived 
{
    int CurrentHP { get; }
    int MaxHP { get; }
    void TakeDamage(int value);
}
