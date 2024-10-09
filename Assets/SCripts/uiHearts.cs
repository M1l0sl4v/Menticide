using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiHearts : MonoBehaviour
{
    public GameObject heartUii;
    public List<Image> hearts;

    // Start is called before the first frame update
    public void StartHealth(int health)
    {
        for (int i = 0; i < health; i++)
        {
            GameObject h = Instantiate(heartUii, transform);
            hearts.Add(h.GetComponent<Image>());
        }
    }
    public void updateHealth(int health)
    {
        int heartFill = health;
        foreach (Image i in hearts)
        {
            i.fillAmount = heartFill;
            heartFill -= 1;
        }
    }
}