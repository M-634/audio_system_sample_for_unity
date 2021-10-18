using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M_634.Audio
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
                if(instance == null)
                {
                    instance = FindObjectOfType<AudioManager>();
                    if(instance == null)
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
        #endregion



        private void Start()
        {
          
        }

        public void Play()
        {
            Debug.Log("a");
        }
    }
}
