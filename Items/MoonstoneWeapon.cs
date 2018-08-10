using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Moonstones.Items
{
	public class MoonstoneWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Rightclick to apply its modifier to your held weapon.");
            DisplayName.SetDefault("Moonstone (Weapons)");
        }

		public override void SetDefaults()
		{
			item.value = 10000;
			item.rare = 2;
			item.maxStack = 1;
            item.melee = true;
            item.ranged = true;
            item.magic = true;
            item.thrown = true;
            item.summon = true;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
            byte roll = (byte)rand.Next(1, 65);
            // Legendary = 81; Unreal = 82; Mythical = 83;
            // Therefore those are mapped to 62, 63, 64 (thus the + 19 in the following line)
            return roll <= 61 ? roll : roll + 19;
		}

        public override bool CanRightClick()
        {
            return true;
        }

		public override void RightClick(Player player)
		{
			Item item = player.HeldItem;
			if (item != null && item.damage > 0)
			{
                item.Prefix(this.item.prefix);
			}
			else
			{
				int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, this.item.type, 1, false, this.item.prefix, false, false);
				if (Main.netMode == 1)
				{
				NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
				}
			}
		}
	}
}