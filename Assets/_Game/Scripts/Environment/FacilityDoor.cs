using UnityEngine;
namespace Connections {
public sealed class FacilityDoor : MonoBehaviour {
 public Transform leaf; public Vector3 openOffset=new Vector3(1.6f,0,0);
 [Min(.1f)]public float openSeconds=2;public SoundEmitter sound;
 public bool IsOpen {get;private set;} Vector3 closed;
 void Awake(){if(!leaf)leaf=transform;closed=leaf.localPosition;}
 public void Open(){if(IsOpen)return;IsOpen=true;if(sound)sound.Play(SoundCue.Door);}
 void Update(){if(IsOpen&&leaf)leaf.localPosition=Vector3.MoveTowards(leaf.localPosition,closed+openOffset,Time.deltaTime*openOffset.magnitude/openSeconds);}
}
}