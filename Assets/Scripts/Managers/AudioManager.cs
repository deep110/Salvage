using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip audioClip;

    public AudioMixerGroup output;

    [Range(0, 1f)]
    public float volume = 0.7f;

    [Range(0.5f, 1f)]
    public float pitch = 1f;

    public bool loop;

    public bool isSFX;

    private AudioSource audioSource;

    public void SetSource(AudioSource source) {
        audioSource = source;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
        audioSource.outputAudioMixerGroup = output;
    }

    public void Play() {
        audioSource.Play();
    }

    public void Stop() {
        audioSource.Stop();
    }
}

public class AudioManager : PersistentSingleton<AudioManager> {

    [SerializeField]
    private Sound[] sounds;

    private DataManager dataManager;

    protected override void Awake() {
        base.Awake();
        for (int i = 0; i < sounds.Length; i++) {
            var _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        dataManager = DataManager.Instance;
    }

    public void PlaySound(string name, bool force = false) {
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == name) {
                if (force) {
                    sounds[i].Play();
                } else {
                    if (sounds[i].isSFX) {
                        if (isSFXActive())
                            sounds[i].Play();
                    } else {
                        if (isMusicActive())
                            sounds[i].Play();
                    }
                }
                return;
            }
        }
    }

    public void StopSound(string name) {
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == name) {
                sounds[i].Stop();
                return;
            }
        }
    }

    private bool isSFXActive() {
        return dataManager.GetSettingsData().isVfxOn;
    }

    private bool isMusicActive() {
        return dataManager.GetSettingsData().isSoundOn;
    }
}
