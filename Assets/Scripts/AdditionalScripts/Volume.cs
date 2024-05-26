using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour {
    private const string _label = "Volume: ";
    private Slider _volSlider;
    private Toggle _volToggle;
    private Text _volText;

    void Awake() {
        _volSlider = gameObject.GetComponentInChildren<Slider>();
        _volText = _volSlider.GetComponentInChildren<Text>();
        _volToggle = gameObject.GetComponentInChildren<Toggle>();
    }

    void Start() {
        _volSlider.value = AudioManager.instance.Volume * 100.0f;
        _volText.text = _label + _volSlider.value;

        _volSlider.onValueChanged.AddListener(delegate { 
            valueChangeCheck(); 
        });
        _volToggle.onValueChanged.AddListener(delegate {
            toggleMute();
        });
    }

    void toggleMute() {
        AudioManager.instance.toggleMuteAll();
    }

    void valueChangeCheck() {
        _volText.text = _label + _volSlider.value;
        AudioManager.instance.setAllVolume(_volSlider.value / 100.0f );
    }

}
