using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wise : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject UI_Entity = Instantiate(UI, transform.position, Quaternion.identity);
            Destroy(UI_Entity, 7f);
        }
    }
}
