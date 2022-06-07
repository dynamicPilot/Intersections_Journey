using UnityEngine;

public class SceneViewControl : MonoBehaviour
{
    //[SerializeField] private CrossroadsView crossroadsView;
    [SerializeField] private GameUIView gameUIView;

    public void SetColors(Location location)
    {
        //if (location.CrossroadsColors.Length > 2)
        //    crossroadsView.SetColors(location.CrossroadsColors[0], location.CrossroadsColors[1], location.CrossroadsColors[2]);

        if (location.UIColors.Length > 2)
            gameUIView.SetColors(location.UIColors[0], location.UIColors[1], location.UIColors[2]);
    }
}
