using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace konyvtar
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string nev = "";
		int eletkor = 0;
		string mufaj = "";
		string ertesitesek = "";
		string tagsag = "";

		string filePath = "olvasok.txt";

		public MainWindow()
		{
			InitializeComponent();
			Beolvasas(); 
		}

		
		private void txt_nev_TextChanged(object sender, TextChangedEventArgs e)
		{
			nev = txt_nev.Text;
		}

		
		private void txt_eletkor_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (int.TryParse(txt_eletkor.Text, out int kor))
			{
				eletkor = kor;
			}
			else
			{
				eletkor = 0; 
			}
		}

	
		private void cbox_mufaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cbox_mufaj.SelectedItem is ComboBoxItem item)
			{
				mufaj = item.Content.ToString();
			}
		}

		
		private void Ertesites_Checked(object sender, RoutedEventArgs e)
		{
			List<string> lista = new List<string>();
			if (cb_hirlevel.IsChecked == true) lista.Add("Hírlevél");
			if (cb_sms.IsChecked == true) lista.Add("SMS");
			ertesitesek = lista.Count > 0 ? string.Join(", ", lista) : "nincs";
		}

	
		private void rb_norm_Checked(object sender, RoutedEventArgs e)
		{
			tagsag = "Normál";
		}
		private void rb_diak_Checked(object sender, RoutedEventArgs e)
		{
			tagsag = "Diák";
		}
		private void rb_nyugdij_Checked(object sender, RoutedEventArgs e)
		{
			tagsag = "Nyugdíjas";
		}

		
		private void btn_mentes_Click(object sender, RoutedEventArgs e)
		{
			Olvaso uj = new Olvaso
			{
				Nev = nev,
				Eletkor = eletkor,
				Mufaj = mufaj,
				Ertesitesek = ertesitesek,
				Tagsag = tagsag
			};

			
			File.AppendAllText(filePath, uj.ToString() + Environment.NewLine);

			
			tbl_status.Text = "Regisztráció sikeres!";

			
			lbox_olvasok.Items.Add(uj.Nev);
		}

		
		private void Beolvasas()
		{
			if (File.Exists(filePath))
			{
				string[] sorok = File.ReadAllLines(filePath);
				foreach (string sor in sorok)
				{
					string[] adatok = sor.Split(';');
					if (adatok.Length >= 1)
					{
						lbox_olvasok.Items.Add(adatok[0]); 
					}
				}
			}
		}
	}


	public class Olvaso
	{
		public string Nev { get; set; }
		public int Eletkor { get; set; }
		public string Mufaj { get; set; }
		public string Ertesitesek { get; set; }
		public string Tagsag { get; set; }

		public override string ToString()
		{
			return $"{Nev};{Eletkor};{Mufaj};{Ertesitesek};{Tagsag}";
		}
	}
}