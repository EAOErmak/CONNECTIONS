using UnityEngine;
namespace Connections {
[RequireComponent(typeof(BoxCollider))]
public sealed class ExitThreshold:MonoBehaviour {
 public GameSession session;bool triggered;
 void OnTriggerEnter(Collider other){if(triggered||!session.CommunicationEstablished||!other.GetComponent<FirstPersonController>())return;triggered=true;session.Finish();}
}
}