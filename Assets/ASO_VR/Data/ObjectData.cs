using System;
using System.Collections.Generic;
using UnityEngine;

namespace ASO_VR
{
    [Serializable]
    public abstract class ObjectData : InterfaceSaveLoad
    {
        [field: NonSerialized] public event Action DataChanged;
        
        [SerializeField] protected string Name;
        [SerializeField] protected string LocalName;

        [NonSerialized] protected IWorkCorrector _workCorrector;
        
        public virtual JSONObject Save()
        {
            JSONObject jObj = new JSONObject();
            
            jObj.AddField("Name", Name);
            jObj.AddField("LocalName", LocalName);
            jObj.AddField("Work", GetWork());
            
            return jObj;
        }

        public virtual void Load(JSONObject jsonObject)
        {
            
        }

        public virtual void SetName(string name)
        {
            Name = name;
        }

        public virtual string GetName()
        {
            return Name;
        }

        public virtual void SetLocalName(string localName)
        {
            LocalName = localName;
        }

        public virtual string GetLocalName()
        {
            return LocalName;
        }

        public virtual void Init(IWorkCorrector corrector)
        {
            _workCorrector = corrector;
            foreach (var child in GetChilds())
            {
                child.Init(corrector);
                child.DataChanged += EmitDataChanged;
            }
        }

        public void SetWorkCorrector(IWorkCorrector corrector)
        {
            _workCorrector = corrector;
        }
        
        public virtual bool GetWork()
        {
            foreach (var child in GetChilds())
            {
                if (child.GetWork())
                {
                    return true;
                }
            }
            
            return false;
        }

        protected virtual void EmitDataChanged()
        {
            DataChanged?.Invoke();
        }

        protected abstract List<ObjectData> GetChilds();

    }
}
