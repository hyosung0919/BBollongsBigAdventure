using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleReturnManager : MonoBehaviour
{
    public static void ReturnToStage()
    {
        // 커서 다시 보이게
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        string stageName = $"Stage_{BattleData.stageIndex}";
        SceneManager.LoadScene(stageName, LoadSceneMode.Single);
    }
}
