using System.IO;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private string savePath;
        [SerializeField]
        private TextMeshProUGUI inputField;
        public static GameManager Instance;
        public SaveData saveData;
        public string playingUser;
        private void Awake()
        {
            savePath = Application.persistentDataPath + "/savefile.json";
            if (Instance is not null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }

        private void Start()
        {
            inputField = GameObject.Find("UsernameInputField").GetComponentInChildren<TextMeshProUGUI>();
        }

        public void StartGame()
        {
            SetUserName();
            Save();
            SceneManager.LoadScene(1);
        }

        public void SetUserName()
        {
            playingUser = inputField.text;
        }

        public void Save()
        {
            if (saveData is null)
                saveData = new SaveData();
            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(savePath,json);
        }

        private void LoadData()
        {
            if (File.Exists(savePath))
            {
                var json = File.ReadAllText(savePath);
                saveData = JsonUtility.FromJson<SaveData>(json);
                if (saveData is null)
                    saveData = new SaveData();
            }
            else
            {
                saveData = new SaveData();
                Save();
            }
        }
    }
}
