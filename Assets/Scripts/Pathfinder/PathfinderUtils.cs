using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PathFinderUtils
{
    private const int PERPENDICULAR_COST = 10;
    private const int DIAGONAL_COST = 14;
    public static GridNode GetLowestCost(this List<GridNode> list)
    {
        if (list == null)
        {
            Debug.LogError("Node List is null.");
            return null;
        }
        if (list.Count == 0)
        {
            Debug.LogError("Node List is empty.");
            return null;
        }
        GridNode node = list[0];
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].FCost < node.FCost || list[i].FCost == node.FCost && list[i].HCost < node.HCost)
            {
                node = list[i];
            }
        }
        return node;
    }

    public static int Distance(this GridNode nodeA, GridNode nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.GridPosition.x - nodeB.GridPosition.x);
        int distanceY = Mathf.Abs(nodeA.GridPosition.y - nodeB.GridPosition.y);

        if (distanceX > distanceY)
        {
            return distanceY * DIAGONAL_COST + PERPENDICULAR_COST * (distanceX - distanceY);
        }
        return distanceX * DIAGONAL_COST + PERPENDICULAR_COST * (distanceY - distanceX);
    }

    public static bool IsValid(this Vector2Int pos, int sizeX, int sizeY)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < sizeX && pos.y < sizeY;
    }
}