
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
        switch (newScene.name)
        {
            case "Main Menu":
                mainMenuScene();
                break;
            case "main":
                mainScene();
                break;
            case "Cutscene":
                cutScene();
                break;
            default:
                Debug.LogWarning($"Scene '{newScene.name}' does not have a specific handler.");
                break;
        }
    }
    
    public void mainMenuScene()
    {
        MusicManager.instance.PlayMusic("Menu");
    }
    public void cutScene()
    {

    }
    public void mainScene()
    {
        MusicManager.instance.PlayMusic("Gameplay");

    }
}

