using IJ.UIElements;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    [Header("Tutorial Component")]
    [SerializeField] private TutorialElement _tutorialHand;

    public void StartTutorial()
    {
        if (_tutorialHand == null) return;

        Logging.Log("Start tutorial hand");
        _tutorialHand.gameObject.SetActive(true);
        _tutorialHand.StartElement();
    }
}
