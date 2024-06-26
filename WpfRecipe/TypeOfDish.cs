using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfRecipe
{

    public class JsonTypeOfDish
    {
        public String Name { get; set; }
        public int id { get; set; }
        public List<Recipe> recipes { get; set; }
    }
    public class TypeOfDish
    {
        public String Name;
        public int id;
        public TypeOfDish(String name,int id)
        {
            this.Name = name;
            this.id = id;
        } public TypeOfDish(String name,int id, List<Recipe> recipes)
        {
            this.Name = name;
            this.id = id;
            this.recipes = recipes;
        }

        public List<Recipe> recipes = new List<Recipe>();

        public int getIdRecipes()
        {
            return recipes.Count;
        }

        ~TypeOfDish()
        {
            Control.SaveTypesOfDish();
            
        }
    }
}
