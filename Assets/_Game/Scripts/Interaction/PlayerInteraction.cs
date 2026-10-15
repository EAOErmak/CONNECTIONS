using UnityEngine;
using UnityEngine.InputSystem;
namespace Connections {
public sealed class PlayerInteraction:MonoBehaviour {
 public Camera view;public GameSession session;public float maxRange=3;
 public Interactable LookingAt {get;private set;}
 ButtonCommand interact;
 void Awake(){interact=new ButtonCommand("Interact",()=>{if(LookingAt&&session.CanMove)LookingAt.Interact(session);},"<Keyboard>/e");}
 void OnDestroy(){interact?.Dispose();}
 void Update(){
  LookingAt=null;
  if(session&&session.CanMove&&Physics.Raycast(view.transform.position,view.transform.forward,out var hit,maxRange,~(1<<2),QueryTriggerInteraction.Ignore)){
   var item=hit.collider.GetComponentInParent<Interactable>();if(item&&hit.distance<=item.range)LookingAt=item;
  }
  if(session&&session.ui)session.ui.prompt.text=LookingAt?"[E] INTERACT":"";

 }
}
}