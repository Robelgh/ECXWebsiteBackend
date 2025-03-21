using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Response
{
    
    public class Delivery
    {
        public string Contact { get; set; }
        public string SendStatus { get; set; }
        public string SendTime { get; set; } // You can use DateTime if you want to convert it to DateTime
    }

    public class Question
    {
        public string QuestionText { get; set; }
    }

    public class MiniOrangeResponse
    {
        public string TxId { get; set; }
        public string AuthType { get; set; }
        public string ResponseType { get; set; }
        public Delivery PhoneDelivery { get; set; }
        public Delivery EmailDelivery { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string QrCode { get; set; }
        public List<Question> Questions { get; set; }

        // Constructor to initialize the object
        public MiniOrangeResponse(string txId, string authType, string responseType, Delivery phoneDelivery, Delivery emailDelivery, string status, string message, string qrCode, List<Question> questions)
        {
            TxId = txId;
            AuthType = authType;
            ResponseType = responseType;
            PhoneDelivery = phoneDelivery;
            EmailDelivery = emailDelivery;
            Status = status;
            Message = message;
            QrCode = qrCode;
            Questions = questions;
        }
    }

}
