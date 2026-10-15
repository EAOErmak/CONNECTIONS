using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace Connections {
public sealed class MenuController:MonoBehaviour {
 public GameObject mainPanel,settingsPanel,creditsPanel;
 [TextArea]public string credits="CONNECTIONS\n\nA psychological suspense puzzle demo.\n\nDesign and development\nProject team\n\nProcedural prototype art and audio.";
 public TMPro.TMP_Text creditsText;
 [Min(0)]public float startTransitionDelay=.12f;bool starting;
 public void StartGame(){if(starting)return;starting=true;StartCoroutine(StartRun());}
 IEnumerator StartRun(){yield return new WaitForSecondsRealtime(startTransitionDelay);SceneManager.LoadScene("Gameplay");}
 public void Settings(){mainPanel.SetActive(false);settingsPanel.SetActive(true);}
 public void Credits(){creditsText.text=credits;mainPanel.SetActive(false);creditsPanel.SetActive(true);}
 public void Back(){settingsPanel.SetActive(false);creditsPanel.SetActive(false);mainPanel.SetActive(true);}
 public void Exit(){
 #if UNITY_EDITOR
 UnityEditor.EditorApplication.isPlaying=false;
 #else
 Application.Quit();
 #endif
 }
 void Awake(){Time.timeScale=1;GameSession.SetCursor(true);AudioListener.volume=PlayerPrefs.GetFloat("Volume",.65f);}
}
}