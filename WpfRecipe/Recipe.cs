using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace  WpfRecipe
{
    public class Recipe
    {

        public Recipe(string name, int id, List<int> Gramms)
        {

            this.Name = name; this.id = id;this.Gramms= Gramms;


        }
        public List<int> Gramms;
     
        public string Name { get; set; }

         public int id;

        public int getId()
        { return id; }



        public void getPriceePerGrams()
        {

        }
    }
}
