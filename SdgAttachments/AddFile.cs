using System;
using System.Windows.Forms;
using LSSERVICEPROVIDERLib;
using Patholab_Common;
using Patholab_DAL_V1;
using Patholab_XmlService;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SdgAttachments
{
    public partial class AddFile : Form
    {
        private INautilusServiceProvider _sp;
        private SDG _sdg;
        public event Action<Attached> FileAdded;
        public AddFile(SDG sdg, INautilusServiceProvider _sp1)
        {
            this._sp = _sp1;
            this._sdg = sdg;

            InitializeComponent();
            OpenFileDialog dialog = (OpenFileDialog)radBrowseEditor1.Dialog;
            dialog.Filter = "(*.pdf)|*.pdf";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(radTextBox1.Text) || string.IsNullOrEmpty(radBrowseEditor1.Value))
                {
                    MessageBox.Show("חובה למלא את כל השדות!"); return;
                }
                var attt = new Attached()
                    {

                        Title = radTextBox1.Text,
                        Path = radBrowseEditor1.Value
                    };

                CreateStaticEntity newSdgAtt = new CreateStaticEntity(_sp);
                newSdgAtt.Login("U_SDG_ATTACHMENT", "Sdg Attachment", string.Format("{0}-{1}-{2}", radTextBox1.Text, _sdg.SDG_ID, DateTime.Now));
                newSdgAtt.AddProperties("U_TITLE", radTextBox1.Text);
                newSdgAtt.AddProperties("U_SDG_ID", _sdg.SDG_ID.ToString());
                newSdgAtt.AddProperties("U_PATH", radBrowseEditor1.Value);


                var s = newSdgAtt.ProcssXml();
                if (!s)
                {
                    MessageBox.Show(string.Format("Error on AddNovellusLink  {0}", newSdgAtt.ErrorResponse), "NAUTILUS");
                }
                else
                {
                 //   MessageBox.Show("המסמך נוסף לדרישה!");
                    string newId = newSdgAtt.GetValueByTagName("U_SDG_ATTACHMENT_ID");

                    attt.AttachId = long.Parse(newId);

                    if (FileAdded != null) FileAdded(attt);
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                Logger.WriteLogFile(ex);

                MessageBox.Show(ex.Message);
            }
        }
    }


}
