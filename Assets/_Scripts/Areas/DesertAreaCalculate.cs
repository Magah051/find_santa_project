using UnityEngine;
using UnityEngine.UI;

public class DesertAreaCalculate : MonoBehaviour
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
        if (PlayerPrefs.HasKey("DesertData"))
        {
            LoadDesert();
        }
        else
        {
            InitializeVariables();
        }
        uiTextComponent.text = percentage.ToString("F2") + "%";
    }

    private void InitializeVariables()
    {
        totalArea = GetComponent<BoxCollider2D>().bounds.size.x * GetComponent<BoxCollider2D>().bounds.size.y;
        lastPlayerPosition = Vector2.zero;
        visitedTiles = new bool[(int)GetComponent<BoxCollider2D>().bounds.size.x, (int)GetComponent<BoxCollider2D>().bounds.size.y];
    }

    private void EnsureVisitedTilesInitialized()
    {
        if (visitedTiles == null)
        {
            InitializeVariables();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastPlayerPosition = other.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        EnsureVisitedTilesInitialized(); // Garante que visitedTiles está inicializado.

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
                    Debug.Log("Desert: " + percentage.ToString("F2") + "%");
                    SaveDesert();
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

    public class DesertData
    {
        public float percent;
        public float areaCovered;
    }

    public void SaveDesert()
    {
        DesertData data = new DesertData
        {
            percent = percentage,
            areaCovered = playerInsideArea
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("DesertData", json);
        PlayerPrefs.Save();
    }

    public void LoadDesert()
    {
        string json = PlayerPrefs.GetString("DesertData");
        if (!string.IsNullOrEmpty(json))
        {
            DesertData data = JsonUtility.FromJson<DesertData>(json);
            percentage = data.percent;
            playerInsideArea = data.areaCovered;
            // Importante: Re-inicializa 'visitedTiles' para evitar referências nulas
            InitializeVariables();
        }
    }
}
