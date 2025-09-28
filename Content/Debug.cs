using ModJam2.Common.Subworlds;
using ModJam2.Texture;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content
{
    internal class TestEnterSubWorld : ModItem
    {
        public override string Texture => ModTexture.MissingTexture_Default;
        public override void SetDefaults()
        {
            Item.consumable = false;
            Item.width = Item.height = 32;
            Item.useAnimation = Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }
        public override bool? UseItem(Player player)
        {
            if (SubworldSystem.Current == null && player.ItemAnimationJustStarted)
                SubworldSystem.Enter<CursedKingdomSubworld>();
            return base.UseItem(player);
        }
    }
}
