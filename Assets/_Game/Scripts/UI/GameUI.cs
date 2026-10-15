using TMPro;
using UnityEngine;
namespace Connections {
[DefaultExecutionOrder(100)]
public sealed class GameUI:MonoBehaviour {
 public GameObject pausePanel,settingsPanel,resultsPanel;
 public TMP_Text prompt,resultsText;
 public UnityEngine.UI.Image fade;
 GameObject modal;
 void Awake(){var module=UnityEngine.EventSystems.EventSystem.current?.GetComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();if(module){module.submit=null;module.cancel=null;}}
 public void ShowModal(GameObject panel){HideModal();modal=panel;panel.SetActive(true);prompt.text="";}
 public void HideModal(){if(modal)modal.SetActive(false);modal=null;}
 public void ShowResults(GameSession session){resultsPanel.SetActive(true);resultsText.text="TOTAL TIME\n"+GameSession.Format(session.TotalTime)+"\n\nELECTRICAL PANEL\n"+GameSession.Format(session.ElectricalTime)+"\n\nMORSE COMMUNICATION\n"+GameSession.Format(session.MorseTime);}
}
}