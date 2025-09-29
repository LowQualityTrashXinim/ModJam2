using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ModJam2.Common.Utils
{
    public static partial class ModUtils
    {
        /// <summary>
        /// Spawn combat text above player without the random Y position
        /// </summary>
        /// <param name="location">player hitbox</param>
        /// <param name="color"></param>
        /// <param name="combatMessage"></param>
        /// <param name="offsetposY"></param>
        /// <param name="dramatic"></param>
        /// <param name="dot"></param>
        public static void CombatTextRevamp(Rectangle location, Color color, string combatMessage, int offsetposY = 0, int timeleft = 30, bool dramatic = false, bool dot = false)
        {
            int drama = 0;
            if (dramatic)
            {
                drama = 1;
            }
            int text = CombatText.NewText(new Rectangle(), color, combatMessage, dramatic, dot);
            if (text < 0 || text >= Main.maxCombatText)
            {
                return;
            }
            CombatText cbtext = Main.combatText[text];
            Vector2 vector = FontAssets.CombatText[drama].Value.MeasureString(cbtext.text);
            cbtext.position.X = location.X + location.Width * 0.5f - vector.X * 0.5f;
            cbtext.position.Y = location.Y + offsetposY + location.Height * 0.25f - vector.Y * 0.5f;
            cbtext.lifeTime += timeleft;
        }
        public static Item Get_ItemInfo(int ID) => ContentSamples.ItemsByType[ID];
        public static Point Get_ItemTextureSize(int ID)
        {
            Main.instance.LoadItem(ID);
            return TextureAssets.Item[ID].Value.Size().ToPoint();
        }
        /// <summary>
        /// Use to order 2 values from smallest to biggest
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static int Safe_SwitchValue(int value, int max, int min = 0, int extraspeed = 0)
        {
            if (max <= 0)
            {
                return value;
            }
            return ++value > max ? min : value + extraspeed;
        }
        public static int ToMinute(float minute) => (int)(ToSecond(60) * minute);
        public static int ToSecond(float second) => (int)(second * 60); public static void Item_DefaultToScroll(this Item Item, int width, int height, int buffType, int buffTime)
        {
            Item.width = width; Item.height = height;
            Item.buffType = buffType;
            Item.buffTime = buffTime;
            Item.useTime = Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }
    }
}

