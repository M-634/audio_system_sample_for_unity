using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using M_634.UGUIExtension;
using UnityEngine.Events;

namespace M_634.Audio
{

    /// <summary>
    /// UnityのSlider機能を利用して、音量調整のUIを制御するクラス。
    public class VolumeConfigUGUI : MonoBehaviour
    {
        [SerializeField, Header("各種Sliderをセットする")]
        Slider masterSlider, bgmSlider, gameSeSlider, envSlider;

        AudioVolumeData volumeData;
        string path;

        void Start()
        {
#if UNITY_EDITOR
            path = Application.dataPath + "/AudioVolumeData.json";
#else
            path = Application.persistentDataPath + "/AudioVolumeData.json"; 
#endif
            LoadAudioVolume();

            //set events
            SetAudioVolumesEvent(masterSlider, vol => SetMasterVolumeData(vol));
            SetAudioVolumesEvent(bgmSlider, vol => SetBGMVolumeData(vol));
            SetAudioVolumesEvent(gameSeSlider, vol => SetGameSeVolumeData(vol));
            SetAudioVolumesEvent(envSlider, vol => SetEnviromentVolumeData(vol));


            //Init volume on slider value
            masterSlider.SetSliderValue(AudioManager.Instance.MasterVolume);
            bgmSlider.SetSliderValue(AudioManager.Instance.BGMVolume);
            gameSeSlider.SetSliderValue(AudioManager.Instance.GameSeVolume);
            envSlider.SetSliderValue(AudioManager.Instance.EnviromentVolume);

            //アプリケーション終了時、音量データをセーブする
            Application.quitting += SaveAuiodVolume;
        }

        private void LoadAudioVolume()
        {
            volumeData = new AudioVolumeData();
            JsonCutomUtility.LoadDataFromJson(path,ref volumeData);

            AudioManager.Instance.MasterVolume = volumeData.masterVolume;
            AudioManager.Instance.BGMVolume= volumeData.bgmVolume;
            AudioManager.Instance.GameSeVolume = volumeData.gameSeVolume;
            AudioManager.Instance.EnviromentVolume= volumeData.envVolume;
        }

        private void SaveAuiodVolume()
        {
            volumeData.masterVolume = AudioManager.Instance.MasterVolume;
            volumeData.bgmVolume = AudioManager.Instance.BGMVolume;
            volumeData.gameSeVolume = AudioManager.Instance.GameSeVolume;
            volumeData.envVolume = AudioManager.Instance.EnviromentVolume;

            JsonCutomUtility.SaveDataToJson(path, volumeData);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        private void SetAudioVolumesEvent(Slider slider, UnityAction<float> callBack)
        {
            slider.SetValueChangedEvent(callBack);
        }

        private void SetMasterVolumeData(float volume)
        {
            AudioManager.Instance.MasterVolume = volume;
      
        }
        private void SetBGMVolumeData(float volume)
        {
            AudioManager.Instance.BGMVolume = volume;
         
        }

        private void SetGameSeVolumeData(float volume)
        {
            AudioManager.Instance.GameSeVolume = volume;
          
        }
        private void SetEnviromentVolumeData(float volume)
        {
            AudioManager.Instance.EnviromentVolume = volume;
          
        }
    }
}