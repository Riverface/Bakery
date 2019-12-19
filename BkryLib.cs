using System;
using System.Collections.Generic;
namespace Bkrylib
{
    public delegate void bakeAction();
    public class Establishment
    {
        public Item SearchItem(string thisinput, List<Item> curlist)
        {
            thisinput = thisinput.ToLower();
            return curlist.Find(thing => thing.name.ToLower().Contains(thisinput.ToLower()));
        }
        public float total(bool usingcpns = false)
        {
            float workingtotal = 0;
            foreach (Item thing in curOrder.order)
            {
                if (usingcpns)
                {
                
                DeployDeals(thing);
                workingtotal += thing.total();
                }
            }
            
            return workingtotal;
        }
        public void DeployDeals(Item dealtwith){
                    foreach (Coupon thiscpn in coupons)
                    {
                        if (dealtwith.name.ToLower() == thiscpn.appliesto.ToLower())
                        {
                            
                            thiscpn.deal();

                        }
                    }
        }
        public List<Coupon> coupons;
        public List<Item> itemMenu;
        public Order curOrder = new Order();
        public List<Order> history = new List<Order>();
        public Establishment()
        {
            itemMenu = new List<Item>();
            coupons = new List<Coupon>();
        }
        public void Pushorder()
        {
          history.Add(curOrder);  
        }
        public void AddItem(string Name, string Description, float Priceper, string Plural = null)
        {
            itemMenu.Add(new Item(Name, Description, Priceper, 1));
        }

        public void ToCart(Item added, int quantity)
        {
            Item tempquant = added;
            if (curOrder.order.Contains(added))
            {
                Item foundinorder = SearchItem(added.name, curOrder.order);
                foundinorder.quantity += quantity;
            }
            else
            {
                tempquant.quantity = quantity;
                curOrder.order.Add(added);
            }

        }
    }

    public class Order
    {
        public List<Item> order;
        public float totalafterdeals;

        public Order()
        {
            order = new List<Item>();
         totalafterdeals = 0;

        }
        
    }
    public class Item
    {
        public string name;
        public float pricePer;
        public int quantity;
        public float totalprice;
        public string description;
        public string plural;
        public float TpriceAfterDeals;
        public Item(string Name, string Description, float Priceper, int Quantity = 1)
        {
            name = Name;
            pricePer = Priceper;
            quantity = Quantity;
            totalprice = pricePer * Quantity;
            description = Description;
            
        }
        public float total()
        {
            TpriceAfterDeals = totalprice;
            return pricePer * quantity;
        }
        public Item(string Name, string Description, float Priceper, int Quantity = 1, string Plural = "")
        {
            name = Name;
            pricePer = Priceper;
            quantity = Quantity;
            totalprice = pricePer * Quantity;
            description = Description;
            plural = Plural;
        }
    }
    public class Coupon
    {
        public bakeAction deal;
        public bool done;
        public string name;
        public string description;
        public string appliesto;
        public Coupon(bakeAction Tdeal, string Name, string Description, string Appliesto)
        {
            name = Name;
            description = Description;
            done = false;
            deal = Tdeal;
            appliesto = Appliesto;
        }
    }
}