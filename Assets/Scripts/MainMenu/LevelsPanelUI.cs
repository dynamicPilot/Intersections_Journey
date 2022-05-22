using System.Collections.Generic;
using UnityEngine;

public class LevelsPanelUI : MonoBehaviour
{
    public enum CROSS { tCross, cross, doubleCross, cross3222}
    [SerializeField] private GameObject[] backgroundsForLocations;
    [SerializeField] private List<LevelUI> levelUIs;

    [Header("Crossroads Schemas")]
    [SerializeField] private Sprite tCross;
    [SerializeField] private Sprite cross;
    [SerializeField] private Sprite doubleCross;
    [SerializeField] private Sprite cross3222;

    [Header("Schemas Colors")]
    [SerializeField] private Color[] colors;

    [Header("Scripts")]
    [SerializeField] private MenuMaster menuMaster;

    private GameObject activeBackground = null;
    private int locationIndex = -1;
    private IDictionary<CROSS, Sprite> schemas = new Dictionary<CROSS, Sprite>();

    private void Awake()
    {
        CreateDictionary();
        locationIndex = -1;
    }

    void CreateDictionary()
    {
        schemas[CROSS.tCross] = tCross;
        schemas[CROSS.cross] = cross;
        schemas[CROSS.doubleCross] = doubleCross;
        schemas[CROSS.cross3222] = cross3222;
    }

    Color GetColor(int index)
    {
        index %= colors.Length;
        return colors[index];
    }

    public void SetLocation(Location location, PlayerState playerState)
    {
        if (locationIndex == location.LocationIndex)
        {
            Logging.Log("LevelsPanelUI: the same location");
            return;
        }

        ChangeBackground(location.LocationIndex);
        locationIndex = location.LocationIndex;
        
        // set levels 

        Logging.Log("LevelsPanelUI: set levels for location ... " + location.LocationName);
        int index = 0;

        for (int i = 0; i < location.Levels.Count; i++)
        {
            if (i < levelUIs.Count)
            {
                levelUIs[i].SetLevelUI(location.Levels[i], this, playerState.LevelsProgress[location.Levels[i].LevelIndex], schemas[location.Levels[i].CrossType], GetColor(i));
                levelUIs[i].gameObject.SetActive(true);
                index++;
            }
            else
            {
                Logging.Log("LevelsPanelUI: need more LEVEL!");
                break;
            }
        }

        if (index < levelUIs.Count)
        {
            for (int i = index; i < levelUIs.Count; i++)
            {
                levelUIs[i].gameObject.SetActive(false);
            }
        }
    }

    void ChangeBackground(int index)
    {
        if (activeBackground != null)
        {
            activeBackground.SetActive(false);
            if (activeBackground.GetComponent<ActivateBackgroundLocation>() != null)
            {
                activeBackground.GetComponent<ActivateBackgroundLocation>().SetBackgroundAsDisactive();
            }
        }

        if (index < backgroundsForLocations.Length)
        {
            activeBackground = backgroundsForLocations[index];
            if (activeBackground.GetComponent<ActivateBackgroundLocation>() != null)
            {
                activeBackground.GetComponent<ActivateBackgroundLocation>().SetBackgroundAsActive();
            }
            activeBackground.SetActive(true);
        }

    }

    public void LoadLevel(Level level)
    {
        menuMaster.LoadLevel(level);
    }
}
