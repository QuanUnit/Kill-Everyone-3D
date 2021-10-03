using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                    throw new System.NullReferenceException(nameof(T));
            }

            return instance;
        }
    }

    private static T instance;
}
