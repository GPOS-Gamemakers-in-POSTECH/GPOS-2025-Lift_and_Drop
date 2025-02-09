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
        private static SceneNumber currentSceneName = SceneNumber.Start_scene;
        private int numberOFScenes = Enum.GetValues(typeof(SceneNumber)).Length;

        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                sceneName = SceneManager.GetActiveScene().name;
                if(System.Enum.TryParse(sceneName, out SceneNumber parsedSceneName))
                {
                    currentSceneName = parsedSceneName;
                    Debug.Log("��ȯ �Ϸ�");
                }
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
                _ => throw new ArgumentOutOfRangeException(nameof(sceneName), $"�� {sceneName}�� �������� �ʽ��ϴ�!")
            };
            Debug.Log("�� �ε���");
            SceneManager.LoadScene(sceneName);
        }
        public void LoadNextStageScene(SceneNumber currentSceneNumber)
        {
            SceneNumber nextSceneNumber = (SceneNumber)((int)currentSceneNumber + 1);
            currentSceneName = nextSceneNumber;
            if((int)nextSceneNumber != (numberOFScenes))
            {
                Debug.Log(nextSceneNumber.ToString());
                AudioManager.Instance.playOnlyNormal();
                SceneManager.LoadScene(nextSceneNumber.ToString());
            } 
        }
        public void RestartScene(SceneNumber currentSceneNumber)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void OnDollReachedGoal()
        {
            Clear();
        }
        public void OnSwitchReached()
        {
            GameObject tilemapObj = GameObject.FindWithTag("SwitchTile");
            Debug.Log("������ ��");
            if (tilemapObj != null) 
            {
                Tilemap tilemap = tilemapObj.GetComponent<Tilemap>();
                TilemapCollider2D tilemapCollider = tilemapObj.GetComponent<TilemapCollider2D>();

                if (tilemap != null) tilemap.gameObject.SetActive(false); // ������ OFF
                if (tilemapCollider != null) tilemapCollider.enabled = false; // �浹 OFF
            }
            else
            {
                Debug.LogError("SwitchTile �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
}

