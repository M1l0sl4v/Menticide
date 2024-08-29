using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class points : MonoBehaviour
{
    //points
    public TMP_Text pointAmount;
    public seasons seasonScript;
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
            seasonScript.spring();
        }
        else if (transform.position.y >= 400)
        {
            seasonScript.winter();
        }
        else if (transform.position.y >= 200)
        {
            seasonScript.fall();
        }
    }
}
