using LSSERVICEPROVIDERLib;
using Patholab_DAL_V1;
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
using System.Windows.Input;

namespace SdgAttachments
{
    public partial class PdfViewer : Form
    {
        private INautilusServiceProvider _sp;
        private SDG _sdg;
        private List<Attached> _attachedList;

        public PdfViewer ( List<Attached> lsit, INautilusServiceProvider serviceProvider, SDG sdg )
        {
            try
            {
                InitializeComponent();
                this._attachedList = lsit;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView1_DataBindingComplete);
   

                
                this._sp = serviceProvider;
                this._sdg = sdg;

                Text = string.Format("{0} - {1}", sdg.SDG_USER.U_PATHOLAB_NUMBER, sdg.NAME);

                dataGridView1.DataSource = lsit;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public bool CanLoadPdf = false;


        void dataGridView1_DataBindingComplete ( object sender, DataGridViewBindingCompleteEventArgs e )
        {
            dataGridView1.SelectionChanged += new EventHandler ( dataGridView1_SelectionChanged );
        }
        void radPdfViewer1_DocumentLoaded ( object sender, EventArgs e )
        {
            CanLoadPdf = true;
        }

        private void dataGridView1_SelectionChanged ( object sender, EventArgs e )
        {
            try
            {
                var sr = dataGridView1.SelectedRows;

                if (sr.Count > 0)
                {
                    Attached sdgAttachment = sr[0].DataBoundItem as Attached;

                    if (sdgAttachment != null)
                    {
                        if (sdgAttachment.Path != null)
                        {
                            CanLoadPdf = false;
                            if (File.Exists(sdgAttachment.Path))
                            {
                                this.axAcroPDF1.LoadFile(sdgAttachment.Path + "#toolbar=0");
                                this.axAcroPDF1.src = sdgAttachment.Path + "#toolbar=0";
                                this.axAcroPDF1.setShowToolbar(false);
                                this.axAcroPDF1.setLayoutMode("SinglePage");
                                this.axAcroPDF1.setPageMode("none");
                            }
                            else
                            {
                                MessageBox.Show(sdgAttachment.Path + " doesn't exists", "Nautilus");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error loading PDF." + Environment.NewLine + ex.Message, "Nautilus");
            }
        }

        private void ListBox_Format ( object sender, ListControlConvertEventArgs e )
        {
            if ( e.ListItem.ToString ( ) == null )
                e.Value = string.Empty;
        }


        #region Add File

        private void button1_Click ( object sender, EventArgs e )
        {
            AddFile add = new AddFile ( _sdg, _sp );
            add.FileAdded += AddFileAdded;
            add.ShowDialog ( );
        }

        private void AddFileAdded ( Attached obj )
        {
            //  dataGridView1.Rows.Clear();

            dataGridView1.DataSource = null;

            _attachedList.Add ( obj );
            dataGridView1.DataSource = _attachedList;
        }

        #endregion

        private void RadForm1_Initialized ( object sender, EventArgs e )
        {

        }

        private void radPdfViewerNavigator1_Click ( object sender, EventArgs e )
        {

        }

        private void button2_Click ( object sender, EventArgs e )
        {
            this.Close ( );

            axAcroPDF1.Dispose();
        }

        private void PdfViewer_Shown(object sender, EventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            //System.Threading.Thread.Sleep(1000);

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
            }

            Mouse.OverrideCursor = null;
        }
    }
}
