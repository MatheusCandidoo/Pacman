using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public void Iniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
