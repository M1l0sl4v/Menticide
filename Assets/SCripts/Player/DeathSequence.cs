using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSequence : MonoBehaviour
{
    public static DeathSequence instance;
    public bool controlLock;
    public GameObject blackoutPanel;
    public GameObject endScreen;
    private float endScreenDelay = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

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
        UIStack.Push(endScreen);
        
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
