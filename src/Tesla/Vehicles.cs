using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace Tesla
{
    // Represents the Vehicle as returned from the Tesla API
    public struct Vehicle
    {
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long ID { get; set; }

        [JsonProperty(PropertyName = "option_codes")]
        public string OptionCodes { get; set; }

        [JsonProperty(PropertyName = "vehicle_id")]
        public long VehicleID { get; set; }

        [JsonProperty(PropertyName = "vin")]
        public string Vin { get; set; }

        [JsonProperty(PropertyName = "tokens")]
        public string[] Tokens { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "id_s")]
        public string IDS { get; set; }

        [JsonProperty(PropertyName = "remote_start_enabled")]
        public bool RemoteStartEnabled { get; set; }

        [JsonProperty(PropertyName = "calendar_enabled")]
        public bool CalendarEnabled { get; set; }

        [JsonProperty(PropertyName = "notifications_enabled")]
        public bool NotificationsEnabled { get; set; }

        [JsonProperty(PropertyName = "backseat_token")]
        public string BackseatToken { get; set; }

        [JsonProperty(PropertyName = "backseat_token_updated_at")]
        public string BackseatTokenUpdatedAt { get; set; }
    }

    // The response that contains the Vehicle details from the Tesla API
    public struct VehicleResponse
    {
        [JsonProperty(PropertyName = "response")]
        public Vehicle[] Vehicles { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }
}
