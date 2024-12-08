using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform target;
    public FadeScreen fadeScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(fadeScreen.FadeAndTeleport(collision.transform, target.position));
            Debug.Log("Colidiu");
        }
    }
}
