using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace M_634.Audio
{
    //*音量設定は、ユーザーにセーブデータをいじられてもさして問題ないため、JSON形式を選んだ。

    /// <summary>
    /// UnityのSlider機能を利用して、音量調整のUIを制御するクラス。
    /// ユーザーが調整するたびに、音量を*json形式でセーブする。
    /// </summary>
    public class VolumeConfig : MonoBehaviour
    {

        [SerializeField, Header("各種Sliderをセットする")]
        Slider masterSlider, bgmSlider, gameSeSlider, envSlider;

        void Start()
        {
            
        }

        private void SetVolumeSliderEvent(Slider slider,UnityAction<float> sliderCallback)
        {
            slider.SetValueChangedEvent(sliderCallback);
        }
    }

    /// <summary>
    /// UnityEngine.UIに含まれる各種クラスの拡張クラス
    /// </summary>
   public static class UGUIListnerExtension
    {
        public static void SetValueChangedEvent(this Slider slider,UnityAction<float> sliderCallback)
        {
            if (slider.onValueChanged != null)
            {
                slider.onValueChanged.RemoveAllListeners();
            }
            slider.onValueChanged.AddListener(sliderCallback);
        }
    }
}