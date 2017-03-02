using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_DEV
{

    public partial class CrearModulo : Form
    {
        public Point formPosition;
        public Boolean mouseAction;

        //Se instancia la clase conexion
        Conexion c = new Conexion();

        public CrearModulo()
        {
            InitializeComponent();
        }

        private void BarraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void BarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void BarraSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {

            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Se instancia la clase conexion
            //Conexion c = new Conexion();

            //Se declaran variables locales
            string cvmodulo, descripcion, cvmodpad, ambiente, modulo, usuumod, prgumod, fhumod, fecha, hora, fecha_hora;
            DateTime fhumod1, fh;
            int orden;

            // Se asginan valores de los componentes
            cvmodulo = txtCvModulo.Text;
            descripcion = txtDescripcion.Text;
            cvmodpad = txtCvModPad.Text;
            ambiente = txtAmbiente.Text;
            modulo = txtModulo.Text;
            usuumod = txtUsuUmod.Text;
            prgumod = txtPrguMod.Text;


            //fhumod = dtpFhuMod.Text;
            //Se parsea el texto tomado del datetimepicker 
            //fhumod1 = DateTime.Parse(fhumod);

            orden = int.Parse(txtOrden.Text);

            //se arma la fecha 
            fecha = DateTime.Now.ToShortDateString();
            hora = DateTime.Now.ToLongTimeString();

            fecha_hora = fecha + " " + hora;

            fh = DateTime.Parse(fecha_hora);
            //MessageBox.Show(fecha_hora);

            // pasamos parametros a la funcion
            c.crearModulo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, usuumod, fh, prgumod);

            txtCvModulo.Text = "";
            txtDescripcion.Text = "";
            txtCvModPad.Text = "";
            txtOrden.Text = "";
            txtAmbiente.Text = "";
            txtModulo.Text = "";
            txtUsuUmod.Text = "";
            txtPrguMod.Text = "";

            CrearModulo_Load(sender, e);
        }

        private void CrearModulo_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            c.mostrarModulo(dgvModulo);
        }

        private void txtBCvModulo_KeyUp(object sender, KeyEventArgs e)
        {
            string cvmodulo;

            cvmodulo = txtBCvModulo.Text;

            dgvModulo.DataSource = c.buscarModulo(cvmodulo);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string cvmodulo, descripcion, cvmodpad, ambiente, modulo, usuumod, prgumod, fecha, hora, fecha_hora;
            int orden;
            DateTime fh;

            cvmodulo = txtCvModuloA.Text;
            descripcion = txtDescripcionA.Text;
            cvmodpad = txtCvModPadA.Text;
            orden = int.Parse(txtOrdenA.Text);
            ambiente = txtAmbienteA.Text;
            modulo = txtModuloA.Text;
            usuumod = txtUsuUmodA.Text;
            prgumod = txtPrgUmodA.Text;

            fecha = DateTime.Now.ToShortDateString();
            hora = DateTime.Now.ToLongTimeString();

            fecha_hora = fecha + " " + hora;

            fh = DateTime.Parse(fecha_hora);

            c.actualizarCatalogo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, usuumod, fh, prgumod);
            CrearModulo_Load(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cvmodulo;

            cvmodulo = txtCvModuloB.Text;

            c.eliminarCatalogo(cvmodulo);

            CrearModulo_Load(sender, e);
        }

        private void dgvModulo_SelectionChanged(object sender, EventArgs e)
        {
            string cvmodulo, descripcion, cvmodpad, ambiente, modulo, usuumod, prgumod;
            int orden;

            if (dgvModulo.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvModulo.SelectedRows[0];

                cvmodulo = row.Cells["CVMODULO"].Value.ToString();
                descripcion = row.Cells["DESCRIPCION"].Value.ToString();
                cvmodpad = row.Cells["CVMODPAD"].Value.ToString();
                orden = Convert.ToInt32(row.Cells["ORDEN"].Value.ToString());
                ambiente = row.Cells["AMBIENTE"].Value.ToString();
                modulo = row.Cells["MODULO"].Value.ToString();
                usuumod = row.Cells["USUUMOD"].Value.ToString();
                prgumod = row.Cells["PRGUMOD"].Value.ToString();


                //cajas de texto panel actualizar
                txtCvModuloA.Text = cvmodulo;
                txtDescripcionA.Text = descripcion;
                txtCvModPadA.Text = cvmodpad;
                txtOrdenA.Text = Convert.ToString(orden);
                txtAmbienteA.Text = ambiente;
                txtModuloA.Text = modulo;
                txtUsuUmodA.Text = usuumod;
                txtPrgUmodA.Text = prgumod;

                //cajas de texto borrar
                txtCvModuloB.Text = cvmodulo;
                txtDescripcionB.Text = descripcion;
                txtCvModPadB.Text = cvmodpad;
                txtOrdenB.Text = Convert.ToString(orden);
                txtAmbienteB.Text = ambiente;
                txtModuloB.Text = modulo;
                txtUsuUmodB.Text = usuumod;
                txtPrgUmodB.Text = prgumod;
            }
        }
    }
}
