using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiHearts : MonoBehaviour
{
    public GameObject heartUii;
    public List<Image> hearts;
    private Image editorImage;
    // Start is called before the first frame update
    public void StartHealth(int health)
    {
        editorImage = GetComponent<Image>();
        editorImage.enabled = false;
        for (int i = 0; i < health; i++)
        {
            GameObject h = Instantiate(heartUii, transform);
            hearts.Add(h.GetComponent<Image>());
        }
    }
    // called when player is damaged
    public void updateHealth(int health)
    {
        int heartFill = health;
        foreach (Image i in hearts)
        {
            /*i.fillAmount = heartFill;
            heartFill -= 1;*/
            
            
            if (heartFill > 0)
            {
                i.fillAmount = 1; 
            }
            else if (i.fillAmount != 0) 
            {
                //i.GetComponentInParent<Animator>().SetTrigger("HeartLost");
                i.fillAmount = 0;
            }
            heartFill -= 1;
            
        }
    }

}