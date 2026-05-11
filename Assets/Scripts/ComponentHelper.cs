using UnityEngine;

public static class ComponentHelper
{
    public static T GetComponentInChildrenWithoutParent<T>(this Component obj) where T : Component
    {
        foreach (var comp in obj.GetComponentsInChildren<T>())
            if (comp.gameObject != obj.gameObject)
                return comp;

        return null;
    }
}