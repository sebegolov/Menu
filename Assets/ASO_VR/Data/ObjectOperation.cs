using System;
using System.Collections.Generic;
using UnityEngine;

namespace ASO_VR
{
    [Serializable]
    public class ObjectOperation : ObjectData
    {
        [SerializeField] private bool _work;
        public override JSONObject Save()
        {
            JSONObject saveJson = new JSONObject();

            saveJson.AddField("Data", base.Save());
            
            return saveJson;
        }

        public override void Load(JSONObject jsonObject)
        {
            JSONObject jObj = jsonObject.GetField("Data");
            
            if (jObj.GetField("Name").str == Name)
            {
                _work = jObj.GetField("Work").b;
            }
            base.Load(jsonObject.GetField("Data"));
        }

        public override void Init(IWorkCorrector corrector)
        {
            _work = false;
            base.Init(corrector);
        }

        protected override List<ObjectData> GetChilds()
        {
            return new List<ObjectData>();
        }

        public void SetWork(bool workState)
        {
            _work = _workCorrector?.CorrectWork(workState) ?? workState;
            EmitDataChanged();
        }

        public override bool GetWork()
        {
            return _work;
        }

    }
}
