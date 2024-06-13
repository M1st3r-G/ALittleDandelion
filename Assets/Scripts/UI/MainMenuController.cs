using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene(1);
        }
        
        public void NewGame()
        {
            PlayerPrefs.SetString(SaveGameManager.PrefSaveKey, "");
            LoadGame();
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}