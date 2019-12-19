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
            Pierres.AddItem("Muffin", "Just a muffin.", 5F, "muffins");
            Pierres.AddItem("Loaf", "A loaf of bread. Comes in different varieties.", 2F, "loaves");
            bakeAction threeforfive = () =>
            {

                Item loaf = Pierres.SearchItem("Loaf", Pierres.curOrder.order);
                float indPrice = loaf.pricePer;
                loaf.totalprice = loaf.total();
                float o = 0;
                if (loaf.quantity % 3 == 0)
                {
                    o = loaf.quantity / 3;
                    loaf.totalprice = (loaf.totalprice - (1.0F * o));      
                }
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
                Pierres.SearchItem("Loaf", Pierres.curOrder.order).totalprice = loaf.totalprice;
            };
            bakeAction twoForOne = () =>
            {
                Item muffin = Pierres.SearchItem("Muffin", Pierres.curOrder.order);
                float indPrice = muffin.pricePer;
                muffin.totalprice = muffin.total();
                float o = 0;
                if (muffin.quantity % 3 == 0)
                {
                    o = muffin.quantity / 3;
                    muffin.totalprice = (muffin.totalprice - (2.50F * o));
                }
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
                Console.WriteLine("---------------------------");
                Pierres.SearchItem("Muffin", Pierres.curOrder.order).totalprice = muffin.totalprice;
            };
            Pierres.coupons.Add(new Coupon(twoForOne, "Buy 2 get one free", "Savings of 2.50$ per coupon!", "muffin"));
            Pierres.coupons.Add(new Coupon(threeforfive, "Get 3 loaves ", "Savings of 1$ per coupon!", "loaf"));
            Console.WriteLine("Welcome to Pierre's!");
            Console.WriteLine("------------");
            Console.WriteLine("type menu for our selection of baked goods.");
            Console.WriteLine("type 'cart' to see what you've already added to your order.");
            Console.WriteLine("type 'Add' then the item you'd like to order something.");
            Console.WriteLine("type 'checkout' to send the order.");
            Console.WriteLine("------------");
            while (bake == true)
            {
                Buffer(Pierres);
            }
        }

        public static void Help(){

        }
        public static bool Lengthchk(string[] inputs, int expectedmin, int expectedmax)
        {
            if (inputs.Length == expectedmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Buffer(Establishment bakery)
        {
            Console.WriteLine("");
            string input = Console.ReadLine();
            string[] inputchunks = input.Split(" ");
            string inputnoun = "";
            string inputverb = "";
            for (int ct = 0; ct < inputchunks.Length; ct++)
            {
                if (ct == 0)
                {
                    inputverb = inputchunks[0];
                }
                else if (ct > 0)
                {
                    inputnoun += inputchunks[ct];
                }
            }

            switch (inputverb.ToLower())
            {
                case "deals":
                    foreach (Coupon d in bakery.coupons)
                    {
                        Console.WriteLine(d.name + " on " + bakery.SearchItem(d.appliesto, bakery.itemMenu).name);
                        Console.WriteLine(d.description);
                    }
                    break;
                case "add":
                    if( bakery.SearchItem(inputnoun, bakery.itemMenu ) != null){
                    Item found = bakery.SearchItem(inputnoun, bakery.itemMenu);
                    Console.WriteLine("How many?");
                    int quant = Convert.ToInt32(Console.ReadLine());
                    string printadd = "";
                    printadd += "Added ";
                    printadd += quant + " " + found.name;
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
                    foreach (Item bakeitem in bakery.itemMenu)
                    {
                    Console.WriteLine("--------------");
                        Console.WriteLine(bakeitem.name);
                        Console.WriteLine(bakeitem.description);
                        Console.WriteLine("$" + bakeitem.pricePer);
                    }
                    break;
                case "cart":
                    Console.WriteLine("Your Cart:");
                    Console.WriteLine("--------------");
                    bakery.curOrder.totalafterdeals = bakery.total(true);
                    foreach (Item bakeitem in bakery.curOrder.order)
                    {
                        Console.WriteLine(bakeitem.quantity);
                        if (bakeitem.plural == null)
                        {
                            Console.WriteLine(bakeitem.name);
                        }
                        else
                        {
                            Console.WriteLine(bakeitem.plural);
                        }
                        Console.WriteLine(bakeitem.description);
                        Console.WriteLine("$" + bakeitem.pricePer+" per "+ bakeitem.name);
                        Console.WriteLine("$" + bakeitem.totalprice + " for " + bakeitem.quantity +" "+ bakeitem.name+"s");

                        Console.WriteLine("--------------");
                    }
                        Console.WriteLine("Total:");
                        Console.WriteLine("$" + bakery.curOrder.totalafterdeals);
                    break;
                case "push":
                bakery.history.Add(bakery.curOrder);
                Console.WriteLine("Order sent! Use 'history' to view this order.");
                //Not actually implemented lol
                bakery.curOrder = new Order();
                    break;
            }
        }
    }

}