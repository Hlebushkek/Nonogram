using UnityEngine;

public struct CellStruct
{
    public Color color;
    public CellState state;
    public CellStruct(Color c, CellState s)
    {
        color = c;
        state = s;
    }
}
public struct CellInfo
{
    public int x;
    public int y;
    public CellState state;
    public CellInfo(int x, int y, CellState s)
    {
        this.x = x;
        this.y = y;
        state = s;
    }
}
public enum CellState
{
    Empty,
    Filled,
    Crossed,
}
