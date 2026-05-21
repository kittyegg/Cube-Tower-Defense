using UnityEngine;

[CreateAssetMenu(fileName = "TurretLvl_", menuName = "Scriptable Objects/Turret Upgrade")]
public class TurretUpgrade : ScriptableObject
{
    // TODO: инкапсулировать поля
    public float UpgradeDamageMultiplier = 1.5f;
    public float UpgradeDistanceMultiplier = 1.5f;
    public float UpgradeReloadTimeMultiplier = 0.75f;
}
