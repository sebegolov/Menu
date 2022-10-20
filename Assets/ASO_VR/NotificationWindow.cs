using TMPro;
using UnityEngine;

namespace ASO_VR
{
    public class NotificationWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _notification;

        public void ShowMessage(string message)
        {
            _notification.SetActive(true);
            _text.text = message;
        }

        public void HideMessage()
        {
            _notification.SetActive(false);
        }
    }
}
