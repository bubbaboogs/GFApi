
namespace GFApi.Creation
{
    public class Biome
    {
        public void RegisterBiome(string biomeName)
        {
            MainPlugin.registeredBiomes.Add(biomeName);
        }
    }
}