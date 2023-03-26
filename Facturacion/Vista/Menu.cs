﻿using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            UsuariosForm userForm = new UsuariosForm();
            userForm.MdiParent = this;
            userForm.Show();
        }

        private void ProductosToolStripButton_Click(object sender, EventArgs e)
        {
            ServiciosForm productosForm = new ServiciosForm();
            productosForm.MdiParent = this;
            productosForm.Show();
        }

        private void NuevaFacturaToolStripButton_Click(object sender, EventArgs e)
        {
            TicketForm facturaForm = new TicketForm();
            facturaForm.MdiParent = this;
            facturaForm.Show();
        }

        private void backStageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
