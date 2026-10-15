using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
namespace Connections {
public enum RunState { Intro, Playing, Modal, Paused, Ending, Results }
public enum PuzzleKind { Electrical, Morse }
public sealed class GameSession : MonoBehaviour {
 public static GameSession Current { get; private set; }
 public GameUI ui;
 public GameObject[] introObjects = new GameObject[0];
 [Min(0)] public float introDelay = 2;
 [Min(.1f)] public float fadeDuration = 1.8f;
 public RunState State { get; private set; }
 public double TotalTime {get;private set;}
 public double ElectricalTime {get;private set;}
 public double MorseTime {get;private set;}
 public bool PowerRestored {get;private set;}
 public bool CommunicationEstablished {get;private set;}
 public bool ElectricalStarted {get;private set;}
 public bool MorseStarted {get;private set;}
 public int ResumeFrame {get;private set;}
 public bool CanMove => State == RunState.Playing && Time.frameCount > ResumeFrame;
 public bool IsPaused => State == RunState.Paused;
 public GameObject[] restoredObjects = new GameObject[0];
 public FacilityDoor roomDoor, exitDoor;
 public SoundEmitter feedback;
 RunState beforePause;
 ButtonCommand escape;
 void Awake(){Current=this;Application.runInBackground=true;Time.timeScale=1; State=RunState.Intro; AudioListener.volume=PlayerPrefs.GetFloat("Volume",.65f);escape=new ButtonCommand("Escape",Escape,"<Keyboard>/escape");}
 IEnumerator Start(){foreach(var g in introObjects) if(g)g.SetActive(false); SetCursor(false); yield return new WaitForSecondsRealtime(introDelay);foreach(var g in introObjects)if(g)g.SetActive(true); if(feedback)feedback.Play(SoundCue.Relay); State=RunState.Playing;ResumeFrame=Time.frameCount;}
 void Update(){
  if(State!=RunState.Paused && State!=RunState.Ending && State!=RunState.Results){
   double d=Time.unscaledDeltaTime;TotalTime+=d;
   if(ElectricalStarted&&!PowerRestored)ElectricalTime+=d;
   if(MorseStarted&&!CommunicationEstablished)MorseTime+=d;
  }
 }
 public void Escape(){if(State==RunState.Modal)CloseModal();else if(State==RunState.Playing)Pause();else if(State==RunState.Paused)Resume();}
 public void OpenModal(GameObject panel,PuzzleKind kind){
  if(!CanMove)return;
  if(kind==PuzzleKind.Electrical)ElectricalStarted=true;else MorseStarted=true;
  State=RunState.Modal;ui.ShowModal(panel);SetCursor(true);
 }
 public void CloseModal(){if(State!=RunState.Modal)return;ui.HideModal();State=RunState.Playing;ResumeFrame=Time.frameCount+1;SetCursor(false);}
 public void Pause(){if(State!=RunState.Playing)return;beforePause=State;State=RunState.Paused;Time.timeScale=0;ui.pausePanel.SetActive(true);SetCursor(true);}
 public void Resume(){if(State!=RunState.Paused)return;ui.pausePanel.SetActive(false);ui.settingsPanel.SetActive(false);Time.timeScale=1;State=beforePause;ResumeFrame=Time.frameCount+1;SetCursor(false);}
 public void RestorePower(){if(PowerRestored)return;PowerRestored=true;foreach(var g in restoredObjects)if(g)g.SetActive(true);if(roomDoor)roomDoor.Open();if(feedback)feedback.Play(SoundCue.Power);}
 public void EstablishCommunication(){if(CommunicationEstablished)return;CommunicationEstablished=true;if(exitDoor)exitDoor.Open();if(feedback)feedback.Play(SoundCue.Accept);}
 public void Finish(){if(!CommunicationEstablished||State==RunState.Ending||State==RunState.Results)return;State=RunState.Ending;ui.HideModal();ui.prompt.text="";SetCursor(true);StartCoroutine(Ending());}
 IEnumerator Ending(){ui.fade.gameObject.SetActive(true);float t=0;while(t<fadeDuration){t+=Time.unscaledDeltaTime;ui.fade.color=new Color(0,0,0,Mathf.Clamp01(t/fadeDuration));yield return null;}State=RunState.Results;ui.ShowResults(this);}
 public void ReturnToMenu(){Time.timeScale=1;SetCursor(true);SceneManager.LoadScene("MainMenu");}
 public static void SetCursor(bool free){Cursor.lockState=free?CursorLockMode.None:CursorLockMode.Locked;Cursor.visible=free;}
 public static string Format(double seconds){var t=System.TimeSpan.FromSeconds(seconds);return t.TotalHours>=1?$"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}":$"{(int)t.TotalMinutes:00}:{t.Seconds:00}";}
 void OnDestroy(){escape?.Dispose();if(Current==this)Current=null;Time.timeScale=1;SetCursor(true);}
}
}