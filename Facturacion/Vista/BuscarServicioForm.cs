using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class BuscarServicioForm : Form
    {
        public BuscarServicioForm()
        {
            InitializeComponent();
        }
        ServicioDB productoDB = new ServicioDB();
        public servicio producto = new servicio();
        private void BuscarProductoForm_Load(object sender, EventArgs e)
        {
            ProductosDataGridView.DataSource = productoDB.DevolverServicio();
        }

        private void DescripcionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ProductosDataGridView.DataSource = productoDB.DevolverServicioPorDescripcion(DescripcionTextBox.Text);
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (ProductosDataGridView.RowCount > 0)
            {
                if (ProductosDataGridView.SelectedRows.Count > 0)
                {
                    producto.Codigo = ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                    producto.Descripcion = ProductosDataGridView.CurrentRow.Cells["Descripcion"].Value.ToString();
                    //producto.Existencia = Convert.ToInt32(ProductosDataGridView.CurrentRow.Cells["Existencia"].Value);//
                    producto.Precio = Convert.ToDecimal(ProductosDataGridView.CurrentRow.Cells["Precio"].Value);
                    producto.EstaDisponible = Convert.ToBoolean(ProductosDataGridView.CurrentRow.Cells["EstaDisponible"].Value);
                    Close();
                }
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
