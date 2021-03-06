﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lojadalala
{
  public partial class FormGridServicos : Form
  {
    //Atributos
    private DataSet ds;
    private BancoDados bd;
    private FormCadServicos frm;

    public FormGridServicos(FormCadServicos frm1)
    {
      InitializeComponent();
      bd = new BancoDados();
      frm = frm1;
      tbPesquisa.Focus();
    }

    private void FormGridServicos_Load(object sender, EventArgs e)
    {
      string SQL = "SELECT * FROM servico ORDER BY id";
      string tabela = "servico";
      ds = new DataSet();
      ds = bd.ConsultarParaGrid(SQL, tabela);
      dataGridView1.DataSource = ds.Tables["servico"];
    }

    private void butSair_Click(object sender, EventArgs e)
    {
      if (frm != null)
      {
        frm.tbCodigo.Focus();
      }
      this.Close();
    }

    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
      {
      }
      else
      {
        if (frm != null)
        {
          frm.tbCodigo.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
          frm.tbDescricao.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
          frm.tbValor.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
          frm.tbDetalhes.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

          frm.butAlterar.Enabled = true;
          frm.butExcluir.Enabled = true;
          frm.butSalvar.Enabled = false;
          frm.butNovo.Focus();
        }
        this.Dispose();
      }
    }

    private void btnPesquisar_Click(object sender, EventArgs e)
    {
      if (radioButton1.Checked)
      {
        bool encontrado = false;
        for (int i = 0; i < dataGridView1.RowCount; i++)
        {
          if (dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(tbPesquisa.Text))
          {
            dataGridView1.Focus();
            dataGridView1.FirstDisplayedScrollingRowIndex = i;
            dataGridView1.Refresh();
            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
            dataGridView1.Rows[i].Selected = true;            
            encontrado = true;
            this.CancelButton = null;
            break;
          }
        }
        if (!encontrado)
        {
          MessageBox.Show("Nenhum registro encontrado com essas informações!", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          tbPesquisa.SelectAll();
        }
      }      
    }

    private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        DataGridViewRow linhaAtual = dataGridView1.CurrentRow;
        int i = linhaAtual.Index;
        if (frm != null)
        {
          frm.tbCodigo.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
          frm.tbDescricao.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
          frm.tbValor.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
          frm.tbDetalhes.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();

          frm.butAlterar.Enabled = true;
          frm.butExcluir.Enabled = true;
          frm.butSalvar.Enabled = false;
          frm.butNovo.Focus();
        }
        this.Dispose();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        tbPesquisa.Clear();
        tbPesquisa.Focus();
        this.CancelButton = butSair;
      }
    }

    private void dataGridView1_Leave(object sender, EventArgs e)
    {
      this.CancelButton = butSair;
    }
  }
}
