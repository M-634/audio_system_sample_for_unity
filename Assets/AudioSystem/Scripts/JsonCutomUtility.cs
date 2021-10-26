using UnityEngine;
using System.IO;

namespace M_634.AudioSystemSample
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
        private enum ErrorType
        {
            None, PathIsNull, DataIsNotInstance
        }

        private static ErrorType CheackErrorType<T>(string path, T data) where T : ISerializeToJson
        {
            if (path == "")
            {
                Debug.LogError("pathが指定されていません");
                return ErrorType.PathIsNull;
            }

            if (data == null)
            {
                Debug.LogError($"{typeof(T).Name}のデータオブジェクトがインスタンス化されていません");
                return ErrorType.DataIsNotInstance;
            }
            return ErrorType.None;
        }

        public static void SaveDataToJson<T>(string path, T data) where T : ISerializeToJson
        {
            if (CheackErrorType(path, data) != ErrorType.None) return;

            var json = JsonUtility.ToJson(data, true);

            Debug.Log("シリアライズされた　JSONデータ" + json);

            using var writer = new StreamWriter(path, false);
            writer.Write(json);
            Debug.Log($"Save {typeof(T).Name} Data...");
        }

        public static void LoadDataFromJson<T>(string path, ref T data) where T : ISerializeToJson
        {
            if (CheackErrorType(path, data) != ErrorType.None) return;

            //初期ロード
            if (!File.Exists(path))
            {
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