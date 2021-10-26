using System;

namespace M_634.AudioSystemSample
{
    /// <summary>
    /// 音量データ。
    /// </summary>
    [Serializable]
    public class AudioVolumeData　: ISerializeToJson
    {
        public float masterVolume;
        public float bgmVolume;
        public float gameSeVolume;
        public float envVolume;

        public void InitMemberValues()
        {
            masterVolume = 0.5f;
            bgmVolume = 0.5f;
            gameSeVolume = 0.5f;
            envVolume = 0.5f;
        }
    }
}