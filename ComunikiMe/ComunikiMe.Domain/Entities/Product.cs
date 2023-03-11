using ComunikiMe.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Entities
{
    public class Product : Base
    {
        public Product(string name, string description, decimal price, int stock, string image)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(name, "Name", "The 'Name' field cannot be empty!")
                .IsNotEmpty(description, "Description", "The 'Description' field cannot be empty!")
                .IsNotNull(price, "Price", "The 'Price' field cannot be null!")
                .IsNotNull(stock, "Stock", "The 'Stock' field cannot be null!")
                .IsNotEmpty(image, "Image", "The 'Image' field cannot be empty!")
            );

            if (IsValid)
            {
                Name = name;
                Description = description;
                Price = price;
                Stock = stock;
                Image = image;
                ModifyDate = null;
            }
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        public DateTime? ModifyDate { get; set; }

        //Compositions
        public IReadOnlyCollection<Cart> Carts { get; private set; }
        private List<Cart> _cart { get; set; }

        // Configs:
        public void UpdateProduct(string name, string description, decimal price, int stock, string image)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(name, "Name", "The 'Name' field cannot be empty!")
                .IsNotEmpty(description, "Description", "The 'Description' field cannot be empty!")
                .IsNotNull(price, "Price", "The 'Price' field cannot be null!")
                .IsNotNull(stock, "Stock", "The 'Stock' field cannot be null!")
                .IsNotEmpty(image, "Image", "The 'Image' field cannot be empty!")
            );

            if (IsValid)
            {
                Name = name;
                Description = description;
                Price = price;
                Stock = stock;
                Image = image;
                ModifyDate = DateTime.Now;
            }
        }
    }
}
