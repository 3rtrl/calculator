using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class SaveMessageModel
    {
        [JsonProperty(PropertyName = "sayi1")]
        public double sayi1 { get; set; }

        [JsonProperty(PropertyName = "sayi2")]
        public double sayi2 { get; set; }

        [JsonProperty(PropertyName = "operation")]
        public string operation { get; set; }
    }
    public class SaveResultModel
    {
        [JsonProperty(PropertyName = "sonuc")]
        public double sonuc { get; set; }
        
    }
}
