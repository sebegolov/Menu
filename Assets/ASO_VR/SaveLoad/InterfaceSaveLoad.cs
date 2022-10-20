namespace ASO_VR
{
    public interface InterfaceSaveLoad
    {
        JSONObject Save();
        void Load(JSONObject jsonObject);
    }
}
