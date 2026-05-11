using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level_No", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private Enemy[] _enemiesPrefabs;
    [SerializeField] private float _levelTimeSec = 10f; 
    
    public IReadOnlyCollection<Enemy> EnemiesPrefabs => _enemiesPrefabs;
    public float LevelTimeSec => _levelTimeSec;
}
