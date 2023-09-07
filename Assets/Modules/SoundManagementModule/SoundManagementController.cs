using UnityEngine;

namespace Modules.SoundManagementModule {
    class SoundManagementController: MonoBehaviour, SoundManagementControllerInterface {
        [Range(0, 100)]
        public int SFXVolume = 100;

        [Range(0, 100)]
        public int BackgroundMusicVolume = 100;

        private AudioListener audioListener;
        private AudioSource bgMusic;

        private SoundManagementController AudioListener() {
            if (audioListener == null) {
                audioListener = gameObject.GetComponent<AudioListener>();
            }
            return this;
        }

        private SoundManagementController Music() {
            if (bgMusic == null) {
                bgMusic = gameObject.GetComponent<AudioSource>();
            }


            return this;
        }

        public void PlayBackgroundMusic(string path) {
            SetClip(path).bgMusic.Play();
            Debug.Log("--SoundManagementController: PlayBackgroundMusic");
        }

        public void PlaySFX(string path) {
            Debug.Log("--SoundManagementController: PlaySFX");
        }

        private SoundManagementController SetClip(string path) {
            AudioClip clip = Resources.Load<AudioClip>(path);
            AudioListener().Music().bgMusic.clip = clip;
            return this;
        }
        // private SoundManagementController FadeToMusic(float volume, float duration) {
        //     AudioListener().Music().bgMusic.volume = volume;
        //     return this;
        // }

        public void SetBGMusicVolume(int volume) {
            float vol = (float)volume / 100f;
            AudioListener().Music().bgMusic.volume = vol;
            Debug.Log("--SoundManagementController: SetBGMusicVolume to " + volume);
        }
    }
}