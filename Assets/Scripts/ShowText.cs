using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.SetActive(true);
            StartCoroutine(HideText());
        }

    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(10f);
        text.SetActive(false);
    }
}
