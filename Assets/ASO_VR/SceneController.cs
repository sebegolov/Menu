using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASO_VR
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode1 = KeyCode.Escape;
        [SerializeField] private Scene _scene1;

        void Update()
        {
            if (Input.GetKeyUp(_keyCode1))
            {
                SceneManager.LoadScene(_scene1.handle);
            }
        }
    }
}
