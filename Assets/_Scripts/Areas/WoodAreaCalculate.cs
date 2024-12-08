using UnityEngine;
using UnityEngine.UI;

public class WoodAreaCalculate : MonoBehaviour
{
    private float totalArea;
    private Vector2 lastPlayerPosition;
    private float playerInsideArea;
    private bool[,] visitedTiles;
    public float percentage;
    public Text uiTextComponent;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("WoodData"))
        {
            LoadWood();
            uiTextComponent.text = percentage.ToString("F2") + "%";
        }
        else
        {
            totalArea = GetComponent<BoxCollider2D>().bounds.size.x * GetComponent<BoxCollider2D>().bounds.size.y;
            lastPlayerPosition = Vector2.zero;
            visitedTiles = new bool[(int)GetComponent<BoxCollider2D>().bounds.size.x, (int)GetComponent<BoxCollider2D>().bounds.size.y];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastPlayerPosition = other.transform.position;
        }
    }

    private void EnsureVisitedTilesInitialized()
    {
        if (visitedTiles == null)
        {
            totalArea = GetComponent<BoxCollider2D>().bounds.size.x * GetComponent<BoxCollider2D>().bounds.size.y;
            visitedTiles = new bool[(int)GetComponent<BoxCollider2D>().bounds.size.x, (int)GetComponent<BoxCollider2D>().bounds.size.y];
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        EnsureVisitedTilesInitialized();


        if (other.CompareTag("Player"))
        {
            if ((Vector2)other.transform.position != lastPlayerPosition)
            {
                float playerArea = other.bounds.size.x * other.bounds.size.y;
                int xIndex = Mathf.Clamp(Mathf.RoundToInt(other.transform.position.x - transform.position.x), 0, visitedTiles.GetLength(0) - 1);
                int yIndex = Mathf.Clamp(Mathf.RoundToInt(other.transform.position.y - transform.position.y), 0, visitedTiles.GetLength(1) - 1);

                if (!visitedTiles[xIndex, yIndex])
                {
                    playerInsideArea += playerArea;
                    visitedTiles[xIndex, yIndex] = true;
                    percentage = (playerInsideArea / totalArea) * 6000f;
                    uiTextComponent.text = percentage.ToString("F2") + "%";
                    Debug.Log("Wood: " + percentage.ToString("F2") + "%");
                    SaveWood();
                }
                lastPlayerPosition = other.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastPlayerPosition = other.transform.position;
        }
    }

    public class WoodData
    {
        public float percent;
        public float areaCovered;
    }

    public void SaveWood()
    {
        WoodData data = new WoodData
        {
            percent = percentage,
            areaCovered = playerInsideArea
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("WoodData", json);
        PlayerPrefs.Save();
    }

    public void LoadWood()
    {
        string json = PlayerPrefs.GetString("WoodData");
        if (!string.IsNullOrEmpty(json))
        {
            WoodData data = JsonUtility.FromJson<WoodData>(json);
            percentage = data.percent;
            playerInsideArea = data.areaCovered;
            uiTextComponent.text = percentage.ToString("F2") + "%";
        }
    }
}
