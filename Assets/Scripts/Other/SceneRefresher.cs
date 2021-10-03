using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRefresher : MonoBehaviour
{
    public void RefreshScene()
    {
        StartCoroutine(Tools.Invoke(1, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)));
    }
}
