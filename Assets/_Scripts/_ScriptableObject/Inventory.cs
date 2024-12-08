using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item ShowenItem;
    public List<Item> Items = new List<Item>();
    public int NumberOfKeys = 0;
    public int NumberOfCoins = 0;

    private const string CoinsKey = "NumberOfCoins";

    public void Awake()
    {
        if (PlayerPrefs.HasKey("NumberOfCoins"))
        {
            Load();
        }
    }

    public void AddItem(Item ItemToAdd)
    {
        // is the Item a key ?
        if (ItemToAdd.IsKey)
        {
            NumberOfKeys++;
        }
        else if (ItemToAdd.IsCoin)
        {
            NumberOfCoins++;
            Save(); // Call Save after incrementing NumberOfCoins
        }
        else
        {
            if (!Items.Contains(ItemToAdd))
            {
                Items.Add(ItemToAdd);
            }
        }
    }

    // Saves the current number of coins to PlayerPrefs
    public void Save()
    {
        PlayerPrefs.SetInt(CoinsKey, NumberOfCoins);
        PlayerPrefs.Save(); // Make sure to save PlayerPrefs after setting the value
    }

    // Loads the number of coins from PlayerPrefs
    public void Load()
    {
        NumberOfCoins = PlayerPrefs.GetInt(CoinsKey, 0); // The second parameter is the default value if the key doesn't exist
    }
}
