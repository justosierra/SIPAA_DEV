using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIPAA_DEV.Recursos_Humanos.App_Code;
using System.Threading;

namespace SIPAA_DEV.Recursos_Humanos.Administracion
{
    public partial class Crear_Perfil : Form
    {
        public Point formPosition;
        public Boolean mouseAction;
        public int IdPerfil;

        public Crear_Perfil()
        {
            InitializeComponent();
        }

        private void Crear_Perfil_Load(object sender, EventArgs e)
        {

            PanelBuscar.Location = new Point(93,226);
            Perfiles objPerfil = new Perfiles();

            string strPerfil = "";

            if (txtBuscarPerfil.Text != String.Empty) {

                strPerfil = txtBuscarPerfil.Text;
            }

           DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(strPerfil);

            dgvPerfiles.DataSource = dtPerfiles;
            int icolumns = dgvPerfiles.ColumnCount;
            for (int iContador = 0;iContador < icolumns ; iContador++) {

                dgvPerfiles.Columns[iContador].ReadOnly = true;

            }

            dgvPerfiles.Columns[0].Visible = false;
            
        }

        private void BarraSuperior_Paint(object sender, PaintEventArgs e)
        {

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

        private void dgvPerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

            int selectedrowindex = dgvPerfiles.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPerfiles.Rows[selectedrowindex];
            string ValorRow = Convert.ToString(selectedRow.Cells["Descripcion"].Value);
            IdPerfil = Convert.ToInt32(selectedRow.Cells["CVPerfil"].Value);
            txtPerfilEditar.Text = ValorRow;
            txtEliminar.Text = ValorRow;
            txtPerfilEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnUPD.Enabled = true;
        }

        private void panelBack_Paint(object sender, PaintEventArgs e)
        {

        }

 
        private void btnBuscar_Click(object sender, EventArgs e)
        {


            string strPerfil = "";

            if (txtBuscarPerfil.Text != String.Empty)
            {

                strPerfil = txtBuscarPerfil.Text;
            }
            else {
                strPerfil = "%";
            }
            Perfiles objPerfil = new Perfiles();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(strPerfil);

            dgvPerfiles.DataSource = dtPerfiles;
            int icolumns = dgvPerfiles.ColumnCount;
            for (int iContador = 0; iContador < icolumns; iContador++)
            {

                dgvPerfiles.Columns[iContador].ReadOnly = true;

            }
        }

        private void btnUPD_Click(object sender, EventArgs e)
        {
            Perfiles objPerfil = new Perfiles();
            objPerfil.CVPerfil = IdPerfil;
            objPerfil.Descripcion = txtPerfilEditar.Text;
            objPerfil.PrguMod = "Recursos Humanos";
            objPerfil.UsuuMod = "vjiturburuv";

            GestionarPefilesxOpcion(txtPerfilEditar, objPerfil, "Perfil Actualizado Correctamente", 2, sender, e);

            PanelEditar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            Perfiles objPerfil = new Perfiles();
            objPerfil.CVPerfil = IdPerfil;
            objPerfil.Descripcion = txtEliminar.Text;
            objPerfil.PrguMod = "Recursos Humanos";
            objPerfil.UsuuMod = "vjiturburuv";

            GestionarPefilesxOpcion(txtEliminar, objPerfil, "Perfil Eliminado Correctamente", 3, sender, e);



        }

        private void btnGuardarNvo_Click(object sender, EventArgs e)
        {
            Perfiles objPerfil = new Perfiles();

           
            objPerfil.Descripcion = txtNvoPerfil.Text;
            objPerfil.PrguMod = "Recursos Humanos";
            objPerfil.UsuuMod = "vjiturburuv";

            GestionarPefilesxOpcion(txtNvoPerfil, objPerfil, "Perfil Guardado Correctamente", 1, sender, e);


            panelNuevo.Visible = false;
            PanelBuscar.Location = new Point(93, 226);
            btnAdd.Visible = true;
            btnClear.Visible = false;
            txtNvoPerfil.Text = String.Empty;
            txtBuscarPerfil.Text = String.Empty;
            btnBuscar_Click(sender, e);
        }


        private  void GestionarPefilesxOpcion(TextBox txt, Perfiles objPerfil,
                        string strMensaje, int iOpcion, object sender, EventArgs e)
        {

            if (txt.Text != String.Empty || txt.Text != "Sin Selección")
            {
                
                try
                {
                    int iResponse = objPerfil.GestionarPerfiles(objPerfil, iOpcion);
                    if (iResponse == iOpcion)
                    {
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                        lbMensaje.Text = strMensaje;
                      //  Thread.Sleep(3000);
                        Crear_Perfil_Load(sender, e);
                    }
                    else if (iResponse == 0)
                    {
                            panelTag.Visible = true;
                            panelTag.BackColor = ColorTranslator.FromHtml("#29b6f6");
                            lbMensaje.Text = "El Perfil Ingresado ya se encuentra registrado.";
                    }

                }
                catch (Exception ex)
                {

                    panelTag.Visible = true;
                    panelTag.BackColor = ColorTranslator.FromHtml("#ef5350");
                    lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde";
                }
            }
            else
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#29b6f6");
                lbMensaje.Text = "El Campo Editar no puede ir Vacio";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            panelNuevo.Visible = true;
            PanelBuscar.Location = new Point(538,504);
            btnAdd.Visible = false;
            btnClear.Location = new Point(11, 66);
            btnClear.Visible = true;
            txtBuscarPerfil.Text = String.Empty;
            btnBuscar_Click(sender, e);
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            panelNuevo.Visible = false;
            PanelBuscar.Location = new Point(93,226);
            btnAdd.Visible = true;
            btnClear.Visible = false;
            txtNvoPerfil.Text = "";
        }

        private void dgvPerfiles_SelectionChanged(object sender, EventArgs e)
        {
            

            if (dgvPerfiles.SelectedRows.Count != 0)
            {

                PanelEditar.Enabled = true;
                panelEliminar.Enabled = true;
                DataGridViewRow row = this.dgvPerfiles.SelectedRows[0];

                IdPerfil = Convert.ToInt32(row.Cells["CVPerfil"].Value.ToString());
                string ValorRow =  row.Cells["Descripcion"].Value.ToString();
                txtPerfilEditar.Text = ValorRow;
                txtEliminar.Text = ValorRow;
                txtPerfilEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnUPD.Enabled = true;
            }

           /* int selectedrowindex = dgvPerfiles.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPerfiles.Rows[selectedrowindex];
            string ValorRow = Convert.ToString(selectedRow.Cells["Descripcion"].Value);
            IdPerfil = Convert.ToInt32(selectedRow.Cells["CVPerfil"].Value);
            txtPerfilEditar.Text = ValorRow;
            txtEliminar.Text = ValorRow;
            txtPerfilEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnUPD.Enabled = true;*/
        }

        private void dgvPerfiles_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

