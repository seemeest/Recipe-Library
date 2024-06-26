using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace WpfRecipe
{
    class BlockR
    {
        int idTypeOfDish;

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


        StackPanel panelR;
        LinearGradientBrush bgR;
        WrapPanel warp = new WrapPanel();
        StackPanel labelR = new StackPanel();
        TextBlock ttt = new TextBlock();
        Button btnRdR = new Button();
        Button btnDelR = new Button();
        string text;
        public BlockR(StackPanel panelR,string text, int idTypeOfDish,int i ,int j )
        {
            this.idTypeOfDish= idTypeOfDish;
            this.text = text;

            StackPanel stackPanelR = new StackPanel();
            UniformGrid uniformGridR = new UniformGrid();
            StackPanel stackPanelTR = new StackPanel();
            UniformGrid uniformGridTR = new UniformGrid();

            this.panelR = panelR;
            bgR = new LinearGradientBrush(color1[i], color2[i], 2F);

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
            ttt.Margin = new Thickness(0, 0, 0, 114);
            warp.HorizontalAlignment = HorizontalAlignment.Right;
            ttt.Text = text;
            btnRdR.Content = "ред.";
            btnDelR.Content = "удл.";
            uniformGridR.Children.Add(labelR);
            labelR.Children.Add(ttt);
            labelR.Children.Add(warp);


            btnDelR.Click += (ss, ee) => { Console.WriteLine($"del {ttt.Text}"); };
            btnRdR.Click += (ss, ee) => { Console.WriteLine($"red {ttt.Text}"); };

            warp.Children.Add(btnDelR);
            warp.Children.Add(btnRdR);

            i++;
            if (i == 8) i = 0;
            this.idTypeOfDish = idTypeOfDish;
        }
    }
}
