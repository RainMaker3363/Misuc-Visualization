using UnityEngine;
using UnityEditor;
using System.Collections;
// Path를 위한 include
using System.IO;

//AssetPostprocessor 안에는 파일이 import할때 이벤트를 알려준다.
public class MidiAssetImporter : AssetPostprocessor
{

    // Asset이 생성, 삭제, 이동, 경로 바뀜등등의 이벤트가 발생하면 호출된다.
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach(string import in importedAssets)
        {
            // 확장자를 추출해준다.
            string Extention = Path.GetExtension(import);

            if(Extention.Equals(".mid") == true)
            {
                Debug.Log("[mid] File Found it");

                MidiAsset midAsset = ScriptableObject.CreateInstance<MidiAsset>();

                // 확장자를 변경시키고 덮어주는 함수
                string newFilename = Path.ChangeExtension(import, ".asset");

                // Load Midi data
                midAsset.FileLoad(import);

                AssetDatabase.CreateAsset(midAsset, newFilename);

                AssetDatabase.SaveAssets();
            }
            
        }
    }
}
