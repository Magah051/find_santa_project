using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FairyText : MonoBehaviour
{
    public float typeDelay = 0.05f;
    public TextMeshProUGUI textObject;
    private List<string> motivationalTexts = new List<string>()
    {
        "Persistence is the key to success.",
        "Success comes from the will, determination, and persistence to achieve a goal.",
        "Even if you don't hit the target, those who search and overcome obstacles will at least do admirable things.",
        "Set your goals high, and don't stop till you get there.",
        "Be the change you wish to see in the world.",
        "What you can do, or dream you can, begin it. Boldness has genius, power, and magic in it.",
        "The best way to predict the future is to create it.",
        "You miss 100% of the shots you don't take.",
        "Your only limit is you.",
        "Do what you can, with what you have, where you are.",
        "Dream big and dare to fail.",
        "Life is 10% what happens to us and 90% how we react to it.",
        "Your desire for success should be greater than your fear of failure.",
        "Believe you can and you're halfway there.",
        "Action is the foundational key to all success.",
        "Whatever the mind of man can conceive and believe, it can achieve.",
        "Don't wait for extraordinary opportunities. Seize common occasions and make them great.",
        "The pessimist sees difficulty in every opportunity. The optimist sees the opportunity in every difficulty.",
        "It's not about having everyone by your side, but having the right people.",
        "Motivation is what gets you started. Habit is what keeps you going.",
        "Success is going from failure to failure without losing your enthusiasm.",
        "You can only achieve great things if you stay true to yourself.",
        "Turn your life into a masterpiece.",
        "Knowing is not enough; we must apply. Wishing is not enough; we must do.",
        "Courage is not the absence of fear, but the triumph over it.",
        "The only place where success comes before work is in the dictionary.",
        "Life is about making an impact, not making an income.",
        "Progress always involves risks. You can't steal second base and keep your foot on first.",
        "With great power comes great responsibility.",
        "You don't have to be great to start, but you have to start to be great."
    };

    private string fullText;

    void Start()
    {
        fullText = motivationalTexts[UnityEngine.Random.Range(0, motivationalTexts.Count)];
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textObject.text = fullText;
        textObject.maxVisibleCharacters = 0;
        for (int i = 0; i <= textObject.text.Length; i++)
        {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeDelay);
        }
    }
}
