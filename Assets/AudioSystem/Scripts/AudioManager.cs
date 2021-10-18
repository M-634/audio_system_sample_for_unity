using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
