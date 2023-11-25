using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderAgent : MonoBehaviour
{
    [SerializeField] private PathfinderGrid _grid;
    [SerializeField] private float _repathDelay = 0.5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    private Transform _target;
    private List<GridNode> _path;
    private Coroutine _repathCoroutine;

    private const float MIN_DISTANCE = 0.3f;

    public Vector2 Velocity => _rb.velocity;
    public bool IsStoppedByDistance => Vector2.Distance(transform.position, _target.position) <= _stopDistance;

    public PathfinderGrid Grid { get => _grid; set => _grid = value; }

    private void FixedUpdate()
    {
        if (_path == null) return;
        if (_path.Count == 0) return;

        if (Vector2.Distance(transform.position, _path[0].WorldPosition) > MIN_DISTANCE)
        {
            if (Vector2.Distance(transform.position, _target.position) > _stopDistance)
            {
                Vector2 dir = _path[0].WorldPosition - transform.position;
                _rb.AddForce(dir.normalized * _speed);
            }
        }
        else
        {
            _path.RemoveAt(0);
            if (_path.Count == 0)
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }

    public void SetDestination(Transform target)
    {
        _target = target;
        GetPath(_target.position);
        _repathCoroutine = StartCoroutine(Repath());
    }

    public void Stop()
    {
        StopCoroutine(_repathCoroutine);
        _target = null;
        _path = new List<GridNode>() { _grid.GetNodeFromWorldPosition(transform.position) };
    }

    private IEnumerator Repath()
    {
        while (_target)
        {
            yield return new WaitForSeconds(_repathDelay);
            GetPath(_target.position);
        }
    }

    private void GetPath(Vector2 target)
    {
        List<GridNode> open = new List<GridNode>();
        HashSet<GridNode> closed = new HashSet<GridNode>();
        GridNode start = _grid.GetNodeFromWorldPosition(transform.position);
        GridNode end = _grid.GetNodeFromWorldPosition(target);

        if (start == null)
        {
            Debug.LogError("Agent out of Grid.", this);
            return;
        }
        open.Add(start);
        while (open.Count > 0)
        {
            GridNode current = open.GetLowestCost();
            open.Remove(current);
            closed.Add(current);

            if (current == end)
            {
                GeneratePath(start, end);
                return;
            }

            foreach (GridNode neighbour in _grid.GetNeighbours(current))
            {
                if (!neighbour.IsWalkable || closed.Contains(neighbour))
                    continue;

                int neighbourDistance = current.GCost + current.Distance(neighbour);

                if (current.GCost > neighbourDistance || !open.Contains(neighbour))
                {
                    neighbour.GCost = neighbourDistance;
                    neighbour.HCost = neighbour.Distance(end);
                    neighbour.Parent = current;

                    if (!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                    }
                }
            }
        }
    }

    private void GeneratePath(GridNode start, GridNode end)
    {
        _path = new List<GridNode>();
        GridNode current = end;
        while (current != start)
        {
            _path.Add(current);
            current = current.Parent;
        }
        _path.Reverse();
    }

    private void OnDrawGizmos()
    {
        if (_path == null) return;
        if (_path.Count == 0) return;

        for (int i = 1; i < _path.Count; i++)
        {
            Gizmos.DrawLine(_path[i - 1].WorldPosition, _path[i].WorldPosition);
        }
    }
}
