using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WiseText : MonoBehaviour
{
    public float typeDelay = 0.05f;
    public TextMeshProUGUI textObject;
    private List<string> motivationalTexts = new List<string>()
    {
        "Patience is the companion of wisdom. Embrace it.",
        "A life lived in fear is a life half-lived. Seek courage.",
        "Wisdom is the reward you get for a lifetime of listening.",
        "The art of being wise is knowing what to overlook.",
        "We are what we repeatedly do. Excellence, then, is a habit.",
        "Change is the end result of all true learning. Adapt and grow.",
        "The only true wisdom is in knowing you know nothing.",
        "Do not be wise in words - be wise in deeds. Act justly.",
        "In every walk with nature, one receives far more than he seeks.",
        "The heart of the wise makes his speech judicious.",
        "Not all those who wander are lost. Explore to understand.",
        "Kindness is the language which the deaf can hear and the blind can see.",
        "The wise man does at once what the fool does finally.",
        "Silence is often the loudest voice. Listen deeply.",
        "Life is the sum of all your choices. Choose wisely.",
        "Wisdom is not a product of schooling but of the lifelong attempt to acquire it.",
        "It is better to remain silent at the risk of being thought a fool, than to talk and remove all doubt of it.",
        "The only true wisdom is in knowing you know nothing.",
        "Education is the kindling of a flame, not the filling of a vessel.",
        "The fool wonders, the wise man asks. Be curious, seek answers.",
        "Beware of the man who does not talk, and the dog that does not bark.",
        "He who learns but does not think, is lost. Think and reflect.",
        "Knowledge speaks, but wisdom listens. Hear more than you speak.",
        "A jug fills drop by drop. Every effort counts.",
        "The roots of education are bitter, but the fruit is sweet.",
        "To know and not do is really not to know. Action matters.",
        "The best time to plant a tree was 20 years ago. The next best time is now.",
        "An investment in knowledge pays the best interest. Keep learning.",
        "Do not seek to follow in the footsteps of the wise. Seek what they sought.",
        "Wisdom is the daughter of experience. Live and learn.",
        "Knowing yourself is the beginning of all wisdom.",
        "The only limit to your impact is your imagination and commitment.",
        "Try to be a rainbow in someone's cloud. Be a light.",
        "What we achieve inwardly will change outer reality.",
        "The true sign of intelligence is not knowledge but imagination.",
        "Wisdom begins in wonder. Never lose your curiosity.",
        "It is not the strongest of the species that survive, nor the most intelligent, but the one most responsive to change.",
        "A wise man can learn more from a foolish question than a fool can learn from a wise answer.",
        "To be old and wise, you must first be young and stupid. Experience teaches.",
        "Happiness is not something ready-made. It comes from your own actions.",
        "Speak less than you know; have more than you show. Discretion is a virtue.",
        "The only way to do great work is to love what you do. Find your passion.",
        "The wise man knows himself to be a fool. Embrace learning.",
        "The journey of a thousand miles begins with one step. Begin now.",
        "In teaching others, we teach ourselves. Share your knowledge.",
        "The only thing more expensive than education is ignorance. Value learning.",
        "To avoid criticism, do nothing, say nothing, and be nothing. Dare to live.",
        "Not everything that is faced can be changed, but nothing can be changed until it is faced.",
        "Life is really simple, but we insist on making it complicated. Seek simplicity.",
        "Do not go where the path may lead, go instead where there is no path and leave a trail."
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
