using UnityEngine;
using System.IO;

namespace M_634.Audio
{
    public interface ISerializeToJson
    {
        /// <summary>
        /// JSON形式で送るメンバー変数の初期化
        /// </summary>
        void InitMemberValues();
    }

    public static class JsonCutomUtility
    {
        public static void SaveDataToJson<T>(string path, T data) where T: ISerializeToJson
        {
            var json = JsonUtility.ToJson(data, true);

            Debug.Log("シリアライズされた　JSONデータ" + json);

            using var writer = new StreamWriter(path, false);
            writer.Write(json);
            Debug.Log($"Save {typeof(T).Name} Data...");
        }

        public static void LoadDataFromJson<T>(string path,ref T data) where T :ISerializeToJson
        {
            if(data == null)
            {
                Debug.LogError($"{data}がインスタンス化されていません");
                return;
            }

            if (!File.Exists(path))
            {
                Debug.LogWarning($"{path}が存在しません");
                data.InitMemberValues();
                return;
            }

            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            data = JsonUtility.FromJson<T>(json);

            Debug.Log($"Loading {typeof(T).Name}...");
        }
    }
}