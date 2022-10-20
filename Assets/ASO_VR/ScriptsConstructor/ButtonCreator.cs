using TMPro;

namespace ASO_VR
{
    using UnityEngine;

    public class ButtonCreator : SaveLoad
    {
        [SerializeField] private RectTransform _objectTypesPanel;
        [SerializeField] private RectTransform _objectsPanel;
        [SerializeField] private TextMeshProUGUI _textState;
        
        [SerializeField] private RectTransform _typePrefab;
        [SerializeField] private RectTransform _objectPrefab;

        private void Start()
        {
            base.Start();
            _objectsList.Init();
            foreach (var type in _objectsList.GetTypeObjectsList())
            {
                type.DataChanged += UpdateTextField;
                AddTypeField(type);
            }
        }

        private void AddTypeField(TypeObject typeObject)
        {
            GameObject instance = Instantiate(_typePrefab.gameObject);
            instance.transform.SetParent(_objectTypesPanel, false);
            
            TypeField typeField = instance.GetComponent<TypeField>();
            typeField.SetParameters(typeObject);
            typeField.click += TypeFieldIsClick;
        }

        private void TypeFieldIsClick(TypeObject typeObject)
        {
            ClearObjectsPanel();
            
            foreach (var opaObject in typeObject.GetObjectsList())
            {
                AddObjectField(opaObject);
            }
        }

        private void ClearObjectsPanel()
        {
            foreach (Transform child in _objectsPanel)
            {
                Destroy(child.gameObject);
            }
        }

        private void AddObjectField(OPAObject opaObject)
        {
            GameObject instance = Instantiate(_objectPrefab.gameObject);
            instance.transform.SetParent(_objectsPanel, false);
            
            ObjectField objectField = instance.GetComponent<ObjectField>();
            objectField.SetParameters(opaObject);
        }

        private void UpdateTextField()
        {
            _textState.text =
                CommentStateConstrucor.ParseJsonState(new Lesson(_objectsList.GetTypeObjectsList()).Save());
        }

    }
}
