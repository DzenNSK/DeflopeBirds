using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class ScoreScreenController : MonoBehaviour
    {
        [SerializeField] private TMP_Text highScoreField;
        [SerializeField] private Button closeButton;

        [Inject] private UserDataService userDataService;

        private void OnCloseButton()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            highScoreField.text = $"High score: {userDataService.HighScore}";
        }

        private void Start()
        {
            closeButton.onClick.AddListener(OnCloseButton);
        }
    }
}
