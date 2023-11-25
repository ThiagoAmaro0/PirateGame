using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderGrid : MonoBehaviour
{
    private const float DEBUG_CELL_SIZE = 0.1f;
    [SerializeField] private Vector2 _worldSize = new Vector2(10, 10);
    [SerializeField] private float _nodeRadius = 1f;
    [SerializeField] private LayerMask _obstacleLayerMask;
    [SerializeField] private Transform _player;
    [SerializeField] private bool _debug;
    private GridNode[,] _grid;
    private int _gridSizeX;
    private int _gridSizeY;
    private Vector2 _leftBottomPoint;
    private float _nodeDiameter;

    public List<GridNode> DebugPath { get; internal set; }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_worldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_worldSize.y / _nodeDiameter);

        _leftBottomPoint = (Vector2)transform.position - _worldSize / 2;

        _grid = new GridNode[_gridSizeX, _gridSizeY];

        for (int y = 0; y < _gridSizeY; y++)
        {
            for (int x = 0; x < _gridSizeX; x++)
            {
                Vector3 position = _leftBottomPoint + new Vector2(x * _nodeDiameter + _nodeRadius, y * _nodeDiameter + _nodeRadius);
                bool isWalkable = !Physics2D.CircleCast(position, _nodeRadius, Vector3.forward, _nodeRadius, _obstacleLayerMask);
                _grid[x, y] = new GridNode(isWalkable, position, new Vector2Int(x, y));
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!_debug)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, _worldSize);

        if (_grid == null)
            return;

        GridNode playerNode = GetNodeFromWorldPosition(_player.position);
        for (int y = 0; y < _gridSizeY; y++)
        {
            for (int x = 0; x < _gridSizeX; x++)
            {
                Gizmos.color = _grid[x, y].IsWalkable ? Color.white : Color.red;
                if (_grid[x, y] == playerNode)
                {
                    Gizmos.color = Color.cyan;
                }

                if (DebugPath != null)
                {
                    if (DebugPath.Contains(_grid[x, y]))
                    {
                        Gizmos.color = Color.green;
                    }
                }
                Gizmos.DrawWireCube(_grid[x, y].WorldPosition, Vector2.one * (_nodeDiameter - DEBUG_CELL_SIZE));

            }
        }
    }

    public GridNode GetNodeFromWorldPosition(Vector2 position)
    {
        if (_grid == null)
            return null;
        float xPercent = Mathf.Clamp01((position.x + _worldSize.x * 0.5f) / _worldSize.x);
        float yPercent = Mathf.Clamp01((position.y + _worldSize.y * 0.5f) / _worldSize.y);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * xPercent);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * yPercent);
        return _grid[x, y];
    }

    public List<GridNode> GetNeighbours(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector2Int next = node.GridPosition + new Vector2Int(x, y);

                if (next.IsValid(_gridSizeX, _gridSizeY))
                {
                    neighbours.Add(_grid[next.x, next.y]);
                }
            }
        }

        return neighbours;
    }
}