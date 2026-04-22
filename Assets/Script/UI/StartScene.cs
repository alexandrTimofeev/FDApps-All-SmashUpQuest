using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private void Awake()
    {
        //StartSceneButton();
    }

    public void StartSceneButton()
    {
        // Проверяем, есть ли сохраненная сцена
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            // Получаем сохраненное название сцены
            int savedScene = PlayerPrefs.GetInt("SavedScene");

            if (savedScene <= 0 || savedScene >= SceneManager.sceneCountInBuildSettings)
            {
                savedScene = 2;
            } 
            // Загружаем сохраненную сцену
            SceneManager.LoadScene(savedScene);
            
            Debug.Log("Загружается сцена: " + savedScene);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
