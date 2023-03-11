using ComunikiMe.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Commands.Products
{
    public class CreateProductCommand : Notifiable<Notification>, ICommand
    {
        public CreateProductCommand() { }

        public CreateProductCommand(string name, string description, decimal price, int stock, string image)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(Name, "Name", "The 'Name' field cannot be empty!")
                .IsNotEmpty(Description, "Description", "The 'Description' field cannot be empty!")
                .IsNotNull(Price, "Price", "The 'Price' field cannot be null!")
                .IsNotNull(Stock, "Stock", "The 'Stock' field cannot be null!")
                .IsNotEmpty(Image, "Image", "The 'Image' field cannot be empty!")
            );
        }
    }
}
