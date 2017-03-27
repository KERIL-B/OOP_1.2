using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crowd
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool created;

        public MainWindow()
        {
            InitializeComponent();
            created = false;

        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int widthC = Convert.ToInt32(WTB.Text);
                int heightC = Convert.ToInt32(HTB.Text);
                DrawGreed.CreateGrid(widthC, heightC, canvas);
                created = true;
                createBtn.IsEnabled = false;
            }
            catch (Exception)
            { }


        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (created)
            {
                DrawGreed.Add_Remove_Person(e.GetPosition(canvas).X, e.GetPosition(canvas).Y, canvas);
            }
        }
    }
}
