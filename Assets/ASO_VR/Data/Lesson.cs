using System;
using System.Collections.Generic;
using System.Linq;

namespace ASO_VR
{
    [Serializable]
    public class Lesson : InterfaceSaveLoad
    {
        protected List<TypeObject> _objectstype = new List<TypeObject>();
        protected string _name;
        protected string _comment = "";

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
        
        public string GetComment()
        {
            return
                "<lesson.state>" + CommentStateConstrucor.ParseJsonState(Save()) + "</lesson.state>" +
                "<lesson.comment>" + _comment + "</lesson.comment>";
        }

        public void SetComment(string comment)
        {
            _comment = comment;
        }

        public Lesson( List<TypeObject> objectsType)
        {
            _objectstype = ObjectCopier.Clone(objectsType);
        }

        public List<TypeObject> GetListObjectTypes()
        {
            return _objectstype;
        }
        
        public JSONObject Save()
        {
            JSONObject lesson = new JSONObject();
            
            JSONObject state = new JSONObject();
            if (_comment.Length > 0)
            {
                state.AddField("Comment", _comment);
            }
            state.AddField("Name", _name);
            JSONObject objectTypes = new JSONObject();
            foreach (var objectType in _objectstype)
            {
                objectTypes.AddField(objectType.GetName(), objectType.Save());
            }
            state.AddField("ObjectTypes", objectTypes);
            lesson.AddField("Lesson", state);
            
            return lesson;
        }

        public void Load(JSONObject jsonObject)
        {
            JSONObject Lesson = jsonObject.GetField("Lesson");
            if (Lesson.HasField("Comment"))
            {
                _comment = Lesson.GetField("Comment").str;
            }
            _name = Lesson.GetField("Name").str;

            JSONObject objectTypes = Lesson.GetField("ObjectTypes");
            
            foreach (var objectType in _objectstype)
            {
                if (objectTypes.HasField(objectType.GetName()))
                {
                    objectType.Load(objectTypes.GetField(objectType.GetName()));
                }
            }
        }
    }
}
