using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace WpfRecipe
{

  
    public static class Control
    {

        public static List<TypeOfDish> Types = new List<TypeOfDish>();

        public static  List<Product> Products = new List<Product>();


        public static Product GetIndexNameProducts(string name)
        {
            Product number = Products.Find(p=>p.name==name);
            return number;
        }

        //id последнего рицепта
        public static int GetLastIdRecipes()
        {

            //int id = Types[^1].getIdRecipes();
            int id = 0;

            foreach (var type in Types)
            {
              foreach(var res in type.recipes)
                {
                    if(res.id>id)id=res.id;
                }
            }

            return id;
        }

    
        public static void removeType(int index)
        {

            string sMessageBoxText = "Удалиние Типа блюда удалит все рецепты в нём продолжить ? ";
            string sCaption = "Предупреждение";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    Types.RemoveAt(index);
                    SaveTypesOfDish();
                    SaveProduct();
                    break;

                case MessageBoxResult.No:
                 
                    break;

              
            }


        }
        public static int FindIndexTypes(string name, int indexRec)
        {
            int index = 0;

          
            foreach (var type in Types[indexRec].recipes)
            {
                
                //Console.WriteLine(type.Name);

                //Console.WriteLine("_______");
                if (type.Name == name.ToLower())
                {
                 
                    return index;
                }
                index++;
            }


            return -1;
        }


        //поиск  индекса рецепта в типе
        public static int FindNameInDexRecTtpe(String name, int indexRec)
        {

            int i = 0;
            foreach (var type in Types[indexRec].recipes)
            {
                if (type.Name == name.ToLower())
                {      
                    return i;
                }

            }
            return -1;
            
        }

        //поиск id рицепта по имени везде
        public static int FindNameIdRec(String name)
        {
            int id = 0;
            foreach (var type in Types)
            {

                foreach (var recipe in type.recipes)
                {
                    if (recipe.Name == name.ToLower())
                    {
                        id = recipe.getId();
                        return id;
                    }
                }


            }
            return -1;
        }

        //поиск id типа блюда по названию
        public static int FindIdType(String name)
        {
            int id = 0;
            foreach (var type in Types)
            {
                if (type.Name == name.ToLower())
                {
                    id = type.id;
                    return id;
                }

            }
            return -1;
        }
        //поиск индекса типа блюда по названию
        public static int FindIndexType(String name)
        {
            int i = 0;
            foreach (var type in Types)
            {
               
                if (type.Name == name.ToLower())
                {
                    return i;
                }
                i++;

            }
            return -1;
        }
        public static bool addTypes(string name)
        {
            name=name.ToLower();
            if (Types.Count > 0)
            {
                foreach (var type in Types)
                {
                    if (type.Name == name.ToLower())
                    {
                       Console.WriteLine("Старый тип блюда");
                        return false;
                    }
                    //}
                    //MessageBox.Show($"addTypes{name}");
                    
                }
                int id = Types[^1].id;
                id++;
                Types.Add(new TypeOfDish(name,id));
                Console.WriteLine($"Добавлен Новый Тип Блюда {name} id= {id}");
            }
            else
            {
                Types.Add(new TypeOfDish(name,0));
                Console.WriteLine($"Добавлен Новый Тип Блюда {name} id= {0}");
            }
            
            return true;
            //добавление рицепта уже прописано в  TypeOfDish
            //в начле вызвать функцию добавления рицепта после чего в рицепте вызываем функцию добавления продукта
            //но это не точно :)))))


        }



        public static void SaveTypesOfDish()
        {


            string path = @"save";
           
           
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string product = @"\TypesOfDish";

            System.IO.DirectoryInfo di = new DirectoryInfo(path+ product);
            di.Delete(true);
            if (!Directory.Exists(path + product))
            {
                Directory.CreateDirectory(path + product);
            }

            foreach (var type in Types)
            {

                string jsondata = JsonConvert.SerializeObject(type);
                File.WriteAllText(path + product + @"\" + type.Name + ".json", jsondata);


            }
        }
        public static void SaveProduct()
        {



            //Console.WriteLine($"Начало сохранения продукта");
            string path = @"save";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string product=@"\product";
       
            if (!Directory.Exists(path+ product))
            {
                Directory.CreateDirectory(path+ product);
            }

            System.IO.DirectoryInfo di = new DirectoryInfo(path + product);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            //Продукты
            foreach (var type in Products)
            {



                string jsondata = JsonConvert.SerializeObject(type);
                File.WriteAllText(path + product + @"\" + type.name + ".json", jsondata);


                //Console.WriteLine($"Продукт {type} Save");

            }


        }

        public static void removeRecipte(string name,int idTypeOfDish)
        {
            //Types[idTypeOfDish]
            int index = FindIndexTypes(name.ToLower(), idTypeOfDish);
            //Console.WriteLine($"{index} {idTypeOfDish}");

            for(int i = 0; i < Products.Count; i++) {
                for (int j = 0; j < Products[i].id.Count; j++)
                {

                    if (Types[idTypeOfDish].recipes[index].id == Products[i].id[j])
                    {
                        Products[i].id.RemoveAt(j);
                    }
                }


            }

            //foreach (var product in Products) {
            
            //    foreach (var id in product.id) {

            //      if(Types[idTypeOfDish].recipes[index].id == id)
            //        {

            //        }
            //    }
            //}
          
            Types[idTypeOfDish].recipes.RemoveAt(index);
            
   


            SaveTypesOfDish();
            SaveProduct();


        }
        public static void LoadPruducts()
        {
            string path = @"save";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string product = @"\product";

            if (!Directory.Exists(path + product))
            {
                Directory.CreateDirectory(path + product);
            }
            else
            {
                List<string> filenames= new List<string>();
                Directory
                    .GetFiles(path + product, "*", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(f => filenames.Add(Path.GetFileName(f)));

                foreach (string filename in filenames)
                {
                    Console.WriteLine(path + product + @"\" + filename);
                   
                    string jsondata = File.ReadAllText(path + product + @"\" + filename);
                    Console.WriteLine (jsondata);

                    var obj = JsonConvert.DeserializeObject<JsonProduct>(jsondata);
                    List <int> id = obj.id.ToList();
                    string name = obj.name;
                    float price = obj.price;
                    float OneHundredGrams =obj.OneHundredGrams;
                    Control.Products.Add(new Product(id,name,price,OneHundredGrams));
                }
            }
           foreach(var prod in Products)
            {
                prod.id=prod.id.Distinct().ToList(); 
            }

        }  
        public static void LoadTypesOfDish()
        {
            string path = @"save";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string product = @"\TypesOfDish";

            if (!Directory.Exists(path + product))
            {
                Directory.CreateDirectory(path + product);
            }
            else
            {
                List<string> filenames= new List<string>();
                Directory
                    .GetFiles(path + product, "*", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(f => filenames.Add(Path.GetFileName(f)));

                foreach (string filename in filenames)
                {
                    Console.WriteLine(path + product + @"\" + filename);
                   
                    string jsondata = File.ReadAllText(path + product + @"\" + filename);
                    Console.WriteLine (jsondata);

                    var obj = JsonConvert.DeserializeObject<JsonTypeOfDish>(jsondata);
                    string Name=obj.Name;
                    int id = obj.id;
                    List<Recipe> recipes= obj.recipes.ToList();

                    Control.Types.Add(new TypeOfDish(Name, id, recipes));
                    
                    
                }
            }
                
        }
      

        public static bool AddRecipes(string NameType, string name, List<int> Gramms)

        {

            name = name.ToLower();
            NameType= NameType.ToLower();

            int IndexType = FindIndexType(NameType);

            if (Types[IndexType].recipes.Count > 0)
            {
                int IndexRec = FindNameInDexRecTtpe(name, IndexType);
                if (IndexRec != -1)
                {
                    return false;
                }
            }

          

            Console.WriteLine($"FindIndexType({NameType}) = {FindIndexType(NameType)}");
                    if (Types[0].recipes.Count > 0)
                    {


                        int id = GetLastIdRecipes();
                        id++;
                        Types[IndexType].recipes.Add(new Recipe(name, id, Gramms));
                        Console.WriteLine($"Добавлен ещё Рецепт name={name} id= {id}");
                    }
                    else
                    {
                        Types[IndexType].recipes.Add(new Recipe(name, 0, Gramms));
                        Console.WriteLine($"Добавлен Рецепт name={name} id= {0}");
                    }
            return true;
        }


        public static void addOnwProduct(string name, int price)
        {
            name= name.ToLower();
            foreach (Product Product in Products.ToList())
            {
                if (Product.name == name.ToLower())
                {
                    return;
                }

            }
            Products.Add(new Product(name, price));


        }

        public static void addProduct(string name, string nameType,int price)
        {
            name= name.ToLower();

            int id =  Types[FindIndexType(nameType)].recipes[^1].id;
            if (Products.Count > 0)
            {
                foreach (Product Product in Products.ToList())
                {
                    if (Product.name == name.ToLower())
                    {
                        Product.AddId(id);
                        SaveProduct();
                        Console.WriteLine($"Старый продукт {name}");
                        return;
                    }
             
                }
                
                    Console.WriteLine($"новый продукт {name}");

                    Products.Add(new Product(id,name,price,100));
                SaveProduct();
                return;
                

            }
            else
            {
                Console.WriteLine($"новый продукт {name}");
                Products.Add(new Product(0, name, price, 100));
                SaveProduct();
            }
        }
        public static void EditProduct(string name, int id, int price)
        {
            foreach (Product Product in Products.ToList())
            {
                if (Product.name == name.ToLower())
                {
                    Product.price = price;
                    foreach (var idP in Product.id)
                        if (idP == id)  return;
                    Product.AddId(id);

                }
                
            }


            foreach (Product Product in Products.ToList())
            {
                if (Product.name == name.ToLower())
                {
                    return;
                }

            }
            Products.Add(new Product(id, name, price, 100));
        }
    }
}

