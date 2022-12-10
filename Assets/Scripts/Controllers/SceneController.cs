using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static string lastScene = "MainMenu";
    public static string buttonClicked { get; set; } = "NewGame";
    /*void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name.ToLower())
        {
            case "menu":
                // Do some "menu" initialisation here...
                break;

            case "game":
                // Do some "game" initialisation here...
                break;

            case "gameover":
                // Do some "gameover" initialisation here...
                break;
        }
    }

    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;*/
}