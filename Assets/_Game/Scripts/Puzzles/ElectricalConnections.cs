using UnityEngine;
namespace Connections {
[CreateAssetMenu(menuName="Connections/Electrical Connections")]
public sealed class ElectricalConnections:ScriptableObject {
 [Tooltip("Each mask uses bit 0 for lamp 1 through bit 5 for lamp 6.")]
 public int[] lampMasks={38,9,4,18,8,32};
 public int Evaluate(int switches){int lamps=0;for(int i=0;i<6;i++)if((switches&(1<<i))!=0)lamps^=lampMasks[i];return lamps&63;}
 public bool IsValid()=>lampMasks!=null&&lampMasks.Length==6;
}
}