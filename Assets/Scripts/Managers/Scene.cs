using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public string nextLevel;
    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MovetoNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
