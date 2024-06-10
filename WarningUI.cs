using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace NoMoreAgroRunner
{
	public class WarningUI : UIState
	{
		private CustomUIText warningText;
		private CustomUIText distanceWarningText;
		private CustomUIText distanceText;

		public override void OnInitialize()
		{
			try
			{
				warningText = new CustomUIText("WARNING", 5f);
				warningText.HAlign = 0.5f;
				warningText.Top.Set(50, 0f);
				Append(warningText);

				distanceWarningText = new CustomUIText("Too far from nearest player", 1f, Color.Red);
				distanceWarningText.HAlign = 0.5f;
				distanceWarningText.Top.Set(50 + warningText.Height.Pixels, 0f);
				Append(distanceWarningText);

				distanceText = new CustomUIText("Distance: 0.0 tiles", 1.5f, Color.Red);
				distanceText.HAlign = 0.5f;
				distanceText.Top.Set(distanceWarningText.Top.Pixels + distanceWarningText.Height.Pixels + 10, 0f);
				Append(distanceText);
				
				Recalculate();
			}
			catch (Exception e)
			{
				ModContent.GetInstance<NoMoreAgroRunner>().Logger.Error($"Error initializing WarningUI: {e}");
			}
		}

		public void UpdateWarning(float distance, float warningTimer)
		{
			try
			{
				float hue = warningTimer / 10f % 1f;
				Color textColor = Color.Lerp(Color.Red, Color.Yellow, hue);

				if (warningText != null)
				{
					warningText.SetTextColor(textColor);
				}

				if (distanceText != null)
				{
					distanceText.SetText($"Distance: {distance / 16:F1} tiles");
				}
			}
			catch (Exception e)
			{
				ModContent.GetInstance<NoMoreAgroRunner>().Logger.Error($"Error updating WarningUI: {e}");
			}
		}
	}
}