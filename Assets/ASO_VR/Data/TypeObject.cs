using System;
using System.Collections.Generic;
using UnityEngine;

namespace ASO_VR
{
    [Serializable]
    public class TypeObject : ObjectData
    {
        [SerializeField] private List<OPAObject> _opaObjects = new List<OPAObject>();
        public override JSONObject Save()
        {
            JSONObject saveJson = new JSONObject();
            
            saveJson.AddField("Data", base.Save());
            JSONObject saveObjects = new JSONObject();
            foreach (var opaObject in _opaObjects)
            {
                saveObjects.AddField(opaObject.GetName(), opaObject.Save());
            }
            saveJson.AddField("OpaObjects", saveObjects);
            
            return saveJson;
        }

        public override void Load(JSONObject jsonObject)
        {
            base.Load(jsonObject.GetField("Data"));

            foreach (var opaObject in _opaObjects)
            {
                if (jsonObject.GetField("OpaObjects").HasField(opaObject.GetName()))
                {
                    opaObject.Load(jsonObject.GetField("OpaObjects").GetField(opaObject.GetName()));
                }
            }
        }

        protected override List<ObjectData> GetChilds()
        {
            return _opaObjects.ConvertAll(item => (ObjectData) (object) item);
        }

        public List<OPAObject> GetObjectsList()
        {
            return _opaObjects;
        }
    }
}
