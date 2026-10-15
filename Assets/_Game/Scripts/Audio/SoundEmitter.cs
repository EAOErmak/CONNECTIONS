using UnityEngine;
namespace Connections {
public enum SoundCue {Click,Relay,Power,Door,Dot,Dash,Accept,Reject}
[RequireComponent(typeof(AudioSource))]
public sealed class SoundEmitter:MonoBehaviour {
 public AudioClip click,relay,power,door,dot,dash,accept,reject,loop;
 [Range(0,1)]public float volume=.35f, spatialBlend=1;
 public float minDistance=1,maxDistance=10;public bool loopOnEnable;
 public AudioRolloffMode rolloff=AudioRolloffMode.Logarithmic;
 public AnimationCurve customRolloff=AnimationCurve.Linear(0,1,1,0);
 AudioSource source;
 void Awake(){Configure();}
 void OnEnable(){Configure();if(loopOnEnable&&loop){source.clip=loop;source.loop=true;source.Play();}}
 void Configure(){if(!source)source=GetComponent<AudioSource>();source.playOnAwake=false;source.volume=volume;source.spatialBlend=spatialBlend;source.minDistance=minDistance;source.maxDistance=maxDistance;source.rolloffMode=rolloff;if(rolloff==AudioRolloffMode.Custom)source.SetCustomCurve(AudioSourceCurveType.CustomRolloff,customRolloff);}
 public void Play(SoundCue cue){Configure();AudioClip clip=cue switch{SoundCue.Click=>click,SoundCue.Relay=>relay,SoundCue.Power=>power,SoundCue.Door=>door,SoundCue.Dot=>dot,SoundCue.Dash=>dash,SoundCue.Accept=>accept,_=>reject};if(clip)source.PlayOneShot(clip);}
 void OnDisable(){if(source)source.Stop();}
}
}