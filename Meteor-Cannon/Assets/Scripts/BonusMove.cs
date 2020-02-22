using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{
    float move = 15F;
    private void Update()
    {
        transform.position = new Vector3(0f, move -= 0.02f, -4f);
    }
}
