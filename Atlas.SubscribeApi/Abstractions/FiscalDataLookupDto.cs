namespace Atlas.SubscribeApi.Abstractions
{
    public class FiscalDataLookupDto
    {
        public long status_code { get; set; }

        public string message { get; set; }

        public string terminal_id { get; set; }

        public long receipt_id { get; set; }

        public string date { get; set; }

        public string fiscal_sign { get; set; }

        public string qr_code_url { get; set; }
    }
}