using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;//AudioMixer�N���X���g�����߂ɕK�v

namespace M_634.AudioSystemSample
{
    /// <summary>
    /// �Q�[���S�̂�Audio�@�\���Ǘ�����N���X�B
    /// SE�ABGM�A��������2D�����ɕK�v��AudioSouce�R���|�[�l���g��\�ߐ������Ă����A���ꂼ��K�v�ȃ^�C�~���O�ŌĂяo���B
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
        List<AudioClip> menuSeAuidoClipList = new List<AudioClip>();
        AudioSource menuSeAuidoSource;

        [SerializeField, Header("GameSE")]
        List<AudioClip> gameSeAuidoClipList = new List<AudioClip>();
        AudioSource gameSeAuidoSource;

        [SerializeField, Header("EnviromentSE")]
        List<AudioClip> enviromentAuidoClipList = new List<AudioClip>();
        AudioSource enviromentAuidoSource;

        [SerializeField, Header("VoiceSE")]
        List<AudioClip> voiceAudioClipList = new List<AudioClip>();
        AudioSource voiceAudioSource;

        [SerializeField, Header("BGM")]
        List<AudioClip> bgmAuidoClipList = new List<AudioClip>();
        List<AudioSource> bgmAudioSourceList = new List<AudioSource>();
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
            menuSeAuidoSource = InitializeAudioSource(this.gameObject, false, menuSeAMG);
            gameSeAuidoSource = InitializeAudioSource(this.gameObject, false, gameSeAMG);
            enviromentAuidoSource = InitializeAudioSource(this.gameObject, false, envAMG);
            voiceAudioSource = InitializeAudioSource(this.gameObject, false, gameSeAMG);
            bgmAudioSourceList = InitializeAuioSources(this.gameObject, true, bgmAMG);
        }

        /// <summary>
        /// AudioSource���Q�[���J�n���ɏ���������i�X�^�[�g�֐�����Ă΂��j
        /// </summary>
        /// <param name="parentGameObject">AudioSource���A�b�^�b�`����Q�[���I�u�W�F�b�g</param>
        /// <param name="isLoop">���[�v�����邩�ǂ���</param>
        /// <param name="amg">�w���AudioMixerGrop</param>
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
        /// ������AudioSource���Q�[���J�n���ɏ���������i�X�^�[�g�֐�����Ă΂��j
        /// </summary>
        /// <param name="parentGameObject">AudioSource���A�b�^�b�`����Q�[���I�u�W�F�b�g</param>
        /// <param name="isLoop">���[�v�����邩�ǂ���</param>
        /// <param name="amg">�w���AudioMixerGrop</param>
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
            var audioClip = menuSeAuidoClipList.FirstOrDefault(clip => clip.name == clipName);
             
            if (audioClip == null)
            {
                Debug.LogWarning(clipName + "�͌�����܂���");
                return;
            }

            menuSeAuidoSource.Play(audioClip);
        }

        /// <summary>
        /// �Q�[���V�[���ŌJ��Ԃ��Ȃ鉹�̓s�b�`�������_���ɁA
        /// ���ꉉ�o�Ȃǂ̉����͏����{�����[�����グ��
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="pitchRandom"></param>
        /// <param name="range"></param>
        public void PlayGameSe(string clipName, bool pitchRandom = true, float range = 0.5f)
        {
            var audioClip = gameSeAuidoClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.LogWarning(clipName + "�͌�����܂���");
                return;
            }

            if (pitchRandom)
            {
                range = Mathf.Clamp(range, 0.1f, 1f);
                gameSeAuidoSource.pitch = Random.Range(1f - range, 1f + range);
            }
            else
            {
                gameSeAuidoSource.pitch = 1;
            }
            gameSeAuidoSource.Play(audioClip);
        }

        public void PlayEnviroment(string clipName)
        {
            var audioClip = enviromentAuidoClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.LogWarning(clipName + "�͌�����܂���");
                return;
            }

            enviromentAuidoSource.Play(audioClip);
        }

        public void StopEnviroment()
        {
            if (enviromentAuidoSource.isPlaying)
            {
                enviromentAuidoSource.Stop();
            }
        }

        public void PlayBGMWithFadeIn(string clipName, float fadeTime = 2f)
        {
            var audioClip = bgmAuidoClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.LogWarning(clipName + "�͌�����܂���");
                return;
            }

            if (bgmAudioSourceList.Any(source => source.clip == audioClip))
            {
                Debug.Log(clipName + "�͂��łɍĐ�����Ă��܂�");
                return;
            }

            StopBGMWithFadeOut(fadeTime);//�Đ�����BGM��FadeOut

            var audioSource = bgmAudioSourceList.FirstOrDefault(bgm => bgm.isPlaying == false);

            if (audioSource)
            {
                audioSource.PlayWithFadeIn(audioClip, fadeTime);
            }
        }

        public void StopBGMWithFadeOut(string clipName, float fadeTime = 2f)
        {
            var audioSource = bgmAudioSourceList.FirstOrDefault(bgm => bgm.clip.name == clipName);

            if (audioSource == null || audioSource.isPlaying == false)
            {
                Debug.Log(clipName + "�͍Đ�����Ă��܂���I�I");
                return;
            }

            audioSource.StopWithFadeOut(fadeTime);
        }

        public void StopBGMWithFadeOut(float fadeTime = 2f)
        {
            var audioSorceList = bgmAudioSourceList.Where(bgm => bgm.isPlaying).ToList();
            foreach (var audioSource in audioSorceList)
            {
                audioSource.StopWithFadeOut(fadeTime);
            }
        }


        public void PlayVoice(string clipName, float delayTime = 0f)
        {
            var audioClip = voiceAudioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip == null)
            {
                Debug.LogWarning(clipName + "�͌�����܂���");
                return;
            }

            voiceAudioSource.clip = audioClip;
            voiceAudioSource.PlayDelayed(delayTime);

            voiceAudioSource.PlayScheduled(AudioSettings.dspTime + delayTime);
        }
        #endregion
    }
}
