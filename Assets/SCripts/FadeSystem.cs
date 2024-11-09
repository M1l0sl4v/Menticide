using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartFadeOut(float speed, params Color[] objects)
    {
        throw new System.NotImplementedException();
    }
    private void FadeOut(float speed, Color[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            var obj = objects[i];
            objects[i] = new Color(obj.r, obj.g, obj.b, obj.a - speed);
        }
    }
}
