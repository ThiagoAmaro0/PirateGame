using UnityEngine;

public class GridNode
{
    private bool _isWalkable;
    private Vector3 _worldPosition;
    private Vector2Int _gridPosition;
    private GridNode _parent;

    private int _hCost;
    private int _gCost;

    public int FCost => HCost + GCost;
    public int HCost { get => _hCost; set => _hCost = value; }
    public int GCost { get => _gCost; set => _gCost = value; }
    public Vector3 WorldPosition { get => _worldPosition; set => _worldPosition = value; }
    public bool IsWalkable { get => _isWalkable; set => _isWalkable = value; }
    public Vector2Int GridPosition { get => _gridPosition; private set => _gridPosition = value; }
    public GridNode Parent { get => _parent; set => _parent = value; }

    public GridNode(bool isWalkable, Vector3 worldPosition, Vector2Int gridPosition)
    {
        IsWalkable = isWalkable;
        WorldPosition = worldPosition;
        GridPosition = gridPosition;
    }

}
