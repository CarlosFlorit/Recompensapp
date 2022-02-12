using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Recompensapp01
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        public bool UserOK { get; set; }


        public Window1()
        {
            InitializeComponent();
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            UserOK = false;
            //Creamos una instancia de la base de datos que hemos creado (UserEntities)
            DatabaseEntitiesAlumnos basededatos = new DatabaseEntitiesAlumnos();
            Profesor usuario = basededatos.Profesor.SingleOrDefault(
            us => us.usr.Equals(
            usuar.Text, StringComparison.InvariantCultureIgnoreCase)
            );

            if (usuario != null)
            {

                string passHash = CalcHash(password.Password);
                if (passHash.Equals(usuario.psw))
                {
                    UserOK = true; //Si coinciden UserOK=true y pasamos el control
                                   //a la pantalla principal
                }
                else
                { //Si no coinciden, mostramos el error y cerramos la aplicación.
                    MessageBox.Show("Contraseña " + passHash + " no válidos");
                    DialogResult = true;
                    return;
                }
                DialogResult = true;
            }
        }

        private string CalcHash(string contra)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(contra);
            byte[] result;
            SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
            result = sha.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // Convertimos los valores en hexadecimal
                // cuando tiene una cifra hay que rellenarlo con cero
                // para que siempre ocupen dos dígitos.
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString().ToUpper();
           

        }



    }

 
    }
