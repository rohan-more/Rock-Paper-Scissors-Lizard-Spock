using UnityEngine;


namespace RPSLS.Core
{
    /// <summary>
    /// Handles the save data for the game
    /// </summary>
    public static class SaveDataManager
    {
        public static void SaveKey(string key, int value)
        {
            if (value > PlayerPrefs.GetInt(key, 0))
            {
                PlayerPrefs.SetInt(key, value);
                PlayerPrefs.Save();
            }
        }

        public static void SaveKey(string key, float value)
        {
            if (value > PlayerPrefs.GetFloat(key, 0))
            {
                PlayerPrefs.SetFloat(key, value);
                PlayerPrefs.Save();
            }
        }

        public static int GetKey(string key)
        {
            return PlayerPrefs.GetInt(key, 0);
        }

        public static float GetFloatKey(string key)
        {
            return PlayerPrefs.GetFloat(key, 0);
        }

        public static void ResetKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
    }
}

