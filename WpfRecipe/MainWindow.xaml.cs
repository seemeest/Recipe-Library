using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WpfRecipe {
    public partial class MainWindow : Window {
        int i = 0, j = 7;

        

        StackPanel stackPanelR = new StackPanel();
        UniformGrid uniformGridR = new UniformGrid();
        StackPanel stackPanelTR = new StackPanel();
        UniformGrid uniformGridTR = new UniformGrid();


        WrapPanel WarpNameProductR = new WrapPanel();
        WrapPanel WarpGramsOnePR = new WrapPanel();
    

        StackPanel bar;
        StackPanel barC;
        ScrollViewer typeR;
        StackPanel dobR;
        StackPanel dobRRd;
        StackPanel dobTR;
        StackPanel dobTRRd;
        StackPanel dobPr;
        TextBlock txt;

        //редоктирование

        StackPanel EditR;
        WrapPanel EditWarpNameProduct;
        WrapPanel EditWarpGramsOneP;
        WrapPanel EditWarpPriceAndOneGramms;


        Color[] color1 = {
                Color.FromRgb(107,201,254),
                Color.FromRgb(247,95,95),
                Color.FromRgb(118,255,132),
                Color.FromRgb(223,94,195),
                Color.FromRgb(255,212,101),
                Color.FromRgb(213,255,123),
                Color.FromRgb(151,151,151),
                Color.FromRgb(136,85,244)
            };
        Color[] color2 = {
                Color.FromRgb(55,183,255),
                Color.FromRgb(246,50,50),
                Color.FromRgb(45,255,66),
                Color.FromRgb(217,20,174),
                Color.FromRgb(217,161,19),
                Color.FromRgb(189,255,48),
                Color.FromRgb(102,101,100),
                Color.FromRgb(88,11,254)
            };

        List<BlockR> blockRs =new List<BlockR>();

        public void CreateBlocksR(string text, int idTypeOfDish)
        {
            StackPanel panelR = (StackPanel)FindName("blockR");
            LinearGradientBrush bgR = new LinearGradientBrush(color1[i], color2[i], 2F);
            WrapPanel warpBTN = new WrapPanel();
            StackPanel labelR = new StackPanel();
            TextBlock tttR = new TextBlock();
            Button btnRdR = new Button();
            Button btnDelR = new Button();

            TextBlock nameT = new TextBlock(); // имя продукта
            TextBlock kolvoT = new TextBlock(); // количество грамм продукта
            TextBlock priceT = new TextBlock(); // цена количества грамм продукта

            if (i == 0 || i == 4)
            {
                stackPanelR = new StackPanel();
                uniformGridR = new UniformGrid();
                panelR.Children.Add(stackPanelR);
                stackPanelR.Children.Add(uniformGridR);
            }

            labelR.Background = bgR;
            labelR.Margin = new Thickness(50);
            labelR.Height = 150;
            labelR.MaxWidth = 350;

            btnRdR.Width = 50;
            btnRdR.Height = 20;
            btnDelR.Width = 50;
            btnDelR.Height = 20;
            btnRdR.Content = "открыть";
            btnDelR.Content = "удл.";

            tttR.Margin = new Thickness(10, 5, 0, 98);
            tttR.FontSize = 20;
            tttR.Text = text;

            nameT.Text = "мука: ";
            kolvoT.Text = "100гр.   ";
            priceT.Text = "цена: 50р.";

            warpBTN.HorizontalAlignment = HorizontalAlignment.Right;

            //btnDelR.Click += (ss, ee) => { Console.WriteLine($"del {ttt.Text}"); };
            //btnRdR.Click += (ss, ee) => { Console.WriteLine($"red {ttt.Text}"); };

            btnDelR.Click += (ss, ee) => { string texts=text;  int idTypeOfDishs = idTypeOfDish; Control.removeRecipte(text, idTypeOfDishs); ReloadRec(); };
            btnRdR.Click += (ss, ee) => { string texts = text; int idTypeOfDishs = idTypeOfDish;  EditdobRPanel(ss, ee, texts, idTypeOfDishs); };

            

            uniformGridR.Children.Add(labelR);
            labelR.Children.Add(tttR);
            labelR.Children.Add(warpBTN);
            warpBTN.Children.Add(btnDelR);
            warpBTN.Children.Add(btnRdR);

            i++;
            if (i == 8) i = 0;
        }


        public void GridAddProduct()
        {
            //gridContr

            for (int i = 0; i < Control.Products.Count; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                gridControl.RowDefinitions.Add(rowDefinition);

                TextBox textBox1 = new TextBox();
                textBox1.SetValue(Grid.RowProperty, 2+i);
                textBox1.Name = $"t{2+i}{0}{index}";
                textBox1.SetValue(Grid.ColumnProperty, 0);
                textBox1.Text = Control.Products[i].name;


                TextBox textBox2 = new TextBox();
                textBox2.SetValue(Grid.RowProperty, 2+i);
                textBox2.Name = $"t{2+i}{1}{index}";

                textBox2.SetValue(Grid.ColumnProperty, 1);
                textBox2.Text = Control.Products[i].price.ToString();

                textBox1.Tag = i;
                textBox2.Tag = i;


                //textBox2.Tag = Control.Products[i].id;
                textBox2.TextChanged += textBoxProductPrice_TextChanged;
                textBox1.TextChanged += textBoxProductName_TextChanged;
               

                gridControl.Children.Add(textBox1);
                gridControl.Children.Add(textBox2);
            }

            mainComboboxAddItems();


        }
        public void CreateBlocksTR(string text, int indexType)
        {
            StackPanel panelTR = (StackPanel)FindName("blockTR");
            LinearGradientBrush bgTR = new LinearGradientBrush(color1[j], color2[j], 2F);
            TextBlock tttTR = new TextBlock();
            WrapPanel warp = new WrapPanel();
            StackPanel labelTR = new StackPanel();
            Button btnRdTR = new Button();
            Button btnDelTR = new Button();

            if (j == 7 || j == 3)
            {
                stackPanelTR = new StackPanel();
                uniformGridTR = new UniformGrid();
                panelTR.Children.Add(stackPanelTR);
                stackPanelTR.Children.Add(uniformGridTR);
            }

            labelTR.Background = bgTR;
            labelTR.Margin = new Thickness(50);
            labelTR.Height = 150;
            labelTR.MaxWidth = 350;

            tttTR.Text = text;
            tttTR.Margin = new Thickness(10, 5, 0, 98);
            tttTR.FontSize = 20;

            btnRdTR.Width = 50;
            btnRdTR.Height = 20;
            btnDelTR.Width = 50;
            btnDelTR.Height = 20;
            btnRdTR.Content = "открыть";
            btnDelTR.Content = "удл.";

            warp.HorizontalAlignment = HorizontalAlignment.Right;
            //Control.removeType(1);

            btnDelTR.Click += (ss, ee) => {  int idTypeOfDishs = indexType; Control.removeType(idTypeOfDishs); ReloadTypes(); };
            btnRdTR.Click += (ss, ee) => { dobTRRdPanel(ss, ee); };

            uniformGridTR.Children.Add(labelTR);
            labelTR.Children.Add(tttTR);
            labelTR.Children.Add(warp);
            warp.Children.Add(btnDelTR);
            warp.Children.Add(btnRdTR);


            j--;
            if (j == -1) j = 7;
        }

        private void textBoxProductPrice_TextChanged(object sender, EventArgs e)
        {

            
            // Получаем новое значение цены продукта из TextBox
            int newPrice;
            if (int.TryParse(((TextBox)sender).Text, out newPrice))
            {
                // Получаем id продукта, который отображается в данном TextBox (например, через Tag)
                int productId = (int)((TextBox)sender).Tag;
                    
                Control.Products[productId].price = newPrice;
            }
            Control.SaveProduct();
        }      
        private void textBoxProductName_TextChanged(object sender, EventArgs e)
        {

       
                // Получаем id продукта, который отображается в данном TextBox (например, через Tag)
                int productId = (int)((TextBox)sender).Tag;

                Control.Products[productId].name = ((TextBox)sender).Text;
            
            Control.SaveProduct();
        }

        private void recipsList(object sender, RoutedEventArgs e) {


            if (recips.Visibility == Visibility.Hidden) {
                barC.Visibility = Visibility.Visible;
                recips.Visibility = Visibility.Visible;
                typeR.Visibility = Visibility.Hidden;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                product.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "Рецепты";
                Grid.SetColumnSpan(bar, 6);
            }

            
        }
        private void typeList(object sender, RoutedEventArgs e) {


            if (typeR.Visibility == Visibility.Hidden) {
                barC.Visibility = Visibility.Visible;
                recips.Visibility = Visibility.Hidden;
                typeR.Visibility = Visibility.Visible;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                product.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "Типы блюд";
                Grid.SetColumnSpan(bar, 6);
            }
           
        }

        private void prodList(object sender, RoutedEventArgs e)
        {


            if (product.Visibility == Visibility.Hidden)
            {
                barC.Visibility = Visibility.Visible;
                recips.Visibility = Visibility.Hidden;
                typeR.Visibility = Visibility.Hidden;
                product.Visibility = Visibility.Visible;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "Продукты";
                Grid.SetColumnSpan(bar, 6);
            }
            GridAddProduct();
        }

        private void dobProd(object sender, RoutedEventArgs e)
        {


            if (dobPr.Visibility == Visibility.Hidden)
            {
                barC.Visibility = Visibility.Hidden;
                recips.Visibility = Visibility.Hidden;
                typeR.Visibility = Visibility.Hidden;
                product.Visibility = Visibility.Hidden;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Visible;
                txt.Text = "Продукты";
                Grid.SetColumnSpan(bar, 6);
            }
            GridAddProduct();
        }


        private void dobRPanel(object sender, RoutedEventArgs e) {
  

            if (dobR.Visibility == Visibility.Hidden) {
                barC.Visibility = Visibility.Hidden;
                recips.Visibility = Visibility.Hidden;
                typeR.Visibility = Visibility.Hidden;
                dobR.Visibility = Visibility.Visible;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                product.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "";
                Grid.SetColumnSpan(bar, 6);
             
            }
        }

        private void EditdobRPanel(object sender, RoutedEventArgs e, string texts, int idTypeOfDishs) {

            editNameDish.Text= texts;


            if (dobR.Visibility == Visibility.Hidden) {
                barC.Visibility = Visibility.Hidden;
                recips.Visibility = Visibility.Hidden;
                typeR.Visibility = Visibility.Hidden;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                EditR.Visibility= Visibility.Visible;
                product.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "";
                Grid.SetColumnSpan(bar, 6);

            }
            EditWarpGramsOneP.Children.Clear();
            EditWarpNameProduct.Children.Clear();
            EditWarpPriceAndOneGramms.Children.Clear();

            ButtonEditRec.Click += (ss, ee) => { editButton_Click_AddRisept(ss, ee, texts, idTypeOfDishs); };
            int index = Control.FindIndexTypes(texts.ToLower(), idTypeOfDishs);
            foreach(var rec in Control.Types[idTypeOfDishs].recipes[index].Gramms)
            {
                TextBox textBoxRGOP = new TextBox();
                textBoxRGOP.Width = 75;
                textBoxRGOP.Text = rec.ToString();
                EditWarpGramsOneP.Children.Add(textBoxRGOP);
            }
            int indexTB = 0;
            for (int i = 0; i < Control.Products.Count; i++)
            {
                for (int j = 0; j < Control.Products[i].id.Count; j++)
                {

                    if (Control.Types[idTypeOfDishs].recipes[index].id == Control.Products[i].id[j])
                    {

                       

                                  TextBox textBoxRNP = new TextBox();
                        textBoxRNP.Width = 75;
                        textBoxRNP.Text = Control.Products[i].name;
                        textBoxRNP.LostFocus += (ss, ee) =>{ int ind = indexTB; TextBoxRNP_LostFocus(ss, ee, textBoxRNP); };


                        TextBox textBoxRPAOG = new TextBox();
                        textBoxRPAOG.Width = 75;
                        textBoxRPAOG.Text = Control.Products[i].price.ToString();
                        EditWarpNameProduct.Children.Add(textBoxRNP);

                        EditWarpPriceAndOneGramms.Children.Add(textBoxRPAOG);
                        indexTB++;
                    }
                }


            }

        }
        private void TextBoxRNP_LostFocus(object sender, RoutedEventArgs e, TextBox tb)
        {
            foreach (Product Product in Control.Products.ToList())
            {
                if (Product.name == tb.Text.ToLower())
                {

                    Console.WriteLine(tb.Text.ToLower());
              

                    int i = 0;
                    foreach (UIElement el in EditWarpNameProduct.Children)
                        if (el is TextBox)
                        {
                            if(((TextBox)el).Text== tb.Text.ToLower())
                            {
                                ((TextBox)EditWarpPriceAndOneGramms.Children[i]).Text = Product.price.ToString();
                                Console.WriteLine(i);
                            }
                            i++;
                        }

                }

            }
        }


        private void dobTRRdPanel(object sender, RoutedEventArgs e)
        {


            if (dobTR.Visibility == Visibility.Hidden)
            {
                barC.Visibility = Visibility.Hidden;
                recips.Visibility = Visibility.Hidden;
                typeR.Visibility = Visibility.Hidden;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Visible;
                product.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "";
                Grid.SetColumnSpan(bar, 6);
            }
        }

        int index = 0;
        private void Button_Click_AddRisept(object sender, RoutedEventArgs e) {


            //WarpNameProductR - Название продукта
            //WarpGramsOnePR  -   Сколько грам
            //WarpPriceAndOneGrammsR  - Цена (за 100 грамм)

            Console.WriteLine("===================");
            TextBox NameDish = (TextBox)FindName("NameDish");//название
            TextBox NamePizDish = (TextBox)FindName("NamePizDish");//тип Рецепта

           

            List<string> NameProductT = new List<string>();
             List<int> GramsOnePT = new List<int>();
             List<int> PriceAndOneGrammsT = new List<int>();


            //название Продукта
            foreach (UIElement el in WarpNameProductR.Children)
                if (el is ComboBox)
                {


                    //ComboBoxItem typeItem = (ComboBoxItem)el.SelectedItem;
                    //string value = typeItem.Content.ToString();
                    //NameProductT.Add(value);
                    ComboBox item = (ComboBox)el;
                    ComboBoxItem items2 = item.SelectedItem as ComboBoxItem;
                    MessageBox.Show(items2.Content.ToString()) ;
                }
            //Колво гр на 1 порцию
            foreach (UIElement el in WarpGramsOnePR.Children)
                if (el is TextBox)
                {
                    try
                    {
                        String text = ((TextBox)el).Text;
                        GramsOnePT.Add(int.Parse(text));
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Вводите Числа в Кол-во грам");
                        return;
                    }
                 
                  
                }
            //Цена (за 100 грамм)


            

           
            Control.addTypes(NamePizDish.Text);

            if (!Control.AddRecipes(NamePizDish.Text, NameDish.Text, GramsOnePT))
            {
                MessageBox.Show("Такой Рецепт в этом Типе блюда уже есть");
                return;
            }
            else
            {

                for (int i = 0; i < PriceAndOneGrammsT.Count; i++)
                {
                    Control.addProduct(NameProductT[i], NamePizDish.Text, PriceAndOneGrammsT[i]);
                }
            }
            Control.SaveTypesOfDish();


            StackPanel stR = (StackPanel)FindName("blockR");
            StackPanel stTR = (StackPanel)FindName("blockTR");
            stR.Children.Clear();
            stTR.Children.Clear();
            i = 0;
            j = 7;
    
            foreach (var types in Control.Types)
            {
               
                CreateBlocksTR(types.Name, index);
                index++;

            }
            foreach (var types in Control.Types)
            {
                foreach (Recipe type in types.recipes)
                {
                    string text = type.Name;
                    int idTypeOfDish = types.id;
                    CreateBlocksR(text,idTypeOfDish);
                }
            }
            index = 0;
            barC.Visibility = Visibility.Visible;
            recips.Visibility = Visibility.Visible;
            typeR.Visibility = Visibility.Hidden;
            dobR.Visibility = Visibility.Hidden;
            dobRRd.Visibility = Visibility.Hidden;
            dobTR.Visibility = Visibility.Hidden;
            dobTRRd.Visibility = Visibility.Hidden;
            product.Visibility = Visibility.Hidden;
            dobPr.Visibility = Visibility.Hidden;
            txt.Text = "Рецепты";
            Grid.SetColumnSpan(bar, 6);

        }
      
        public void columnAdd(object sender, RoutedEventArgs e) {
            ComboBox comboBoxName = new ComboBox();
            comboBoxName.Width = 75;

            foreach(var _product in Control.Products)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = _product.name;
                comboBoxName.Items.Add(comboBoxItem);
            }

            TextBox textBoxRGOP = new TextBox();
            textBoxRGOP.Width = 75;

            //comboBoxName.SelectionChanged += comboBox_SelectionChanged;
          
            WarpNameProductR.Children.Add(comboBoxName);
            WarpGramsOnePR.Children.Add(textBoxRGOP);
          
        }

        public void columnDel(object sender, RoutedEventArgs e) {

            List<string> NameProductT = new List<string>();
            List<string> GramsOnePT = new List<string>();
            List<string> PriceAndOneGrammsT = new List<string>();

            foreach (UIElement el in WarpNameProductR.Children)

                if (el is TextBox)
                {
                    String text = ((TextBox)el).Text;
                    NameProductT.Add(text);
                }
            

            foreach (UIElement el in WarpGramsOnePR.Children)
                if (el is TextBox)
                {
                    String text = ((TextBox)el).Text;
                    GramsOnePT.Add(text);
                }

   



            if (WarpNameProductR.Children.Count == 1 || WarpGramsOnePR.Children.Count == 1 ) return;



            WarpNameProductR.Children.Clear();
            WarpGramsOnePR.Children.Clear();
          

   

            NameProductT.RemoveAt(NameProductT.Count - 1);
            GramsOnePT.RemoveAt(GramsOnePT.Count - 1);
            PriceAndOneGrammsT.RemoveAt(PriceAndOneGrammsT.Count - 1);



           


            foreach (string str in NameProductT)
            {
                TextBox textBox = new TextBox();
                textBox.Width=75;
                textBox.Text = str;
                WarpNameProductR.Children.Add(textBox);

            }
           
            foreach (string str in GramsOnePT)
            {
                TextBox textBox = new TextBox();
                textBox.Width = 75;
                textBox.Text = str;
                WarpGramsOnePR.Children.Add(textBox);
            }

      

        }

        void ReloadRec()
        {
            StackPanel panelR = (StackPanel)FindName("blockR");
            panelR.Children.Clear();
            i = 0;
            foreach (var types in Control.Types)
            {
                foreach (Recipe type in types.recipes)
                {
                    string text = type.Name;
                    int idTypeOfDish = types.id;
                    CreateBlocksR(text, idTypeOfDish);
                }
            }
        }

        void ReloadTypes()
        {
            StackPanel panelTR = (StackPanel)FindName("blockTR");
            panelTR.Children.Clear();
            j = 0;
            foreach (var types in Control.Types)
            {

                CreateBlocksTR(types.Name, index);
                index++;

            }
            ReloadRec();
        }

        private void editcolumnDel(object sender, RoutedEventArgs e)
        {
            List<string> NameProductT = new List<string>();
            List<string> GramsOnePT = new List<string>();
            List<string> PriceAndOneGrammsT = new List<string>();


       
            foreach (UIElement el in EditWarpNameProduct.Children)

                if (el is TextBox)
                {
                    String text = ((TextBox)el).Text;
                    NameProductT.Add(text);
                }


            foreach (UIElement el in EditWarpGramsOneP.Children)
                if (el is TextBox)
                {
                    String text = ((TextBox)el).Text;
                    GramsOnePT.Add(text);
                }

           


            if (EditWarpNameProduct.Children.Count == 1 || EditWarpGramsOneP.Children.Count == 1 || EditWarpPriceAndOneGramms.Children.Count == 1) return;



            EditWarpNameProduct.Children.Clear();
            EditWarpGramsOneP.Children.Clear();
           
       

            NameProductT.RemoveAt(NameProductT.Count - 1);
            GramsOnePT.RemoveAt(GramsOnePT.Count - 1);
       


            foreach (string str in NameProductT)
            {
                TextBox textBox = new TextBox();
                textBox.Width = 75;
                textBox.Text = str;
                EditWarpNameProduct.Children.Add(textBox);

            }

            foreach (string str in GramsOnePT)
            {
                TextBox textBox = new TextBox();
                textBox.Width = 75;
                textBox.Text = str;
                EditWarpGramsOneP.Children.Add(textBox);
            }

            foreach (string str in PriceAndOneGrammsT)
            {
                TextBox textBox = new TextBox();
                textBox.Width = 75;
                textBox.Text = str;
                EditWarpPriceAndOneGramms.Children.Add(textBox);
            }

        
    }
        private void editcolumnAdd(object sender, RoutedEventArgs e)
        {
            TextBox textBoxRNP = new TextBox();
            textBoxRNP.Width = 75;
            textBoxRNP.LostFocus += (ss, ee) => { TextBoxRNP_LostFocus(ss, ee, textBoxRNP); };

            TextBox textBoxRGOP = new TextBox();
            textBoxRGOP.Width = 75;
            TextBox textBoxRPAOG = new TextBox();
            textBoxRPAOG.Width = 75;
            EditWarpNameProduct.Children.Add(textBoxRNP);
            EditWarpGramsOneP.Children.Add(textBoxRGOP);
            EditWarpPriceAndOneGramms.Children.Add(textBoxRPAOG);
        }
        private void addOneProduct(object sender, RoutedEventArgs e)
        {
            var name = nameProd.Text;
            var price = int.Parse(priceProd.Text);
            Control.addOnwProduct(name, price);


            Control.SaveProduct();
            Control.SaveTypesOfDish();
            mainComboboxAddItems();
        }
        private void editButton_Click_AddRisept(object sender, RoutedEventArgs e, string texts, int idTypeOfDishs)
        {
        
            List<string> NameProductT = new List<string>();
            List<int> GramsOnePT = new List<int>();
            List<int> PriceAndOneGrammsT = new List<int>();

            //название Продукта
            foreach (UIElement el in EditWarpNameProduct.Children)
                if (el is TextBox)
                {
                    String text = ((TextBox)el).Text;
                    NameProductT.Add(text);
                }
            //Колво гр на 1 порцию
            foreach (UIElement el in EditWarpGramsOneP.Children)
                if (el is TextBox)
                {
                    try
                    {
                        String text = ((TextBox)el).Text;
                        GramsOnePT.Add(int.Parse(text));
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Вводите Числа в Кол-во грам");
                        return;
                    }


                }
            //Цена (за 100 грамм)
            foreach (UIElement el in EditWarpPriceAndOneGramms.Children)
                if (el is TextBox)
                {


                    try
                    {
                        String text = ((TextBox)el).Text;
                        PriceAndOneGrammsT.Add(int.Parse(text));
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Вводите Числа в Цену");
                        return;
                    }
                }

            int index = Control.FindIndexTypes(texts.ToLower(), idTypeOfDishs);
            Control.Types[idTypeOfDishs].recipes[index].Gramms.Clear();
            int id = Control.Types[idTypeOfDishs].recipes[index].id;


            foreach (var Grams in GramsOnePT) Control.Types[idTypeOfDishs].recipes[index].Gramms.Add(Grams);

            for (int i = 0; i < NameProductT.Count; i++) Control.EditProduct(NameProductT[i], id, PriceAndOneGrammsT[i]);

            Control.SaveProduct();
            Control.SaveTypesOfDish();

            Console.WriteLine("editButton_Click_AddRisept");

        }
        private void editrecipsList(object sender, RoutedEventArgs e)
        {

            if (recips.Visibility == Visibility.Hidden)
            {
                barC.Visibility = Visibility.Visible;
                recips.Visibility = Visibility.Visible;
                typeR.Visibility = Visibility.Hidden;
                dobR.Visibility = Visibility.Hidden;
                dobRRd.Visibility = Visibility.Hidden;
                dobTR.Visibility = Visibility.Hidden;
                dobTRRd.Visibility = Visibility.Hidden;
                EditR.Visibility = Visibility.Hidden;
                product.Visibility = Visibility.Hidden;
                dobPr.Visibility = Visibility.Hidden;
                txt.Text = "Рецепты";
                Grid.SetColumnSpan(bar, 6);
            }
        }

        private void addProd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mainComboboxAddItems()
        {
            MainCombobox.Items.Clear();
            foreach (var _product in Control.Products)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = _product.name;
                MainCombobox.Items.Add(comboBoxItem);
            }
        }
        private void calculate(object sender, RoutedEventArgs e)
        {
            var number_of_servings = Number_of_servings.Text;
            float total_price = 0;
            List<int> totalGrams = new List<int>();
            List<string> Name = new List<string>();
            foreach(UIElement i in editWarpGramsOneP.Children)
            {
                if(i is TextBox)
                {
                    int val = int.Parse(((TextBox)i).Text);
                    totalGrams.Add(val);
                    Debug.WriteLine(val);
                }
            } 
            foreach(UIElement i in editWarpNameProduct.Children)
            {
                if(i is TextBox)
                {
                    string val = ((TextBox)i).Text;
                    Name.Add(val);
                    Debug.WriteLine(val);
                }
            }

            for( int i =0;i< editWarpPriceAndOneGramms.Children.Count;i++)
            {
                if(editWarpPriceAndOneGramms.Children[i] is TextBox)
                {

                    Product number = Control.GetIndexNameProducts(Name[i]);
                    float prise = float.Parse(number.price.ToString()) * (totalGrams[i] / 100);
                    Console.WriteLine($" prise = { number.price} * ( {totalGrams[i]} / {number.OneHundredGrams} ) = {prise}");
                    try
                    {
                      
                        prise *= int.Parse(((TextBox)Number_of_servings).Text);
                    }
                    catch { }
                    ((TextBox)editWarpPriceAndOneGramms.Children[i]).Text=prise.ToString();
                    total_price += prise;
                   

                }
            }

            Total_price.Text= total_price.ToString();
        }

        public MainWindow() {

            InitializeComponent();

            bar = (StackPanel)FindName("toolBar");
            barC = (StackPanel)FindName("tbc");
            recips = (ScrollViewer)FindName("recips");
            product = (ScrollViewer)FindName("product");
            typeR = (ScrollViewer)FindName("typeRecips");
            dobR = (StackPanel)FindName("dobavitR");
            dobRRd = (StackPanel)FindName("dobavitRRd");
            dobTR = (StackPanel)FindName("dobavitTR");
            dobTRRd = (StackPanel)FindName("dobavitTRRd");
            dobPr = (StackPanel)FindName("addProd");
            txt = (TextBlock)FindName("info");


             WarpNameProductR = (WrapPanel)FindName("WarpNameProduct");
             WarpGramsOnePR = (WrapPanel)FindName("WarpGramsOneP");
            


             EditR=(StackPanel)FindName("editR");
            EditWarpNameProduct = (WrapPanel)FindName("editWarpNameProduct");
            EditWarpGramsOneP = (WrapPanel)FindName("editWarpGramsOneP");
            EditWarpPriceAndOneGramms = (WrapPanel)FindName("editWarpPriceAndOneGramms");


            Control.LoadPruducts();
            Console.WriteLine("\tLoadTypesOfDish");
            Control.LoadTypesOfDish();
            int index = 0;
            foreach (var types in Control.Types)
            {
                CreateBlocksTR(types.Name, index);
                index++;
            }

            foreach (var types in Control.Types)
            {
                foreach (Recipe type in types.recipes)
                {
                    string text = type.Name;
                    int idTypeOfDish = types.id;
                    CreateBlocksR(text, idTypeOfDish);
                }
            }
            mainComboboxAddItems();

        }
    }
}
