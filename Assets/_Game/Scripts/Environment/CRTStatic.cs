using UnityEngine;
namespace Connections {
public sealed class CRTStatic:MonoBehaviour {
 public Renderer screen;public Light glow;
 [Range(1,30)] public float framesPerSecond=12;
 [Range(.01f,1)] public float brightness=.38f;
 [Range(0,.2f)]public float instability=.035f;
 Texture2D texture;Color32[] pixels;Material material;float next;
 uint seed=78123;
 void Awake(){texture=new Texture2D(192,144,TextureFormat.RGB24,false);texture.filterMode=FilterMode.Bilinear;texture.wrapMode=TextureWrapMode.Clamp;pixels=new Color32[192*144];material=screen.material;material.EnableKeyword("_EMISSION");material.SetTexture("_BaseMap",texture);material.SetTexture("_EmissionMap",texture);}
 void Update(){if(Time.unscaledTime<next)return;next=Time.unscaledTime+1/framesPerSecond;int band=(int)(Time.unscaledTime*13)%144;float b=brightness+Mathf.Sin(Time.unscaledTime*2.4f)*instability;for(int y=0;y<144;y++)for(int x=0;x<192;x++){seed^=seed<<13;seed^=seed>>17;seed^=seed<<5;float v=(seed&255)/255f;v=.15f+v*.65f;if(y%3==0)v*=.75f;if(Mathf.Abs(y-band)<2)v*=.45f;byte n=(byte)(v*b*255);pixels[y*192+x]=new Color32(n,n,n,255);}texture.SetPixels32(pixels);texture.Apply(false);material.SetColor("_EmissionColor",Color.white*1.5f);material.color=Color.white;if(glow)glow.intensity=.08f+brightness*.15f;}
 void OnDestroy(){if(texture)Destroy(texture);if(material)Destroy(material);}
}
}