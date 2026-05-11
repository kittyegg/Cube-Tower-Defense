using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public enum BlockType
    {
        None,
        Grass,
        Sand,
        PathFinish
    }

    [SerializeField] private BlockType _type = BlockType.None;
    public BlockType Type { get => _type; protected set => _type = value; }
    
    public bool IsPath => _type is BlockType.Sand or BlockType.PathFinish;

    public List<BlockScript> GetNeighbors()
    {
        var neighbors = new List<BlockScript>();
        var ray = new Ray(transform.position, Vector3.up);

        if (CheckNeighbor(ray, out var neighbor))
            neighbors.Add(neighbor);
        ray.direction = Vector3.forward;
        if (CheckNeighbor(ray, out neighbor))
            neighbors.Add(neighbor);
        ray.direction = Vector3.right;
        if (CheckNeighbor(ray, out neighbor))
            neighbors.Add(neighbor);
        ray.direction = Vector3.down;
        if (CheckNeighbor(ray, out neighbor))
            neighbors.Add(neighbor);
        ray.direction = Vector3.back;
        if (CheckNeighbor(ray, out neighbor))
            neighbors.Add(neighbor);
        ray.direction = Vector3.left;
        if (CheckNeighbor(ray, out neighbor))
            neighbors.Add(neighbor);

        return neighbors;
    }

    private static bool CheckNeighbor(Ray ray, out BlockScript neighbor)
    {
        if (Physics.Raycast(ray, out var hit, 1f))
            return hit.transform.TryGetComponent(out neighbor);

        neighbor = null;
        return false;
    }
}
