using PizzaWebSite.Models.Helpers;
using PizzaWebSite.Models.PizzaCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace PizzaWebSite.Controllers
{
    public class CartController : Controller
    {
        public static void ResetCart(Order cart)
        {
            cart = new Order();
        }

        public static void Add(int pizzaId, string selectedToppingString, Order cart)
        {
            OrderItem order = new OrderItem();

            order.Pizza = DatabaseContextHelper.Db.Pizzas.Find(pizzaId);

            order.Price += order.Pizza.Price;

            if (selectedToppingString != null)
            {
                var selectedToppings = selectedToppingString.Split(',');

                foreach (string toppingId in selectedToppings)
                {
                    Topping topping = DatabaseContextHelper.Db.Toppings.Find(Convert.ToInt32(toppingId));

                    order.ExtraToppings.Add(topping);
                    order.Price += topping.Price;
                }
            }

            cart.OrderItems.Add(order);

            AddDealToCart(cart.CurrentVoucher, cart);
        }

        public static void Remove(Pizza pizza, Order cart)
        {
            var itemToRemove = cart.OrderItems.Single(r => r.Pizza.PizzaId == pizza.PizzaId);

            cart.OrderItems.Remove(itemToRemove);
        }

        public static void Submit(string userId, Order cart)
        {
            cart.UserId = userId;

            foreach (var item in cart.OrderItems)
            {
                cart.Price += item.Price;
            }

            DatabaseContextHelper.Db.Orders.Add(cart);
            DatabaseContextHelper.Db.SaveChanges();
        }

        public static void ApplyDelivery(bool Delivery, Order cart)
        {
            cart.Delivery = Delivery;
        }

        public static void ReorderOrder(int orderId, Order currentCart)
        {
            ResetCart(SessionHelper.Current.Cart);
            var cartToAdd = DatabaseContextHelper.Db.Orders.Find(orderId);

            foreach (var item in cartToAdd.OrderItems)
            {
                currentCart.OrderItems.Add(new OrderItem
                {
                    Pizza = item.Pizza,
                    ExtraToppings = item.ExtraToppings,
                    UserId = item.UserId,
                    Price = item.Price
                });
            }
        }

        public static void AddDealToCart(string voucher, Order cart)
        {
            switch (voucher)
            {
                case "2FOR1TUE":
                    if (cart.OrderItems.Count == 2)
                    {
                        var validPizzas = cart.OrderItems.Where(o => o.Pizza.Size == "medium" || o.Pizza.Size == "large").Count();

                        if (validPizzas == 2)
                        {
                            decimal price = 0;

                            if (cart.OrderItems[0].Price > cart.OrderItems[1].Price)
                            {
                                price = cart.OrderItems[1].Price;
                            } else
                            {
                                price = cart.OrderItems[0].Price;
                            }

                            cart.Discount = price;
                            cart.CurrentVoucher = voucher;
                        }
                        
                    }
                    break;
                case "3FOR2THUR":

                    break;
                case "FAMFRIDAYCOLL":

                    break;
                case "2LARGECOLL":

                    break;
                case "2MEDIUMCOLL":

                    break;
                case "2SMALLCOLL":

                    break;
                default:
                    cart.Discount = 0;
                    cart.CurrentVoucher = "";
                    break;
            }
        }
    }
}