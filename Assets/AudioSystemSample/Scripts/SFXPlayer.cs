using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace M_634.AudioSystemSample
{
    /// <summary>
    /// 3D音源プレイヤー
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SFXPlayer : MonoBehaviour
    {
        [SerializeField] List<AudioClip> audioClipList = new List<AudioClip>();
        AudioSource audioSource;

        /// <summary>
        /// このコンポーネントをアタッチされた時に呼ばれる関数
        /// </summary>
        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0.7f;
        }

        public void PlayFirstAudioClip()
        {
            if (audioClipList.Count > 0)
            {
                audioSource.pitch = 1f;
                audioSource.Play(audioClipList[0]);
            }
        }

        public void PlaySe(string clipName)
        {
            var audioClip = audioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip)
            {
                audioSource.pitch = 1f;
                audioSource.Play(audioClip);
            }
        }

        /// <summary>
        /// pitchをランダムにして音を鳴らす（繰り返し音を鳴らすものなどに）
        /// </summary>
        public void PlaySePitchRandomize(string clipName, float range = 0.5f)
        {
            var audioClip = audioClipList.FirstOrDefault(clip => clip.name == clipName);

            if (audioClip)
            {
                audioSource.pitch = Random.Range(1f - range, 1f + range);
                audioSource.Play(audioClip);
            }
        }

    }
}