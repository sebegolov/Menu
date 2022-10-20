
namespace ASO_VR
{
    public class Report : InterfaceSaveLoad
    {
        private JSONObject _lesson;
        private string _name;
        private string _comment;
        
        public Report(){}

        public Report(JSONObject lesson, string name, string comment)
        {
            _lesson = lesson;
            _name = name;
            _comment = comment;
        }

        public string GetComment()
        {
            return
                "<lesson.state>" + CommentStateConstrucor.ParseJsonState(_lesson) + "</lesson.state>" +
                "<lesson.comment>" + _lesson.GetField("Lesson").GetField("Comment").str + "</lesson.comment>"+
                "<report.comment>" + _comment + "</report.comment>";
        }

        public JSONObject Save()
        {
            JSONObject Report = new JSONObject();
            
            JSONObject jObj = new JSONObject();

            jObj.AddField("Name", _name);
            if (_comment.Length > 0)
            {
                jObj.AddField("Comment", _comment);
            }
            
            Report.AddField("Report", jObj);
            Report.Absorb(_lesson);
            
            return Report;
        }

        public void Load(JSONObject jsonObject)
        {
            JSONObject Report = jsonObject.GetField("Report");

            _name = Report.GetField("Name").str;
            if (Report.HasField("Comment"))
            {
                _comment = Report.GetField("Comment").str;
            }
            _lesson = new JSONObject();
            _lesson.AddField("Lesson", jsonObject["Lesson"]);
        }

        public string GetName()
        {
            return _name;
        }
    }
}
