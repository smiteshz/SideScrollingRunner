using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour
{
    public bool clearOn;
    BoxCollider2D col;
    private void OnTriggerEnter2D()
    {
        Debug.Log("There is an object inside me");
        clearOn = true;
    }
    private void OnTriggerExit2D()
    {
        Debug.Log("There was an object inside me");
        clearOn = false;
    }
}
