using UnityEngine;
using UnityEngine.UI;

public class ForestAreaCalculate : MonoBehaviour
{
    private float totalArea;
    private Vector2 lastPlayerPosition;
    private float playerInsideArea;
    private bool[,] visitedTiles;
    public float percentage; // Variável de instância usada para manter a porcentagem
    public Text uiTextComponent;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("ForestData"))
        {
            LoadForest();
            uiTextComponent.text = percentage.ToString("F2") + "%";
        }

        totalArea = GetComponent<BoxCollider2D>().bounds.size.x * GetComponent<BoxCollider2D>().bounds.size.y;

        visitedTiles = new bool[(int)GetComponent<BoxCollider2D>().bounds.size.x, (int)GetComponent<BoxCollider2D>().bounds.size.y];
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
                    Debug.Log("Forest: " + percentage.ToString("F2") + "%");
                    lastPlayerPosition = other.transform.position;
                    SaveForest(percentage, lastPlayerPosition, playerInsideArea);
                }
            }
        }
    }

    public class ForestData
    {
        public float percent;
        public Vector2 position;
        public float areaInside; // Adicionado para salvar a área percorrida
    }

    public void SaveForest(float perc, Vector2 pos, float areaInside)
    {
        ForestData data = new ForestData
        {
            percent = perc,
            position = pos,
            areaInside = areaInside // Salvando a área percorrida
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("ForestData", json);
        PlayerPrefs.Save();
        Debug.Log("Saving ForestCalculate data...");
        Debug.Log("Percentage saved: " + perc);
    }

    public void LoadForest()
    {
        string json = PlayerPrefs.GetString("ForestData");
        if (!string.IsNullOrEmpty(json))
        {
            ForestData data = JsonUtility.FromJson<ForestData>(json);
            percentage = data.percent;
            playerInsideArea = data.areaInside; // Carregando a área percorrida
            // lastPlayerPosition = data.position; // Descomente se precisar
            Debug.Log("Applying ForestCalculate data...");
            Debug.Log("Percentage loaded: " + percentage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastPlayerPosition = other.transform.position;
        }
    }
}
