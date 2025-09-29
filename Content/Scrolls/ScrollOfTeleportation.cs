using ModJam2.Texture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class ScrollOfTeleportation : ModItem
{
    public override string Texture => ModTexture.Scroll;
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useTime = Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.maxStack = 99;
    }
    public override bool? UseItem(Player player)
    {
        if (player.ItemAnimationJustStarted)
        {
            player.Teleport(Main.MouseWorld, TeleportationStyleID.TeleportationPotion);
        }
        return true;
    }
}
