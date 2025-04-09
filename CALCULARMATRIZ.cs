using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APE2
{
    public partial class CALCULARMATRIZ : Form
    {
        private TextBox[,] matrizA;
        private TextBox[] matrizResultado;
        //Almacenar filas y columnas
        int fila1, columna1;
        public CALCULARMATRIZ()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            groupBoxMatrizA.Controls.Clear();
            groupBoxMatrizB.Controls.Clear();
            label5.Text = "";
            label10.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            groupBoxMatrizA.Controls.Clear();
            groupBoxMatrizB.Controls.Clear();
            label5.Text = "";
            label10.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            VerificarSimetria();

            if (matrizA == null)
            {
                MessageBox.Show("Debe crear la matriz antes de calcular.", "Error");
                return;
            }

            int[,] matrizAValues = new int[fila1, columna1];
            bool hayAlgunValorDiferenteDeCero = false;

            for (int i = 0; i < fila1; i++)
            {
                for (int j = 0; j < columna1; j++)
                {
                    if (!int.TryParse(matrizA[i, j].Text, out matrizAValues[i, j]))
                    {
                        MessageBox.Show("Valores no válidos en la matriz A", "Error");
                        return;
                    }
                    if (matrizAValues[i, j] != 0)
                    {
                        hayAlgunValorDiferenteDeCero = true;
                    }
                }
            }

            if (!hayAlgunValorDiferenteDeCero)
            {
                MessageBox.Show("La matriz contiene solo ceros. No se realizarán cálculos.", "Matriz vacía");
                return;
            }

            int[,] matrizTranspuesta = new int[columna1, fila1];
            for (int i = 0; i < fila1; i++)
            {
                for (int j = 0; j < columna1; j++)
                {
                    matrizTranspuesta[j, i] = matrizAValues[i, j];
                }
            }

            groupBoxMatrizB.Controls.Clear();
            int tamañoMatriz = groupBoxMatrizB.Width / fila1 - 10;
            for (int x = 0; x < columna1; x++)
            {
                for (int y = 0; y < fila1; y++)
                {
                    TextBox txt = new TextBox()
                    {
                        Text = matrizTranspuesta[x, y].ToString(),
                        Top = (x * 25) + 20,
                        Left = y * tamañoMatriz + 6,
                        Width = tamañoMatriz
                    };
                    groupBoxMatrizB.Controls.Add(txt);
                }
            }

            int sumaDiagPrincipalA = 0, sumaDiagSecundariaA = 0;
            int sumaDiagPrincipalB = 0, sumaDiagSecundariaB = 0;

            for (int i = 0; i < Math.Min(fila1, columna1); i++)
            {
                sumaDiagPrincipalA += matrizAValues[i, i];
                sumaDiagSecundariaA += matrizAValues[i, columna1 - i - 1];
            }

            for (int i = 0; i < Math.Min(columna1, fila1); i++)
            {
                sumaDiagPrincipalB += matrizTranspuesta[i, i];
                sumaDiagSecundariaB += matrizTranspuesta[i, fila1 - i - 1];
            }

            label5.Text = $"Suma de la diagonal principal de la matriz normal: {sumaDiagPrincipalA}";
            label6.Text = $"Suma de la diagonal secundaria de la matriz normal: {sumaDiagSecundariaA}";
            label7.Text = $"Suma de la diagonal principal de la matriz transpuesta: {sumaDiagPrincipalB}";
            label8.Text = $"Suma de la diagonal secundaria de la matriz transpuesta: {sumaDiagSecundariaB}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Soporte m = new Soporte();
            //this.Hide();  // Oculta la ventana actual (Caratula)
            m.ShowDialog();  // Abre la nueva ventana de forma modal (espera a que se cierre para continuar)
            this.Show();
        }

        private void btnCrearMatriz_Click(object sender, EventArgs e)
        {
            groupBoxMatrizA.Controls.Clear();
            if ((int)numericUpDown1.Value != 0)
            {
                fila1 = (int)numericUpDown1.Value;
            }
            else
            {
                MessageBox.Show("El valor de las filas no es valido", "Error en matriz A");
            }

            if ((int)numericUpDown2.Value != 0)
            {
                columna1 = (int)numericUpDown2.Value;
            }
            else
            {
                MessageBox.Show("El valor de las Columnas no es valido", "Error en matriz A");
            }

            matrizA = new TextBox[fila1, columna1];
            int tamañoMatriz = groupBoxMatrizA.Width / columna1 - 10;
            for (int x = 0; x < matrizA.GetLength(0); x++)
            {
                for (int y = 0; y < matrizA.GetLength(1); y++)
                {
                    matrizA[x, y] = new TextBox();
                    matrizA[x, y].Text = "0";
                    matrizA[x, y].Top = (x * matrizA[x, y].Height) + 20;
                    matrizA[x, y].Left = y * tamañoMatriz + 6;
                    matrizA[x, y].Width = tamañoMatriz;
                    groupBoxMatrizA.Controls.Add(matrizA[x, y]);
                }
            }

        }

        private void VerificarSimetria()
        {
            if (matrizA == null)
            {
                MessageBox.Show("Debe crear la matriz antes de verificar simetría.", "Error");
                return;
            }

            if (fila1 != columna1)
            {
                label10.Text = "No existe simétria.";
                return;
            }

            int[,] valores = new int[fila1, columna1];

            for (int i = 0; i < fila1; i++)
            {
                for (int j = 0; j < columna1; j++)
                {
                    if (!int.TryParse(matrizA[i, j].Text, out valores[i, j]))
                    {
                        MessageBox.Show("Valores no válidos en la matriz A", "Error");
                        return;
                    }
                }
            }

            bool simetrica = true;
            for (int i = 0; i < fila1 && simetrica; i++)
            {
                for (int j = 0; j < columna1; j++)
                {
                    if (valores[i, j] != valores[j, i])
                    {
                        simetrica = false;
                        break;
                    }
                }
            }

            label10.Text = simetrica ? "La matriz es simétrica." : "La matriz no es simétrica.";
        }



    }
}
