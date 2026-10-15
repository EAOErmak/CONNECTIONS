using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
namespace Connections {
public sealed class MorsePuzzle:Interactable {
 public GameSession session;public GameObject modal;
 public TMP_Text question,raw,decoded,feedback,chartLabel;
 [Tooltip("Assign your own Morse chart Sprite to this Image.")]public UnityEngine.UI.Image referenceImage;
 public UnityEngine.UI.Button transmit,clear;
 public SoundEmitter sound;
 [Min(.01f)]public float dotSeconds=.10f;
 [Range(2,3)]public float responseDelay=2.5f;
 public string[] questions={"What did the first panel restore?","What can open a lock?","Confirm that you are ready to continue."};
 public string[] answers={"POWER","KEY","READY"};
 public MorseMessage message=new MorseMessage();
 public int QuestionIndex{get;private set;}public bool Busy{get;private set;}public bool Solved{get;private set;}
 int openedFrame;
 readonly List<ButtonCommand> commands=new List<ButtonCommand>();
 readonly List<RaycastResult> hits=new List<RaycastResult>();
 void Awake(){
 transmit.onClick.AddListener(Transmit);clear.onClick.AddListener(Clear);
 commands.Add(new ButtonCommand("Keyboard Dot",()=>InputSymbol('.'),"<Keyboard>/g"));
 commands.Add(new ButtonCommand("Keyboard Dash",()=>InputSymbol('-'),"<Keyboard>/j"));
 commands.Add(new ButtonCommand("Mouse Dot",()=>{if(!OverControl())InputSymbol('.');},"<Mouse>/leftButton"));
 commands.Add(new ButtonCommand("Mouse Dash",()=>{if(!OverControl())InputSymbol('-');},"<Mouse>/rightButton"));
 commands.Add(new ButtonCommand("Finish Letter",FinishLetter,"<Keyboard>/enter"));
 commands.Add(new ButtonCommand("Correct",Backspace,"<Keyboard>/backspace"));
 Refresh();
 }
 void InputSymbol(char symbol){if(Time.frameCount>openedFrame)AddSymbol(symbol);}
 void OnDestroy(){foreach(var command in commands)command.Dispose();}
 public override void Interact(GameSession s){s.OpenModal(modal,PuzzleKind.Morse);openedFrame=Time.frameCount;Refresh();}
 void LateUpdate(){if(referenceImage.sprite)chartLabel.gameObject.SetActive(false);}
 bool OverControl(){
  if(!EventSystem.current||Mouse.current==null)return false;hits.Clear();
  var e=new PointerEventData(EventSystem.current){position=Mouse.current.position.ReadValue()};EventSystem.current.RaycastAll(e,hits);
  foreach(var h in hits)if(h.gameObject.GetComponentInParent<UnityEngine.UI.Selectable>())return true;return false;
 }
 bool Editable=>!Busy&&!Solved&&session.State==RunState.Modal&&modal.activeInHierarchy;
 public void AddSymbol(char symbol){if(!Editable)return;if(!message.Add(symbol))feedback.text="LETTER LIMIT REACHED  /  ENTER OR BACKSPACE";else{feedback.text="";if(sound)sound.Play(symbol=='.'?SoundCue.Dot:SoundCue.Dash);}Refresh();}
 public void FinishLetter(){if(!Editable)return;if(!message.FinishLetter())feedback.text="UNKNOWN LETTER  /  CORRECT WITH BACKSPACE";else feedback.text="";Refresh();}
 public void Backspace(){if(!Editable)return;message.Backspace();feedback.text="";Refresh();}
 public void Clear(){if(!Editable)return;message.Clear();feedback.text="";Refresh();}
 public void Transmit(){
  if(!Editable)return;
  if(message.current.Length>0){feedback.text="FINISH THE CURRENT LETTER WITH ENTER";return;}
  if(message.letters.Count==0){feedback.text="ENTER A MESSAGE FIRST";return;}
  StartCoroutine(Transmission());
 }
 IEnumerator Transmission(){
  Busy=true;feedback.text="TRANSMITTING…";Refresh();string outgoing=message.Decoded;var codes=message.letters.ToArray();
  for(int l=0;l<codes.Length;l++){
   for(int i=0;i<codes[l].Length;i++){
    char symbol=codes[l][i];if(sound)sound.Play(symbol=='.'?SoundCue.Dot:SoundCue.Dash);
    yield return Delay(dotSeconds*(symbol=='.'?1:3));
    if(i<codes[l].Length-1)yield return Delay(dotSeconds);
   }
   if(l<codes.Length-1)yield return Delay(dotSeconds*3);
  }
  feedback.text="AWAITING RESPONSE…";yield return Delay(responseDelay);
  bool accepted=outgoing.Trim().ToUpperInvariant()==answers[QuestionIndex];
  if(sound)sound.Play(accepted?SoundCue.Accept:SoundCue.Reject);
  if(accepted){
   QuestionIndex++;feedback.text="MESSAGE ACCEPTED";message.Clear();
   if(QuestionIndex>=answers.Length){Solved=true;session.EstablishCommunication();feedback.text="COMMUNICATION ESTABLISHED";}
  }else feedback.text="INVALID MESSAGE  /  CORRECT AND RETRANSMIT";
  Busy=false;Refresh();
 }
 IEnumerator Delay(float seconds){float elapsed=0;while(elapsed<seconds){if(!session.IsPaused)elapsed+=Time.unscaledDeltaTime;yield return null;}}
 public void Refresh(){question.text=Solved?"Connection established.":$"QUESTION {QuestionIndex+1} / {answers.Length}\n{questions[Mathf.Min(QuestionIndex,questions.Length-1)]}";raw.text=message.Raw.Length>0?message.Raw:"· · ·";decoded.text=message.Decoded.Length>0?message.Decoded:"—";transmit.interactable=clear.interactable=!Busy&&!Solved;}
}
}