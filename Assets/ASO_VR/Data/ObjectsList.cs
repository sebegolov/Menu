using System;
using System.Collections.Generic;
using UnityEngine;

namespace ASO_VR
{
    [CreateAssetMenu(), Serializable]
    public class ObjectsList : ScriptableObject
    {
        [SerializeField] private int _maxOperation = 10;

        [SerializeField] private List<TypeObject> _objectTypes = new List<TypeObject>();

        public void Init()
        {
            OperationWorkCorrector corrector = new OperationWorkCorrector(_maxOperation);
            
            foreach (var objectType in _objectTypes)
            {
                objectType.Init(corrector);
            }
        }

        public List<TypeObject> GetTypeObjectsList()
        {
            return _objectTypes;
        }

        public void SetTypeObjectsList(List<TypeObject> typeObjects)
        {
            _objectTypes = typeObjects;
        }
    }
}

