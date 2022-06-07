using UnityEngine;

public enum TYPE { car, fastCar, busCar, emergencyCar, taxi, repairCar, truck, train }
public static class CustomFunctions
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToViewportPoint(position);
    }
}
