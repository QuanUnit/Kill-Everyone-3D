using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static IEnumerator Invoke(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);

        action?.Invoke();
    }
}
