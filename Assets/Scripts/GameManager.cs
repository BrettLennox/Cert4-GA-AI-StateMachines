using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    private void QuitApplication()
    {
        //closes the application
        Application.Quit();
#if UNITY_EDITOR
        //stops run of application if in the unity editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
