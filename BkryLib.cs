using System;
using System.Collections.Generic;

namespace Bkrylib
{
    public delegate void BakeAction();
    public class Establishment
    {
        public List<Coupon> Coupons;
        public List<Item> ItemMenu;
        public Order Tab = new Order();
        public List<Order> History = new List<Order>();

        public Establishment()
        {
            ItemMenu = new List<Item>();
            Coupons = new List<Coupon>();
        }

        public Item SearchItem(string searchTerm, List<Item> searchList)
        {
            searchTerm = searchTerm.ToLower();
            return searchList.Find(thing => thing.Tag.ToLower().Contains(searchTerm.ToLower()));
        }

        public float CountOrderTotal(bool usingCpns = false)
        {
            float workingTotal = 0;
            foreach (Item thing in Tab.Cart)
            {
                if (usingCpns)
                {
                    DeployDeals(thing);
                    workingTotal += thing.Total;
                }
            }
            return workingTotal;
        }

        public void DeployDeals(Item dealtWith)
        {
            foreach (Coupon thisCpn in Coupons)
            {
                if (dealtWith.Tag.ToLower() == thisCpn.Target.ToLower())
                {
                    thisCpn.Deal();
                }
            }
        }

        public void Pushorder()
        {
            History.Add(Tab);
        }

        public void AddItem(string name, string itemDesc, float individualPrice, string Plural = null)
        {
            ItemMenu.Add(new Item(name, itemDesc, individualPrice, 1));
        }

        public void ToCart(Item added, int Amount)
        {
            Item tempQuant = added;
            if (Tab.Cart.Contains(added))
            {
                Item foundInOrder = SearchItem(added.Tag, Tab.Cart);
                foundInOrder.Amount += Amount;
            }
            else
            {
                tempQuant.Amount = Amount;
                Tab.Cart.Add(added);
            }
        }
    }

    public class Order
    {
        public List<Item> Cart;
        public float TotalAfterDeals;
        public Order()
        {
            Cart = new List<Item>();
            TotalAfterDeals = 0;
        }
    }
    public class Item
    {
        public string Tag;
        public float PricePer;
        public int Amount;
        public float Total;
        public string ItemDesc;
        public string Plural;

        public Item(string name, string itemDesc, float individualPrice, int quantity = 1)
        {
            Tag = name;
            PricePer = individualPrice;
            Amount = quantity;
            Total = PricePer * quantity;
            ItemDesc = itemDesc;
        }

        public Item(string name, string itemDesc, float individualPrice, int quantity = 1, string plural = "")
        {
            Tag = name;
            PricePer = individualPrice;
            Amount = quantity;
            Total = PricePer * quantity;
            ItemDesc = itemDesc;
            Plural = plural;
        }

        public float CountItemTotal()
        {
            return PricePer * Amount;
        }
    }
    public class Coupon
    {
        public BakeAction Deal;
        public bool Done;
        public string Tag;
        public string ItemDesc;
        public string Target;

        public Coupon(BakeAction tDeal, string name, string itemDesc, string appliesTo)
        {
            Tag = name;
            ItemDesc = itemDesc;
            Done = false;
            Deal = tDeal;
            Target = appliesTo;
        }
    }
}