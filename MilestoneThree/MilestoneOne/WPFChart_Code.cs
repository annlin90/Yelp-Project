using Npgsql;
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

namespace testTTTT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ColumnChart();

        }

        private void ColumnChart()
        {
            List<KeyValuePair<string, int>> myChartData = new List<KeyValuePair<string, int>>();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = "select postal_code, count(business_id) from businesstable where city = 'Madison' group by postal_code order by postal_code;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        myChartData.Add(new KeyValuePair<string, int>(reader.GetString(0), reader.GetInt32(1)));

                    }


                }////select name, checknum from businesstable, checkin where businesstable.business_id = checkin.business_id order by name;
            }

            comm.Close();
            myChart.DataContext = myChartData;
        }

        private string buildCommString()
        {

            return "Host=localhost; Port = 5432; User Id = postgres; Password = persona41; Database = milestone2";

        }

    }
}
