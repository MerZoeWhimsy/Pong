using UnityEngine;

[System.Serializable]
public struct Coord
{
    public int x;
    public int z;

    public Coord(int x, int z)
    {
        this.x = x; 
        this.z = z;
    }

    public Vector3 ToWorldPosition(float cellSize)
    {
        return new Vector3(x * cellSize, 0f, z * cellSize); //convert world pos
    }
}