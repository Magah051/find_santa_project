using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class Bear : MonoBehaviour
{
    public Animator anim;
    public float bearVelocity;
    public GameObject honeyUI;
    public GameObject wallBear;

    void Start()
    {
        anim.SetBool("Alert", false);
    }

    private void Update()
    {
        if (anim.GetBool("canRun"))
        {
            transform.position += Vector3.right * bearVelocity * Time.deltaTime;
            wallBear.SetActive(true);
            Invoke("DisableObject", 4f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NPCVerify honeyCom = collision.gameObject.GetComponentInChildren<NPCVerify>();


        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(honeyCom.Honeycomb);
            anim.SetBool("Alert", true);
            if (honeyCom.Honeycomb)
            {
                StartCoroutine(delayRunAnimation());
            }
            else
            {
                StartCoroutine(delayAnimation());
            }
            
        }

    }

    IEnumerator delayAnimation()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("Alert", false);
    }

    IEnumerator delayRunAnimation()
    {
        yield return new WaitForSeconds(3f);
        honeyUI.SetActive(false);
        anim.SetBool("canRun", true);
        Destroy(gameObject, 20f);
    }

    void DisableObject()
    {
        wallBear.SetActive(false);
    }
}
