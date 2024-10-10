using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSequence : MonoBehaviour
{
    public static DeathSequence instance;
    public bool controlLock;
    public GameObject blackoutPanel;
    public GameObject endScreen;
    private float endScreenDelay = 5f;

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
        blackoutPanel.SetActive(true);
        yield return new WaitForSeconds(endScreenDelay);
        
    }


}
