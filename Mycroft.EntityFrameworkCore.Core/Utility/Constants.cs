namespace Mycroft.EntityFrameworkCore.Core.Utility
{
    public static class Constants
    {
        # region Configuration
        public static string ALL_TERMINAL_CONFIGURATION { get; } = "AllTerminalConfiguration";
        public static string ALL_AGENT_CONFIGURATION { get; } = "AllAgentConfiguration";
        public static string ALL_ADMIN_CONFIGURATION { get; } = "AllAdminConfiguration";
        #endregion

        #region Lists

        public static string ALL_BANKS { get; } = "AllBanks";
        public static string ALL_CUSTOMER_TYPES { get; } = "AllCustomerTypes";
        public static string ALL_CUSTOMER_CATEGORIES { get; } = "AllCustomerCategories";
        public static string ALL_MAILS { get; } = "AllMails";
        public static string ALL_SMS { get; } = "AllSms";
        public static object ALLBONUSES { get; } = "AllBonuses";
        public static string ALL_CURRENCIES { get; } = "AllCurrencies";
        public static string ALL_TRANSACTION_CATEGORIES { get; } = "AllTransactionCategories";
        public static string ALL_TICKET_ACTIONS { get; } = "AllTicketActions";
        public static string ALL_TICKET_STATUS { get; } = "AllTicketStatus";
        public static string ALL_TICKET_SERVICE_RESPONSE_TYPE { get; } = "AllTicketServiceResponseTypes";
        public static string ALL_REQUEST_RESPONSE_TYPE { get; } = "AllRequestResponseTypes";
        public static string ALL_BET_TYPES { get; } = "AllBetTypes";
        public static string ALL_CUSTOMER_PAYOUT_STATUS { get; } = "AllPayoutStatus";
        public static string ALL_CUSTOMER_DEPOSIT_STATUS { get; } = "AllDepositStatus";
        public static string ALL_CUSTOMER_DEPOSIT_PAYOUT_TYPE { get; } = "AllDepositPayoutType";
        public static string ALL_CUSTOMER_DEPOSIT_PAYOUT_DETAIL { get; } = "AllDepositPayoutDetail";
        public static string ALL_TICKET_TYPE { get; } = "AllTicketTypes";
        #endregion

        #region BetTypes
        public static string AGAINST { get; } = "AG";
        public static string AGAINSTSINGLES { get; } = "AGS";
        public static string ONE_DIRECT { get; } = "D1";
        public static string TWO_DIRECT { get; } = "D2";
        public static string PERM_TWO { get; } = "P2";
        public static string THREE_DIRECT { get; } = "D3";
        public static string PERM_THREE { get; } = "P3";
        public static string FOUR_DIRECT { get; } = "D4";
        public static string PERM_FOUR { get; } = "P4";
        public static string FIVE_DIRECT { get; } = "D5";
        public static string PERM_FIVE { get; } = "P5";
        #endregion

        #region Messages
        public static string LOW_BALANCE_MESSAGE { get; } = "Insufficient Wallet Balance";
        public static string TICKET_PAYOUT_REEQUEST_SUCCESS_MESSAGE { get; } = $"Ticket Payout Request Submitted Succesfully And Awaiting Approval";
        public static string WALLET_PAYOUT_REQUEST_SUCCESS_MESSAGE { get; } = $"Request Submitted Succesfully And Awaiting Approval";
        public static string ACCOUNT_DETAILS_NOT_SPECIFIED_MESSAGE { get; } = "No Account Details Specified, Please create One";
        public static string DESPOSIT_REQUEST_FAILED_MESSAGE { get; } = $"Transcation Failed";
        public static string DESPOSIT_REQUEST_CANCEL_MESSAGE { get; } = $"Transcation Failed Cancelled By User";
        #endregion

        #region Ticket and Accounts
        public static decimal MAXIMUM_DEPOSIT_AMOUNT { get; } = 500000;
        public static decimal MINIMUM_DEPOSIT_AMOUNT { get; } = 5;
        public static decimal MINIMUM_STAKE_AMOUNT { get; } = 5;
        public static decimal MAXIMUM_STAKE_AMOUNT { get; } = 2500;
        public static decimal MINIMUM_PAYOUT_AMOUNT { get; } = 1000;
        public static decimal MAXIMUM_PAYMENT_AMOUNT { get; } = 500000;
        public static decimal TICKET_MINIMUM_PAYOUT_AMOUNT { get; } = 1000;
        public static decimal TICKET_MAXIMUM_PAYMENT_AMOUNT { get; } = 50000;
        #endregion

        #region Codes
        public static string ONLINE_USER_OWNER_TYPE_CODE { get; } = "OU";
        public static string ONLINE_CUSTOMER_CATEGORY_CODE { get; } = "OC";
        public static int ONLINE_USER_OWNER_TYPE_ID { get; } = 1;
        public static string BET_PLACED_CODE { get; } = "BP";
        public static string TICKET_STATUS_WON_CODE { get; } = "W";
        public static string PAYOUT_REQUESTED_CODE { get; } = "PR";
        public static string DEPOSIT_STATUS_PENDING_CODE { get; } = "PEND";
        public static string DEPOSIT_STATUS_FAILED_CODE { get; } = "FAIL";
        public static string DEPOSIT_STATUS_SUCCESS_CODE { get; } = "SCS";
        public static string WALLET_PAYOUT_CODE { get; } = "WP";
        public static string WEB_TICKET_REGISTRATION_METHOD_CODE { get; } = "Web";
        public static string BONUS_ON_DEPOSIT_CODE { get; } = "BOD";
        public static string WALLET_DEPOSIT_CODE { get; } = "WD";
        public static string WALLET_BONUS_CODE { get; } = "WB";
        public static string WALLET_BONUS_REVERSAL_CODE { get; } = "WBR";
        public static string BONUS_ON_PAYMENT_TYPE_CODE { get; } = "BOP";
        public static string RECOVERY_MAIL_CODE { get; } = "REC";
        public static string FORGOT_PASSWORD { get; } = "FPWD";
        public static string ACCOUNT_CREATION_MAIL_CODE { get; } = "AC";
        public static string BANK_PAYOUT_CODE { get; } = "BP";
        public static string REFERRAL_ON_DEPOSIT_CODE { get; } = "RD";
        public static string REFERRAL_ON_TICKET_PLAYED_CODE { get; } = "RT";
        public static string REFERRAL_ON_PAYOUT_CODE { get; } = "RP";
        public static string REFEERAL_BONUS_DEPOSIT_TRANSACTION_CODE { get; } = "RBD";
        public static string REFEERAL_BONUS_DEPOSIT_REVERSAL_TRANSACTION_CODE { get; } = "RBDR";
        public static string BONUS_ON_REFERRAL_DEPOSIT { get; } = "BORD";
        public static string BONUS_ON_REFERRAL_PAYOUT { get; } = "BORP";
        public static string BONUS_FOR_REFERRING_DEPOSIT { get; } = "BFRD";
        public static string BONUS_FOR_REFERRING_PAYOUT { get; } = "BFRP";
        public static string PERCENTAGE_ON_DEPOSIT_CODE { get; } = "POD";
        public static string FIXED_AMOUNT_CODE { get; } = "FA";
        public static string AFTER_FIRST_TIME_CODE { get; } = "AFT";
        public static string EVERYTIME_CODE { get; } = "ET";
        public static string FIRST_TIME_ONLY_CODE { get; } = "FTO";
        public static string NUMBER_OF_TIMES_CODE { get; } = "NT";
        public static string WHEN_VALUE_IS_ABOVE_CODE { get; } = "WVA";
        public static string WALLET_PAYOUT_CODE_REVERSAL { get; } = "WPR";
        public static string PAYDIRECT_DEPOSIT_CODE { get; set; } = "PDBD";
        public static string PAYDIRECT_DEPOSIT_REVERSAL_CODE { get; set; } = "PDBDR";

        #endregion

        #region enums

        public enum BlogActivityType
        {
            LIKE = 0,
            UNLIKE = 1,
            COMMENT = 2
        };

        #endregion

        #region Refferrals
        public static string REFERRAL_COMMISSION_TYPE { get; } = "REFERRALCOMMISSIONTYPE";
        public static string REFERRAL_TYPE { get; } = "REFERRALTYPE";
        public static string REFERRAL_COMMISSION_SCHEDULE { get; } = "REFERRALCOMMISSIONSCHEDULE";

        #endregion

        #region CacheConstants
        public static string ALL_BETTYPES = "ALL_BETTYPES";
        public static string ALL_GAMES = "ALL_GAMES";
        public static string ALL_DAILYGAMES = "ALL_DAILYGAMES";
        public static string ALL_STATES = "ALL_STATES";
        public static string ALL_TERMINALS = "ALL_TERMINALS";
        public static string ALL_TERMINALS_CONFIGURATION = "ALL_TERMINALS_CONFIGURATION";
        public static string ALL_AGENTS_CONFIGURATION = "ALL_AGENTS_CONFIGURATION";
        public static string ALL_AGENTS = "ALLAGENTS";
        public static string ALL_ROLES = "ALLROLES";
        public static string ALL_ACTIONS = "AllACTIONS";
        public static string ALL_APPROVAL_CONFIGURATIONS = " ALLAPPROVALCONFIGURATION";
        #endregion
    }
}