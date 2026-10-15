using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Connections;
namespace Connections.Editor {
public static partial class DemoBuilder {
 public static Material Material(string name,Color color,float metallic=0,float smooth=.2f){string path=Root+"Materials/"+name+".mat";var existing=AssetDatabase.LoadAssetAtPath<Material>(path);if(existing)return existing;var m=new Material(Shader.Find("Universal Render Pipeline/Lit"));m.name=name;m.color=color;m.SetFloat("_Metallic",metallic);m.SetFloat("_Smoothness",smooth);AssetDatabase.CreateAsset(m,path);return m;}
 public static SoundEmitter Sound(GameObject g,float volume=.3f,bool spatial=true){var s=g.AddComponent<SoundEmitter>();s.volume=volume;s.spatialBlend=spatial?1:0;return s;}
 public static TMP_Text WorldText(string name,Transform parent,string text,Vector3 pos,float size=1){var g=Node(name,parent);g.transform.localPosition=pos;var t=g.AddComponent<TextMeshPro>();t.font=Font;t.text=text;t.fontSize=size;t.alignment=TMPro.TextAlignmentOptions.Center;t.rectTransform.sizeDelta=new Vector2(2,.4f);t.color=Ink;return t;}
 public static GameObject Lamp(string name,Transform parent,Vector3 pos,Color color,float intensity,float range=7){var g=Node(name,parent);g.transform.localPosition=pos;var l=g.AddComponent<Light>();l.type=LightType.Point;l.color=color;l.intensity=intensity;l.range=range;l.shadows=LightShadows.Soft;Sound(g,.045f).loopOnEnable=true;var bulb=GameObject.CreatePrimitive(PrimitiveType.Sphere);bulb.name="Bulb";bulb.transform.SetParent(g.transform,false);bulb.transform.localScale=Vector3.one*.14f;Object.DestroyImmediate(bulb.GetComponent<Collider>());var mat=Material("Warm Glass",new Color(.7f,.59f,.4f));mat.EnableKeyword("_EMISSION");mat.SetColor("_EmissionColor",new Color(1,.7f,.36f)*.4f);bulb.GetComponent<Renderer>().sharedMaterial=mat;Cube("Socket",g.transform,new Vector3(0,.13f,0),new Vector3(.18f,.17f,.18f),Material("Charcoal Metal",new Color(.055f,.065f,.064f),.65f));return g;}
 public static FacilityDoor Door(string name,Vector3 pos,Transform parent,Material metal){var root=Node(name,parent);root.transform.localPosition=pos;var d=root.AddComponent<FacilityDoor>();d.leaf=Cube("Sliding Leaf",root.transform,new Vector3(0,1.25f,0),new Vector3(1.65f,2.5f,.18f),metal).transform;d.openOffset=new Vector3(-1.85f,0,0);d.sound=Sound(root,.24f);Cube("Lintel",root.transform,new Vector3(0,2.7f,0),new Vector3(2.1f,.3f,.4f),metal);for(int i=-1;i<=1;i+=2)Cube("Frame",root.transform,new Vector3(i*.94f,1.25f,0),new Vector3(.16f,2.5f,.4f),metal);var label=WorldText("Door Identifier",d.leaf,name,new Vector3(0,.3f,-.11f),.65f);return d;}
 public static void RoomOne(){
  EditorSceneManager.OpenScene(Root+"Scenes/Gameplay.unity");if(GameObject.Find("Room 1"))throw new System.Exception("Room already built.");
  var session=Object.FindAnyObjectByType<GameSession>();var ui=session.ui;
  var concrete=Material("Damp Concrete",new Color(.23f,.26f,.25f));var tile=Material("Worn Ceramic",new Color(.34f,.37f,.35f),0,.32f);var metal=Material("Worn Steel",new Color(.14f,.18f,.18f),.75f,.3f);
  var room=Node("Room 1");var floor=GameObject.Find("Foundation Floor");floor.name="Room 1 Floor";floor.transform.SetParent(room.transform);floor.GetComponent<Renderer>().sharedMaterial=tile;
  Cube("West Wall",room.transform,new Vector3(-4,1.65f,0),new Vector3(.25f,3.3f,8.3f),concrete);Cube("East Wall",room.transform,new Vector3(4,1.65f,0),new Vector3(.25f,3.3f,8.3f),concrete);Cube("South Wall",room.transform,new Vector3(0,1.65f,-4),new Vector3(8.3f,3.3f,.25f),concrete);Cube("Ceiling",room.transform,new Vector3(0,3.4f,0),new Vector3(8.3f,.25f,8.3f),concrete);
  Cube("Partition Left",room.transform,new Vector3(-1.375f,1.65f,4.15f),new Vector3(5.25f,3.3f,.3f),concrete);Cube("Partition Right",room.transform,new Vector3(3.525f,1.65f,4.15f),new Vector3(.95f,3.3f,.3f),concrete);
  session.roomDoor=Door("02",new Vector3(2.2f,0,4.15f),room.transform,metal);
  var start=Lamp("Intro Warm Bulb",room.transform,new Vector3(0,2.6f,-1),new Color(1,.68f,.35f),1.15f,4.5f);session.introObjects=new[]{start};
  var lights=Node("Restored Lighting",room.transform);Lamp("West Practical",lights.transform,new Vector3(-2.7f,2.9f,1.9f),new Color(.55f,.7f,.72f),2.8f,7);Lamp("Panel Practical",lights.transform,new Vector3(1.5f,2.85f,-1.8f),new Color(.95f,.78f,.56f),2.6f,7);lights.SetActive(false);session.restoredObjects=new[]{lights};
  var panel=Node("Electrical Panel",room.transform);panel.transform.localPosition=new Vector3(0,1.35f,-.4f);var puzzle=panel.AddComponent<ElectricalPanel>();puzzle.session=session;puzzle.sound=Sound(panel,.28f);session.feedback=Sound(session.gameObject,.25f,false);
  Cube("Housing",panel.transform,Vector3.zero,new Vector3(1.95f,.85f,.25f),metal);Cube("Stand",panel.transform,new Vector3(0,-.9f,.13f),new Vector3(.45f,1.1f,.4f),metal);
  puzzle.connections=ScriptableObject.CreateInstance<ElectricalConnections>();AssetDatabase.CreateAsset(puzzle.connections,Root+"ScriptableObjects/Electrical Connections.asset");
  puzzle.physicalLamps=new Renderer[6];puzzle.physicalSwitches=new Transform[6];puzzle.engravings=new TMPro.TMP_Text[6];
  string[] digits={"5","0","1","2","4","7"};
  for(int i=0;i<6;i++){float x=-.75f+i*.3f;puzzle.engravings[i]=WorldText("Engraving "+digits[i],panel.transform,digits[i],new Vector3(x,.28f,-.135f),.75f);
   var lamp=GameObject.CreatePrimitive(PrimitiveType.Sphere);lamp.name="Indicator "+(i+1);lamp.transform.SetParent(panel.transform,false);lamp.transform.localPosition=new Vector3(x,.04f,-.15f);lamp.transform.localScale=new Vector3(.11f,.11f,.07f);lamp.GetComponent<Renderer>().sharedMaterial=Material("Indicator Off",new Color(.05f,.08f,.065f));Object.DestroyImmediate(lamp.GetComponent<Collider>());puzzle.physicalLamps[i]=lamp.GetComponent<Renderer>();
   var pivot=Node("Switch "+(i+1),panel.transform);pivot.transform.localPosition=new Vector3(x,-.19f,-.17f);Cube("Lever",pivot.transform,new Vector3(0,.07f,-.035f),new Vector3(.045f,.2f,.07f),metal);puzzle.physicalSwitches[i]=pivot.transform;
  }
  puzzle.modal=Panel("Electrical Modal",ui.transform,Vector2.zero,new Vector2(1160,650),new Color(.035f,.045f,.047f,.99f));
  Text("Panel Title",puzzle.modal.transform,"ELECTRICAL DISTRIBUTION",new Vector2(0,245),new Vector2(950,65),34).characterSpacing=4;
  Text("Serial",puzzle.modal.transform,"UNIT 01     /     SIX CHANNEL RELAY",new Vector2(0,183),new Vector2(900,45),17).color=new Color(.46f,.5f,.47f);
  puzzle.status=Text("Status",puzzle.modal.transform,"RESTORE ALL SIX INDICATORS",new Vector2(0,-174),new Vector2(1000,45),23);
  puzzle.switches=new UnityEngine.UI.Button[6];puzzle.lamps=new UnityEngine.UI.Image[6];
  for(int i=0;i<6;i++){float x=-430+i*172;Text("Engraved "+digits[i],puzzle.modal.transform,digits[i],new Vector2(x,118),new Vector2(90,50),31);puzzle.lamps[i]=Panel("Indicator "+(i+1),puzzle.modal.transform,new Vector2(x,40),new Vector2(58,38),new Color(.07f,.12f,.1f)).GetComponent<UnityEngine.UI.Image>();puzzle.switches[i]=Button("Switch "+(i+1),puzzle.modal.transform,"OFF",new Vector2(x,-65),new Vector2(106,94));}
  Button("Close",puzzle.modal.transform,"CLOSE  /  ESC",new Vector2(0,-263),new Vector2(260,50),session.CloseModal);puzzle.modal.SetActive(false);
  puzzle.Refresh();EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());EditorSceneManager.SaveScene(SceneManager.GetActiveScene());AssetDatabase.SaveAssets();
 }
}
}