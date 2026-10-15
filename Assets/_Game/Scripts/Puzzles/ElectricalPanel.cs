using UnityEngine;
using TMPro;
namespace Connections {
public sealed class ElectricalPanel:Interactable {
 public ElectricalConnections connections;public GameObject modal;
 public UnityEngine.UI.Button[] switches;public UnityEngine.UI.Image[] lamps;
 public Renderer[] physicalLamps;public Transform[] physicalSwitches;public TMP_Text[] engravings;
 public TMP_Text status;public SoundEmitter sound;public GameSession session;
 public Color onColor=new Color(.43f,.8f,.6f),offColor=new Color(.08f,.12f,.11f);
 [SerializeField] int switchBits;public int SwitchBits=>switchBits;
 public int LampBits=>connections.Evaluate(switchBits);
 public bool Solved{get;private set;}
 void Awake(){for(int i=0;i<switches.Length;i++){int index=i;switches[i].onClick.AddListener(()=>Toggle(index));}Refresh();}
 public override void Interact(GameSession s){s.OpenModal(modal,PuzzleKind.Electrical);Refresh();}
 public void Toggle(int index){if(Solved||index<0||index>=6||session.State!=RunState.Modal)return;switchBits^=1<<index;if(sound)sound.Play(SoundCue.Click);if(LampBits==63){Solved=true;session.RestorePower();}Refresh();}
 public void Refresh(){
  int bits=LampBits;
  for(int i=0;i<6;i++){
   bool on=(bits&(1<<i))!=0,enabledSwitch=(switchBits&(1<<i))!=0;
   lamps[i].color=on?onColor:offColor;switches[i].interactable=!Solved;
   switches[i].GetComponentInChildren<TMP_Text>().text=enabledSwitch?"ON":"OFF";
   if(physicalLamps[i]){var mat=physicalLamps[i].material;mat.color=on?onColor:offColor;mat.EnableKeyword("_EMISSION");mat.SetColor("_EmissionColor",on?onColor*.8f:Color.black);}
   if(physicalSwitches[i])physicalSwitches[i].localRotation=Quaternion.Euler(enabledSwitch?-25:25,0,0);
   if(engravings[i])engravings[i].color=Solved?new Color(.7f,.72f,.67f):new Color(.14f,.15f,.13f);
  }
  status.text=Solved?"POWER RESTORED  /  CIRCUIT LOCKED":"RESTORE ALL SIX INDICATORS";
 }
}
}