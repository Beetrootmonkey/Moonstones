using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Moonstones.Items
{
	public class MoonstoneAccessory: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Rightclick to apply its modifier to your held accessory.");
            DisplayName.SetDefault("Moonstone (Accessories)");
        }

		public override void SetDefaults()
        {
            item.value = 10000;
			item.rare = 2;
			item.maxStack = 1;
            //item.accessory = true;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
		    return (byte)rand.Next(62, 81);
		}

        private int GetIndexInArray(object[] arr, object obj)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(obj))
                {
                    return i;
                }
            }

            return -1;
        }

        public override bool CanRightClick()
        {
            Item heldItem = Main.LocalPlayer.HeldItem;
            return !heldItem.IsAir && heldItem.accessory && heldItem.prefix != item.prefix;
        }

        public override void RightClick(Player player)
		{
            Item item = player.HeldItem;
            bool favorited = item.favorited;
            int stack = item.stack;
            Item obj1 = new Item();
            obj1.netDefaults(item.netID);
            Item obj2 = obj1.CloneWithModdedDataFrom(item);
            obj2.Prefix(this.item.prefix);
            int index = GetIndexInArray(player.inventory, item);
            item = obj2.Clone();
            item.position.X = player.position.X + (float)(player.width / 2) - (float)(item.width / 2);
            item.position.Y = player.position.Y + (float)(player.height / 2) - (float)(item.height / 2);
            item.favorited = favorited;
            item.stack = stack;
            player.inventory[index] = item;
            ItemLoader.PostReforge(item);
            ItemText.NewText(item, item.stack, true, false);
            Main.PlaySound(SoundID.Item37, -1, -1);
        }
	}
}