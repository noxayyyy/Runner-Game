using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    private float _volume;
    public float Volume {
        get { return _volume; }
    }
    private Button[] _buttons;
    private AudioSource[] _sources;
    [SerializeField] private AudioSource _bg, _sfx;

    void Awake() {
        if (instance && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
        SceneManager.activeSceneChanged += findButtonsInScene;
        _sources = gameObject.GetComponentsInChildren<AudioSource>();
        setAllVolume(1.0f);
    }

    void findButtonsInScene(Scene arg0, Scene arg1) {
        _buttons = FindObjectsOfType<Button>(true);
        foreach (var button in _buttons) {
            button.onClick.AddListener(() => buttonPressSound());
        }
    }

    void buttonPressSound() {
        _sfx.Play();
    }
    
    void updateAllVolumes() {
        foreach (var source in _sources) {
            source.volume = _volume;
        }
    }

    public void setAllVolume(float vol) {
        _volume = vol;
        updateAllVolumes();
    }

    public void toggleMuteAll() {
        foreach (var source in _sources) {
            source.mute = !source.mute;
        }
    }
}
