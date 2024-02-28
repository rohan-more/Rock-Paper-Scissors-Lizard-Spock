using UnityEngine;


namespace RPSLS.Core
{
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

        public static int GetKey(string key)
        {
            return PlayerPrefs.GetInt(key, 0);
        }

        public static void ResetKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
    }
}

