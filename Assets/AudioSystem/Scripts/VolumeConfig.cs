using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using M_634.UGUIExtension;
using System;
using System.IO;
using UnityEngine.Events;

namespace M_634.Audio
{

    /// <summary>
    /// UnityのSlider機能を利用して、音量調整のUIを制御するクラス。
    /// ユーザーが調整するたびに、音量を*json形式で自動セーブする。
    /// ゲーム開始時に音量データがあればロードしてAudioMixerのVolumeにセットする。
    /// *音量設定は、ユーザーにセーブデータをいじられてもさして問題ないため、JSON形式を選んだ。
    /// </summary>
    public class VolumeConfig : MonoBehaviour
    {

        [Serializable]
        struct AudioVolumeData
        {
            public float masterVolume;
            public float bgmVolume;
            public float gameSeVolume;
            public float envVolume;
        }

        [SerializeField, Header("各種Sliderをセットする")]
        Slider masterSlider, bgmSlider, gameSeSlider, envSlider;

        private string path;

        void Start()
        {
            path = Application.dataPath + "/AudioVolumeData.json";

            //set events
            SetAudioVolumesEvent(masterSlider, vol => AudioManager.Instance.MasterVolume = vol);
            SetAudioVolumesEvent(bgmSlider, vol => AudioManager.Instance.BGMVolume = vol);
            SetAudioVolumesEvent(gameSeSlider, vol => AudioManager.Instance.GameSeVolume = vol);
            SetAudioVolumesEvent(envSlider, vol => AudioManager.Instance.EnviromentVolume = vol);

            //Load volume
            LoadVolumes();

            //Init volume on slider value
            masterSlider.SetSliderValue(AudioManager.Instance.MasterVolume);
            bgmSlider.SetSliderValue(AudioManager.Instance.BGMVolume);
            gameSeSlider.SetSliderValue(AudioManager.Instance.GameSeVolume);
            envSlider.SetSliderValue(AudioManager.Instance.EnviromentVolume);
        }

        private void SetAudioVolumesEvent(Slider slider, UnityAction<float> callBack)
        {
            callBack += (v) => AutoSaveVolumes();
            slider.SetValueChangedEvent(callBack);
        }

        private void AutoSaveVolumes()
        {
            AudioVolumeData data;
            data.masterVolume = AudioManager.Instance.MasterVolume;
            data.bgmVolume = AudioManager.Instance.BGMVolume;
            data.gameSeVolume = AudioManager.Instance.GameSeVolume;
            data.envVolume = AudioManager.Instance.EnviromentVolume;

            var json = JsonUtility.ToJson(data, true);
            Debug.Log("シリアライズされた　JSONデータ" + json);


            using var writer = new StreamWriter(path, false);
            writer.Write(json);
            Debug.Log("Save AudioVolume Data...");
        }

        private void LoadVolumes()
        {
            if (!File.Exists(path))
            {
                AudioManager.Instance.MasterVolume = 0.5f;
                AudioManager.Instance.BGMVolume = 0.5f;
                AudioManager.Instance.GameSeVolume = 0.5f;
                AudioManager.Instance.EnviromentVolume = 0.5f;
                Debug.Log("Init Audio Volume...");
                return;
            }

            using var reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            var data = JsonUtility.FromJson<AudioVolumeData>(json);

            //set volume
            AudioManager.Instance.MasterVolume = data.masterVolume;
            AudioManager.Instance.BGMVolume = data.bgmVolume;
            AudioManager.Instance.GameSeVolume = data.gameSeVolume;
            AudioManager.Instance.EnviromentVolume = data.envVolume;

            Debug.Log("Loading Audio Volumes...");
        }
    }
}