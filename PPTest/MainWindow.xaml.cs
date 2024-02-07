using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace PPTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string login;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataBaseClass.Users_ID = "sa";
            DataBaseClass.Password = "12345";
            DataBaseClass.ConnectionStrig = string.Format(DataBaseClass.ConnectionStrig, DataBaseClass.Users_ID, DataBaseClass.Password);
            DataBaseClass dataBaseClass = new DataBaseClass();
           
                //Visibility = Visibility.Hidden;
                //SqlCommand Command = new SqlCommand("SELECT Post_ID FROM [Employee] WHERE Login = @Login AND Password = @Password");

                //Command.Parameters.AddWithValue("@Login", Login.Text);
                //Command.Parameters.AddWithValue("@Password", Password.Text);
                //Command.Connection = dataBaseClass.connection;
                //dataBaseClass.connection.Open();
                //int rule = (int)Command.ExecuteScalar();
                //login = Login.Text;
                //if (rule == 1)
                //{
                    Admin simpleTablesWindow = new Admin();
                    simpleTablesWindow.Show();
                    Close();
                //}
                //else if (rule == 2)
                //{
                //    Animal_trainer simpleTablesWindow = new Animal_trainer();
                //    simpleTablesWindow.Show();
                //    Close();
                //}
                //else if (rule == 3)
                //{
                //    Veterinarian simpleTablesWindow = new Veterinarian();
                //    simpleTablesWindow.Show();
                //    Close();
                //}
                //else
                //{
                //    MessageBox.Show("Логин или пароль неверен");
                //}
            //}
            //catch
            //{
            //    MessageBox.Show("Не верный логин или пароль!", DataBaseClass.App_Name);
            //    Login.Focus();
            //}
            //finally
            //{
            //    dataBaseClass.connection.Close();
            //    Login.Clear();
            //    Password.Clear();
            //}
        }
    }
}
