using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APE2;

namespace APE1
{
    public partial class Caratula: Form
    {
        public Caratula()
        {
            InitializeComponent();
        }


        private void Caratula_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            CALCULARMATRIZ m = new CALCULARMATRIZ();
            this.Hide();  // Oculta la ventana actual (Caratula)
            m.ShowDialog();  // Abre la nueva ventana de forma modal (espera a que se cierre para continuar)
            this.Show();
        }
    }
}
