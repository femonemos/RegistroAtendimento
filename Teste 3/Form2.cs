using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace Teste_3
{
    public partial class Form2 : Form
    {

        string Nome;
        string Empresa;
        string Email;
        string DDD;
        string Telefone;
        string Historico;
        string TipoAtendimento;
        string CanalAtendimento;
        string SituacaoAtendimento;
        string Protocolo = "";
        int teste = 0;
        string DataAtendimento;
        string HoraAtendimento;
        string TipoPessoa;
        string iduserloged;
        string tiposolicitacao;
        string idregistroatendimento;
        const int maxtamanhodata = 10;
        const int maxtamanhohora = 5;



        public Form2(string idusuario)
        {
            InitializeComponent();
            //WindowState = FormWindowState.Maximized;
            iduserloged = idusuario;

            SqlConnection conn = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
            conn.Open();
            //MONTANDO A COMBO DE SITUACAO
            SqlCommand CMB1 = conn.CreateCommand();
            CMB1.CommandType = CommandType.Text;
            CMB1.CommandText = "select situacaoatendimento from situacaoatendimento where idstatus = 1";
            CMB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(CMB1);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["situacaoatendimento"].ToString());
            }

            //Montando a Combo de Tipo de Solicitação
            SqlCommand CMB2 = conn.CreateCommand();
            CMB2.CommandType = CommandType.Text;
            CMB2.CommandText = "select tiposolicitacao from tiposolicitacao where idstatus =1";
            CMB2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(CMB2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox2.Items.Add(dr2["tiposolicitacao"].ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e) //função buscar
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Preencha o Protocolo", "Erro");
            }
            else if (int.TryParse(textBox1.Text, out teste))
            {
                //metodo de consulta por protocolo
                SqlConnection conn = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT NOME, EMPRESA, EMAIL, DDD, TELEFONE, HISTORICO, IDTIPOATENDIMENTO, IDCANALATENDIMENTO, IDSITUACAOATENDIMENTO," +
                    "PROTOCOLO, CONVERT(DATE,DATAATENDIMENTO,102), HORAATENDIMENTO,IDTIPOPESSOA,IDTIPOSOLICITACAO,idregistroatendimento FROM REGISTROATENDIMENTO WHERE PROTOCOLO = '" + textBox1.Text + "' AND IDSTATUS = 1", conn);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //GRAVANDO O RESULTADO DA CONSULTA EM VARIAVEIS 
                    Nome = dr[0].ToString();
                    Empresa = dr[1].ToString();
                    Email = dr[2].ToString();
                    DDD = dr[3].ToString();
                    Telefone = dr[4].ToString();
                    Historico = dr[5].ToString();
                    TipoAtendimento = dr[6].ToString();
                    CanalAtendimento = dr[7].ToString();
                    SituacaoAtendimento = dr[8].ToString();
                    Protocolo = dr[9].ToString();
                    DataAtendimento = dr[10].ToString();
                    HoraAtendimento = dr[11].ToString();
                    TipoPessoa = dr[12].ToString();
                    tiposolicitacao = dr[13].ToString();
                    idregistroatendimento = dr[14].ToString();

                    if (DataAtendimento.Length > maxtamanhodata)
                    {
                        DataAtendimento = DataAtendimento.Substring(0, maxtamanhodata);
                    }

                    if (HoraAtendimento.Length > maxtamanhohora)
                    {
                        HoraAtendimento = HoraAtendimento.Substring(0, maxtamanhohora);
                    }

                    //TRAZENDO O TIPO DE ATENDIMENTO DO DB
                    if (TipoAtendimento == "1")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }

                    //TRAZENDO O CANAL DE ATENDIMENTO DO DB
                    if (CanalAtendimento == "1")
                    {
                        radioButton3.Checked = true;
                    }
                    else if (CanalAtendimento == "2")
                    {
                        radioButton4.Checked = true;
                    }
                    else
                    {
                        radioButton5.Checked = true;
                    }

                    //TRAZENDO A SITUACAO DO ATENDIMENTO DO DB

                    if (SituacaoAtendimento == "1")
                    {
                        SituacaoAtendimento = "Pendente";
                    }
                    else
                    {
                        SituacaoAtendimento = "Finalizado";
                    }

                    //TRAZENDO O TIPO DE PESSOA DO DB

                    if (TipoPessoa == "1")
                    {
                        radioButton7.Checked = true;
                    }
                    else
                    {
                        radioButton6.Checked = true;
                    }
                    //TRAZENDO A COMBO DO TIPO DE SOLICITACAO DO DB
                    if (tiposolicitacao == "1")
                    {
                        tiposolicitacao = "Cadastro de Cliente";
                    }
                    else if (tiposolicitacao == "2")
                    {
                        tiposolicitacao = "Solicitação de Coleta";
                    }
                    else if (tiposolicitacao == "3")
                    {
                        tiposolicitacao = "Cotação";
                    }



                    //TRANSFERINDO AS VARIAVEIS PARA OS CAMPOS DO FORMULARIO
                    textBox1.Text = Protocolo;
                    textBox2.Text = Nome;
                    textBox3.Text = Empresa;
                    textBox4.Text = Email;
                    textBox5.Text = DDD;
                    textBox6.Text = Telefone;
                    textBox7.Text = Historico;
                    comboBox1.Text = SituacaoAtendimento;
                    textBox8.Text = DataAtendimento;
                    textBox9.Text = HoraAtendimento;
                    comboBox2.Text = tiposolicitacao;

                }

                else
                {
                    MessageBox.Show("Protocolo não encontrado", "Erro");
                }
            }
            else
            {
                MessageBox.Show("Informe apenas números no protocolo", "Erro");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            TipoAtendimento = "1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            TipoAtendimento = "2";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CanalAtendimento = "1";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CanalAtendimento = "2";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            CanalAtendimento = "3";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //função criar e alterar
        {

            if (Protocolo == "") //função salvar

            {
                //identificar o tipo de atendimento
                if (radioButton1.Checked == true)
                {
                    TipoAtendimento = "1";
                }
                else if (radioButton2.Checked == true)
                {
                    TipoAtendimento = "2";
                }
                else
                {
                    TipoAtendimento = "";
                }
                //identificar o tipo de pessoa
                if (radioButton6.Checked == true)
                {
                    TipoPessoa = "2";
                }
                else if (radioButton7.Checked == true)
                {
                    TipoPessoa = "1";
                }
                else
                {
                    TipoPessoa = "";
                }
                //identificar o canal de atendimento
                if (radioButton3.Checked == true)
                {
                    CanalAtendimento = "1";
                }
                else if (radioButton4.Checked == true)
                {
                    CanalAtendimento = "2";
                }
                else if (radioButton5.Checked == true)
                {
                    CanalAtendimento = "3";
                }
                else
                {
                    CanalAtendimento = "";
                }

                //verifico o tipo de solicitacao
                if (comboBox2.Text == "Cadastro de Cliente")
                {
                    tiposolicitacao = "1";
                }
                else if (comboBox2.Text == "Solicitação de Coleta")
                {
                    tiposolicitacao = "2";
                }
                else if (comboBox2.Text == "Cotação")
                {
                    tiposolicitacao = "3";
                }
                else
                {
                    tiposolicitacao = "";
                }


                //verifico a situacao do atendimento
                if (comboBox1.Text == "Pendente")
                {
                    SituacaoAtendimento = "1";
                }
                else if (comboBox1.Text == "Finalizado")
                {
                    SituacaoAtendimento = "2";
                }
                else
                {
                    SituacaoAtendimento = "";
                }

                //faço consistencia de preenchimento de campos
                if (tiposolicitacao == "")
                {
                    MessageBox.Show("Preencha o tipo de solicitação", "Erro");
                }

                else if (TipoAtendimento == "")
                {
                    MessageBox.Show("Preencha o tipo de atendimento", "Erro");
                }
                else if (TipoPessoa == "")
                {
                    MessageBox.Show("Preencha o tipo de pessoa", "Erro");
                }
                else if (CanalAtendimento == "")
                {
                    MessageBox.Show("Preencha o canal de atendimento", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Preencha o Nome", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Preencha a Empresa", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Preencha o Email", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("Preencha o DDD", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox6.Text))
                {
                    MessageBox.Show("Preencha o telefone", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("Preencha o Histórico", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox8.Text))
                {
                    MessageBox.Show("Preencha a data do atendimento", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox9.Text))
                {
                    MessageBox.Show("Preencha a hora do atendimento", "Erro");
                }
                else if (SituacaoAtendimento == "")
                {
                    MessageBox.Show("Preencha a Situação do Atendimento", "Erro");
                }
                else
                {

                    //Recupera o próximo protocolo disponível
                    SqlConnection conn = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
                    conn.Open();
                    SqlCommand cmdPROTOCOLO = new SqlCommand("SELECT NEXT VALUE FOR DBO.PROTOCOLO_SEQ", conn);
                    SqlDataReader drPROTOCOLO;
                    drPROTOCOLO = cmdPROTOCOLO.ExecuteReader();



                    if (drPROTOCOLO.Read())
                        Protocolo = drPROTOCOLO[0].ToString();
                    {
                        try
                        {
                            //Recupera o próximo idregistroatendimento_seq disponível
                            SqlConnection conn3 = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
                            conn3.Open();
                            SqlCommand cmdREGISTROATENDIMENTO = new SqlCommand("SELECT NEXT VALUE FOR DBO.REGISTROATENDIMENTO_SEQ", conn3);
                            SqlDataReader drREGISTROATENDIMENTO;
                            drREGISTROATENDIMENTO = cmdREGISTROATENDIMENTO.ExecuteReader();

                            if (drREGISTROATENDIMENTO.Read())
                                idregistroatendimento = drREGISTROATENDIMENTO[0].ToString();

                            try
                            {

                                SqlConnection connInsert = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
                                connInsert.Open();
                                string incluiSQL = @"INSERT INTO REGISTROATENDIMENTO (IDREGISTROATENDIMENTO, NOME, DDD, TELEFONE, EMAIL, PROTOCOLO, EMPRESA, HISTORICO, IDSTATUS, IDUSUARIO, IDTIPOPESSOA, IDTIPOATENDIMENTO, IDCANALATENDIMENTO, IDSITUACAOATENDIMENTO, IDTIPOSERVICO, DATAATENDIMENTO, HORAATENDIMENTO, DATACRIACAO, IDTIPOSOLICITACAO) VALUES ('" + idregistroatendimento + "', '" + textBox2.Text + "', '" + textBox5.Text + "','" + textBox6.Text + "','" + textBox4.Text + "', '" + Protocolo + "', '" + textBox3.Text + "', '" + textBox7.Text + "', 1, '" + iduserloged + "', '" + TipoPessoa + "', '" + TipoAtendimento + "', '" + CanalAtendimento + "', '" + SituacaoAtendimento + "', 2, '" + textBox8.Text + "', '" + textBox9.Text + "',getdate(),'" + tiposolicitacao + "')";
                                SqlCommand cmd = new SqlCommand(incluiSQL, connInsert);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Protocolo " + Protocolo + " criado!");
                                //Protocolo = ""; comando antigo para limpar depois do create
                                textBox1.Text = Protocolo;
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                string str;
                                str = "Source:" + ex.Source;
                                str += "\n" + "Message:" + ex.Message;
                                MessageBox.Show(str, "Database Exception");
                                //limpo as variaveis em caso de erro
                                idregistroatendimento = "";
                                Protocolo = "";
                                //MessageBox.Show("Registro: " + idregistroatendimento + "/ Protocolo: " + Protocolo);
                            }
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            string str;
                            str = "Source:" + ex.Source;
                            str += "\n" + "Message:" + ex.Message;
                            MessageBox.Show(str, "Database Exception");
                        }





                    }
                }

            }

            else //rotina de alteracao ao clicar no salvar
            {
                //MessageBox.Show(Protocolo);

                //identificar o tipo de atendimento
                if (radioButton1.Checked == true)
                {
                    TipoAtendimento = "1";
                }
                else if (radioButton2.Checked == true)
                {
                    TipoAtendimento = "2";
                }
                else
                {
                    TipoAtendimento = "";
                }
                //identificar o tipo de pessoa
                if (radioButton6.Checked == true)
                {
                    TipoPessoa = "2";
                }
                else if (radioButton7.Checked == true)
                {
                    TipoPessoa = "1";
                }
                else
                {
                    TipoPessoa = "";
                }
                //identificar o canal de atendimento
                if (radioButton3.Checked == true)
                {
                    CanalAtendimento = "1";
                }
                else if (radioButton4.Checked == true)
                {
                    CanalAtendimento = "2";
                }
                else if (radioButton5.Checked == true)
                {
                    CanalAtendimento = "3";
                }
                else
                {
                    CanalAtendimento = "";
                }
                //verifico o tipo de solicitacao
                if (comboBox2.Text == "Cadastro de Cliente")
                {
                    tiposolicitacao = "1";
                }
                else if (comboBox2.Text == "Solicitação de Coleta")
                {
                    tiposolicitacao = "2";
                }
                else if (comboBox2.Text == "Cotação")
                {
                    tiposolicitacao = "3";
                }
                else
                {
                    tiposolicitacao = "";
                }

                //verifico a situacao do atendimento
                if (comboBox1.Text == "Pendente")
                {
                    SituacaoAtendimento = "1";
                }
                else if (comboBox1.Text == "Finalizado")
                {
                    SituacaoAtendimento = "2";
                }
                else
                {
                    SituacaoAtendimento = "";
                }

                //faço consistencia de preenchimento de campos

                if (tiposolicitacao == "")
                {
                    MessageBox.Show("Preencha o tipo de solicitação", "Erro");
                }

                else if (TipoAtendimento == "")
                {
                    MessageBox.Show("Preencha o tipo de atendimento", "Erro");
                }
                else if (TipoPessoa == "")
                {
                    MessageBox.Show("Preencha o tipo de pessoa", "Erro");
                }
                else if (CanalAtendimento == "")
                {
                    MessageBox.Show("Preencha o canal de atendimento", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Preencha o Nome", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Preencha a Empresa", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Preencha o Email", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("Preencha o DDD", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox6.Text))
                {
                    MessageBox.Show("Preencha o telefone", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox7.Text))
                {
                    MessageBox.Show("Preencha o Histórico", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox8.Text))
                {
                    MessageBox.Show("Preencha a data do atendimento", "Erro");
                }
                else if (string.IsNullOrEmpty(textBox9.Text))
                {
                    MessageBox.Show("Preencha a hora do atendimento", "Erro");
                }
                else if (SituacaoAtendimento == "")
                {
                    MessageBox.Show("Preencha a Situação do Atendimento", "Erro");
                }
                else
                {
                    //muda os protocolos antigos para idstatus =3 - histórico
                    try
                    {
                        SqlConnection connUpdate = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
                        connUpdate.Open();
                        string alteraSQL = @"UPDATE REGISTROATENDIMENTO SET IDSTATUS = 3 WHERE IDREGISTROATENDIMENTO ='" + idregistroatendimento + "'";
                        SqlCommand cmdUpdate = new SqlCommand(alteraSQL, connUpdate);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string str;
                        str = "Source:" + ex.Source;
                        str += "\n" + "Message:" + ex.Message;
                        MessageBox.Show(str, "Database Exception");

                        //idregistroatendimento = "";
                        //Protocolo = "";
                        //MessageBox.Show("Registro: "+idregistroatendimento+"/ Protocolo: "+Protocolo);
                    }
                    //insere o novo registro para o protocolo
                    try
                    {
                        SqlConnection connInsert2 = new SqlConnection("Data Source=WD007ASC;Initial Catalog=CRC;Persist Security Info=True;User ID=crc;Password=crc");
                        connInsert2.Open();
                        string incluiSQL = @"INSERT INTO REGISTROATENDIMENTO (IDREGISTROATENDIMENTO, NOME, DDD, TELEFONE, EMAIL, PROTOCOLO, EMPRESA, HISTORICO, IDSTATUS, IDUSUARIO, IDTIPOPESSOA, IDTIPOATENDIMENTO, IDCANALATENDIMENTO, IDSITUACAOATENDIMENTO, IDTIPOSERVICO, DATAATENDIMENTO, HORAATENDIMENTO, DATACRIACAO,IDTIPOSOLICITACAO) VALUES (NEXT VALUE FOR DBO.REGISTROATENDIMENTO_SEQ, '" + textBox2.Text + "', '" + textBox5.Text + "','" + textBox6.Text + "','" + textBox4.Text + "', '" + Protocolo + "', '" + textBox3.Text + "', '" + textBox7.Text + "', 1, '" + iduserloged + "','" + TipoPessoa + "', '" + TipoAtendimento + "', '" + CanalAtendimento + "', '" + SituacaoAtendimento + "', 2, (CONVERT(DATETIME,'" + textBox8.Text + "',103)), '" + textBox9.Text + "',getdate(),'" + tiposolicitacao + "')";
                        SqlCommand cmd = new SqlCommand(incluiSQL, connInsert2);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Protocolo " + Protocolo + " Atualizado!");
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string str;
                        str = "Source:" + ex.Source;
                        str += "\n" + "Message:" + ex.Message;
                        MessageBox.Show(str, "Database Exception");
                    }
                }
            }
        }


        private void button3_Click(object sender, EventArgs e) //função limpar
        {

            Nome = "";
            Empresa = "";
            Email = "";
            DDD = "";
            Telefone = "";
            Historico = "";
            TipoAtendimento = "";
            CanalAtendimento = "";
            SituacaoAtendimento = "";
            Protocolo = "";
            tiposolicitacao = "";
            idregistroatendimento = "";

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";

            radioButton1.TabStop = true;
            radioButton2.TabStop = true;
            radioButton3.TabStop = true;
            radioButton4.TabStop = true;
            radioButton5.TabStop = true;
            radioButton6.TabStop = true;
            radioButton7.TabStop = true;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;



            return;

            //MessageBox.Show("Limpar protocolo: "+Protocolo);

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true)
            {
                switch (textBox8.TextLength)
                {
                    //case 0:
                    //    textBox8.Text = "";

                    case 2:
                        textBox8.Text = textBox8.Text + "/";
                        textBox8.SelectionStart = 4;

                        break;

                    case 5:
                        textBox8.Text = textBox8.Text + "/";
                        textBox8.SelectionStart = 9;

                        break;
                }
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true)
            {
                switch (textBox9.TextLength)
                {
                    case 2:
                        textBox9.Text = textBox9.Text + ":";
                        textBox9.SelectionStart = 4;

                        break;
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true)
            {
                switch (textBox6.TextLength)
                {
                    case 5:
                        textBox6.Text = textBox6.Text + "-";
                        textBox6.SelectionStart = 6;

                        break;
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (char.IsNumber(e.KeyChar) == true)
            //{
            //    switch (textBox5.TextLength)
            //    {
            //        //case 0:
            //        //    textBox8.Text = "";

            //        case 1:
            //            textBox8.Text = textBox8.Text + "(";
            //            textBox8.SelectionStart = 2;

            //            break;

            //        case 3:
            //            textBox8.Text = textBox8.Text + ")";
            //            //textBox8.SelectionStart = 9;

            //            break;
            //    }
            //}
        }
    }
}




