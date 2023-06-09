﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Tp_1
{
    public partial class Form1 : Form

    {
        private List<Articulo> listaOriginal;

        public Form1()
        {
            InitializeComponent();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)  //apenas nace, el Form llama a la función cargar() para traer la info de la base
        {
            cargar();  
        }

        private void cargar()  //la función crea una instancia de ArticuloNegocio, del cual usa el método listar(),
                               //el return lo pasa a la dgvLista para mostrarlo en el Form
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaOriginal = negocio.listar();
            dgvLista.DataSource = listaOriginal;
            dgvLista.Columns[0].Visible = false; //permite no mostrar una columna en el Form
            dgvLista.Columns[6].Visible = false;    //aún no logro resolver lo del ImgUrl
        }

        //tanto el Load del Form como este tendrían que traer la imagen, pero no lo hace... mmm
        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo = (Articulo)dgvLista.CurrentRow.DataBoundItem;
                pbArticulo.Load(articulo.ImagenUrl.ImagenUrl);
            }
            catch (Exception)
            {

            }

        }
        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Agregar alta = new Agregar();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo artic;
            artic = (Articulo)dgvLista.CurrentRow.DataBoundItem;


            //Agregar modificar = new Agregar(artic);     // el nuevo constructor pasa los datos del actual elemento 
            Modificar modificar = new Modificar(artic);
            modificar.Text = "Modificar";
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Confirma eliminar el artículo?", "Eliminar artículo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.eliminar(((Articulo)dgvLista.CurrentRow.DataBoundItem).ID);
                MessageBox.Show("Operación realizada exitosamente", "Éxito");
                cargar();
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(((Articulo)dgvLista.CurrentRow.DataBoundItem).Descripcion, "Detalle del artículo");
        }

        private void pbArticulo_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)dgvLista.DataSource;
            List<Articulo> listaFiltrada = listaOriginal.FindAll(x => x.Codigo.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvLista.DataSource = listaFiltrada;

        }
    }
}
