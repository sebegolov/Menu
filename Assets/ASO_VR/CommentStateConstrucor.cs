namespace ASO_VR
{
    public static class CommentStateConstrucor
    {
        public static string ParseJsonState(JSONObject jObj)
        {
            string state = "";

            JSONObject ObjectTypes = jObj.GetField("Lesson").GetField("ObjectTypes");

            int typeNumber = 1;
            
            foreach (var type in ObjectTypes.list)
            {
                if (!type.GetField("Data").GetField("Work").b) continue;

                state += typeNumber + ") ";
                string TypeName = type.GetField("Data").GetField("LocalName").str;
                state += TypeName + ":\n";
                ++typeNumber;

                foreach (var opaObject in type.GetField("OpaObjects").list)
                {
                    if (!opaObject.GetField("Data").GetField("Work").b) continue;

                    state += "\t- " + opaObject.GetField("Data").GetField("LocalName").str + ":\n";

                    foreach (var operation in opaObject.GetField("OpaOperations").list)
                    {
                        if (!operation.GetField("Data").GetField("Work").b) continue;
                        
                        state += "\t\t· " + operation.GetField("Data").GetField("LocalName").str + ";\n";
                    }
                }
            }
            
            return state;
        }
    }
}

