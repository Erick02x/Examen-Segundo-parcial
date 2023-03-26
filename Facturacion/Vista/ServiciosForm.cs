using Datos;
using Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vista
{
    public partial class ServiciosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ServiciosForm()
        {
            InitializeComponent();
        }

        string operacion;
        servicio servicio;
        ServicioDB ServicioDB = new ServicioDB();

        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;
            DescripcionRespuestaTextBox.Enabled = true;
            //ExistenciaTextBox.Enabled = true;
            PrecioTextBox.Enabled = true;
            AdjuntarImagenButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            NuevoButton.Enabled = false;
        }
        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            DescripcionRespuestaTextBox.Clear();
            //ExistenciaTextBox.Clear();
            PrecioTextBox.Clear();
            EstaDisponibleCheckBox.Checked = false;
            ImagenPictureBox.Image = null;
            servicio = null;
        }

        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            DescripcionRespuestaTextBox.Enabled = false;
            //ExistenciaTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            AdjuntarImagenButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            NuevoButton.Enabled = true;
        }

        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles();
        }

        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            operacion = "Modificar";
            if (ServiciosDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = ServiciosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                DescripcionTextBox.Text = ServiciosDataGridView.CurrentRow.Cells["Descripcion"].Value.ToString();
                DescripcionRespuestaTextBox.Text = ServiciosDataGridView.CurrentRow.Cells["DescripcionRespuesta"].Value.ToString();
                //ExistenciaTextBox.Text = ProductosDataGridView.CurrentRow.Cells["Existencia"].Value.ToString();//
                PrecioTextBox.Text = ServiciosDataGridView.CurrentRow.Cells["Precio"].Value.ToString();
                EstaDisponibleCheckBox.Checked = Convert.ToBoolean(ServiciosDataGridView.CurrentRow.Cells["EstaDisponible"].Value);

                //byte[] img = productoDB.DevolverFoto(ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                //if (img.Length > 0)
                //{
                //    MemoryStream ms = new MemoryStream(img);
                //    ImagenPictureBox.Image = System.Drawing.Bitmap.FromStream(ms);
                //}
                HabilitarControles();
                CodigoTextBox.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            servicio = new servicio();
            servicio.Codigo = CodigoTextBox.Text;
            servicio.Descripcion = DescripcionTextBox.Text;
            servicio.DescripcionRespuesta = DescripcionRespuestaTextBox.Text;
            servicio.Precio = Convert.ToDecimal(PrecioTextBox.Text);
            //producto.Existencia = Convert.ToInt32(ExistenciaTextBox.Text);
            servicio.EstaDisponible = EstaDisponibleCheckBox.Checked;

            if (ImagenPictureBox.Image != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                ImagenPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                servicio.Foto = ms.GetBuffer();
            }

            if (operacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un código");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionTextBox, "Ingrese una descripción");
                    DescripcionTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(DescripcionRespuestaTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionRespuestaTextBox, "Ingrese una descripción");
                    DescripcionRespuestaTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                //if (string.IsNullOrEmpty(ExistenciaTextBox.Text))
                //{
                //    errorProvider1.SetError(ExistenciaTextBox, "Ingrese una existencia");
                //    ExistenciaTextBox.Focus();
                //    return;
                //}
                //errorProvider1.Clear();

                if (string.IsNullOrEmpty(PrecioTextBox.Text))
                {
                    errorProvider1.SetError(PrecioTextBox, "Ingrese un precio");
                    PrecioTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                bool inserto = ServicioDB.Insertar(servicio);
                if (inserto)
                {
                    DeshabilitarControles();
                    LimpiarControles();
                    TraerServicio();
                    MessageBox.Show("Registro guardado con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puro guardar el registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (operacion == "Modificar")
            {
                bool modifico = ServicioDB.Editar(servicio);
                if (modifico)
                {
                    CodigoTextBox.ReadOnly = false;
                    DeshabilitarControles();
                    LimpiarControles();
                    TraerServicio();
                    MessageBox.Show("Registro actualizado con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puro actualizar el registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExistenciaTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void PrecioTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void AdjuntarImagenButton_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult resultado = dialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                ImagenPictureBox.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void ProductosForm_Load(object sender, EventArgs e)
        {
            TraerServicio();
        }

        private void TraerServicio()
        {
            ServiciosDataGridView.DataSource = ServicioDB.DevolverServicio();
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (ServiciosDataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    bool elimino = ServicioDB.Eliminar(ServiciosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        TraerServicio();
                        MessageBox.Show("Registro eliminado");
                    }
                    else
                    { MessageBox.Show("No se pudo eliminar el registro"); }
                }
            }
        }
    }
}
