using SingletonAudioManager;
using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
namespace SingletonGameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        public static SceneNumber sceneNumber;
        private string sceneName;
        public static SceneNumber currentSceneName = SceneNumber.Start_scene;
        private int numberOFScenes = Enum.GetValues(typeof(SceneNumber)).Length;
        bool timeStop = false;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                sceneName = SceneManager.GetActiveScene().name;
                if (System.Enum.TryParse(sceneName, out SceneNumber parsedSceneName))
                {
                    currentSceneName = parsedSceneName;
                }
                Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow); //해상도 설정
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (timeStop)
                {
                    Time.timeScale = 1f;
                    timeStop = false;
                }
                else
                {
                    Time.timeScale = 0f;
                    timeStop = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (timeStop)
                {
                    timeStop = false;
                    Time.timeScale = 1f;
                    RestartScene(currentSceneName);
                }
            }
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
            currentSceneName = nextSceneNumber;
            if ((int)nextSceneNumber != (numberOFScenes))
            {
                Debug.Log(nextSceneNumber.ToString());
                AudioManager.Instance.playOnlyNormal();
                SceneManager.LoadScene(nextSceneNumber.ToString());
            }
        }
        public void RestartScene(SceneNumber currentSceneNumber)
        {
            SceneManager.LoadScene(sceneName);
            if (!AudioManager.bgmSource.isPlaying)
            {
                AudioManager.Instance.playBgm();
            }
        }
        public void OnDollReachedGoal()
        {
            Clear();
        }
        public void OnSwitchReached()
        {
            GameObject tilemapObj = GameObject.FindWithTag("SwitchTile");
            if (tilemapObj != null)
            {
                Tilemap tilemap = tilemapObj.GetComponent<Tilemap>();
                TilemapCollider2D tilemapCollider = tilemapObj.GetComponent<TilemapCollider2D>();

                if (tilemap != null) tilemap.gameObject.SetActive(false); // 렌더링 OFF
                if (tilemapCollider != null) tilemapCollider.enabled = false; // 충돌 OFF
            }
            else
            {
                Debug.LogError("SwitchTile 태그를 가진 오브젝트를 찾을 수 없습니다.");
            }
        }
        public void StopGame()
        {
            Time.timeScale = 0;
        }
        public void RestartGame()
        {
            Time.timeScale = 1;
        }
    }
}

