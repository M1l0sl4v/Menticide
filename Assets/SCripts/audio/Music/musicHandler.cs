
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "Main Menu") mainMenuScene();
        else if (newScene.name == "main") mainScene();
    }

    public void mainMenuScene()
    {
        MusicManager.instance.PlayMusic("Menu");
    }

    public void mainScene()
    {
        MusicManager.instance.PlayMusic("Gameplay");
    }
}

