using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TMP_Text splashText;
    public Transform loadingWheel;

    public TMP_InputField inputField;
    public TMP_Text removeButtonText;

    public bool splashFadeOut;
    public bool wheelFadeOut;

    public void FixedUpdate()
    {
        if (splashFadeOut)
        {
            splashText.color = new Color(255, 0, 0, splashText.color.a - 0.02f);
        }

        if (wheelFadeOut)
        {
            loadingWheel.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, loadingWheel.gameObject.GetComponent<Image>().color.a - 0.02f);
        }
    }

    public void PlayGame(){
        StartCoroutine(StartSequence());
    }

    public void QuitGame(){
        StartCoroutine(DelayThenQuit());
    }

    public string RandomSplashText(){
        string[] textOptions = {"The world is a desolate place of despair","There is no happiness","the world's seach for recreation and adventure is lost","This story is about a hopeless man.",
        "Hunted by demons of the forgotten and unwatned, the man faces dangers out of his control","His end will come shortly", "The only path of salvation is to protect him", "The demons will stop at nothing",
        "They will take him. They will not slow.", "There is no hope. There is no happiness", "The world is disconnected", "Forgotten and Unwanted, the man travels alone",
        "They are catching up. Hunting him. they will not stop until they've seen this through", "All is lost. The darkness creeps. There is no saving. Theres is no hope. When will it end?",
        "What's the end? Where? It will not. There is no ending to this story until he is gone. When is a question that will be answered soon.", "The forgotten world. Unwanted by its people. Dangers out of man's control hunt.",
        "there is no salvation. Protect until the end.", "The world is hurting. The people in it are hurting. Save them", "Why them? Why not.", "The world is not what it used to be", "There's no going back", "Forces of evil will hunt. Don't let them",
        "Joy is a thing of the past. Hope is gone. He will be gone shortly.", "they hunt him to feast on his darkest fears. They will not stop until he is in their grasp. Protect him.", "Save him",
        "The man travels alone. Forced to keep eyes on the back of his head. Never able to rest. He is to be protected.", "It's tiring", "I'm tired", "His end will come soon. It's only a matter of time until they catch up",
        "It's already too late. There is no hope", "Hold on a little bit longer. The day is almost over. There is nothing else you can do.", "Be his guardian angel. No one else will. He is forgotten and unwanted. Except by you.",
        "The sun does not shine on this man anymore.", "There is nothing shining on this man. He is in darkness. Protect him from it", "The darkness protrudes closer and closer. Don't let it.",
        "Don't let it get closer", "Where is the end? Where does the path diverge? What else is there in this world", "Why?", "The world is full of despair and agony. Don't let it hurt him", "There is no one left to help. There is no one left to protect. Besides him.",
        "How did this happen? Doesn't matter anymore."};//40 something different options
        return textOptions[Random.Range(0, textOptions.Length)];
    }

    public void SetSplashText(string text) {
        splashText.text = text;
    }

    public void SetRandomSplashText() {
        splashText.text = RandomSplashText();
    }

    IEnumerator DelayThenQuit()
    {
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

    IEnumerator StartSequence()
    {
        Blackout.instance.On();
        yield return new WaitForSeconds(2.0f);
        splashText.gameObject.SetActive(true);
        SetRandomSplashText();
        loadingWheel.gameObject.SetActive(true);
        loadingWheel.GetComponent<LoadingWheel>().active = true;
        yield return new WaitForSeconds(2.5f);
        splashFadeOut = true;
        wheelFadeOut = true;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
    }

    public void ClearName()
    {
        Score.RemoveEntry(inputField.text);
        StartCoroutine(ResetText());
    }

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(1.25f);
        removeButtonText.text = "Remove Entry";
    }
}
