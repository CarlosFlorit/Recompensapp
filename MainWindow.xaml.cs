
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
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
using Firebase.Database;
using Firebase.Database.Query;


namespace Recompensapp01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int puntosToAdd = 0;
        int valorVentajas = 0;


        public MainWindow()
        {
            
            if (!CallLogin())
            {
                Application.Current.Shutdown(-1);
            }
            


            //FireBaseHelper firebaseHelper = new FireBaseHelper(); No utilizado
            InitializeComponent();
            Mostrar_Datos();
   




        }


        //carga combobox
        private void Mostrar_Datos()
        {
            DatabaseEntitiesAlumnos entidades = new DatabaseEntitiesAlumnos();




            List<Alumno> alumnos = entidades.Alumno.ToList<Alumno>();

            List<Ventaja> ventajasObtenidas = entidades.Ventaja.ToList<Ventaja>();


            lstAlumnos.Items.Clear();
            lstAlumnos.ItemsSource = alumnos;
            lstAlumnos.DisplayMemberPath = "Apellidos_alumno";
            lstAlumnos.SelectedValuePath = "Apellidos_alumno";
            lstAlumnos.Items.Refresh();
        }


        private bool CallLogin()
        {
            Window1 login = new Window1();
            login.ShowDialog();
            //Si la pantalla de login proporciona un valor true a la variable UserOK
            //gestionamos la identidad del usuario
            if (login.UserOK)
            {
                //Para trabajar con usuarios que no son de Windows, sino de la BBDD
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal
                );
                //Crear una identidad a partir del usuario (la cadena “Database”
                //simplemente nos indica de dónde hemos obtenido el usuario
                IIdentity identity = new GenericIdentity(login.usuar.Text, "Database");
                //Definimos unos roles, aunque por ahora nos los utilizaremos. La clase
                //GeneriPrincipal nos los exige.
                string[] roles = { "Usuario", "Administrador" };
                GenericPrincipal credencial = new GenericPrincipal(identity, roles);
                //Asignar esta credencial a la aplicación
                //Introducimos en la variable del sistema
                //System.Threading.Thread.CurrentPrincipal la identidad obtenida
                //con sus roles.
                System.Threading.Thread.CurrentPrincipal = credencial;
            }
            return login.UserOK;
        }


        //Seleccionar alumno en el combobox y mostrar datos
        private void lstAlumnos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseEntitiesAlumnos entidades = new DatabaseEntitiesAlumnos();

            Alumno alumno = lstAlumnos.SelectedItem as Alumno;


            boxNia.Text = alumno.Nia;
            boxCurso.Text = alumno.Curso;
            boxGrupo.Text = alumno.Grupo;
            boxPuntos.Text = Convert.ToString(alumno.Saldo);

            puntosNombreAlumno.Text = alumno.Apellidos_alumno;
            puntosNiaAlumno.Text = alumno.Nombre_alumno;
            boxSaldoActual.Text = Convert.ToString(alumno.Saldo);

            ventajasSaldoActual.Text = Convert.ToString(alumno.Saldo);
            ventajasPrecio.Text = Convert.ToString(valorVentajas);

            

            try
            {

   


                List<Obtencion> ventajasObtenidas = (from obt in entidades.Obtencion
                                                     where obt.niaAlumno == alumno.Nia 
                                                     select obt).ToList();

                List<Ventaja> ventajas = (from obt in entidades.Ventaja
                                          join Obtencion in entidades.Obtencion on obt.idVentaja equals Obtencion.idrVentaja
                                          where Obtencion.niaAlumno == alumno.Nia
                                          select obt).ToList();




                lstVentajas.ItemsSource = ventajas;
                lstVentajas.DisplayMemberPath = "nombreVentaja";
                lstVentajas.SelectedValuePath = "idVentaja";
            }

            catch
            {
                MessageBox.Show("El alumno todavía no ha adquirido ninguna ventaja");
                lstVentajas.ItemsSource = null;
                //lstAlumnos.Items.Clear();
            }


        }


        //
        //Comprobación de checkboxes para saldo 
        private void Check1_Checked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd + 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check1_Unchecked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd - 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check2_Checked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd + 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check2_Unchecked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd - 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check3_Checked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd + 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check3_Unchecked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd - 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check4_Checked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd + 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check4_Unchecked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd - 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check5_Checked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd + 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check5_Unchecked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd - 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check6_Checked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd + 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }

        private void Check6_Unchecked(object sender, RoutedEventArgs e)
        {
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
            puntosToAdd = puntosToAdd - 10;
            boxSaldoAdd.Text = Convert.ToString(puntosToAdd);
        }
        //Fin de comprobaciones para checkboxes de añadir saldo



        //
        //Botón añadir saldo
        private void AddSaldo(object sender, RoutedEventArgs e)
        {
            DatabaseEntitiesAlumnos entidades = new DatabaseEntitiesAlumnos();
            Alumno alumno = lstAlumnos.SelectedItem as Alumno;
            alumno.Saldo = alumno.Saldo + puntosToAdd;

            entidades.SaveChanges();
            boxSaldoActual.Text = Convert.ToString(alumno.Saldo);
            ventajasSaldoActual.Text = Convert.ToString(alumno.Saldo);

            //Ponemos checkboxes de saldo en blanco
            Check1.IsChecked = false;
            Check2.IsChecked = false;
            Check3.IsChecked = false;
            Check4.IsChecked = false;
            Check5.IsChecked = false;
            Check6.IsChecked = false;
        }



        //
        //Comprobación de checkboxes para "precio" de obtención de ventajas
        //
        private void vCheck1_Checked(object sender, RoutedEventArgs e)
        {
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
            valorVentajas = valorVentajas + 80;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }

        private void vCheck1_Unchecked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas - 80;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }

        private void vCheck2_Checked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas + 20;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }


        private void vCheck2_Unchecked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas - 20;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }

        private void vCheck3_Checked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas + 40;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }


        private void vCheck3_Unchecked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas - 40;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }

        private void vCheck4_Checked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas + 80;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }


        private void vCheck4_Unchecked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas - 80;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }

        private void vCheck5_Checked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas + 40;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }


        private void vCheck5_Unchecked(object sender, RoutedEventArgs e)
        {
            valorVentajas = valorVentajas - 40;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }

        private void vCheck6_Checked(object sender, RoutedEventArgs e)
        {
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
            valorVentajas = valorVentajas + 10;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }


        private void vCheck6_Unchecked(object sender, RoutedEventArgs e)
        {
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
            valorVentajas = valorVentajas - 10;
            ventajasPrecio.Text = Convert.ToString(valorVentajas);
        }
        //Fin de comprobación de checkboxes de ventajas
        //


        //Añadir las ventajas seleccionadas mediante botón
        private void AddVent(object sender, RoutedEventArgs e)
        {

            //Comprobamos si hay saldo suficiente
            if (valorVentajas > int.Parse(boxSaldoActual.Text)) 
            
            {
                MessageBox.Show("No tiene saldo suficiente");

            }

            //Si hay saldo suficiente:
            else { 

                //Actualizamos saldo actual del alumno
                DatabaseEntitiesAlumnos entidades = new DatabaseEntitiesAlumnos();

                Alumno alumno = entidades.Alumno.ToList<Alumno>().Where(c => c.Nia.Equals(boxNia.Text)).FirstOrDefault<Alumno>();
                alumno.Saldo = alumno.Saldo - valorVentajas;
                entidades.SaveChanges();
                boxSaldoActual.Text = Convert.ToString(alumno.Saldo);
                ventajasSaldoActual.Text = Convert.ToString(alumno.Saldo);


                //Añadimos las ventajas seleccionadas en los checkboxes
                if ((bool)vCheck1.IsChecked == true) 
                {
                
                    Obtencion vent1 = new Obtencion();
                    vent1.niaAlumno = boxNia.Text;
                    vent1.idrVentaja = 1;

                    try
                    {
                        entidades.Obtencion.AddObject(vent1);
                        entidades.SaveChanges();
                        MessageBox.Show("Ventaja añadida con éxito");
                        vCheck1.IsChecked = false;
                    }
                    catch
                    {
                        MessageBox.Show("Error al añadir ventaja");
                    }

                }

                if ((bool)vCheck2.IsChecked == true)
                {
                
                    Obtencion vent2 = new Obtencion();
                    vent2.niaAlumno = boxNia.Text;
                    vent2.idrVentaja = 2;

                    try{ 
                    entidades.Obtencion.AddObject(vent2);
                    entidades.SaveChanges();
                    MessageBox.Show("Ventaja añadida con éxito");
                    vCheck2.IsChecked = false;
                    }

                    catch
                    {
                        MessageBox.Show("Error al añadir ventaja");
                    }
                }

                if ((bool)vCheck3.IsChecked == true)
                {
                
                    Obtencion vent3 = new Obtencion();
                    vent3.niaAlumno = boxNia.Text;
                    vent3.idrVentaja = 3;

                    try
                    {
                        entidades.Obtencion.AddObject(vent3);
                        entidades.SaveChanges();
                        MessageBox.Show("Ventaja/s añadida/s con éxito");
                        vCheck3.IsChecked = false;
                    }

                    catch
                    {
                        MessageBox.Show("Error al añadir ventaja");
                    }
                }

                if ((bool)vCheck4.IsChecked == true)
                {
                
                    Obtencion vent4 = new Obtencion();
                    vent4.niaAlumno = boxNia.Text;
                    vent4.idrVentaja = 4;

                    try
                    {
                        entidades.Obtencion.AddObject(vent4);
                        entidades.SaveChanges();
                        MessageBox.Show("Ventaja añadida con éxito");
                        vCheck4.IsChecked = false;
                    }

                    catch
                    {
                        MessageBox.Show("Error al añadir ventaja");
                    }
                }


                if ((bool)vCheck5.IsChecked == true)
                {
               
                    Obtencion vent5 = new Obtencion();
                    vent5.niaAlumno = boxNia.Text;
                    vent5.idrVentaja = 5;

                    try
                    {
                        entidades.Obtencion.AddObject(vent5);
                        entidades.SaveChanges();
                        MessageBox.Show("Ventaja añadida con éxito");
                        vCheck5.IsChecked = false;
                    }

                    catch
                    {
                        MessageBox.Show("Error al añadir ventaja");
                    }
                }


                if ((bool)vCheck6.IsChecked == true)
                {
                
                    Obtencion vent6 = new Obtencion();
                    vent6.niaAlumno = boxNia.Text;
                    vent6.idrVentaja = 6;

                    try
                    {
                        entidades.Obtencion.AddObject(vent6);
                        entidades.SaveChanges();
                        MessageBox.Show("Ventaja añadida con éxito");
                        vCheck6.IsChecked = false;
                    }

                    catch
                    {
                        MessageBox.Show("Error al añadir ventaja");
                    }
                }


                //Limpiamos los checkboxes

                
                
               


                //Actualizamos la lista de ventajas

                Alumno alu = lstAlumnos.SelectedItem as Alumno;

                Obtencion ventajasObt = entidades.Obtencion.FirstOrDefault(c => c.niaAlumno.Equals(alu.Nia));




                List<Obtencion> ventajasObtenidas = (from obt in entidades.Obtencion
                                                 where obt.niaAlumno == alumno.Nia
                                                 select obt).ToList();

                try
                {

                    /*
                    List<Ventaja> ventajas = (from obt in entidades.Ventaja
                                          where obt.idVentaja == ventajasObt.idrVentaja
                                          select obt).ToList();
                    */


                    List<Ventaja> ventajas = (from obt in entidades.Ventaja
                                              join Obtencion in entidades.Obtencion on obt.idVentaja equals Obtencion.idrVentaja
                                              where Obtencion.niaAlumno == alumno.Nia
                                              select obt).ToList();





                    lstVentajas.DisplayMemberPath = "nombreVentaja";
                    lstVentajas.SelectedValuePath = "idVentaja";
                    lstVentajas.ItemsSource = ventajas;
                    lstVentajas.Items.Refresh();
                }

                catch
                {
                    MessageBox.Show("Error al actualizar la lista de ventajas");
                    //lstVentajas.ItemsSource = null;
                    //lstAlumnos.Items.Clear();
                }


            }

        }
    }
}
