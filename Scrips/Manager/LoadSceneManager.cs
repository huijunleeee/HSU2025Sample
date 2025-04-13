using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * File :   LoadSceneManager.cs
 * Desc :   씬 변환
 */

public class LoadSceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
