using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_dic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fun_brows_excel("XML");

        }
        string s_intial = @"C:\Users\jobs\Desktop\curetn upload";
        public void fun_brows_excel(string type)
        {
            int size = -1;
            openFileDialog1.Title = "Open Text File";
            openFileDialog1.Filter = "EXcel files|*.xlsx";
            openFileDialog1.InitialDirectory = s_intial;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {

                    Cursor.Current = Cursors.WaitCursor;
                    if (type == "XML")
                    {
                        fun_import_student_from_excel(file);
                    }
                    else if(type=="SD")
                    {
                      

                    }
                    s_intial = file;
                    Cursor.Current = Cursors.Default;     

                }

                catch (IOException)
                {
                }
            }
        }


        public void save_text_file(string file_text)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.FileName = textBox1.Text;

            save.Filter = "Text File | *.xml";

            if (save.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(save.OpenFile());

               

                    writer.WriteLine(file_text);

                

                writer.Dispose();

                writer.Close();

            }
        }



        private void fun_import_student_from_excel(string path)
        {
            listView2.Items.Clear();
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Open(@path);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

            int rcount = worksheet.UsedRange.Rows.Count;

            string Question_ID;
            string Typee;
            string IDs;
            string Text;

            int j = 0;
            for (int i = 0; i < rcount; i++)
            {


                Question_ID = worksheet.Cells[i + 2, 2].Value+"";
                Typee = worksheet.Cells[i + 2, 3].Value + "";
                IDs = worksheet.Cells[i + 2, 2].Value + "";
                Text = worksheet.Cells[i + 2, 7].Value + "";

                if (Question_ID.Length != 0)
                {

                    if (!Question_ID.Contains("?"))
                    {
                        listView2.Items.Add(worksheet.Cells[i + 2, 2].Value.Trim());
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 3].Value.Trim());
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 2].Value.Trim());
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 7].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 5].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 6].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 4].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 8].Value + "");

                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 9].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 10].Value + "");

                        j++;
                    }
                    else
                    {
                        listView2.Items.Add(worksheet.Cells[i + 2, 1].Value);
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 3].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 2].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 7].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 5].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 6].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 4].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 8].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 9].Value + "");
                        listView2.Items[j].SubItems.Add(worksheet.Cells[i + 2, 10].Value + "");
                        j++;

                    }
                }


            }

        }



 
        bool rb_not_start=false;
        bool rg_not_closed=false;
        bool CB_not_start = false;
        bool CB_not_closed = false;
        bool new_question_start = false;

        private void generate_XML()
        {
     


            string all_xml = "";
            string quesiont_id = "";
            string xml_header_end = "  </LinearLayout>   </ScrollView> </layout>";
            for (int i = 0; i < listView2.Items.Count; i++)
            {

                string xml_layout_name = textBox1.Text;
   

                string Globquesiont_id = "";

                string typee = "";
                string resp_id = "";
                string skip_on = "";
                string tag = "";
                string rangeMin = "";
                string rangeMax = "";



                Globquesiont_id = listView2.Items[i].SubItems[0].Text;
                typee = listView2.Items[i].SubItems[1].Text;
                resp_id = listView2.Items[i].SubItems[2].Text;
                skip_on = listView2.Items[i].SubItems[4].Text;
                tag = listView2.Items[i].SubItems[5].Text;

                rangeMin = listView2.Items[i].SubItems[8].Text;
                rangeMax = listView2.Items[i].SubItems[9].Text;

                if (Globquesiont_id == "?")
                {
                    new_question_start = true;

                }
                else
                {

                    if (new_question_start == true)
                    {
                        // updation here #new 
                        quesiont_id = resp_id;
                    }

                    string xml_header_start = " " +

                          " <layout xmlns:android='http://schemas.android.com/apk/res/android' " +
                          " xmlns:tools='http://schemas.android.com/tools'" +
                          "xmlns:app='http://schemas.android.com/apk/res-auto'"+
                           ">"+
                          " <data> <import type='android.view.View' /> <variable" +
                          " name='callback'" +
                          " type='edu.aku.hassannaqvi.template.ui." + xml_layout_name + "'/></data>" +
                          " <ScrollView style='@style/i_scrollview'     android:fadeScrollbars='false'  android:fillViewport='true'  android:scrollbarSize='10dip' tools:context='.ui."+xml_layout_name+"'>" +
                          " <LinearLayout android:id='@+id/GrpName' android:layout_width='match_parent'  android:layout_height='wrap_content'" +

                             "android:orientation='vertical'" + ">";

                         


           


                    string xml_comment = "\n\n<!-- " + quesiont_id + "   " + typee + "-->\n";
                    string xml_q_cardview_start = "";
                    if (skip_on.Length == 0)
                    {
                        xml_q_cardview_start = " <android.support.v7.widget.CardView" +
                                                     " android:id='@+id/fldGrpCV" + quesiont_id + "'" +
                                                      " style='@style/cardView'> <LinearLayout style='@style/linearlayout_view_wt_bound'>";

                    }
                    else
                    {
                        xml_q_cardview_start = " <android.support.v7.widget.CardView" +
                                                      " android:id='@+id/fldGrpCV" + quesiont_id + "'" +
                                                       " style='@style/cardView'" +
                                                       " android:visibility='@{" + skip_on + ".checked ? View.VISIBLE : View.GONE}'" +

                                                       "> <LinearLayout style='@style/linearlayout_view_wt_bound'>";

                    }



                    string xml_q_cardview_end = "</LinearLayout> </android.support.v7.widget.CardView>";

                    string xml_textview = "<TextView style='@style/i_q_textview'"+
                        "android:text='"+resp_id+"' />  <TextView style='@style/i_textview'" +
                                        " android:text='@string/" + resp_id + "'/>";

                    string xml_radiogroup_start = "<RadioGroup android:id='@+id/" + quesiont_id + "' style='@style/radiogroup'>";
                    string xml_radiogroup_end = "</RadioGroup>";

                    string xml_radiobutton = "<RadioButton android:id='@+id/" + resp_id + "'" +
                                            " android:text='@string/" + resp_id + "'" +
                                            " style='@style/radiobutton' />";

                    string xml_edtextview_number = "<com.edittextpicker.aliazaz.EditTextPicker " +
                                   " android:id='@+id/" + resp_id + "'" +
                                   " style='@style/EditTextAlphaNumeric'     android:inputType='number' " +
                                   "  android:hint='@string/" + resp_id + "'" +
                                   (tag.Length != 0 ? " android:tag='" + skip_on + "'" : "") +
                                   (skip_on.Length != 0 ? " android:text=*@{" + skip_on + ".checked ? " + resp_id + ".getText.toString : ''}*" : "") +
                                   (skip_on.Length != 0 ? " android:visibility='@{" + skip_on + ".checked ? View.VISIBLE : View.GONE}'" : "")+
                                   " app:maxValue='"+rangeMax+"'" +
                                   " app:minValue='" + rangeMin + "'" +
                                   " app:type='range' />";


                    string xml_edtextview_varchar = "<EditText " +
                                " android:id='@+id/" + resp_id + "'" +
                                " style='@style/EditTextAlphaNumeric'" +
                                "  android:hint='@string/" + resp_id + "'" +
                                (tag.Length != 0 ? " android:tag='" + skip_on + "'" : "") +
                                (skip_on.Length != 0 ? " android:text=*@{" + skip_on + ".checked ? " + resp_id + ".getText.toString : ''}*" : "") +
                                (skip_on.Length != 0 ? " android:visibility='@{" + skip_on + ".checked ? View.VISIBLE : View.GONE}'" : "") +
                                " android:maxLength='30' />";


                    string xml_checkbox = "<CheckBox " +
                                        " android:id='@+id/" + resp_id + "'" +
                                    " style='@style/checkbox'" +
                                    " android:text='@string/" + resp_id + "'/>";


                    if (i == 0)
                    {
                        all_xml = xml_header_start;

                    }


                    if (new_question_start == true || i==0)
                    {
                        new_question_start = false;
                        // this is start of your new quesiont

                        if (rg_not_closed == true)
                        {
                            all_xml = all_xml + " " + xml_radiogroup_end;
                            rg_not_closed = false;
                        }

                        if (CB_not_closed == true)
                        {
                            all_xml = all_xml + " " + " </LinearLayout>";
                            CB_not_closed = false;
                        }


                        if (i != 0)
                        {
                            all_xml = all_xml + " " + xml_q_cardview_end;
                        }


                        all_xml = all_xml + " " + xml_comment + " " + xml_q_cardview_start;
                        rb_not_start = true;
                        CB_not_start = true;


                    }



                    if (typee == "T")
                    {
                        // this is start of your new quesiont

                        /*                    if (rg_not_closed == true)
                                            {
                                                all_xml = all_xml + " " + xml_radiogroup_end;
                                                rg_not_closed = false;
                                            }

                                            if (CB_not_closed == true)
                                            {
                                                all_xml = all_xml + " " +   " </LinearLayout>";
                                                CB_not_closed = false;
                                            }


                                            if (i != 0)
                                            {
                                                all_xml = all_xml + " " + xml_q_cardview_end;
                                            }

                                            */

                        //  all_xml = all_xml + " " + xml_comment + " " + xml_q_cardview_start +
                        //        " " + xml_textview + " ";

                        all_xml = all_xml + " " + xml_textview;
                        //   rb_not_start=true;
                        //  CB_not_start = true;


                    }
                    else if (typee == "R")
                    {

                        rg_not_closed = true;

                        if (rb_not_start == true)
                        {
                            all_xml = all_xml + " " + xml_radiogroup_start;
                            rb_not_start = false;
                        }

                        all_xml = all_xml + " " + xml_radiobutton + " ";


                    }
                    else if (typee == "EN")
                    {
                        all_xml = all_xml + " " + xml_edtextview_number;
                    }

                    else if (typee == "E")
                    {
                        all_xml = all_xml + " " + xml_edtextview_varchar;
                    }
                    //  else if (typee == "CheckBox")
                    //   {

                  //      all_xml = all_xml + " " + xml_checkbox;
                    // }

                    else if (typee == "C")
                    {

                        CB_not_closed = true;

                        if (CB_not_start == true)
                        {
                            all_xml = all_xml + " " + " <LinearLayout android:id='@+id/fldGrpLL" + quesiont_id + "'"+ 
                            
                          "android:tag='0' android:layout_width='match_parent'"+
                        "android:layout_height='wrap_content'   android:orientation='vertical'>";
                     
                            CB_not_start = false;
                        }

                        all_xml = all_xml + " " + xml_checkbox;


                  
                    }

                    int a = i;
                    if (i == listView2.Items.Count - 2)
                    {


                        if (rg_not_closed == true)
                        {
                            all_xml = all_xml + " " + xml_radiogroup_end;
                            rg_not_closed = false;
                        }


                        if (CB_not_closed == true)
                        {
                            all_xml = all_xml + " " + "</LinearLayout>";
                            CB_not_closed = false;
                        }

                        all_xml = all_xml + " " + xml_q_cardview_end;


                     //   all_xml = all_xml + " " + "</LinearLayout>";





                    }


                }
            }



    
          
            all_xml = all_xml.Replace("'","\"");
       all_xml= all_xml.Replace("*","'");



       all_xml = all_xml + "<LinearLayout android:layout_width=\"match_parent\"" +
" android:layout_height=\"wrap_content\""
+ " android:layout_gravity=\"end\""
+ " android:layout_marginTop=\"20dp\""
+ " android:orientation=\"horizontal\">" +


"<Button "
   + "android:id=\"@+id/btn_End\""
   + "style=\"@style/button\""
   + "android:layout_marginRight=\"10dp\""
   + "android:onClick=\"@{() -> callback.BtnEnd()}\""
   + "android:text=\"Cancel\" />"

+ "<Button"
  + " android:id=\"@+id/btn_Continue\""
  + " style=\"@style/button\""
  + " android:onClick=\"@{() -> callback.BtnContinue()}\""
  + " android:text=\"Save\" />" +


"<!--'onClick' for btn_End will NOT change and always call 'endInterview'--> </LinearLayout>" + xml_header_end;


       Cursor.Current = Cursors.Default;     

       save_text_file(all_xml);



        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter Activity Name");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;     
            generate_XML();


        }



        private void generate_SD()
        {


            List<String> lst_string = new List<string>();

            string SD_formate_R = "";
            string SD_formate_E = "";
            string SD_formate_C = "";
            string QID = "";
            bool rb_isstart = false;

            for (int i = 0; i < listView2.Items.Count; i++)
            {

         
                string Variablename = "";
                string typee = "";
                string value = "";






                Variablename = listView2.Items[i].SubItems[0].Text;
                typee = listView2.Items[i].SubItems[1].Text;
                value = listView2.Items[i].SubItems[6].Text;


                if (Variablename == "?")
                {

                    if (rb_isstart == true)
                    {
                        SD_formate_R = SD_formate_R + "\"0\");";
                        lst_string.Add(SD_formate_R);
                        rb_isstart =false;
                    }

                    new_question_start = true;
                    
                

                }
                else
                {

                    if (typee == "T")
                    {
                        QID = Variablename;    

                    }
                    else if(typee=="R")
                    {
                        rb_isstart = true;

                        if (new_question_start == true || i==1)
                        {
                            SD_formate_R = "f1.put(\"" + QID + "\",bi." + Variablename + ".isChecked() ?\"" + value + "\" :";
                            new_question_start =false;

                        }
                        else
                        {
                            SD_formate_R = SD_formate_R + "bi." + Variablename + ".isChecked() ?\"" + value + "\" :";
                        }

                    }
                    else if (typee == "E")
                    {

                        SD_formate_E = "f1.put(\"" + Variablename + "\", bi." + Variablename + ".getText().toString());";

                        lst_string.Add(SD_formate_E);

                    }
                    else if (typee == "C")
                    {
                        SD_formate_C = "f1.put(\"" + Variablename + "\",bi." + Variablename + ".isChecked() ?\"" + value + "\" :\"0\");";


                        lst_string.Add(SD_formate_C);
                    }


             
                }
            }






            string sd = " JSONObject f1 = new JSONObject(); ";
            foreach (string a in lst_string)
            {
                sd =sd+ System.Environment.NewLine + a;


            }

            sd = sd + " fc.setCRFA(String.valueOf(f1));"+ "\n"+"fc.setFormType(\"f1\");";
            save_text_file(sd);

           



        }


        bool it_is_new_question = false;
 

        private void button3_Click(object sender, EventArgs e)
        {
            generate_SD();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            fun_brows_excel("SD");
        }

        private void listView_SD_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            generate_string(true);
        }


        public void generate_string(bool b_eng)
        {

            string all_string = "<!-- String Section ";
            string first_towcharcter = "";

            for (int i = 0; i < listView2.Items.Count; i++)
            {

        
                string quesiont_id = "";
                string text = "";
                string qurent_string = "";

            
             



                quesiont_id = listView2.Items[i].SubItems[0].Text;



                    if (i == 0)
                {
                    first_towcharcter = quesiont_id.Substring(0, 2);
                    all_string = all_string + first_towcharcter + " ....: Started ...... -->";
                }

                if (b_eng == true)
                {
                    text = listView2.Items[i].SubItems[3].Text;
                }
                else
                {
                    text = listView2.Items[i].SubItems[7].Text;
                }


                if (quesiont_id == "?")
                {
                    new_question_start = true;

                }
                else
                {

                    qurent_string = "<string name=\"" + quesiont_id + "\"> " + text + "  </string>";
                }

                all_string = all_string +"\n"+ qurent_string;

                  

       

            }


            all_string =all_string+ "\n" + "<!-- String Section " + first_towcharcter + " .... ends......-->";
            save_text_file(all_string);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            generate_string(false);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
