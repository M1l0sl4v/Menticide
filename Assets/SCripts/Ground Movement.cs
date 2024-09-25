using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
   public Material GroundMaterial;
   public Vector2 playerPos;
   

    // Update is called once per frame
    void Update()
    {
        playermovement.instance.transform.position = playerPos;
        GroundMaterial.SetTextureOffset("_MainTex", playerPos);
    }
}
