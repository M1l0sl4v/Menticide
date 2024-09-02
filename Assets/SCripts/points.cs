using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class points : MonoBehaviour
{
    //points
    public TMP_Text pointAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointAmount.text = ((int)transform.position.y).ToString();
        if (transform.position.y >= 600)
        {
            seasons.instance.spring();
        }
        else if (transform.position.y >= 400)
        {
            seasons.instance.winter();
        }
        else if (transform.position.y >= 200)
        {
            seasons.instance.fall();
        }
    }
}
