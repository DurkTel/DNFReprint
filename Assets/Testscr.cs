using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscr : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name+"进入");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.gameObject.name+"离开");

    }
}
