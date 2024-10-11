using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStack : MonoBehaviour
{

    private static Stack<GameObject> stack = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Push(GameObject uiElement)
    {
        stack.Push(uiElement);
        uiElement.SetActive(true);
    }

    public static GameObject Pop()
    {
        GameObject uiElement = stack.Pop();
        uiElement.SetActive(false);
        return uiElement;
    }

    public static void PopAll()
    {
        foreach (GameObject uiElement in stack)
        {
            uiElement.SetActive(false);
        }
        stack.Clear();
    }
}
