using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASO_VR
{
    public class SceneChanger : MonoBehaviour
    {
        public Scene test1;
    
        public void LoadScene()
        {
            SceneManager.LoadScene(test1.handle);
        }

        public void OnApplicationQuit()
        {
            Application.Quit();
        }
    }
}
