using UnityEngine;
namespace Connections {
public sealed class SettingsPanel:MonoBehaviour {
 public UnityEngine.UI.Slider volume,sensitivity;
 void Awake(){volume.SetValueWithoutNotify(PlayerPrefs.GetFloat("Volume",.65f));sensitivity.SetValueWithoutNotify(PlayerPrefs.GetFloat("Sensitivity",1));volume.onValueChanged.AddListener(SetVolume);sensitivity.onValueChanged.AddListener(SetSensitivity);}
 public void SetVolume(float value){AudioListener.volume=value;PlayerPrefs.SetFloat("Volume",value);}
 public void SetSensitivity(float value){PlayerPrefs.SetFloat("Sensitivity",value);}
 void OnDisable(){PlayerPrefs.Save();}
}
}