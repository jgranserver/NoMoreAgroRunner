using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;

namespace NoMoreAgroRunner
{
    public class WarningDrawLayer : PlayerDrawLayer
	{
		public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => true;

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			var modPlayer = drawInfo.drawPlayer.GetModPlayer<NoMoreAgroRunnerPlayer>();

			if (modPlayer.ShowWarning)
			{
				DynamicSpriteFont font = FontAssets.MouseText.Value;
				string warningText = "WARNING";
				string distanceWarning = "Too far from nearest player";
				string distanceText = $"Distance: {modPlayer.DistanceToNearestPlayer / 16:F1} tiles";

				// Calculate color change effect
				float hue = modPlayer.WarningTimer / 10f % 1f;
				Color textColor = Color.Lerp(Color.Red, Color.Yellow, hue);

				// Measure text sizes and positions
				Vector2 warningSize = font.MeasureString(warningText) * 5f;
				Vector2 warningPosition = new Vector2((Main.screenWidth - warningSize.X) / 2, 50);
				Vector2 distanceWarningPosition = new Vector2((Main.screenWidth - font.MeasureString(distanceWarning).X) / 2, warningPosition.Y + warningSize.Y);
				Vector2 distanceTextPosition = new Vector2((Main.screenWidth - font.MeasureString(distanceText).X) / 2 - 35, distanceWarningPosition.Y + font.MeasureString(distanceWarning).Y);

				// Draw text
				Main.spriteBatch.DrawString(font, warningText, warningPosition, textColor, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0);
				Main.spriteBatch.DrawString(font, distanceWarning, distanceWarningPosition, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
				Main.spriteBatch.DrawString(font, distanceText, distanceTextPosition, Color.Red, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
			}
		}
	}
}
