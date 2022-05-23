using UnityEngine;

public static class CustomFunctions
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToViewportPoint(position);
    }
}
