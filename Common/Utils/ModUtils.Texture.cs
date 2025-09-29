using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModJam2.Texture;

namespace ModJam2.Common.Utils {
	public static partial class ModUtils {
		public static Rectangle GetSource(this Texture2D texture, int verticalFrames, int index) {
			int frameHeight = texture.Height / verticalFrames;
			return new Rectangle(0, (index % verticalFrames) * frameHeight, texture.Width, frameHeight);
		}
		public static string GetTheSameTextureAsEntity<T>() where T : class {
			var type = typeof(T);
			string NameSpace = type.Namespace;
			if (NameSpace == null) {
				return ModTexture.MissingTexture_Default;
			}
			return NameSpace.Replace(".", "/") + "/" + type.Name;
		}
		public static string GetTheSameTextureAs<T>(string altName = "") where T : class {
			var type = typeof(T);
			if (string.IsNullOrEmpty(altName)) {
				altName = type.Name;
			}
			string NameSpace = type.Namespace;
			if (NameSpace == null) {
				return ModTexture.MissingTexture_Default;
			}
			return NameSpace.Replace(".", "/") + "/" + altName;
		}
		public static string GetVanillaTexture<T>(int EntityType) where T : class => $"Terraria/Images/{typeof(T).Name}_{EntityType}";
	}
}
