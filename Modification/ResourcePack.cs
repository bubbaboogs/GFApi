using System.IO;
using System.IO.Compression;
using BepInEx;

namespace GFApi.Modification{
    public class ResourcePack{
        static string packDir = Path.Combine(Paths.GameRootPath, "ResourcePacks");
        public static void LoadResourcePacks(){
            foreach(string file in Directory.GetFiles(packDir, "*.zip")){
                LoadPack(file);
            }
        }

        public static void ChangePackDir(string newDir){
            packDir = newDir;
        }

        public static void LoadPack(string path){
            ZipArchive zip = ZipFile.OpenRead(path);
        }

    }
}