using MailSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Models
{
    /// <summary>
    /// Create a class for Cart Model
    /// </summary>
    public class CartModel
    {
        /// <summary>
        /// To add customer details
        /// </summary>
        /// <param name="email">customer email</param>
        /// <param name="productDetails">product details</param>
        /// <returns></returns>
        public bool AddCartDetais(string email, string productDetails)
        {
            bool isSuccess = false;
            try
            {
                using (var entities = new DataEntities())
                {
                    CartDetail cartDetail = new CartDetail();
                    cartDetail.CustomerEmail = email;
                    cartDetail.ProductDetails = productDetails;
                    cartDetail.CreatedDate = DateTime.Now;
                    cartDetail.IsActive = true;
                    entities.CartDetails.Add(cartDetail);
                    entities.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isSuccess;
        }
    }
}