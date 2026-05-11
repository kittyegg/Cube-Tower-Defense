using System;
using UnityEngine;

[Serializable]
public struct TurretShopItem
{
    [SerializeField] private Turret _turretPrefab;
    [SerializeField] private Sprite _icon;

    public readonly Turret TurretPrefab => _turretPrefab;
    public readonly Sprite Icon => _icon;
    public readonly int Price => _turretPrefab.Price;
}