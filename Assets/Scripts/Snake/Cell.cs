using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; } //auto-property
    public bool IsWall { get; private set; } //wall

    public void Initialize(Vector2Int gridPosition, bool isWall) //where & what
    {
        GridPosition = gridPosition;
        IsWall = isWall;
    }
}