using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApp.WebUI.Models.Carts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.WebUI.Models.Orders
{
    public class OrderModel
    {
        public OrderModel()
        {
            Months = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
            Years = new List<SelectListItem>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string CartName { get; set; }

        [DataType(DataType.CreditCard)]
        public string CartNumber { get; set; }

        public int ExpirationMonthId { get; set; }
        public IList<SelectListItem> Months { get; set; }

        public int CityId { get; set; }
        public IList<SelectListItem> Cities { get; set; }

        public int ExpirationYearId { get; set; }
        public IList<SelectListItem> Years { get; set; }

        public string CVV { get; set; }

        //Sipariş Detayı
        public CartModel CartModel { get; set; }
    }
}
