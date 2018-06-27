using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;
    public AudioClip audioClip;

    [Range(0, 1f)]
    public float volume = 0.7f;

    [Range(0.5f, 1f)]
    public float pitch = 1f;

    public bool loop;

    private AudioSource audioSource;

    public void SetSource(AudioSource source) {
        audioSource = source;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
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

    protected override void Awake() {
        base.Awake();
        for (int i = 0; i < sounds.Length; i++) {
            var _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name) {
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == name) {
                sounds[i].Play();
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
}
