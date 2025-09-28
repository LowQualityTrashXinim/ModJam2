using Terraria.ID;
using Terraria.ModLoader;

namespace ModJam2.Content.ThrowingKnifes;

public abstract class BaseThrowingKnife : ModItem
{
    public sealed override void SetDefaults()
    {
        Item.maxStack = 9999;
        Item.DamageType = DamageClass.Ranged;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.consumable = true;
        Item.UseSound = SoundID.Item1;
        Item.useStyle = ItemUseStyleID.Swing;
        Knife_SetDefaults(out int damage, out int usespeed, out int shoot, out float speed);
        Item.damage = damage;
        Item.useTime = Item.useAnimation = usespeed;
        Item.shoot = shoot;
        Item.shootSpeed = speed;
    }
    public virtual void Knife_SetDefaults(out int damage, out int usespeed, out int shoot, out float speed)
    {
        damage = 1;
        usespeed = 15;
        shoot = 1;
        speed = 15;
    }
}

