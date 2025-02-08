using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SingletonGameManager
{   
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        public static SceneNumber sceneNumber;
        private string sceneName;
        private SceneNumber currentSceneName; 

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
            LoadNextStageScene(currentSceneName);
        }

        public void LoadScene(SceneNumber sceneNumber)
        {
            sceneName = sceneNumber switch
            {
                SceneNumber.Stage_01 => "Stage_01",
                SceneNumber.Stage_02 => "Stage_02",
                SceneNumber.Stage_03 => "Stage_03",
                _ => throw new ArgumentOutOfRangeException(nameof(sceneName), $"씬 {sceneName}은 존재하지 않습니다!")
            };
            Debug.Log("씬 로드중");
            SceneManager.LoadScene(sceneName);
        }
        public void LoadNextStageScene(SceneNumber currentSceneNumber)
        {
            SceneNumber nextSceneNumber = (SceneNumber)((int)currentSceneNumber + 1);
            currentSceneNumber = nextSceneNumber;
            if((int)nextSceneNumber != 4)
            {
                SceneManager.LoadScene(sceneName);
            } 
        }
        public void onDollReachedGoal()
        {
            Clear();
        }
    }
}

