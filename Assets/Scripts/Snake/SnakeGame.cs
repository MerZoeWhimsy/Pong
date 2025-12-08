using System.Collections.Generic;
using UnityEngine;

public class SnakeGame : MonoBehaviour
{
    [SerializeField] private int gridSize = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private int foodSpawnTicks = 6;
    [SerializeField] private GameObject snakeSegmentPrefab;
    [SerializeField] private int initialSnakeLength = 3;
    [SerializeField] private ObjectPool foodPool;

    private Cell[,] gridCells;
    private GameObject currentFood;
    private int ticksSinceLastFood;

    private List<Vector2Int> snakeCoords;
    private List<GameObject> snakeSegments;
    private Vector2Int currentDirection;
    private Vector2Int nextDirection;

    private void OnEnable()
    {
        TickSystem.OnTick += HandleTick; //listener
    }

    private void OnDisable()
    {
        TickSystem.OnTick -= HandleTick;
    }

    private void Start()
    {
        GenerateGrid();
        InitSnake();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleTick() //once every tick
    {
        if (gridCells == null) return;

        if (currentDirection != Vector2Int.zero)//movement
        {
            UpdateSnake();
        }

        ticksSinceLastFood++;
        if (ticksSinceLastFood >= foodSpawnTicks) //check if enough time has passed 
        {
            ticksSinceLastFood = 0;
            SpawnFood();
        }
    }

    private void HandleInput()
    {
        Vector2Int desiredDirection = Vector2Int.zero; //no dir

        if (Input.GetKeyDown(KeyCode.UpArrow))
            desiredDirection = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            desiredDirection = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            desiredDirection = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            desiredDirection = Vector2Int.right;

        if (desiredDirection == Vector2Int.zero)
            return;

        if (currentDirection == Vector2Int.zero)
        {
            currentDirection = desiredDirection;
            nextDirection = desiredDirection;
            return;
        }

        if (desiredDirection != -currentDirection)
        {
            nextDirection = desiredDirection;
        }
    }

    private void InitSnake()
    {
        snakeCoords = new List<Vector2Int>();
        snakeSegments = new List<GameObject>();

        int startX = gridSize / 2;
        int startZ = gridSize / 2;

        currentDirection = Vector2Int.zero;
        nextDirection = Vector2Int.zero; //wait for inpiy

        for (int i = 0; i < initialSnakeLength; i++)
        {
            int x = startX - i;
            int z = startZ;
            Vector2Int coord = new Vector2Int(x, z);
            snakeCoords.Add(coord);

            Cell cell = gridCells[x, z];
            Vector3 pos = cell.transform.position;
            pos.y += 0.4f;

            GameObject segment = Instantiate(snakeSegmentPrefab, pos, Quaternion.identity, transform);
            snakeSegments.Add(segment);
        }
    }

    private void UpdateSnake()
    {
        if (nextDirection != Vector2Int.zero && nextDirection != -currentDirection)
        {
            currentDirection = nextDirection;
        }

        Vector2Int head = snakeCoords[0];
        Vector2Int newHead = head + currentDirection; //add dir to head

        if (newHead.x <= 0 || newHead.x >= gridSize - 1 ||
            newHead.y <= 0 || newHead.y >= gridSize - 1)
        {
            return;
        }

        for (int i = 0; i < snakeCoords.Count; i++)
        {
            if (snakeCoords[i] == newHead)
                return;
        }

        snakeCoords.Insert(0, newHead);
        snakeCoords.RemoveAt(snakeCoords.Count - 1); //head n tail mov 

        for (int i = 0; i < snakeCoords.Count; i++)
        {
            Vector2Int coord = snakeCoords[i];
            Cell cell = gridCells[coord.x, coord.y];
            Vector3 pos = cell.transform.position;
            pos.y += 0.4f;
            snakeSegments[i].transform.position = pos;
        }
    }

    private void GenerateGrid()
    {
        gridCells = new Cell[gridSize, gridSize];

        float halfSize = (gridSize - 1) * cellSize / 2f;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                bool isWall = x == 0 || z == 0 || x == gridSize - 1 || z == gridSize - 1;

                Coord coord = new Coord(x, z);
                Vector3 worldPos = coord.ToWorldPosition(cellSize);
                worldPos.x -= halfSize;
                worldPos.z -= halfSize;

                Cell cell = Instantiate(cellPrefab, worldPos, Quaternion.identity, transform);

                float height = isWall ? 0.2f : 0.05f;
                float innerScale = 0.9f;
                float y = height / 2f;

                cell.transform.position = new Vector3(worldPos.x, y, worldPos.z);
                cell.transform.localScale = new Vector3(cellSize * innerScale, height, cellSize * innerScale);

                Renderer r = cell.GetComponent<Renderer>();
                if (r != null)
                {
                    r.material.color = isWall ? Color.grey : new Color(0f, 0.4f, 0f);
                }

                cell.Initialize(new Vector2Int(x, z), isWall);
                gridCells[x, z] = cell;
            }
        }
    }

    private void SpawnFood()
    {
        if (foodPool == null) return;
        if (gridCells == null) return;

        if (currentFood != null)
            foodPool.ReturnToPool(currentFood);

        int x = Random.Range(1, gridSize - 1);
        int z = Random.Range(1, gridSize - 1);

        Cell cell = gridCells[x, z];
        if (cell == null) return;

        Vector3 pos = cell.transform.position;
        pos.y += 0.3f;

        currentFood = foodPool.GetFromPool(pos, Quaternion.identity);
    }
}