using UnityEngine;

namespace IJ.Core.UIElements.GameFlowPanels
{
    [RequireComponent(typeof(PanelsUISound))]
    public class PanelsUI : MonoBehaviour
    {
        [SerializeField] private StartMenuPanelUI startPanel;
        [SerializeField] private MenuPanelUI endLevelPanel;
        [SerializeField] private MenuPanelUI gameOverPanel;

        private PanelsUISound _sound;

        private void Awake()
        {
            _sound = GetComponent<PanelsUISound>();
        }

        public void SetStartPanel(int levelNumber, int maxCrashesNumber)
        {
            startPanel.gameObject.SetActive(true);
            startPanel.SetPanelHeader(levelNumber.ToString());
            startPanel.SetAdditionalNumber((maxCrashesNumber + 1).ToString());
            _sound.PlayStartSound();
        }

        public void SetEndLevelOrGameOverPanel(int mode, int levelNumber, bool needStopGameTime = true)
        {
            if (needStopGameTime) Time.timeScale = 0f;

            if (mode == 0)
            {
                gameOverPanel.gameObject.SetActive(true);
                gameOverPanel.SetPanelHeader(levelNumber.ToString());
                _sound.PlayLoseSound();
            }
            else if (mode == 1)
            {
                endLevelPanel.gameObject.SetActive(true);
                endLevelPanel.SetPanelHeader(levelNumber.ToString());
                _sound.PlayWinSound();
            }
        }
    }
}
