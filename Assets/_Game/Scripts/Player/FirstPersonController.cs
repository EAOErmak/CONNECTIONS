using UnityEngine;
using UnityEngine.InputSystem;
namespace Connections {
[RequireComponent(typeof(CharacterController))]
public sealed class FirstPersonController:MonoBehaviour {
 public Camera view; public GameSession session;
 [Min(.1f)]public float moveSpeed=2.5f;
 [Min(.1f)]public float acceleration=12;
 [Min(.01f)]public float mouseSensitivity=.09f;
 public float gravity=-18;
 CharacterController body;Vector3 velocity;float vertical,pitch;
 InputAction lookAction;Vector2 lookDelta;
 void Awake(){body=GetComponent<CharacterController>();lookAction=new InputAction("Look",InputActionType.PassThrough,"<Mouse>/delta");lookAction.performed+=c=>{if(session.CanMove&&Cursor.lockState==CursorLockMode.Locked)lookDelta+=c.ReadValue<Vector2>();};lookAction.Enable();}
 void OnDestroy(){lookAction?.Dispose();}
 void Update(){
  if(!session||!session.CanMove){velocity=Vector3.zero;lookDelta=Vector2.zero;return;}
  var k=Keyboard.current;var m=Mouse.current;
  Vector2 axis=Vector2.zero;
  if(k!=null)axis=new Vector2((k.dKey.isPressed?1:0)-(k.aKey.isPressed?1:0),(k.wKey.isPressed?1:0)-(k.sKey.isPressed?1:0));
  axis=Vector2.ClampMagnitude(axis,1);
  Vector3 target=(transform.right*axis.x+transform.forward*axis.y)*moveSpeed;
  velocity=Vector3.MoveTowards(velocity,target,acceleration*Time.deltaTime);
  if(body.isGrounded&&vertical<0)vertical=-2;vertical+=gravity*Time.deltaTime;
  body.Move((velocity+Vector3.up*vertical)*Time.deltaTime);
  if(m!=null && Cursor.lockState==CursorLockMode.Locked){
   Vector2 look=lookDelta*mouseSensitivity*PlayerPrefs.GetFloat("Sensitivity",1);
   transform.Rotate(0,look.x,0);pitch=Mathf.Clamp(pitch-look.y,-80,80);view.transform.localRotation=Quaternion.Euler(pitch,0,0);
  }
  lookDelta=Vector2.zero;
 }
}
}