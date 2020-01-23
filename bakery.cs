using Bkrylib;
using System;
using System.Collections.Generic;

namespace Bakery
{
    public class Bakery
    {
        static void Main()
        {
            bool bake = true;
            Establishment Pierres = new Establishment();
            Pierres.AddItem("Muffin", "Just a muffin.", 2.50F, "muffins");
            Pierres.AddItem("Loaf", "A loaf of bread. Comes in different varieties.", 2F, "loaves");

            BakeAction threeForFive = () =>
            {
                Item loaf = Pierres.SearchItem("Loaf", Pierres.Tab.Cart);
                float indPrice = loaf.PricePer;
                loaf.Total = loaf.CountItemTotal();
                float o = 0;
                o = loaf.Amount / 3;
                loaf.Total = (loaf.Total - (1.0F * o));
                Console.WriteLine("---------------------------");
                Console.Write("Buy 3 loaves for 5$");
                if (o > 0)
                {
                    Console.Write(" applied " + o + " times!");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Write(" applied!");
                    Console.WriteLine("");
                }
                Console.WriteLine("---------------------------");
                Pierres.SearchItem("Loaf", Pierres.Tab.Cart).Total = loaf.Total;
            };

            BakeAction twoForOne = () =>
            {
                Item muffin = Pierres.SearchItem("Muffin", Pierres.Tab.Cart);
                float indPrice = muffin.PricePer;
                muffin.Total = muffin.CountItemTotal();
                Console.WriteLine(muffin.Total);
                float o = 0;

                o = muffin.Amount / 3;
                muffin.Total = (muffin.Total - (2.50F * o));
                Console.WriteLine("---------------------------");
                Console.Write("Buy 2 get one free on muffins");
                if (o > 0)
                {
                    Console.Write(" applied " + o + " times!");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Write(" applied!");
                    Console.WriteLine("");
                }
                Console.Write(muffin.Total);
                Console.WriteLine("---------------------------");
                Pierres.SearchItem("Muffin", Pierres.Tab.Cart).Total = muffin.Total;
            };

            Pierres.Coupons.Add(new Coupon(twoForOne, "Buy 2 get one free", "Savings of 2.50$ per coupon!", "muffin"));
            Pierres.Coupons.Add(new Coupon(threeForFive, "Get 3 loaves ", "Savings of 1$ per coupon!", "loaf"));

            Console.WriteLine("Welcome to Pierre's!");
            Help();
            while (bake == true)
            {
                Buffer(Pierres);
            }
        }
        public static void Help()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("type menu for our selection of baked goods.");
            Console.WriteLine("type 'cart' to see what you've already added to your order.");
            Console.WriteLine("type 'Add' then the item you'd like to order something.");
            Console.WriteLine("type 'push' to send the order.");
            Console.WriteLine("Lastly, type 'deals' to see our specials!");
            Console.WriteLine("--------------------------------------------------------");
        }

        public static void Buffer(Establishment bakery)
        {
            Console.WriteLine("");
            string input = Console.ReadLine();
            string[] inputchunks = input.Split(" ");
            string inputNoun = "";
            string inputVerb = "";

            for (int ct = 0; ct < inputchunks.Length; ct++)
            {
                if (ct == 0)
                {
                    inputVerb = inputchunks[0];
                }
                else if (ct > 0)
                {
                    inputNoun += inputchunks[ct];
                }
            }

            switch (inputVerb.ToLower())
            {
                case "deals":
                    foreach (Coupon d in bakery.Coupons)
                    {
                        Console.WriteLine(d.Tag + " on " + bakery.SearchItem(d.Target, bakery.ItemMenu).Tag);
                        Console.WriteLine(d.ItemDesc);
                    }
                    break;
                case "add":
                    if (bakery.SearchItem(inputNoun, bakery.ItemMenu) != null)
                    {
                        Item found = bakery.SearchItem(inputNoun, bakery.ItemMenu);
                        Console.WriteLine("How many?");
                        int quant = Convert.ToInt32(Console.ReadLine());
                        string printadd = "";
                        printadd += "Added ";
                        printadd += quant + " " + found.Tag;
                        if (quant >= 2)
                        {
                            printadd += "s";
                        }
                        else
                        {
                            printadd += "";
                        }
                        printadd += " to the cart.";
                        Console.Write(printadd);
                        bakery.ToCart(found, quant);
                    }
                    break;
                case "menu":
                    Console.WriteLine("Pierre's Menu:");
                    foreach (Item bakeItem in bakery.ItemMenu)
                    {
                        Console.WriteLine("--------------");
                        Console.WriteLine(bakeItem.Tag);
                        Console.WriteLine(bakeItem.ItemDesc);
                        Console.WriteLine("$" + bakeItem.PricePer);
                    }
                    break;
                case "cart":
                    Console.WriteLine("Your Cart:");
                    Console.WriteLine("--------------");
                    bakery.Tab.TotalAfterDeals = bakery.CountOrderTotal(true);
                    Console.WriteLine(bakery.Tab.TotalAfterDeals);
                    foreach (Item bakeItem in bakery.Tab.Cart)
                    {
                        Console.WriteLine(bakeItem.Amount);
                        if (bakeItem.Plural == null)
                        {
                            Console.WriteLine(bakeItem.Tag);
                        }
                        else
                        {
                            Console.WriteLine(bakeItem.Plural);
                        }
                        Console.WriteLine(bakeItem.ItemDesc);
                        Console.WriteLine("$" + bakeItem.PricePer + " per " + bakeItem.Tag);
                        Console.WriteLine("$" + bakeItem.Total + " for " + bakeItem.Amount + " " + bakeItem.Tag + "s");
                        Console.WriteLine("--------------");
                    }
                    Console.WriteLine("Total:");
                    Console.WriteLine("$" + bakery.Tab.TotalAfterDeals);
                    break;
                case "push":
                    bakery.History.Add(bakery.Tab);
                    Console.WriteLine("Sending.");
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("Order sent, form cleared.");
                    Console.WriteLine("Thank you for shopping with us! You may enter another order.");
                    //Not actually implemented lol
                    bakery.Tab = new Order();
                    break;
                case "help":
                    Help();
                    break;
                default:
                    Console.WriteLine("Wha?");
                    break;
            }
        }
    }
}