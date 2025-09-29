using Microsoft.Xna.Framework;
using ModJam2.Texture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class ScrollOfWater : ModItem
{
    public override string Texture => ModTexture.Scroll;
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useTime = Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 32;
        Item.knockBack = 6;
        Item.maxStack = 99;
        Item.noUseGraphic = true;
        Item.noMelee = true;
    }
    public override bool? UseItem(Player player)
    {
        if (player.ItemAnimationJustStarted)
        {
            Vector2 limitedSpawningPosition = Main.MouseWorld;
            Vector2 distance = (limitedSpawningPosition - player.Center).SafeNormalize(Vector2.Zero);
            float rotate = MathHelper.ToRadians(90);
            for (int i = 0; i < 18; i++)
            {
                Vector2 distribute = distance.RotatedBy(MathHelper.Lerp(rotate, -rotate, i / 18f)) * 3;
                Projectile projectile = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), player.Center, distribute, ProjectileID.WaterBolt, Item.damage, Item.knockBack, player.whoAmI);
                projectile.timeLeft = 360;
            }
        }
        return true;
    }
}
