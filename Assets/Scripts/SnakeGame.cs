using UnityEngine;

public class SnakeGame : MonoBehaviour
{
    [SerializeField] private int gridSize = 10;
    [SerializeField] private float cellSize = 1f;

    private GameObject[,] gridCells;

    private void Start()
    {
        CreateCamera();
        GenerateGrid();
    }

    private void CreateCamera()
    {
        if (Camera.main != null) return;

        GameObject camGO = new GameObject("Main Camera");
        camGO.tag = "MainCamera";
        Camera cam = camGO.AddComponent<Camera>();

        cam.orthographic = true;

        float halfSize = (gridSize * cellSize) / 2f;

        cam.transform.position = new Vector3(0f, 10f, 0f);
        cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        cam.orthographicSize = halfSize;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
    }

    private void GenerateGrid()
    {
        gridCells = new GameObject[gridSize, gridSize];
        float halfSize = (gridSize - 1) * cellSize / 2f;
        float innerScale = 0.9f;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                bool isWall = (x == 0 || z == 0 || x == gridSize - 1 || z == gridSize - 1);

                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cell.name = isWall ? $"Wall_{x}{z}" : $"Cell{x}_{z}";

                float worldX = x * cellSize - halfSize;
                float worldZ = z * cellSize - halfSize;

                float height = isWall ? 0.2f : 0.05f;
                float y = height / 2f;

                cell.transform.position = new Vector3(worldX, y, worldZ);
                cell.transform.localScale = new Vector3(cellSize * innerScale, height, cellSize * innerScale);

                Renderer r = cell.GetComponent<Renderer>();
                if (isWall)
                    r.material.color = Color.grey;
                else
                    r.material.color = new Color(0f, 0.4f, 0f);

                gridCells[x, z] = cell;
            }
        }
    }
}