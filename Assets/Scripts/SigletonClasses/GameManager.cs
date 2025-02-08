using UnityEngine;
using UnityEngine.SceneManagement;
namespace SingletonGameManager
{   
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Clear()
        {
            Debug.Log("Clear");
        }
    }
        
}

