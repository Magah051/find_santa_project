using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boards : MonoBehaviour
{
    public GameObject board1;

    public AudioClip boardSound;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameObject boardInstance1 = Instantiate(board1, transform.position, Quaternion.identity);
            Destroy(boardInstance1, 7f);
            Destroy(gameObject, 7f);
            GameObject soundObject = new GameObject("CollisionSound");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = boardSound;
            audioSource.Play();
            Destroy(soundObject, boardSound.length);

        }

    }
}