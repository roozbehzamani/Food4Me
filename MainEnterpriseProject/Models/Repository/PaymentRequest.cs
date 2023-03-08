using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class PaymentRequest
    {
        public PaymentRequest()
        {
            MultiplexingData = new MultiplexingData();
        }

        public string TerminalId { get; set; }
        public string MerchantId { get; set; }

        [Required(ErrorMessage = "مبلغ اجباری است ")]
        [Display(Name = @"مبلغ")]
        public long Amount { get; set; }
        public string OrderId { get; set; }
        public string AdditionalData { get; set; }
        public DateTime LocalDateTime { get; set; }
        public string ReturnUrl { get; set; }
        public string SignData { get; set; }
        [Display(Name = @"پرداخت تسهیم")]
        public bool EnableMultiplexing { get; set; }
        public MultiplexingData MultiplexingData { get; set; }
        public string MerchantKey { get; set; }
        public string PurchasePage { get; set; }
    }
}