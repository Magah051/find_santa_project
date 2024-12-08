using UnityEngine;
using UnityEngine.UI;

public class ArticAreaCalculate : MonoBehaviour
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
        if (PlayerPrefs.HasKey("ArticData"))
        {
            LoadArtic();
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
                    //Debug.Log("Artic: " + percentage.ToString("F2") + "%");
                    SaveArtic();
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

    public class ArticData
    {
        public float percent;
        public float areaCovered;
    }

    public void SaveArtic()
    {
        ArticData data = new ArticData
        {
            percent = percentage,
            areaCovered = playerInsideArea
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("ArticData", json);
        PlayerPrefs.Save();
    }

    public void LoadArtic()
    {
        string json = PlayerPrefs.GetString("ArticData");
        if (!string.IsNullOrEmpty(json))
        {
            ArticData data = JsonUtility.FromJson<ArticData>(json);
            percentage = data.percent;
            playerInsideArea = data.areaCovered;
            // Importante: Re-inicializa 'visitedTiles' para evitar referências nulas
            InitializeVariables();
        }
    }
}
