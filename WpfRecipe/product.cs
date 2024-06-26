using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfRecipe
{

    public class JsonProduct
    {
        public string name { get; set; }
        public float price { get; set; }
        public float OneHundredGrams { get; set; }
        public int[] id { get; set; }
    }
    public class Product
    {


        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("price")]
        public float price { get; set; }
        [JsonPropertyName("OneHundredGrams")]
        public float OneHundredGrams { get; set; }
        [JsonPropertyName("id")]
        public List<int> id { get; set; }


    public Product(int id, string name, float price, float OneHundredGrams = 100)
        {
            this.id = new List<int>
            {
                id
            };
            this.name = name.ToLower();
            this.price = price;
            this.OneHundredGrams = OneHundredGrams;
            Console.WriteLine($"Продукт \t {name} создан \t id= {id} ");
            Control.SaveProduct();
        }
    public Product(string name, float price, float OneHundredGrams = 100)
        {
           this.name=name;
            this.price = price;
            this.OneHundredGrams= OneHundredGrams;
            id = new List<int>();
        }

        [JsonConstructor]
        public Product(List<int> id, string name, float price, float oneHundredGrams)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            OneHundredGrams = oneHundredGrams;
        }

        public void AddId(int id)
        {
            this.id.Add(id);
            Console.WriteLine($" рецепт с {id}  добвлен к продукту {this.name}");
        }



    }

}
