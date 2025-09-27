using Microsoft.Xna.Framework;
using ModJam2.Texture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Roguelike.Contents.Items.Consumable.Scroll;
internal class ScrollOfFire : ModItem
{
    public override string Texture => ModTexture.MissingTexture_Default;
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useTime = Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 22;
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
            float rotate = MathHelper.ToRadians(45);
            for (int i = 0; i < 12; i++)
            {
                Vector2 distribute = distance.RotatedBy(MathHelper.Lerp(rotate, -rotate, i / 11f)) * 4;
                Projectile projectile = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), player.Center, distribute, ProjectileID.Flamelash, Item.damage, Item.knockBack, player.whoAmI);
                projectile.timeLeft = 180;
                projectile.tileCollide = false;
            }
        }
        return true;
    }
}
