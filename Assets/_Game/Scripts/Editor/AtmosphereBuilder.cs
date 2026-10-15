using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
namespace Connections.Editor {
public static partial class DemoBuilder {
 static System.Random rng=new System.Random(501247);
 public static AudioClip Audio(string name,float duration,float frequency,float noise=0,bool loop=false){
  string path=Root+"Audio/"+name+".wav";var old=AssetDatabase.LoadAssetAtPath<AudioClip>(path);if(old)return old;
  const int sr=22050;int count=(int)(sr*duration);short[] samples=new short[count];
  for(int i=0;i<count;i++){double t=(double)i/sr;double env=loop?1:Math.Min(1,t/.008)*Math.Min(1,(duration-t)/.03);double v=Math.Sin(t*frequency*Math.PI*2)*.32+Math.Sin(t*frequency*2*Math.PI*2)*.07+(rng.NextDouble()*2-1)*noise; samples[i]=(short)(Math.Max(-1,Math.Min(1,v*env))*16000);}
  using(var w=new System.IO.BinaryWriter(System.IO.File.Create(path))){w.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));w.Write(36+count*2);w.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));w.Write(16);w.Write((short)1);w.Write((short)1);w.Write(sr);w.Write(sr*2);w.Write((short)2);w.Write((short)16);w.Write(System.Text.Encoding.ASCII.GetBytes("data"));w.Write(count*2);foreach(var sample in samples)w.Write(sample);}
  AssetDatabase.ImportAsset(path);var imp=(AudioImporter)AssetImporter.GetAtPath(path);var setting=imp.defaultSampleSettings;setting.loadType=AudioClipLoadType.DecompressOnLoad;setting.compressionFormat=AudioCompressionFormat.PCM;imp.defaultSampleSettings=setting;imp.SaveAndReimport();return AssetDatabase.LoadAssetAtPath<AudioClip>(path);
 }
 static void AssignSound(SoundEmitter s){
  s.click=Audio("Mechanical Click",.075f,120,.55f);s.relay=Audio("Relay",.16f,88,.38f);s.power=Audio("Power Restoration",1.2f,65,.08f);s.door=Audio("Door Motor",1.9f,110,.15f);s.dot=Audio("Morse Dot",.10f,690);s.dash=Audio("Morse Dash",.30f,690);s.accept=Audio("Accepted",.36f,930);s.reject=Audio("Rejected",.5f,225);
 }
 static Texture2D Surface(string name,bool tiles){
  string path=Root+"Textures/"+name+".png";var old=AssetDatabase.LoadAssetAtPath<Texture2D>(path);if(old)return old;
  int size=512;var texture=new Texture2D(size,size,TextureFormat.RGB24,false);var px=new Color[size*size];
  for(int y=0;y<size;y++)for(int x=0;x<size;x++){
   float v=Mathf.PerlinNoise(x*.035f,y*.035f)*.18f+Mathf.PerlinNoise(x*.16f,y*.16f)*.05f+(float)rng.NextDouble()*.035f;
   float stain=Mathf.PerlinNoise(x*.009f+9,y*.017f+3);v-=Mathf.Pow(stain,3)*.25f;
   if(tiles){if(x%128<5||y%128<5)v=.08f;else v+=.35f;}else v+=.3f;
   px[y*size+x]=new Color(v*.94f,v,v*.97f);
  }
  texture.SetPixels(px);texture.Apply();System.IO.File.WriteAllBytes(path,texture.EncodeToPNG());UnityEngine.Object.DestroyImmediate(texture);AssetDatabase.ImportAsset(path);return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
 }
 static void AgeMaterials(){
  var tile=Material("Worn Ceramic",Color.gray);tile.color=new Color(.65f,.68f,.62f);tile.SetTexture("_BaseMap",Surface("Aged Tile",true));tile.SetTextureScale("_BaseMap",new Vector2(4,4));
  var concrete=Material("Damp Concrete",Color.gray);concrete.color=new Color(.47f,.5f,.48f);concrete.SetTexture("_BaseMap",Surface("Stained Concrete",false));concrete.SetTextureScale("_BaseMap",new Vector2(3,2));
  var rust=Material("Oxidized Iron",new Color(.26f,.13f,.07f),.55f,.15f);rust.SetTexture("_BaseMap",Surface("Rust",false));rust.SetTextureScale("_BaseMap",new Vector2(1,3));
 }
 static GameObject Cylinder(string name,Transform parent,Vector3 pos,Vector3 scale,Material material,Vector3 rotation){
  var g=GameObject.CreatePrimitive(PrimitiveType.Cylinder);g.name=name;g.transform.SetParent(parent,false);g.transform.localPosition=pos;g.transform.localScale=scale;g.transform.localEulerAngles=rotation;g.GetComponent<Renderer>().sharedMaterial=material;return g;
 }
 static GameObject CreateCRT(){
  var steel=Material("CRT Casing",new Color(.045f,.052f,.047f),.2f,.14f);var bezel=Material("CRT Bezel",new Color(.09f,.105f,.09f),.3f,.2f);
  var root=Node("CRT Television");Cube("Body",root.transform,Vector3.zero,new Vector3(1.1f,.82f,.65f),steel);
  Cube("Screen Recess",root.transform,new Vector3(-.07f,.02f,-.344f),new Vector3(.86f,.64f,.05f),bezel);
  var screen=Cube("Phosphor Screen",root.transform,new Vector3(-.07f,.02f,-.375f),new Vector3(.75f,.54f,.012f),Material("CRT Phosphor",Color.white));UnityEngine.Object.DestroyImmediate(screen.GetComponent<Collider>());
  var crt=root.AddComponent<CRTStatic>();crt.screen=screen.GetComponent<Renderer>();var glow=Node("Screen Glow",root.transform).AddComponent<Light>();glow.transform.localPosition=new Vector3(0,0,-.55f);glow.type=LightType.Point;glow.color=new Color(.68f,.78f,.72f);glow.range=2.3f;glow.intensity=.13f;crt.glow=glow;
  for(int i=0;i<2;i++)Cylinder("Control Knob",root.transform,new Vector3(.46f,-.15f+i*.2f,-.36f),new Vector3(.07f,.025f,.07f),bezel,new Vector3(90,0,0));
  for(int i=0;i<6;i++)Cube("Speaker Slot",root.transform,new Vector3(.43f,.16f+i*.025f,-.355f),new Vector3(.12f,.009f,.008f),steel);
  Cube("Wall Plate",root.transform,new Vector3(0,-.44f,.34f),new Vector3(.35f,.18f,.1f),bezel);Cube("Bracket Arm",root.transform,new Vector3(0,-.5f,.1f),new Vector3(.12f,.12f,.65f),bezel);Cube("Bracket Shelf",root.transform,new Vector3(0,-.44f,0),new Vector3(.85f,.08f,.5f),bezel);
  var audio=Sound(root,.075f);AssignSound(audio);audio.loop=Audio("Television Static",2,20,.6f,true);audio.loopOnEnable=true;audio.minDistance=.7f;audio.maxDistance=7;
  return root;
 }
 static void Details(Transform parent,float centerZ){
  var tile=Material("Worn Ceramic",Color.gray);var rust=Material("Oxidized Iron",Color.gray);var steel=Material("Worn Steel",Color.gray);
  Cube("Lower West Tiles",parent,new Vector3(-3.85f,.78f,centerZ),new Vector3(.04f,1.55f,7.8f),tile);Cube("Lower East Tiles",parent,new Vector3(3.85f,.78f,centerZ),new Vector3(.04f,1.55f,7.8f),tile);
  for(int side=-1;side<=1;side+=2){
   Cylinder("Water Main",parent,new Vector3(side*3.65f,2.65f,centerZ),new Vector3(.11f,3.85f,.11f),rust,new Vector3(90,0,0));
   Cylinder("Vertical Pipe",parent,new Vector3(side*3.65f,1.5f,centerZ+2.6f),new Vector3(.13f,1.5f,.13f),rust,Vector3.zero);
   for(int j=0;j<5;j++)Cube("Pipe Clamp",parent,new Vector3(side*3.68f,2.65f,centerZ-3+j*1.5f),new Vector3(.2f,.19f,.07f),steel);
  }
  Cube("Drain",parent,new Vector3(1.8f,.012f,centerZ-1.8f),new Vector3(.55f,.015f,.45f),steel);
  for(int i=0;i<8;i++)Cube("Drain Slit",parent,new Vector3(1.58f+i*.06f,.022f,centerZ-1.8f),new Vector3(.018f,.005f,.36f),Material("Charcoal Metal",Color.black));
  Cube("Service Cabinet",parent,new Vector3(3.4f,.75f,centerZ-.1f),new Vector3(.7f,1.5f,1.2f),steel);
  for(int i=0;i<7;i++)Cube("Vent Slit",parent,new Vector3(3.025f,1.04f-i*.06f,centerZ-.1f),new Vector3(.015f,.018f,.75f),Material("Charcoal Metal",Color.black));
  Cube("Cable Trunk",parent,new Vector3(-3.76f,2.1f,centerZ),new Vector3(.1f,.12f,7.8f),steel);
 }
 public static void Atmosphere(){
  Folders();AgeMaterials();
  EditorSceneManager.OpenScene(Root+"Scenes/Gameplay.unity");var session=UnityEngine.Object.FindAnyObjectByType<GameSession>();
  if(GameObject.Find("CRT Room 1"))throw new Exception("Atmosphere already added");
  var crt=CreateCRT();PrefabUtility.SaveAsPrefabAsset(crt,Root+"Prefabs/CRT Television.prefab");UnityEngine.Object.DestroyImmediate(crt);
  var prefab=AssetDatabase.LoadAssetAtPath<GameObject>(Root+"Prefabs/CRT Television.prefab");
  var tv1=(GameObject)PrefabUtility.InstantiatePrefab(prefab);tv1.name="CRT Room 1";tv1.transform.SetParent(GameObject.Find("Room 1").transform);tv1.transform.position=new Vector3(-3.35f,2.68f,2.5f);tv1.transform.rotation=Quaternion.Euler(0,-25,0);tv1.SetActive(false);
  var tv2=(GameObject)PrefabUtility.InstantiatePrefab(prefab);tv2.name="CRT Room 2";tv2.transform.SetParent(GameObject.Find("Room 2").transform);tv2.transform.position=new Vector3(3.25f,2.68f,11.4f);tv2.transform.rotation=Quaternion.Euler(0,25,0);tv2.SetActive(false);
  var intro=new System.Collections.Generic.List<GameObject>(session.introObjects){tv1,tv2};session.introObjects=intro.ToArray();
  Details(GameObject.Find("Room 1").transform,0);Details(GameObject.Find("Room 2").transform,8.3f);
  var ambient=Node("Machinery Hum",GameObject.Find("Room 2").transform);ambient.transform.position=new Vector3(3.3f,1,8.2f);var amb=Sound(ambient,.032f);amb.loop=Audio("Machinery",2,44,.02f,true);amb.loopOnEnable=true;amb.minDistance=.8f;amb.maxDistance=9;ambient.SetActive(false);var restored=new System.Collections.Generic.List<GameObject>(session.restoredObjects){ambient};session.restoredObjects=restored.ToArray();
  foreach(var s in UnityEngine.Object.FindObjectsByType<SoundEmitter>(FindObjectsInactive.Include)){AssignSound(s);if(s.gameObject.GetComponent<Light>()){s.loop=Audio("Electrical Hum",2,50,.012f,true);s.loopOnEnable=true;s.minDistance=.6f;s.maxDistance=6;}}
  AddButtonAudio(session.ui.transform,session.feedback);
  EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
  EditorSceneManager.OpenScene(Root+"Scenes/MainMenu.unity");
  var menuTv=(GameObject)PrefabUtility.InstantiatePrefab(prefab);menuTv.name="Menu CRT";menuTv.transform.position=new Vector3(1.75f,-.12f,0);menuTv.transform.localScale=Vector3.one*1.8f;menuTv.transform.rotation=Quaternion.Euler(0,-12,0);menuTv.GetComponent<SoundEmitter>().spatialBlend=0;menuTv.GetComponent<SoundEmitter>().volume=.035f;
  var light=Node("CRT Frame Fill").AddComponent<Light>();light.type=LightType.Point;light.transform.position=new Vector3(1.4f,1,-1.8f);light.color=new Color(.55f,.65f,.6f);light.intensity=.22f;light.range=4;
  var menu=UnityEngine.Object.FindAnyObjectByType<MenuController>();var click=Sound(menu.gameObject,.18f,false);AssignSound(click);AddButtonAudio(GameObject.Find("Interface").transform,click);
  EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());EditorSceneManager.SaveScene(SceneManager.GetActiveScene());AssetDatabase.SaveAssets();
 }
 static void AddButtonAudio(Transform root,SoundEmitter sound){foreach(var b in root.GetComponentsInChildren<UnityEngine.UI.Button>(true)){var click=b.gameObject.AddComponent<ButtonAudio>();click.sound=sound;var nav=b.navigation;nav.mode=UnityEngine.UI.Navigation.Mode.None;b.navigation=nav;}}
}
}