using System.Collections.Generic;
namespace Connections {
public static class MorseCodec {
 public static readonly string[] Codes={".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."};
 public static bool TryDecode(string code,out char letter){int i=System.Array.IndexOf(Codes,code);letter=i>=0?(char)('A'+i):'?';return i>=0;}
 public static string Encode(string text){var list=new List<string>();foreach(char c in text.ToUpperInvariant())if(c>='A'&&c<='Z')list.Add(Codes[c-'A']);else return null;return string.Join(" ",list);}
}
[System.Serializable]
public sealed class MorseMessage {
 public List<string> letters=new List<string>();public string current="";
 public string Raw=>string.Join(" / ",letters)+(current.Length>0?(letters.Count>0?" / ":"")+current:"");
 public string Decoded{get{string s="";foreach(var c in letters){MorseCodec.TryDecode(c,out var l);s+=l;}return s;}}
 public bool Add(char symbol){if(current.Length>=4||letters.Count>=24)return false;current+=symbol;return true;}
 public bool FinishLetter(){if(!MorseCodec.TryDecode(current,out _))return false;letters.Add(current);current="";return true;}
 public void Backspace(){if(current.Length>0)current=current.Substring(0,current.Length-1);else if(letters.Count>0){current=letters[letters.Count-1];letters.RemoveAt(letters.Count-1);}}
 public void Clear(){current="";letters.Clear();}
}
}