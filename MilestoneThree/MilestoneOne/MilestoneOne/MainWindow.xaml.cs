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
using Npgsql;

namespace MilestoneOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Business
        {
            public string name { get; set; }
            public string city { get; set; }
            public string state { get; set; }

        }

        public class Tips
        {
            public string tips { get; set; }

        }

        public MainWindow()
        {
            InitializeComponent();
            addStates();
            addColumns2Grid();
            addColumns2Grid2();
            
            
        }

        private string buildCommString()
        {

            return "Host=localhost; Port = 5432; User Id = postgres; Password = persona41; Database = milestone2";

        }

        public void addStates()
        {
           var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
           using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = "select distinct state from businesstable order by state";
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        comboBox.Items.Add(reader.GetString(0));

                    }


                }
            }

            comm.Close();
        }


        public void addColumns2Grid2()
        {
            DataGridTextColumn cel1 = new DataGridTextColumn();
            cel1.Header = "Tips Text";
            cel1.Binding = new Binding("tips");
            cel1.Width = 1500;
            dataGrid1.Columns.Add(cel1);
        }
          

        public void addColumns2Grid()
        {
            DataGridTextColumn cel1 = new DataGridTextColumn();
            cel1.Header = "Business Name";
            cel1.Binding = new Binding("name");
            cel1.Width = 255;
            dataGrid.Columns.Add(cel1);

            DataGridTextColumn cel2 = new DataGridTextColumn();
            cel2.Header = "city";
            cel2.Binding = new Binding("city");
            cel2.Width = 255;
            dataGrid.Columns.Add(cel2);

            DataGridTextColumn cel3 = new DataGridTextColumn();
            cel3.Header = "State";
            cel3.Binding = new Binding("state");
            cel3.Width = 255;
            dataGrid.Columns.Add(cel3);



            // dataGrid.Items.Add(new Business() { name = "business1", state = "WA" });
            // dataGrid.Items.Add(new Business() { name = "business2", state = "NA" });
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            dataGrid.Items.Clear();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = "select name, city, state from businesstable where state = '" + comboBox.SelectedItem.ToString() + "';";
                 
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataGrid.Items.Add(new Business() { name = reader.GetString(0), city = reader.GetString(1), state = reader.GetString(2) });

                    }


                }
            }

            comboBox1.Items.Clear();
            using (var cmd = new NpgsqlCommand())
            {

                cmd.Connection = comm;
                cmd.CommandText = "select distinct city from businesstable where state = '" + comboBox.SelectedItem.ToString() + "';";
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));

                    }


                }
            }
            
            comm.Close();
           
           
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.Items.Clear();

            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
 
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = comm;
                    cmd.CommandText = "select distinct name, city, state, postal_code from businesstable where city= '" + comboBox1.SelectedItem.ToString() + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            dataGrid.Items.Add(new Business() { name = reader.GetString(0), city = reader.GetString(1), state = reader.GetString(2) });

                        }


                    }
                }
            
        
            comm.Close();

          comm.Open();
           comboBox2.Items.Clear();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = "select distinct postal_code from businesstable where city = '" + comboBox1.SelectedItem.ToString() + "';";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));

                    }


                }
            }

            comm.Close();



        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.Items.Clear();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = "select distinct name, city, state from businesstable where postal_code= '" + comboBox2.SelectedItem.ToString() + "' and city = '" + comboBox1.SelectedItem.ToString() + "' and state = '" + comboBox.SelectedItem.ToString() + "';";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        dataGrid.Items.Add(new Business() { name = reader.GetString(0), city = reader.GetString(1), state = reader.GetString(2) });

                    }


                }
            }

            comm.Close();

            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = " select distinct categories.category from categories order by category;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader.GetString(0));
                        comboBox5.Items.Add(reader.GetString(0));

                    }


                }
            }

            comm.Close();

        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           dataGrid.Items.Clear();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = cmd.CommandText = "select distinct businesstable.name, city, state from businesstable inner join category on businesstable.business_id = category.business_id where state = '" + comboBox.SelectedItem.ToString() + "' and city = '" + comboBox1.SelectedItem.ToString() + "' and postal_code = '" + comboBox2.SelectedItem.ToString() + "' and category.categories ~ '" + comboBox3.SelectedItem.ToString() + "';";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        dataGrid.Items.Add(new Business() { name = reader.GetString(0), city = reader.GetString(1), state = reader.GetString(2) });

                    }


                }
            }

            comm.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Clear();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = cmd.CommandText = "select distinct friends.friends from reviewuser, friends where reviewuser.user_id = '" + textBox.Text.ToString() + "' and reviewuser.friends ~ friends.friends;";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        comboBox4.Items.Add(reader.GetString(0));

                    }


                }
            }

            comm.Close();
        }

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid1.Items.Clear();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = cmd.CommandText = "select text from review where user_id = '" + comboBox4.SelectedItem.ToString() + "' order by date desc limit 1;";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        dataGrid1.Items.Add(new Tips() { tips = reader.GetString(0)});

                    }


                }
            }

            comm.Close();

        }

        private void comboBox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.Items.Clear();
            var comm = new NpgsqlConnection(buildCommString());
            comm.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = comm;
                cmd.CommandText = cmd.CommandText = "select distinct businesstable.name, city, state from businesstable inner join category on businesstable.business_id = category.business_id where state = '" + comboBox.SelectedItem.ToString() + "' and city = '" + comboBox1.SelectedItem.ToString() + "' and postal_code = '" + comboBox2.SelectedItem.ToString() + "' and category.categories ~ '" + comboBox3.SelectedItem.ToString() + "' and category.categories ~ '" + comboBox5.SelectedItem.ToString() + "';";
                ///fd
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        dataGrid.Items.Add(new Business() { name = reader.GetString(0), city = reader.GetString(1), state = reader.GetString(2) });

                    }


                }
            }

            comm.Close();
        }
    }
    }

// select distinct friends.friends from reviewuser, friends where reviewuser.user_id = 'Q_5X6fW5FIpWWRcxn0kgHw' and reviewuser.friends ~ friends.friends;
//select text from review where user_id = 'QSA05SquUKJZnxasypxvjg' order by date desc limit 1;

/*cmd.Connection = comm;
               cmd.CommandText = " select distinct category.categories from businesstable inner join category on businesstable.business_id = category.business_id where postal_code = '" + comboBox2.SelectedItem.ToString() + "' and city = '" + comboBox1.SelectedItem.ToString() + "'";
               using (var reader = cmd.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       string test = reader.GetString(0);
                       test = test.Replace("[", string.Empty).Replace("]", string.Empty);
                       test = test.Replace(",", "|");
                       test = test.Replace("'", string.Empty);
                       test = "'" + test + "'";

                       comboBox3.Items.Add(test);
*/

//select friends from reviewuser inner join review on reviewuser.user_id = review.user_id where reviewuser.user_id = 'XLNaiWF21OsAf0fMV6kuSg';