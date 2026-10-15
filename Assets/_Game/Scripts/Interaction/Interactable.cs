using UnityEngine;
namespace Connections {
public abstract class Interactable:MonoBehaviour {
 [Min(.5f)]public float range=2.5f; public abstract void Interact(GameSession session);
}
}