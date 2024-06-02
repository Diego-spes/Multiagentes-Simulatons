using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void restart()
    {
        SceneManager.LoadScene("Evidencia");
    }


}