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
using Common.Contract.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http;

namespace SignUpFormWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://localhost:7231/api/SignUp/Post";

            using (HttpClient client = new HttpClient())
            {
                var participants = new Participant() { FirstName = firstNameText.Text, LastName = lastNameText.Text, IsFirstTime = isFirstTime.IsChecked.Value };
                try
                {
                    var response = await client.PostAsJsonAsync(url, participants);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonMessage = await response.Content.ReadAsStringAsync();

                        Console.WriteLine(JsonMessage);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                }
                catch 
                {
                    MessageBox.Show("Something is wrong");
                }
            }
            this.firstNameText.Text = "";
            this.lastNameText.Text = "";
            isFirstTime.IsChecked = false;
        }
    }
}
