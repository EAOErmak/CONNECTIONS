using System;
using UnityEngine.InputSystem;
namespace Connections {
/// <summary>Button actions survive input updates between Editor and Player loops.</summary>
public sealed class ButtonCommand:IDisposable {
 readonly InputAction action;
 public ButtonCommand(string name,Action callback,params string[] bindings){action=new InputAction(name,InputActionType.Button);foreach(var binding in bindings)action.AddBinding(binding);action.performed+=_=>callback();action.Enable();}
 public void Dispose(){action.Dispose();}
}
}