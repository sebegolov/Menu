namespace ASO_VR
{
    using UnityEngine;

    public class ObjectController : MonoBehaviour
    {
        [SerializeField] private ObjectsList _objectsList;
        [SerializeField] private StatesList StateList;             //надо чтобы объект не обнулялся

        private void Start()
        {
            if (_objectsList != null)
            {
                foreach (var objectType in _objectsList.GetTypeObjectsList())
                {
                    bool typeWork = objectType.GetWork();
                    ObjectActive(objectType.GetName(), typeWork);

                    if (typeWork)
                    {
                        foreach (var opaObject in objectType.GetObjectsList())
                        {
                            bool opaObjectWork = opaObject.GetWork();
                            
                            ObjectActive(opaObject.GetName(), opaObjectWork);

                            if (opaObjectWork)
                            {
                                if (opaObject.GetOperationsList().Count > 0)
                                {
                                    foreach (var operation in opaObject.GetOperationsList())
                                    {
                                        ObjectActive(operation.GetName(), operation.GetWork());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ObjectActive(string name, bool active)
        {
            GameObject operationGO = GameObject.Find(name);
            if (operationGO != null)
            {
                operationGO.SetActive(active);
            }
        }
    }
}
