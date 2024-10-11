using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathSequence : MonoBehaviour
{
    public static DeathSequence instance;
    public bool controlLock;
    public GameObject blackoutPanel;
    public GameObject endScreen;
    private float endScreenDelay = 1.5f;

    public Image[] buttons;
    public TMP_Text[] texts;
    private float alpha;
    private bool menuActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuActive && alpha < 1)
        {
            //buttons[0].color = new Color(1, 0, 0, alpha);
            //buttons[1].color = new Color(1, 0, 0, alpha);
            texts[0].color = new Color(1, 0, 0, alpha);
            texts[1].color = new Color(1, 0, 0, alpha);
            alpha += 0.01f;
        }
    }

    public void StartDeathSequence()
    {
        StartCoroutine(_StartDeathSequence());
    }

    private IEnumerator _StartDeathSequence()
    {
        controlLock = true;
        UIStack.Push(blackoutPanel);
        yield return new WaitForSeconds(endScreenDelay);
        texts[0].color = Color.black;
        texts[1].color = Color.black;
        UIStack.Push(endScreen);
        menuActive = true;
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        throw new System.NotImplementedException();
        SceneManager.LoadScene(0);
    }


}
