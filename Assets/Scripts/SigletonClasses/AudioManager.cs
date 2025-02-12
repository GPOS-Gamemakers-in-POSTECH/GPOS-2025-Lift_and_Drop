using SingletonGameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonAudioManager
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance { get { return instance; } }

        [Header("#BGM_normal")]
        public static AudioSource bgmSource;
        float bgmVolume = 1;
        public AudioClip bgmClip;

        [Header("#BGM_stealth")]
        public static AudioSource bgmSource2;
        float bgmVolume2 = 0;
        public AudioClip bgmClip2;

        [Header("#BGM_invincible")]
        public static AudioSource bgmSource3;
        float bgmVolume3 = 0;
        public AudioClip bgmClip3;

        [Header("#SFX")]
        public static AudioSource[] sfxSources; 
        public AudioClip[] sfxClips;
        public float sfxVolume;
        public int channels;
        int channelIndex;

        public static bool isMoving = false;
        public static bool isMotorStartPlayed = false;
        public static Coroutine exitCoroutine;
        public static bool isMovingKeyPressed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                Init();
                AudioManager.Instance.playBgm();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Init()
        {
            if (bgmSource == null)
            {
                GameObject bgmObject = new GameObject("BgmPlayer");
                bgmSource = bgmObject.AddComponent<AudioSource>();
                bgmSource.volume = bgmVolume;
                bgmSource.clip = bgmClip;
                bgmSource.loop = true;
                bgmSource.playOnAwake = false;
            }
            DontDestroyOnLoad(bgmSource);

            if (bgmSource2 == null)
            {
                GameObject bgmObject2 = new GameObject("BgmPlayer2");
                bgmSource2 = bgmObject2.AddComponent<AudioSource>();
                bgmSource2.volume = bgmVolume2;
                bgmSource2.clip = bgmClip2;
                bgmSource2.loop = true;
                bgmSource2.playOnAwake = false;
            }
            DontDestroyOnLoad(bgmSource2);

            if (bgmSource3 == null)
            {
                GameObject bgmObject3 = new GameObject("BgmPlayer3");
                bgmSource3 = bgmObject3.AddComponent<AudioSource>();
                bgmSource3.volume = bgmVolume3;
                bgmSource3.clip = bgmClip3;
                bgmSource3.loop = true;
                bgmSource3.playOnAwake = false;
            }
            DontDestroyOnLoad(bgmSource3);

            if(sfxSources == null)
            {
                GameObject sfxObject = new GameObject("SfxPlayer");
                sfxObject.transform.parent= transform;
                sfxSources = new AudioSource[channels];

                for(int index = 0; index < sfxSources.Length; index++)
                {
                    sfxSources[index] = sfxObject.AddComponent<AudioSource>();
                    sfxSources[index].playOnAwake= false;
                    sfxSources[index].volume = sfxVolume;
                }
            }
        }
        public void playBgm()
        {
            bgmSource.Play();
            bgmSource2.Play();
            bgmSource3.Play();
        }

        public void playOnlyNormal()
        {
            bgmSource.volume = 1;
            bgmSource2.volume = 0;
            bgmSource3.volume = 0;
        }
        public void playOnlyStealth()
        {
            bgmSource.volume = 0;
            bgmSource2.volume = 1;
            bgmSource3.volume = 0;
        }
        public void playOnlyInvincible()
        {
            bgmSource.volume = 0;
            bgmSource2.volume = 0;
            bgmSource3.volume = 1;
        }
        public void StopBgm()
        {
            bgmSource.Stop();
            bgmSource2.Stop();
            bgmSource3.Stop();
        }

        public void playSfx(Sfx sfx)
        {
            for(int index=0; index<sfxSources.Length; index++)
            {
                int loopIndex = (index + channelIndex) % sfxSources.Length;

                if (sfxSources[loopIndex].isPlaying)
                {
                    continue;
                }
                channelIndex = loopIndex;
                sfxSources[loopIndex].clip = sfxClips[(int)sfx];
                sfxSources[loopIndex].Play();
                break;
            }
        }
        public void StartMotorSound()
        {
            isMoving = true;

            if(!isMotorStartPlayed)
            {
                isMotorStartPlayed = true;
                sfxSources[0].clip = sfxClips[(int)Sfx.motor_intro];
                sfxSources[0].Play();
                Invoke(nameof(PlayMotorLoop), sfxClips[0].length);
            }
            else
            {
                PlayMotorLoop();
            }
        }
        private void PlayMotorLoop()
        {
            if(isMoving)
            {
                sfxSources[1].clip = sfxClips[(int)Sfx.motor_loop];
                sfxSources[1].loop = true;
                sfxSources[1].Play();
            }
        }
        public IEnumerator WaitAndPlayExitSound()
        {
            yield return new WaitForSeconds(0.05f);
            if(!isMovingKeyPressed)
            {
                StopMotorSound();
            }
            exitCoroutine = null;
        }
        private void StopMotorSound()
        {
            isMoving = false;
            sfxSources[1].loop = false;
            sfxSources[1].Stop();
            sfxSources[0].clip = sfxClips[(int)Sfx.motor_exit];
            sfxSources[0].Play();
            isMotorStartPlayed = false;
        }
        public void StartClearSound()
        {
            sfxSources[2].clip = sfxClips[(int)Sfx.bonus];
            sfxSources[2].loop = false;
            sfxSources[2].Play();
        }
    }
}
