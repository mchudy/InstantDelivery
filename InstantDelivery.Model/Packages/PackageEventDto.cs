using InstantDelivery.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace InstantDelivery.Model.Packages
{
    public class PackageEventDto
    {
        public DateTime Date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PackageEventType EventType { get; set; }
    }
}
