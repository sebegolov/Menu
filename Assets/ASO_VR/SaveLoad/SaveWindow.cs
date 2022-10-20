using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace ASO_VR
{
    public class SaveWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject _window;

        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private TextMeshProUGUI Comment;
        
        [SerializeField] private NotificationWindow _notificationWindow;
        [SerializeField] private SaveLoad _saveSystem;

        [SerializeField] private TypeSave _typeSave;

        private readonly string _forbiddenSymbols = "'!#$%&()*,+-";
        private bool _canSave = false;

        public void ShowWindow(bool show)
        {
            _window.SetActive(show);
        }

        public void Save()
        {
            if (_canSave)
            {
                string name = Name.text;
                name.Remove(Name.text.Length-1);
            
                if (!_saveSystem.TrySave(Name.text.Remove(Name.text.Length-1), Comment.text.Remove(Comment.text.Length-1), _typeSave))
                {
                    _notificationWindow.ShowMessage("Данное имя уже существует");
                }
                else
                {
                    ShowWindow(false);
                }
            }
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                CheckName();
            }
        }

        public void CheckName()
        {
            if (Name.text.Intersect(_forbiddenSymbols).Any())
            {
                _canSave = false;
                _notificationWindow.ShowMessage("Введён недопустимый символ");
            }
            else
            {
                _canSave = Name.text.Length > 1;
                _notificationWindow.HideMessage();
            }
        }
    }
}
