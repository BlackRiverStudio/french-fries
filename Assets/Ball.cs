using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.name.StartsWith("Enemie")) Destroy(_collision.gameObject);
    }
}
