using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace M_634.AudioSystemSample
{
    public static class AudioSourceExtensions
    {
        /// <summary>
        /// Null�`�F�b�N�Ɖ��ʒ��������ĉ����Đ�����g�����\�b�h
        /// </summary>
        public static void Play(this AudioSource audioSource, AudioClip audioClip = null, float volume = 1f)
        {
            if (audioClip)
            {
                audioSource.clip = audioClip;
                //�{�����[�����K�؂ɂȂ�悤�ɒ�������
                audioSource.volume = Mathf.Clamp01(volume);

                audioSource.Play();
            }
        }


        public static void PlayWithFadeIn(this AudioSource audioSource, AudioClip audioClip = null, float fadeTime = 0.1f, float endVolume = 1.0f)
        {
            //�ڕW�{�����[����0~1�ɕ␳
            float targetVolume = Mathf.Clamp01(endVolume);

            //�t�F�[�h���Ԃ���������������␳
            fadeTime = fadeTime < 0.1f ? 0.1f : fadeTime;

            //����0�ōĐ��J�n
            audioSource.Play(audioClip, 0f);

            //DOTween���g���ĖڕW�{�����[���܂�Fade
            audioSource.DOFade(targetVolume, fadeTime);
        }

        public static void StopWithFadeOut(this AudioSource audioSource, float fadeTime = 0.1f)
        {

            //�t�F�[�h���Ԃ���������������␳
            fadeTime = fadeTime < 0.1f ? 0.1f : fadeTime;

            audioSource.DOFade(0f, fadeTime);
            audioSource.Stop();
            audioSource.clip = null;
        }

        /// <summary>
        /// �Đ��ʒu�������_���ɂ���g�����\�b�h�i�����ȂǂɁj
        /// </summary>
        public static void PlayRandomStart(this AudioSource audioSource, AudioClip audioClip, float volume = 1f)
        {
            if (audioClip == null) return;

            audioSource.clip = audioClip;
            audioSource.volume = Mathf.Clamp01(volume);

            //���ʂ�length�Ɠ��l�ɂȂ�ƃV�[�N�G���[���N�������� -0.01�b����
            audioSource.time = Random.Range(0f, audioClip.length - 0.01f);

            PlayWithFadeIn(audioSource, audioClip, volume);
        }
    }
}