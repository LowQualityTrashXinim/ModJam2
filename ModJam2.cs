using System.Collections.Generic;
using Terraria.ModLoader;

namespace ModJam2
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class ModJam2 : Mod
    {

    }
    public class ModJam2System : ModSystem
    {
        public static List<int> Mage_HeldProjectile = new();
        public override void Load()
        {
        }
        public override void Unload()
        {
            Mage_HeldProjectile.Clear();
        }
    }
}
