using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LSSERVICEPROVIDERLib;
using Patholab_DAL_V1;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SdgAttachments
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {

        private INautilusServiceProvider _sp;
        private SDG _sdg;
        private List<Attached> _attachedList;

        protected override void OnVisibleChanged ( EventArgs e )
        {
            base.OnVisibleChanged ( e );
            CanLoadPdf = true;
            dataGridView1_SelectionChanged ( null, null );
        }
        public RadForm1 ( List<Attached> lsit, INautilusServiceProvider serviceProvider, SDG sdg )
        {
            try
            {
                InitializeComponent();
                this._attachedList = lsit;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView1_DataBindingComplete);
                //radPdfViewer1.DocumentLoaded += radPdfViewer1_DocumentLoaded;

                
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

            //if ( !CanLoadPdf ) return;


            
            //if ( radPdfViewer1.Document != null )
            //    radPdfViewer1.UnloadDocument ( );

            //if (pdfViewer2.IsDocumentLoaded)
                //pdfViewer2.CloseDocument();

            //if (webBrowser1.Document != null)
                //webBrowser1.
                //return;

            var sr = dataGridView1.SelectedRows;
            if ( sr.Count > 0 )
            {
                

                Attached sdgAttachment = sr [ 0 ].DataBoundItem as Attached;

                if ( sdgAttachment != null )
                    if ( sdgAttachment.Path != null )
                    {
                        CanLoadPdf = false;
                        if ( File.Exists ( sdgAttachment.Path ) )
                        {
                            this.axAcroPDF1.LoadFile(sdgAttachment.Path + "#toolbar=0");
                            this.axAcroPDF1.src = sdgAttachment.Path + "#toolbar=0";
                            this.axAcroPDF1.setShowToolbar(false);
                            //this.axAcroPDF1.setView("FitH");
                            this.axAcroPDF1.setLayoutMode("SinglePage");
                            //this.axAcroPDF1.Show();
                            this.axAcroPDF1.setPageMode("none");
                        }
                        else
                        {
                            if ( IsLoaded )
                            {
                                MessageBox.Show ( sdgAttachment.Path + " doesn't exists", "Nautilus" );
                            }
                        }


                    }
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

    }


}
