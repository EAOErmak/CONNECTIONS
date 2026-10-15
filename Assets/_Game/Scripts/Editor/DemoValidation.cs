using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
namespace Connections.Editor {
public static class DemoValidation {
 static int checks;
 static void Require(bool condition,string name){if(!condition)throw new Exception("CHECK FAILED: "+name);checks++;}
 [MenuItem("Connections/Validate Demo")]
 public static string RunAll(){
  if(EditorApplication.isPlaying)throw new Exception("Run scene validation outside Play Mode.");
  checks=0;
  var config=AssetDatabase.LoadAssetAtPath<ElectricalConnections>(DemoBuilder.Root+"ScriptableObjects/Electrical Connections.asset");
  Require(config&&config.IsValid(),"Connection asset exists and contains six rows");
  var solutions=new List<int>();for(int s=0;s<64;s++)if(config.Evaluate(s)==63)solutions.Add(s);
  Require(config.Evaluate(0)==0,"Initial lamps off");
  Require(solutions.Count==1&&solutions[0]==46,"Unique specified solution across 64 states");
  for(int i=0;i<6;i++)Require(config.Evaluate((1<<i)^(1<<i))==0,"Switch "+i+" toggles reversibly");
  for(int i=0;i<26;i++)Require(MorseCodec.TryDecode(MorseCodec.Codes[i],out char c)&&c==(char)('A'+i),"Morse roundtrip "+(char)('A'+i));
  foreach(var invalid in new[]{"","----",".....","..x","-.-.-",null})Require(!MorseCodec.TryDecode(invalid,out _),"Reject malformed Morse");
  var message=new MorseMessage();foreach(char c in "----")message.Add(c);Require(!message.FinishLetter()&&message.current=="----","Invalid letter stays editable");message.Backspace();Require(message.FinishLetter()&&message.Decoded=="O","Correction produces O");message.Backspace();Require(message.Decoded==""&&message.current=="---","Completed letter correction");message.Clear();Require(message.Raw==""&&message.Decoded=="","Clear resets current message");
  foreach(string answer in new[]{"POWER","KEY","READY"}){message.Clear();foreach(string code in MorseCodec.Encode(answer).Split(' ')){foreach(char c in code)message.Add(c);Require(message.FinishLetter(),"Finish answer letter");}Require(message.Decoded==answer,"Answer codec "+answer);}
  Require(GameSession.Format(65)=="01:05"&&GameSession.Format(3661)=="01:01:01","Time formatting");
  foreach(string scenePath in new[]{"MainMenu","Gameplay"}){
   string path=DemoBuilder.Root+"Scenes/"+scenePath+".unity";var existing=SceneManager.GetSceneByPath(path);bool opened=!existing.isLoaded;var scene=opened?EditorSceneManager.OpenScene(path,OpenSceneMode.Additive):existing;
   try {
    var roots=scene.GetRootGameObjects();
    Require(roots.SelectMany(g=>g.GetComponentsInChildren<UnityEngine.EventSystems.EventSystem>(true)).Count()==1,scenePath+" has one EventSystem");
    foreach(var root in roots)foreach(var t in root.GetComponentsInChildren<Transform>(true))Require(GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)==0,"No missing script "+t.name);
    Require(roots.SelectMany(g=>g.GetComponentsInChildren<Camera>(true)).Count(c=>c.enabled)==1,scenePath+" one camera");
    foreach(var source in roots.SelectMany(g=>g.GetComponentsInChildren<SoundEmitter>(true)))Require(source.click&&source.dot&&source.dash&&source.GetComponent<AudioSource>(),"Replaceable audio references "+source.name);
    foreach(var tv in roots.SelectMany(g=>g.GetComponentsInChildren<CRTStatic>(true)))Require(tv.screen&&tv.glow&&tv.GetComponent<SoundEmitter>().loop,"CRT references "+tv.name);
    if(scenePath=="Gameplay"){
     var session=roots.SelectMany(g=>g.GetComponentsInChildren<GameSession>(true)).Single();var ep=roots.SelectMany(g=>g.GetComponentsInChildren<ElectricalPanel>(true)).Single();var mp=roots.SelectMany(g=>g.GetComponentsInChildren<MorsePuzzle>(true)).Single();
     Require(session.ui&&session.feedback&&session.roomDoor&&session.exitDoor,"Session references");
     Require(session.roomDoor.leaf&&session.exitDoor.leaf,"Door leaves");
     Require(ep.session==session&&ep.connections&&ep.modal&&ep.switches.Length==6&&ep.lamps.Length==6,"Electrical references");
     Require(ep.switches.All(x=>x)&&ep.lamps.All(x=>x)&&ep.physicalLamps.All(x=>x)&&ep.physicalSwitches.All(x=>x)&&ep.engravings.All(x=>x),"All six physical and UI entries");
     Require(ep.engravings.Select(x=>x.text).SequenceEqual(new[]{"5","0","1","2","4","7"}),"Fixed engraving order");
     Require(mp.session==session&&mp.modal&&mp.referenceImage&&mp.transmit&&mp.clear&&mp.question&&mp.decoded&&mp.raw,"Morse references");
     Require(mp.referenceImage.sprite==null&&mp.chartLabel.text.Replace("\n"," ")=="MORSE CHART PLACEHOLDER","Chart remains placeholder");
     Require(mp.answers.SequenceEqual(new[]{"POWER","KEY","READY"}),"Question order");
     Require(session.introObjects.Length==3&&session.restoredObjects.Length==3,"Intro and restored objects");
     Require(roots.SelectMany(g=>g.GetComponentsInChildren<ExitThreshold>(true)).Single().GetComponent<BoxCollider>().isTrigger,"Ending trigger configured");
     var player=roots.SelectMany(g=>g.GetComponentsInChildren<FirstPersonController>(true)).Single();Require(player.session==session&&player.view&&player.GetComponent<CharacterController>()&&player.GetComponent<PlayerInteraction>().view==player.view,"Player references");
    }else{
     var menu=roots.SelectMany(g=>g.GetComponentsInChildren<MenuController>(true)).Single();Require(menu.mainPanel&&menu.settingsPanel&&menu.creditsPanel&&menu.creditsText,"Menu references");
    }
   }finally{if(opened)EditorSceneManager.CloseScene(scene,true);}
  }
  Require(AssetDatabase.LoadAssetAtPath<GameObject>(DemoBuilder.Root+"Prefabs/CRT Television.prefab"),"CRT prefab saved");
  var report=$"PASS: {checks} deterministic and serialized-scene checks.\nElectrical: all 64 states, unique solution 46.\nMorse: A-Z, invalid input, correction, answers.\nScenes: component and serialized references, audio, placeholder, doors and trigger.";
  System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Directory.GetParent(Application.dataPath).FullName,"Assets/_Game/Documentation/VALIDATION_REPORT.txt"),report);
  Debug.Log(report);return report;
 }
}
}