using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
namespace Connections.Editor {
public static partial class DemoBuilder {
 public static void RoomTwo(){
  EditorSceneManager.OpenScene(Root+"Scenes/Gameplay.unity");if(GameObject.Find("Room 2"))throw new System.Exception("Room already built.");
  var session=Object.FindAnyObjectByType<GameSession>();var ui=session.ui;var room=Node("Room 2");
  var concrete=Material("Damp Concrete",Color.gray);var tile=Material("Worn Ceramic",Color.gray);var steel=Material("Worn Steel",Color.gray);
  Cube("Floor",room.transform,new Vector3(0,-.15f,8.3f),new Vector3(8,.3f,8.3f),tile);
  Cube("West Wall",room.transform,new Vector3(-4,1.65f,8.3f),new Vector3(.25f,3.3f,8.3f),concrete);Cube("East Wall",room.transform,new Vector3(4,1.65f,8.3f),new Vector3(.25f,3.3f,8.3f),concrete);Cube("Ceiling",room.transform,new Vector3(0,3.4f,8.3f),new Vector3(8.3f,.25f,8.3f),concrete);
  Cube("Exit Left Wall",room.transform,new Vector3(-1.375f,1.65f,12.45f),new Vector3(5.25f,3.3f,.3f),concrete);Cube("Exit Right Wall",room.transform,new Vector3(3.525f,1.65f,12.45f),new Vector3(.95f,3.3f,.3f),concrete);
  session.exitDoor=Door("EXIT",new Vector3(2.2f,0,12.45f),room.transform,steel);
  var tunnel=Node("Dark Exit",room.transform);var black=Material("Exit Black",Color.black);
  Cube("Exit Floor",tunnel.transform,new Vector3(2.2f,-.15f,14.3f),new Vector3(2.2f,.3f,4),black);Cube("Exit West",tunnel.transform,new Vector3(1.2f,1.6f,14.3f),new Vector3(.2f,3.2f,4),black);Cube("Exit East",tunnel.transform,new Vector3(3.2f,1.6f,14.3f),new Vector3(.2f,3.2f,4),black);Cube("Exit Ceiling",tunnel.transform,new Vector3(2.2f,3.2f,14.3f),new Vector3(2.2f,.2f,4),black);Cube("Black End",tunnel.transform,new Vector3(2.2f,1.6f,16.3f),new Vector3(2.2f,3.2f,.2f),black);
  var threshold=Node("Ending Threshold",tunnel.transform);threshold.transform.position=new Vector3(2.2f,1.2f,13.4f);var trigger=threshold.AddComponent<BoxCollider>();trigger.size=new Vector3(1.6f,2.4f,.6f);trigger.isTrigger=true;threshold.AddComponent<ExitThreshold>().session=session;
  var lighting=Node("Restored Lighting",room.transform);Lamp("Communication Practical",lighting.transform,new Vector3(-1.5f,2.85f,8.1f),new Color(.86f,.78f,.61f),3.5f,7);Lamp("Cold Practical",lighting.transform,new Vector3(2.9f,2.85f,10.3f),new Color(.5f,.67f,.7f),2.9f,6);lighting.SetActive(false);var restored=new System.Collections.Generic.List<GameObject>(session.restoredObjects){lighting};session.restoredObjects=restored.ToArray();
  var apparatus=Node("Morse Apparatus",room.transform);apparatus.transform.position=new Vector3(-1.7f,1.05f,8.2f);
  Cube("Steel Table",apparatus.transform,new Vector3(0,-.18f,.1f),new Vector3(2.1f,.15f,1),steel);for(int side=-1;side<=1;side+=2)Cube("Table Leg",apparatus.transform,new Vector3(side*.8f,-.65f,.1f),new Vector3(.12f,.95f,.6f),steel);
  Cube("Transceiver",apparatus.transform,new Vector3(0,.22f,.24f),new Vector3(1.5f,.65f,.55f),steel);
  Cube("Telegraph Base",apparatus.transform,new Vector3(.6f,-.02f,-.35f),new Vector3(.4f,.08f,.32f),steel);Cube("Telegraph Key",apparatus.transform,new Vector3(.6f,.07f,-.35f),new Vector3(.06f,.1f,.32f),steel);
  WorldText("Face Label",apparatus.transform,"LINE TERMINAL  /  02",new Vector3(0,.42f,-.045f),.8f);
  for(int i=0;i<3;i++){var dial=GameObject.CreatePrimitive(PrimitiveType.Cylinder);dial.name="Tuning Dial";dial.transform.SetParent(apparatus.transform,false);dial.transform.localPosition=new Vector3(-.45f+i*.4f,.12f,-.065f);dial.transform.localScale=new Vector3(.15f,.05f,.15f);dial.transform.localRotation=Quaternion.Euler(90,0,0);dial.GetComponent<Renderer>().sharedMaterial=steel;}
  var p=apparatus.AddComponent<MorsePuzzle>();p.session=session;p.sound=Sound(apparatus,.2f);
  p.modal=Panel("Morse Modal",ui.transform,Vector2.zero,new Vector2(1280,720),new Color(.035f,.045f,.045f,.99f));
  Text("Title",p.modal.transform,"COMMUNICATION TERMINAL",new Vector2(0,295),new Vector2(1150,60),32).characterSpacing=4;
  p.question=Text("Question",p.modal.transform,"",new Vector2(-225,188),new Vector2(730,110),25);
  Text("Raw Heading",p.modal.transform,"MORSE INPUT",new Vector2(-225,91),new Vector2(700,35),16);
  p.raw=Text("Raw Symbols",p.modal.transform,"",new Vector2(-225,28),new Vector2(720,85),29);
  Text("Decoded Heading",p.modal.transform,"DECODED MESSAGE",new Vector2(-225,-44),new Vector2(700,35),16);
  p.decoded=Text("Decoded Text",p.modal.transform,"",new Vector2(-225,-98),new Vector2(720,65),34);
  var chart=Panel("Morse Reference Image",p.modal.transform,new Vector2(388,60),new Vector2(365,345),new Color(.09f,.12f,.12f));p.referenceImage=chart.GetComponent<UnityEngine.UI.Image>();p.referenceImage.preserveAspect=true;p.chartLabel=Text("Chart Placeholder",chart.transform,"MORSE CHART\nPLACEHOLDER",Vector2.zero,new Vector2(340,220),24);
  p.feedback=Text("Response",p.modal.transform,"",new Vector2(0,-180),new Vector2(1180,45),21);
  Text("Hints",p.modal.transform,"LMB / G  DOT     RMB / J  DASH     ENTER  FINISH LETTER\nBACKSPACE  CORRECT     ESC  CLOSE",new Vector2(0,-300),new Vector2(1100,75),18);
  p.transmit=Button("Transmit",p.modal.transform,"TRANSMIT",new Vector2(-250,-240),new Vector2(280,55));
  p.clear=Button("Clear",p.modal.transform,"CLEAR",new Vector2(65,-240),new Vector2(240,55));
  Button("Close",p.modal.transform,"CLOSE",new Vector2(365,-240),new Vector2(240,55),session.CloseModal);
  p.Refresh();p.modal.SetActive(false);p.modal.transform.SetSiblingIndex(ui.pausePanel.transform.GetSiblingIndex());var electrical=Object.FindAnyObjectByType<ElectricalPanel>();electrical.modal.transform.SetSiblingIndex(ui.pausePanel.transform.GetSiblingIndex());
  EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());EditorSceneManager.SaveScene(SceneManager.GetActiveScene());AssetDatabase.SaveAssets();
 }
}
}