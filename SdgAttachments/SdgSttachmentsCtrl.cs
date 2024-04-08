using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LSSERVICEPROVIDERLib;
using Patholab_DAL_V1;
using Telerik.WinControls.Primitives;

namespace SdgAttachments
{
    public partial class SdgSttachmentsCtrl : UserControl
    {
        private SDG _sdg;

        private readonly Color defColor;
        private List<Attached> list;

        public SdgSttachmentsCtrl()
        {
            InitializeComponent();
            defColor = Color.FromArgb(232, 241, 252);
        }

        public INautilusServiceProvider NautilusServiceProvider { get; set; }
        public DataLayer dal { get; set; }

        public SDG Sdg
        {
            get { return _sdg; }
            set
            {
                _sdg = value;
                ColorBtn();
            }
        }

        private void ColorBtn()
        {
            if (_sdg != null)
            {
                //Ashi 2/6/20 Get attachment also for revisions
                list = (from item in dal.GetAll<U_SDG_ATTACHMENT_USER>()
                        where item.SDG.NAME.Substring(0, 10) == _sdg.NAME.Substring(0, 10)
                        select new Attached
                    {
                        AttachId = item.U_SDG_ATTACHMENT_ID,
                        SdgId = item.U_SDG_ID,
                        Title = item.U_TITLE,
                        Path = item.U_PATH
                    }).ToList();

                //list = ( from item in dal.GetAll<U_SDG_ATTACHMENT_USER> ( ) //sdg.U_SDG_ATTACHMENT_USER
                //         where item.U_SDG_ID == _sdg.SDG_ID
                //         select new Attached
                //         {
                //             AttachId = item.U_SDG_ATTACHMENT_ID,
                //             SdgId = item.U_SDG_ID,
                //             Title = item.U_TITLE,
                //             Path = item.U_PATH
                //         } )
                //    .ToList ( );
                if (list.Count > 0)
                {
                    SetColor(Color.GreenYellow);
                }
                else
                {
                    SetColor(defColor);
                }
            }
            else
            {
                SetColor(defColor);
                list = null;
            }



        }

        private void SetColor(Color color)
        {
            ((FillPrimitive)radButton1.GetChildAt(0).GetChildAt(0)).BackColor =
                color;
            ((FillPrimitive)radButton1.GetChildAt(0).GetChildAt(0)).BackColor2 =
                color;
            ((FillPrimitive)
                radButton1.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((FillPrimitive)
                radButton1.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((FillPrimitive)radButton1.GetChildAt(0).GetChildAt(0)).BackColor =
                color;
        }

        public void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sdg == null)
                {
                    MessageBox.Show("Sdg isn't defiened");
                    return;
                }

                PdfViewer form = new PdfViewer(list, NautilusServiceProvider, Sdg);
                //var radForm1 = new RadForm1(list, NautilusServiceProvider, Sdg);
                form.ShowDialog();
                form.CanLoadPdf = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Reset()
        {
            Sdg = null; // = 0;
        }

        private void SdgSttachmentsCtrl_Load(object sender, EventArgs e)
        {

        }
    }
}