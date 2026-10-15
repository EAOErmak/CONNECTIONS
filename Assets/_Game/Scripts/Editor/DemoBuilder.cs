using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using TMPro;
using Connections;
namespace Connections.Editor {
public static partial class DemoBuilder {
 public const string Root="Assets/_Game/";
 static TMP_FontAsset Font=>AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset");
 static Color Ink=new Color(.76f,.79f,.77f);
 public static void Folders(){foreach(var s in new[]{"Scenes","Prefabs","Materials","Textures","Audio","ScriptableObjects","Documentation"})System.IO.Directory.CreateDirectory(Root+s);}
 public static GameObject Node(string name,Transform parent=null){var g=new GameObject(name);if(parent)g.transform.SetParent(parent,false);return g;}
 public static GameObject Cube(string name,Transform parent,Vector3 pos,Vector3 scale,Material mat=null){var g=GameObject.CreatePrimitive(PrimitiveType.Cube);g.name=name;if(parent)g.transform.SetParent(parent,false);g.transform.localPosition=pos;g.transform.localScale=scale;if(mat)g.GetComponent<Renderer>().sharedMaterial=mat;return g;}
 public static RectTransform Rect(string name,Transform parent,Vector2 center,Vector2 size){var g=new GameObject(name,typeof(RectTransform));g.transform.SetParent(parent,false);var r=g.GetComponent<RectTransform>();r.anchorMin=r.anchorMax=new Vector2(.5f,.5f);r.anchoredPosition=center;r.sizeDelta=size;return r;}
 public static GameObject Panel(string name,Transform parent,Vector2 center,Vector2 size,Color color){var r=Rect(name,parent,center,size);var im=r.gameObject.AddComponent<UnityEngine.UI.Image>();im.color=color;im.raycastTarget=false;return r.gameObject;}
 public static TMP_Text Text(string name,Transform parent,string value,Vector2 pos,Vector2 size,int fontSize=26,TextAlignmentOptions align=TextAlignmentOptions.Center){var r=Rect(name,parent,pos,size);var t=r.gameObject.AddComponent<TextMeshProUGUI>();t.font=Font;t.text=value;t.fontSize=fontSize;t.color=Ink;t.alignment=align;t.raycastTarget=false;return t;}
 public static UnityEngine.UI.Button Button(string name,Transform parent,string title,Vector2 pos,Vector2 size,UnityAction action=null,bool textOnly=false){var g=Panel(name,parent,pos,size,textOnly?new Color(0,0,0,0):new Color(.13f,.16f,.16f));var image=g.GetComponent<UnityEngine.UI.Image>();image.raycastTarget=true;var b=g.AddComponent<UnityEngine.UI.Button>();b.targetGraphic=textOnly?Text("Label",g.transform,title,Vector2.zero,size,27,TextAlignmentOptions.Left):image;if(!textOnly)Text("Label",g.transform,title,Vector2.zero,size,22);var colors=b.colors;colors.normalColor=Color.white;colors.highlightedColor=new Color(1.15f,1.15f,1.15f);colors.pressedColor=new Color(.7f,.75f,.73f);colors.disabledColor=new Color(.4f,.45f,.43f);b.colors=colors;if(action!=null)UnityEventTools.AddPersistentListener(b.onClick,action);return b;}
 public static UnityEngine.UI.Slider Slider(string name,Transform parent,Vector2 pos,float min,float max){var g=Panel(name,parent,pos,new Vector2(410,16),new Color(.14f,.18f,.18f));g.GetComponent<UnityEngine.UI.Image>().raycastTarget=true;var slider=g.AddComponent<UnityEngine.UI.Slider>();slider.minValue=min;slider.maxValue=max;var area=Rect("HandleArea",g.transform,Vector2.zero,new Vector2(390,16));var handle=Panel("Handle",area,Vector2.zero,new Vector2(20,32),Ink);handle.GetComponent<UnityEngine.UI.Image>().raycastTarget=true;slider.handleRect=handle.GetComponent<RectTransform>();slider.targetGraphic=handle.GetComponent<UnityEngine.UI.Image>();return slider;}
 public static GameObject CanvasRoot(){var g=Node("Interface");var c=g.AddComponent<Canvas>();c.renderMode=RenderMode.ScreenSpaceOverlay;var scaler=g.AddComponent<UnityEngine.UI.CanvasScaler>();scaler.uiScaleMode=UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;scaler.referenceResolution=new Vector2(1600,900);scaler.matchWidthOrHeight=.5f;g.AddComponent<UnityEngine.UI.GraphicRaycaster>();var e=Node("EventSystem");e.AddComponent<EventSystem>();e.AddComponent<InputSystemUIInputModule>();return g;}
 public static GameObject Settings(Transform parent,UnityAction back){var p=Panel("Settings",parent,Vector2.zero,new Vector2(750,600),new Color(.025f,.035f,.035f,.98f));Text("Title",p.transform,"SETTINGS",new Vector2(0,215),new Vector2(600,60),36);Text("VolumeLabel",p.transform,"OVERALL VOLUME",new Vector2(0,105),new Vector2(500,50),20);Text("SensitivityLabel",p.transform,"MOUSE SENSITIVITY",new Vector2(0,-30),new Vector2(500,50),20);var s=p.AddComponent<SettingsPanel>();s.volume=Slider("Volume",p.transform,new Vector2(0,50),0,1);s.sensitivity=Slider("Sensitivity",p.transform,new Vector2(0,-90),.2f,3);Button("Back",p.transform,"BACK",new Vector2(0,-220),new Vector2(280,60),back);p.SetActive(false);return p;}
 public static void SceneAtmosphere(){RenderSettings.skybox=null;RenderSettings.ambientMode=UnityEngine.Rendering.AmbientMode.Flat;RenderSettings.ambientLight=new Color(.012f,.017f,.019f);RenderSettings.fog=true;RenderSettings.fogColor=new Color(.012f,.017f,.02f);RenderSettings.fogMode=FogMode.Exponential;RenderSettings.fogDensity=.018f;}
 public static void Foundation(){
  Folders();AssetDatabase.Refresh();
  if(System.IO.File.Exists(Root+"Scenes/MainMenu.unity")||System.IO.File.Exists(Root+"Scenes/Gameplay.unity"))throw new Exception("Demo scenes already exist; do not rebuild.");
  EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);SceneAtmosphere();RenderSettings.ambientLight=Color.black;RenderSettings.fog=false;
  var camera=Node("Menu Camera");var c=camera.AddComponent<Camera>();camera.AddComponent<AudioListener>();camera.tag="MainCamera";c.clearFlags=CameraClearFlags.SolidColor;c.backgroundColor=Color.black;camera.transform.position=new Vector3(0,0,-6);c.fieldOfView=45;
  var canvas=CanvasRoot();var menu=Node("Menu Controller").AddComponent<MenuController>();
  var p=Panel("Main Menu",canvas.transform,new Vector2(-470,0),new Vector2(500,650),Color.clear);menu.mainPanel=p;
  Text("Title",p.transform,"CONNECTIONS",new Vector2(0,220),new Vector2(500,80),50,TextAlignmentOptions.Left).characterSpacing=8;
  Button("Start Game",p.transform,"START GAME",new Vector2(0,50),new Vector2(450,55),menu.StartGame,true);
  Button("Settings Button",p.transform,"SETTINGS",new Vector2(0,-30),new Vector2(450,55),menu.Settings,true);
  Button("Credits Button",p.transform,"CREDITS",new Vector2(0,-110),new Vector2(450,55),menu.Credits,true);
  Button("Exit Button",p.transform,"EXIT",new Vector2(0,-190),new Vector2(450,55),menu.Exit,true);
  menu.settingsPanel=Settings(canvas.transform,menu.Back);
  var credits=Panel("Credits",canvas.transform,Vector2.zero,new Vector2(750,640),new Color(.02f,.03f,.03f,.98f));menu.creditsPanel=credits;menu.creditsText=Text("Credits Text",credits.transform,menu.credits,new Vector2(0,60),new Vector2(680,440),24);Button("Back",credits.transform,"BACK",new Vector2(0,-235),new Vector2(280,60),menu.Back);credits.SetActive(false);
  EditorSceneManager.SaveScene(SceneManager.GetActiveScene(),Root+"Scenes/MainMenu.unity");
  EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);SceneAtmosphere();
  var session=Node("Game Session").AddComponent<GameSession>();
  var floor=Cube("Foundation Floor",null,new Vector3(0,-.15f,0),new Vector3(8,.3f,8));
  var player=Node("Player");player.tag="Player";player.layer=2;player.transform.position=new Vector3(0,.03f,-2.7f);var body=player.AddComponent<CharacterController>();body.height=1.8f;body.radius=.3f;body.center=new Vector3(0,.9f,0);body.stepOffset=.25f;
  var cam=Node("Player Camera",player.transform);cam.transform.localPosition=new Vector3(0,1.65f,0);cam.tag="MainCamera";var pc=cam.AddComponent<Camera>();pc.fieldOfView=65;pc.nearClipPlane=.06f;pc.clearFlags=CameraClearFlags.SolidColor;pc.backgroundColor=Color.black;cam.AddComponent<AudioListener>();
  var fps=player.AddComponent<FirstPersonController>();fps.view=pc;fps.session=session;
  var interaction=player.AddComponent<PlayerInteraction>();interaction.view=pc;interaction.session=session;
  var uiRoot=CanvasRoot();var ui=uiRoot.AddComponent<GameUI>();session.ui=ui;
  ui.prompt=Text("Interaction Prompt",uiRoot.transform,"",new Vector2(0,-220),new Vector2(550,50),22);
  Text("Crosshair",uiRoot.transform,"·",Vector2.zero,new Vector2(30,30),28);
  Text("Control Hints",uiRoot.transform,"WASD MOVE   •   MOUSE LOOK   •   E INTERACT   •   ESC PAUSE",new Vector2(0,-415),new Vector2(1200,40),16).color=new Color(.5f,.55f,.53f,.65f);
  ui.pausePanel=Panel("Pause",uiRoot.transform,Vector2.zero,new Vector2(650,600),new Color(.02f,.025f,.025f,.97f));Text("Pause Title",ui.pausePanel.transform,"PAUSED",new Vector2(0,210),new Vector2(500,60),38);
  Button("Resume",ui.pausePanel.transform,"RESUME",new Vector2(0,65),new Vector2(300,65),session.Resume);
  ui.settingsPanel=Settings(uiRoot.transform,session.Resume);
  Button("Settings",ui.pausePanel.transform,"SETTINGS",new Vector2(0,-35),new Vector2(300,65),null);
  var settingsButton=ui.pausePanel.transform.Find("Settings").GetComponent<UnityEngine.UI.Button>();UnityEventTools.AddBoolPersistentListener(settingsButton.onClick,ui.settingsPanel.SetActive,true);
  Button("Return To Menu",ui.pausePanel.transform,"RETURN TO MENU",new Vector2(0,-160),new Vector2(300,65),session.ReturnToMenu);ui.pausePanel.SetActive(false);
  var fade=Panel("Fade",uiRoot.transform,Vector2.zero,new Vector2(2000,1400),Color.clear);ui.fade=fade.GetComponent<UnityEngine.UI.Image>();ui.fade.raycastTarget=true;fade.SetActive(false);
  ui.resultsPanel=Panel("Results",uiRoot.transform,Vector2.zero,new Vector2(2000,1400),Color.black);Text("Ending Title",ui.resultsPanel.transform,"TO BE CONTINUED",new Vector2(0,250),new Vector2(1200,80),48).characterSpacing=6;ui.resultsText=Text("Times",ui.resultsPanel.transform,"",new Vector2(0,15),new Vector2(800,380),24);Button("Return",ui.resultsPanel.transform,"RETURN TO MENU",new Vector2(0,-285),new Vector2(350,65),session.ReturnToMenu);ui.resultsPanel.SetActive(false);
  EditorSceneManager.SaveScene(SceneManager.GetActiveScene(),Root+"Scenes/Gameplay.unity");
  var scenes=new System.Collections.Generic.List<EditorBuildSettingsScene>();scenes.Add(new EditorBuildSettingsScene(Root+"Scenes/MainMenu.unity",true));scenes.Add(new EditorBuildSettingsScene(Root+"Scenes/Gameplay.unity",true));foreach(var old in EditorBuildSettings.scenes)if(old.path!=Root+"Scenes/MainMenu.unity"&&old.path!=Root+"Scenes/Gameplay.unity")scenes.Add(old);EditorBuildSettings.scenes=scenes.ToArray();
  AssetDatabase.SaveAssets();
 }
}
}