namespace Mycroft.EntityFrameworkCore.Core.Models
{
    public class Enumerators
    {
        public enum AgentPaymentType
        {
            PrePaid,
            PostPaid,
            CreditLimit
        }

        public enum ApprovalStatus
        {
            Pending,
            Approved,
            Declined
        }

        public enum Order
        {
            Ascending = 1, Descending
        }

        public enum ActionMethodType
        {
            Create = 1, Update, Delete
        }

        
        public enum TransactionType
        {
            Credit, Debit
        }

        public enum TicketStatus
        {
            Undecided = 1, Won, Lost, Cancelled
        }

        public enum TicketType
        {
            Singles = 1
        }

        public enum TicketRegistrationMethod
        {
            Web,
            Mobile,
            BetShop,
            BetShopMobile,
            Others
        }

        public enum Currency
        {
            NGN,
            USD,
            GBP,
            EUR,
            YEN
        }

        public enum TicketAction
        {
            Registered,
            Cancelled,
            Won,
            Lost,
            JACKPOT,
            Reversed,
            Paid
        }

        public enum CustomerType
        {
            Online,
            Terminal
        }

        public enum USERTYPE
        {
            ADMIN = 1,
            AGENT = 2,
            TERMINAL = 3,
            ANONYMOUS = 4,
        };

        public enum PAYOUTDETAILUPDATEDBY
        {
            ONLINE_USER = 1,
            ADMIN_USER,
            SHOP_USER
        };

        public enum PAYMENTCHANNELSERVICE
        {
            WEBPAY = 0,
            INTERSWITCH = 1,
            PAYSTACK,
            BANK_PAYOUT
        };
    }
}