using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Product
{
    [DataContract]
    public class ProductImageModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Link")]
        public string Link { get; set; }
    }
}
