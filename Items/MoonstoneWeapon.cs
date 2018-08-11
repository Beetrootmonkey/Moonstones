using Microsoft.Xna.Framework;
using System.Collections.Generic;
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
            item.damage = 5;
            item.crit = 5;
            item.mana = 5;
            item.useTime = 5;
            item.knockBack = 5;
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
            Item obj1 = new Item();
            obj1.netDefaults(heldItem.netID);
            Item obj2 = obj1.CloneWithModdedDataFrom(heldItem);
            obj2.Prefix(item.prefix);
            return !heldItem.IsAir && heldItem.damage > 0 && obj2.prefix == item.prefix && heldItem.prefix != item.prefix;
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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            List<TooltipLine> toRemove = new List<TooltipLine>();
            tooltips.ForEach(t =>
            {
                if (t.Name == "Damage" || t.Name == "CritChance" || t.Name == "Knockback" || t.Name == "UseMana" ||
                    t.Name.StartsWith("Prefix"))
                {
                    toRemove.Add(t);
                }
            });
            toRemove.ForEach(t => tooltips.Remove(t));

            //tooltips.ForEach(t => t.text = t.Name);


            int prefix = item.prefix;
            float damageMult = 1f;
            float knockbackMult = 1f;
            float useTimeMult = 1f;
            float scaleMult = 1f;
            float shootSpeedMult = 1f;
            float manaMult = 1f;
            int critBonus = 0;

            if (prefix == 1)
                scaleMult = 1.12f;
            else if (prefix == 2)
                scaleMult = 1.18f;
            else if (prefix == 3)
            {
                damageMult = 1.05f;
                critBonus = 2;
                scaleMult = 1.05f;
            }
            else if (prefix == 4)
            {
                damageMult = 1.1f;
                scaleMult = 1.1f;
                knockbackMult = 1.1f;
            }
            else if (prefix == 5)
                damageMult = 1.15f;
            else if (prefix == 6)
                damageMult = 1.1f;
            else if (prefix == 81)
            {
                knockbackMult = 1.15f;
                damageMult = 1.15f;
                critBonus = 5;
                useTimeMult = 0.9f;
                scaleMult = 1.1f;
            }
            else if (prefix == 7)
                scaleMult = 0.82f;
            else if (prefix == 8)
            {
                knockbackMult = 0.85f;
                damageMult = 0.85f;
                scaleMult = 0.87f;
            }
            else if (prefix == 9)
                scaleMult = 0.9f;
            else if (prefix == 10)
                damageMult = 0.85f;
            else if (prefix == 11)
            {
                useTimeMult = 1.1f;
                knockbackMult = 0.9f;
                scaleMult = 0.9f;
            }
            else if (prefix == 12)
            {
                knockbackMult = 1.1f;
                damageMult = 1.05f;
                scaleMult = 1.1f;
                useTimeMult = 1.15f;
            }
            else if (prefix == 13)
            {
                knockbackMult = 0.8f;
                damageMult = 0.9f;
                scaleMult = 1.1f;
            }
            else if (prefix == 14)
            {
                knockbackMult = 1.15f;
                useTimeMult = 1.1f;
            }
            else if (prefix == 15)
            {
                knockbackMult = 0.9f;
                useTimeMult = 0.85f;
            }
            else if (prefix == 16)
            {
                damageMult = 1.1f;
                critBonus = 3;
            }
            else if (prefix == 17)
            {
                useTimeMult = 0.85f;
                shootSpeedMult = 1.1f;
            }
            else if (prefix == 18)
            {
                useTimeMult = 0.9f;
                shootSpeedMult = 1.15f;
            }
            else if (prefix == 19)
            {
                knockbackMult = 1.15f;
                shootSpeedMult = 1.05f;
            }
            else if (prefix == 20)
            {
                knockbackMult = 1.05f;
                shootSpeedMult = 1.05f;
                damageMult = 1.1f;
                useTimeMult = 0.95f;
                critBonus = 2;
            }
            else if (prefix == 21)
            {
                knockbackMult = 1.15f;
                damageMult = 1.1f;
            }
            else if (prefix == 82)
            {
                knockbackMult = 1.15f;
                damageMult = 1.15f;
                critBonus = 5;
                useTimeMult = 0.9f;
                shootSpeedMult = 1.1f;
            }
            else if (prefix == 22)
            {
                knockbackMult = 0.9f;
                shootSpeedMult = 0.9f;
                damageMult = 0.85f;
            }
            else if (prefix == 23)
            {
                useTimeMult = 1.15f;
                shootSpeedMult = 0.9f;
            }
            else if (prefix == 24)
            {
                useTimeMult = 1.1f;
                knockbackMult = 0.8f;
            }
            else if (prefix == 25)
            {
                useTimeMult = 1.1f;
                damageMult = 1.15f;
                critBonus = 1;
            }
            else if (prefix == 58)
            {
                useTimeMult = 0.85f;
                damageMult = 0.85f;
            }
            else if (prefix == 26)
            {
                manaMult = 0.85f;
                damageMult = 1.1f;
            }
            else if (prefix == 27)
                manaMult = 0.85f;
            else if (prefix == 28)
            {
                manaMult = 0.85f;
                damageMult = 1.15f;
                knockbackMult = 1.05f;
            }
            else if (prefix == 83)
            {
                knockbackMult = 1.15f;
                damageMult = 1.15f;
                critBonus = 5;
                useTimeMult = 0.9f;
                manaMult = 0.9f;
            }
            else if (prefix == 29)
                manaMult = 1.1f;
            else if (prefix == 30)
            {
                manaMult = 1.2f;
                damageMult = 0.9f;
            }
            else if (prefix == 31)
            {
                knockbackMult = 0.9f;
                damageMult = 0.9f;
            }
            else if (prefix == 32)
            {
                manaMult = 1.15f;
                damageMult = 1.1f;
            }
            else if (prefix == 33)
            {
                manaMult = 1.1f;
                knockbackMult = 1.1f;
                useTimeMult = 0.9f;
            }
            else if (prefix == 34)
            {
                manaMult = 0.9f;
                knockbackMult = 1.1f;
                useTimeMult = 1.1f;
                damageMult = 1.1f;
            }
            else if (prefix == 35)
            {
                manaMult = 1.2f;
                damageMult = 1.15f;
                knockbackMult = 1.15f;
            }
            else if (prefix == 52)
            {
                manaMult = 0.9f;
                damageMult = 0.9f;
                useTimeMult = 0.9f;
            }
            else if (prefix == 36)
                critBonus = 3;
            else if (prefix == 37)
            {
                damageMult = 1.1f;
                critBonus = 3;
                knockbackMult = 1.1f;
            }
            else if (prefix == 38)
                knockbackMult = 1.15f;
            else if (prefix == 53)
                damageMult = 1.1f;
            else if (prefix == 54)
                knockbackMult = 1.15f;
            else if (prefix == 55)
            {
                knockbackMult = 1.15f;
                damageMult = 1.05f;
            }
            else if (prefix == 59)
            {
                knockbackMult = 1.15f;
                damageMult = 1.15f;
                critBonus = 5;
            }
            else if (prefix == 60)
            {
                damageMult = 1.15f;
                critBonus = 5;
            }
            else if (prefix == 61)
                critBonus = 5;
            else if (prefix == 39)
            {
                damageMult = 0.7f;
                knockbackMult = 0.8f;
            }
            else if (prefix == 40)
                damageMult = 0.85f;
            else if (prefix == 56)
                knockbackMult = 0.8f;
            else if (prefix == 41)
            {
                knockbackMult = 0.85f;
                damageMult = 0.9f;
            }
            else if (prefix == 57)
            {
                knockbackMult = 0.9f;
                damageMult = 1.18f;
            }
            else if (prefix == 42)
                useTimeMult = 0.9f;
            else if (prefix == 43)
            {
                damageMult = 1.1f;
                useTimeMult = 0.9f;
            }
            else if (prefix == 44)
            {
                useTimeMult = 0.9f;
                critBonus = 3;
            }
            else if (prefix == 45)
                useTimeMult = 0.95f;
            else if (prefix == 46)
            {
                critBonus = 3;
                useTimeMult = 0.94f;
                damageMult = 1.07f;
            }
            else if (prefix == 47)
                useTimeMult = 1.15f;
            else if (prefix == 48)
                useTimeMult = 1.2f;
            else if (prefix == 49)
                useTimeMult = 1.08f;
            else if (prefix == 50)
            {
                damageMult = 0.8f;
                useTimeMult = 1.15f;
            }
            else if (prefix == 51)
            {
                knockbackMult = 0.9f;
                useTimeMult = 0.9f;
                damageMult = 1.05f;
                critBonus = 2;
            }


            string colorPositive = getColorAsString(new Color(114, 181, 114) * ((float)Main.mouseTextColor / (float)byte.MaxValue));
            string colorNegative = getColorAsString(new Color(181, 114, 114) * ((float)Main.mouseTextColor / (float)byte.MaxValue));
            int index = tooltips.FindIndex(x => x.Name == "Tooltip0");

            if (index >= 0)
            {
                index++;
                if (scaleMult != 1)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipSize",
                        string.Format("[{0}{1}{2}% size]",
                        scaleMult > 1 ? colorPositive : colorNegative,
                        scaleMult > 1 ? "+" : "",
                        (int)((scaleMult - 1) * 100))));
                }
                if (damageMult != 1)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipDamage",
                        string.Format("[{0}{1}{2}% damage]",
                        damageMult > 1 ? colorPositive : colorNegative,
                        damageMult > 1 ? "+" : "",
                        (int)((damageMult - 1) * 100))));
                }
                if (critBonus != 0)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipCrit",
                        string.Format("[{0}{1}{2}% critical strike chance]",
                        critBonus > 1 ? colorPositive : colorNegative,
                        critBonus > 1 ? "+" : "",
                        critBonus)));
                }
                if (knockbackMult != 1)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipKnockback",
                        string.Format("[{0}{1}{2}% knockback]",
                        knockbackMult > 1 ? colorPositive : colorNegative,
                        knockbackMult > 1 ? "+" : "",
                        (int)((knockbackMult - 1) * 100))));
                }
                if (manaMult != 1)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipMana",
                        string.Format("[{0}{1}{2}% mana usage]",
                        manaMult > 1 ? colorNegative : colorPositive,
                        manaMult > 1 ? "+" : "",
                        (int)((manaMult - 1) * 100))));
                }
                if (useTimeMult != 1)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipUseTime",
                        string.Format("[{0}{1}{2}% use time]",
                        useTimeMult > 1 ? colorNegative : colorPositive,
                        useTimeMult > 1 ? "+" : "",
                        (int)((useTimeMult - 1) * 100))));
                }
                if (shootSpeedMult != 1)
                {
                    tooltips.Insert(index, new TooltipLine(this.mod, "TooltipVelocity",
                        string.Format("[{0}{1}{2}% velocity]",
                        shootSpeedMult > 1 ? colorPositive : colorNegative,
                        shootSpeedMult > 1 ? "+" : "",
                        (int)((shootSpeedMult - 1) * 100))));
                }
            }

        }

        private string getColorAsString(Color color)
        {
            return string.Format("c/{0:X2}{1:X2}{2:X2}:", color.R, color.G, color.B);
        }
    }
}