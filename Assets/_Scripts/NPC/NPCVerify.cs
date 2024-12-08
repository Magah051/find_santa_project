using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCVerify : MonoBehaviour
{
    [Header("Objects")]
    public GameObject oldWoman;
    public GameObject Elf;
    public GameObject Bear;
    public GameObject honeycomb;
    public GameObject lumberjack;
    public GameObject wall;
    public GameObject fisherman;
    public GameObject clock;

    public GameObject honeyUI;

    [Header("Itens")]
    public bool Honeycomb;

    public void Start()
    {
        Honeycomb = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gnome"))
        {
            StartCoroutine(ActivateObjectAfterDelay(oldWoman, 2f));
            clock.SetActive(true);
        }

        if (collision.CompareTag("OldWoman"))
        {
            StartCoroutine(ActivateObjectAfterDelay(Elf, 2f));
        }

        if (collision.CompareTag("Elf"))
        {
            StartCoroutine(ActivateObjectAfterDelay(Bear, 2f));
        }

        if (collision.CompareTag("Bear"))
        {
            Elf.SetActive(false);
            StartCoroutine(ActivateObjectAfterDelay(honeycomb, 2f));
        }

        if (collision.CompareTag("Honeycomb"))
        {
            Honeycomb = true;
            honeyUI.SetActive(true);
            Destroy(honeycomb, 1f);
            StartCoroutine(ActivateObjectAfterDelay(lumberjack, 2f));
        }

        if (collision.CompareTag("Lumberjack"))
        {
            wall.SetActive(true);
            StartCoroutine(ActivateObjectAfterDelay(fisherman, 2f));

        }
    }

    private IEnumerator ActivateObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

}
