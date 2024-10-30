using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<GameObject> trackedObjects;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnlyActive(GameObject element)
    {
        if (trackedObjects.Contains(element))
        {
            foreach (GameObject obj in trackedObjects) obj.SetActive(false);
            element.SetActive(true);
        }
        else throw new ArgumentException("element must be a part of trackedObjects");
    }

    public void DeactivateAll()
    {
        foreach (GameObject obj in trackedObjects) obj.SetActive(false);
    }

    public void TrackObject(GameObject obj)
    {
        trackedObjects.Add(obj);
    }

    public void UntrackObject(GameObject obj)
    {
        if (trackedObjects.Contains(obj)) { trackedObjects.Remove(obj); }
        else throw new ArgumentException("obj must be a part of trackedObjects");
    }

}
