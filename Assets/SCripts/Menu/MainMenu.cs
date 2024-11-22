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
    public TMP_Text clearButtonText;

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
        Invoke("DelayThenQuit", 1.5f);
    }

    public string RandomSplashText(){
        string[] textOptions =
        {
            "Grey people, grey buildings, grey food and grey walls.", 
            "This man needs a protector.", 
            "Where are these creatures coming from?",
            "He is dissconnected.",
            "Rejected and unwanted.",
            "His vision is fuzzy, the darkness is creeping.",
            "When does it end?",
            "Save him",
            "Is it already too late?",
            "The daytime will come soon.",
            "Be his guardian. No one else will.",
            "Its getting old.",
            "He is catching up to himself.",
            "Is there even a sun in his world?",
            "Why?",
            "How did it get like this?",
            "All he wanted was a Pepsi...",
            "Protect him from the enemy, himself.",
            "Who is in control?",
            "Its not gonna matter when hes dead and gone.",
            "At the bottom of the barrel, with his back against a wall.",
            "He is filled with thoughts of yesterday."
        };
        return textOptions[Random.Range(0, textOptions.Length)];
    }

    public void SetSplashText(string text)
    {
        splashText.text = text;
    }

    public void SetRandomSplashText() {
        splashText.text = RandomSplashText();
    }

    private void DelayThenQuit()
    {
        //yield return new WaitForSeconds(1.5f);
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

    public void RemoveName()
    {
        ScoreManager.RemoveEntry(inputField.text);
        StartCoroutine(ResetRemoveText());
    }

    public void ClearScores()
    {
        ScoreManager.ClearSavedHighScores();
    }

    IEnumerator ResetRemoveText()
    {
        yield return new WaitForSeconds(1.25f);
        removeButtonText.text = "Remove Entry";
    }

    IEnumerator ResetClearText()
    {
        yield return new WaitForSeconds(1.25f);
        clearButtonText.text = "Clear Scores";
    }
    
    /*The world is a desolate place of despair","There is no happiness","the world's search for purpose and adventure is lost","This story is about a hopeless man.",
    "Hunted by demons of the forgotten and unwanted, the man's world is out of his control","His end will come shortly", "The only path forward is to protect him", "The demons will stop at nothing",
    "They will get him. They will not slow.", "There is no hope. There is no happiness", "The world is disconnected", "Forgotten and Unwanted, the man travels alone",
    "They are catching up. Hunting him. They will not stop until they've seen this through", "All is lost. The darkness creeps. There is no saving. Theres is no hope. When will it end?",
    "What's the end? Where? There is no ending to this story until he is gone.", "The forgotten world. THe unwanted people. Hunted by his lack of control",
    "there is no salvation. Protect.", "The world is hurting. The people in it are hurting. Save them", "Why them? Why not.", "The world is not what it used to be", "There's no going back", "Forces of evil will hunt. Don't let them",
    "Joy is a thing of the past. Hope is gone. He runs.", "Save him",
    "It's already too late. There is no hope", "Hold on a little bit longer. The day is almost over. There is nothing else you can do.", "Be his guardian. No one else will.",
    "The man travels alone. Never able to rest. He will be protected.", "Its getting old.", "I'm tired", "His end will come soon. It's only a matter of time until he catches up to himself",
    "The sun does not shine on this man anymore.", "He is in darkness. Protect him from it", "The darkness grows closer and closer.",
    "Where is the end?", "What else is there in this world", "Why?", "The world is full of despair and agony. Don't let it hurt him",
    "How did this happen? Does it matter?",*/
}
