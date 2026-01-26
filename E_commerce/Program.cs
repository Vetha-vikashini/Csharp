using System;
using System.Collections.Generic;
class Shopping
{
    static Dictionary<int, string> products = new Dictionary<int, string>
    {
        {1,"Bag"},
        {2,"Watch"},
        {3,"Lipstick"},
        {4,"Kajal"},
        {5,"Bottle"}
    };
    static Dictionary<int, int> product_price = new Dictionary<int, int>
    {
        {1,500},
        {2,1500},
        {3,250},
        {4,100},
        {5,110}
    };

    Dictionary<int, int> cart = new Dictionary<int, int>();

    int total_bill = 0;

    public void welcome()
    {
        Console.WriteLine("Welcome to Shopping World");
        Console.WriteLine("Explore Our Products....");
        foreach (var item in Shopping.products)
        {
            Console.WriteLine(item.Key + ":" + item.Value);
        }
    }

    public int calculate(int id, int quan)
    {
        if (Shopping.products.ContainsKey(id))
        {
            int price = Shopping.product_price[id];
            return price * quan;
        }
        return 0;
    }

    public void UpdateCart(int id, int price)
    {
        if (Shopping.products.ContainsKey(id))
        {
            if (cart.ContainsKey(id))
            {
                cart[id] = price;
            }
            else
            {
                cart.Add(id, price);
            }
        }
        else
        {
            Console.WriteLine("Invalid Product");
        }
    }

    public void discount(ref int total_bill, int dis)
    {
        total_bill = total_bill - (total_bill * dis / 100);
    }

    public void checkavail(int id, int quan, out bool avail, out string msg)
    {
        if (Shopping.products.ContainsKey(id))
        {

            if (quan < 10)
            {
                avail = true;
                msg = "This item  can be Placed";
            }
            else
            {
                avail = false;
                msg = "Out of stock";
            }
        }
        else
        {
            avail = false;
            msg = "Product not available";

        }
    }
    public static void Main(String[] args)
    {
        Shopping obj = new Shopping();
        obj.welcome();
        while (true)
        {
            Console.WriteLine("Shop or Exit");
            string choice = Console.ReadLine();
            if (choice == "Exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Enter the product ID");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Quantity");
                int quan = int.Parse(Console.ReadLine());
                bool avail;
                string msg;
                obj.checkavail(id, quan, out avail, out msg);
                if (avail)
                {
                    obj.UpdateCart(id, quan);
                    Console.WriteLine("Your Cart ");
                    foreach (var item in obj.cart)
                    {
                        Console.WriteLine(item.Key + "--" + item.Value);
                    }
                    int total = obj.calculate(id, quan);
                    obj.total_bill += total;
                    Console.WriteLine(msg);
                }

                else
                {
                    Console.WriteLine(msg);
                }
            }


        }
        Console.WriteLine("Your BirthDate");
        int dis = int.Parse(Console.ReadLine());
        Console.WriteLine($"Your total bill Amount {obj.total_bill}");
        obj.discount(ref obj.total_bill, dis);
        Console.WriteLine($"After Discout: Your total bill Amount {obj.total_bill}");



    }
}

