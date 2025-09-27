using StructureHelper.API;
using StructureHelper.Models;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ModJam2.Common.Wrapper
{
    public static class StructureHelperWrapper
    {
        public static void Get_StructureData(string path, Mod mod)
        {
            Generator.GetStructureData(path, mod);
        }
        public static void Generate(StructureData data, Point16 pos)
        {
            Generator.GenerateFromData(data, pos);
        }
    }
}
