using UnityEngine;
using UnityEngine.EventSystems;
namespace Connections {
public sealed class ButtonAudio:MonoBehaviour,IPointerClickHandler {
 public SoundEmitter sound;
 public void OnPointerClick(PointerEventData e){var b=GetComponent<UnityEngine.UI.Button>();if(b&&b.interactable&&e.button==PointerEventData.InputButton.Left&&sound)sound.Play(SoundCue.Click);}
}
}