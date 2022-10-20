using System;
using System.Collections.Generic;
using UnityEngine;

namespace ASO_VR
{
    [Serializable]
    public class OPAObject : ObjectData
    {
        [SerializeField] private List<ObjectOperation> _operations = new List<ObjectOperation>();
        public override JSONObject Save()
        {
            JSONObject saveJson = new JSONObject();

            saveJson.AddField("Data", base.Save());
            JSONObject saveOperations = new JSONObject();
            foreach (var operation in _operations)
            {
                saveOperations.AddField(operation.GetName(), operation.Save());
            }
            saveJson.AddField("OpaOperations", saveOperations);
            
            return saveJson;
        }

        public override void Load(JSONObject jsonObject)
        {
            base.Load(jsonObject.GetField("Data"));

            foreach (var operation in _operations)
            {
                if (jsonObject.GetField("OpaOperations").HasField(operation.GetName()))
                {
                    operation.Load(jsonObject.GetField("OpaOperations").GetField(operation.GetName()));
                }
            }
        }

        public List<ObjectOperation> GetOperationsList()
        {
            return _operations;
        }
        
        protected override List<ObjectData> GetChilds()
        {
            return _operations.ConvertAll(item => (ObjectData) (object) item);
        }

    }
}
