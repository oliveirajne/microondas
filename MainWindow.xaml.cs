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
using System.Windows.Threading;
using System.Threading;

namespace microondas
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
/*
        System.Timers.Timer t;
        int m, s = 30;
*/
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnIniciar_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(lblTempo.Text) || String.IsNullOrEmpty(lblPotencia.Text))
            {
                MessageBox.Show("Há campos em branco!", "AVISO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (Convert.ToInt32(lblTempo.Text) > 0 && Convert.ToInt32(lblTempo.Text) <= 120)
            {
                if (Convert.ToInt32(lblPotencia.Text) > 0 && Convert.ToInt32(lblPotencia.Text) <= 8)
                {
                    Contador(Convert.ToInt32(lblTempo.Text));
                }
                else
                {
                    MessageBox.Show("Potência: Apenas números entre 1 e 8", "AVISO", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("Tempo: Apenas números entre 1 e 120 (2 min = 120 seg)", "AVISO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnInicioRapido_Click(object sender, RoutedEventArgs e)
        {

            lblTempo.Text = "30";
            lblPotencia.Text = "8";
           
            Contador(Convert.ToInt32(lblTempo.Text));

        }
/*
        public void OnTimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Invoke(new Action(() =>
            //{
            s -= 1;
            if (s < 0)
            {
                m -= 1;
                s = 59;
            }
            txtTeste.Text = string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            //    }));
        }
*/
        public void Contador(int tempo)
        {
            /*           t = new System.Timers.Timer
                       {
                           Interval = 1000
                       };
                       t.Elapsed -= OnTimeEvent;


                       t.Start();

                       btnIniciar.IsEnabled = false;
                       btnInicioRapido.IsEnabled = false;
           */
            int min = 0, sec;
            int qtdSimbolo;
            string txtSimbolo = "";
            string texto = "AQUECENDO";
            txtString.Text = texto;
            

            if (tempo > 60)
            {

                min = tempo / 60;
                sec = tempo % 60;
                lblTimerM.Content = Convert.ToString(min);

                if (sec < 10)
                {
                    lblTimerS.Content = "0" + Convert.ToString(sec);
                }
                else
                {
                    lblTimerS.Content = Convert.ToString(sec);
                }

            }
            else
            {
                sec = tempo;
                lblTimerS.Content = sec;
            }

            qtdSimbolo = Convert.ToInt32(lblPotencia.Text);

            for (int i = 0; i < qtdSimbolo; i++)
            {
                txtSimbolo = txtSimbolo + '.';
            }

            Task.Factory.StartNew(() =>
            {

                for (int i = tempo; i > 0; i--)
                {
                    int aux = i;
                    Thread.Sleep(1000);
                    
                    Dispatcher.Invoke(new Action(() =>
                    {
                        
                        sec -= 1;

                        if (sec < 0)
                        {
                            min -= 1;
                            sec = 59;
                        }

                        lblTimerM.Content = Convert.ToString(min);
                        if (sec < 10)
                        {
                            lblTimerS.Content = "0" + Convert.ToString(sec);
                        }
                        else
                        {
                            lblTimerS.Content = Convert.ToString(sec);
                        }

                        txtString.Text = txtString.Text + txtSimbolo;
                        
                        if (i < 2)
                        {
                            txtString.Text = txtString.Text + "AQUECIDO";
                        }
                        
                    }));

                }

                

            });
            
        }

        public void FinalizaAquecimento(string texto)
        {
            txtString.Text = texto + "AQUECIDO";
        }

    }
}
