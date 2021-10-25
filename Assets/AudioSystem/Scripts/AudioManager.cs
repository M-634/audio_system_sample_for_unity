using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;//AudioMixerクラスを使うために必要

namespace M_634.Audio
{
    /// <summary>
    /// ゲーム全体のAudio機能を管理するクラス。
    /// SE、BGM、環境音等の2D音源に必要なAudioSouceコンポーネントを予め生成しておき、それぞれ必要なタイミングで呼び出す。
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region singlton 
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<AudioManager>();
                    if (instance == null)
                    {
                        instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        #endregion

        #region member variables
        [SerializeField, Header("MenuSE")]
        List<AudioClip> menuAuidoClipList = new List<AudioClip>();
        AudioSource menuSeAuidoSouce;

        [SerializeField, Header("GameSE")]
        List<AudioClip> gameSeAuidoClipList = new List<AudioClip>();
        AudioSource gameSeAuidoSouce;

        [SerializeField, Header("EnviromentSE")]
        List<AudioClip> enviromentAuidoClipList = new List<AudioClip>();
        AudioSource enviromentAuidoSouce;

        [SerializeField, Header("VoiceSE")]
        List<AudioClip> voiceAuidoClipList = new List<AudioClip>();
        AudioSource voiceAuidoSouce;

        [SerializeField, Header("BGM")]
        List<AudioClip> bgmAuidoClipList = new List<AudioClip>();
        List<AudioSource> bgmSeAuidoSouceList = new List<AudioSource>();
        [SerializeField, Header("Settings BGM Count")]
        int bgmAudioSourceNum;

        [SerializeField, Header("AudioMixer")]
        AudioMixer audioMixer;
        [SerializeField]
        AudioMixerGroup bgmAMG, menuSeAMG, gameSeAMG, envAMG, voiceAMG;//AMG = Aoudio Mixer Group

        private const string MasterVolumeParaName = "MasterVolume";
        private const string GameSeVolumeParaName = "GameSEVolume";
        private const string BGMVolumeParaName = "BGMVolume";
        private const string EnvVolumeParaName = "EnviroVolume";
        #endregion

        #region  properties
        public float MasterVolume
        {
            get => audioMixer.GetVolumeByLinear(MasterVolumeParaName);
            set => audioMixer.SetVolumeByLinear(MasterVolumeParaName, value);
        }

        public float GameSeVolume
        {
            get => audioMixer.GetVolumeByLinear(GameSeVolumeParaName);
            set => audioMixer.SetVolumeByLinear(GameSeVolumeParaName, value);
        }

        public float BGMVolume
        {
            get => audioMixer.GetVolumeByLinear(BGMVolumeParaName);
            set => audioMixer.SetVolumeByLinear(BGMVolumeParaName, value);
        }

        public float EnviromentVolume
        {
            get => audioMixer.GetVolumeByLinear(EnvVolumeParaName);
            set => audioMixer.SetVolumeByLinear(EnvVolumeParaName, value);
        }

        #endregion

        #region methods
        private void Start()
        {
            menuSeAuidoSouce = InitializeAudioSource(this.gameObject, false, menuSeAMG);
            gameSeAuidoSouce = InitializeAudioSource(this.gameObject, false, gameSeAMG);
            enviromentAuidoSouce = InitializeAudioSource(this.gameObject, false, envAMG);
            voiceAuidoSouce = InitializeAudioSource(this.gameObject, false, gameSeAMG);
            bgmSeAuidoSouceList = InitializeAuioSources(this.gameObject, true, bgmAMG);
        }

        /// <summary>
        /// AudioSourceをゲーム開始時に初期化する（スタート関数から呼ばれる）
        /// </summary>
        /// <param name="parentGameObject">AudioSourceをアッタッチするゲームオブジェット</param>
        /// <param name="isLoop">ループさせるかどうか</param>
        /// <param name="amg">指定のAudioMixerGrop</param>
        /// <returns>f</returns>
        private AudioSource InitializeAudioSource(GameObject parentGameObject, bool isLoop = false, AudioMixerGroup amg = null)
        {
            var audioSource = parentGameObject.AddComponent<AudioSource>();

            audioSource.loop = isLoop;
            audioSource.playOnAwake = false;

            if (amg)
            {
                audioSource.outputAudioMixerGroup = amg;
            }
            return audioSource;
        }



        /// <summary>
        /// 複数のAudioSourceをゲーム開始時に初期化する（スタート関数から呼ばれる）
        /// </summary>
        /// <param name="parentGameObject">AudioSourceをアッタッチするゲームオブジェット</param>
        /// <param name="isLoop">ループさせるかどうか</param>
        /// <param name="amg">指定のAudioMixerGrop</param>
        /// <returns>f</returns>
        private List<AudioSource> InitializeAuioSources(GameObject parentGamaObject, bool isLoop = false,
            AudioMixerGroup amg = null, int count = 1)
        {
            List<AudioSource> audioSources = new List<AudioSource>();

            for (int i = 0; i < count; i++)
            {
                var audioSource = InitializeAudioSource(parentGamaObject, isLoop, amg);
                audioSources.Add(audioSource);
            }
            return audioSources;
        }

        public void PlayMenuSe(string clipName)
        {

        }

        public void PlayGameSe(string clipName)
        {

        }

        public void PlayEnviroment(string clipName)
        {

        }

        public void StopEnviroment()
        {

        }

        public void PlayBGMWithFadeIn(string clipName, float fadeTime = 2f)
        {

        }

        public void StopBGMWithFadeOut(string clipName, float fadeTime = 2f)
        {

        }

        public void StopBGMWithFadeOut(float fadeTime = 2f)
        {

        }


        public void PlayVoice(string clipName, float delayTime = 0f)
        {

        }
        #endregion
    }
}
