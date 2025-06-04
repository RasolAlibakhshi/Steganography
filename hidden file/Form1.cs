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
using static hidden_file.Hash;

namespace hidden_file
{
    public partial class Form1 : Form
    {
        private string PathMainfile = "";
        private string PathOthrFile = "";
        private string Extension = "";
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            label2.Text = "";
            label5.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OrgFile = new OpenFileDialog();
            OrgFile.Title = "انتخاب فایل";
            OrgFile.Filter = "تمام فایل ها (*.*)|*.*";
            
            if (OrgFile.ShowDialog()==DialogResult.OK)
            {
                PathMainfile= OrgFile.FileName;
                label1.Text = PathMainfile;
                Extension = Path.GetExtension(PathMainfile);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OrgFile = new OpenFileDialog();
            OrgFile.Title = "انتخاب فایل";
            OrgFile.Filter = "تمام فایل ها (*.*)|*.*";
            if (OrgFile.ShowDialog()==DialogResult.OK)
            {
                PathOthrFile= OrgFile.FileName;
                label2.Text = PathOthrFile;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PasswordHasher hash = new PasswordHasher();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "ذخیره فایل";
            sfd.Filter = "تمام فایل‌ها (*.*)|*.*";
            sfd.FileName = "output"+"."+PathOthrFile.Split('.')[1];

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream output = new FileStream(sfd.FileName, FileMode.Create))
                {
                    
                    byte[] OtherBytes = File.ReadAllBytes(PathOthrFile);
                    output.Write(OtherBytes, 0, OtherBytes.Length);

                    // نشانه + کد رمز
                    byte[] marker = Encoding.UTF8.GetBytes("===START:"+hash.Hash(textBox1.Text) + "==="+"/" +Extension+"/");
                    output.Write(marker, 0, marker.Length);

                    
                    byte[] MainFile = File.ReadAllBytes(PathMainfile);
                    output.Write(MainFile, 0, MainFile.Length);
                }



                MessageBox.Show("مسیر ذخیره‌سازی:\n" + sfd.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog OrgFile = new OpenFileDialog();
            OrgFile.Title = "انتخاب فایل";
            OrgFile.Filter = "تمام فایل ها (*.*)|*.*";
            if (OrgFile.ShowDialog()==DialogResult.OK)
            {
                PathMainfile= OrgFile.FileName;
                label5.Text = PathMainfile;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PasswordHasher hash = new PasswordHasher();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "انتخاب مسیر ذخیره فایل خروجی";
            sfd.Filter = "تمام فایل‌ها (*.*)|*.*";
            sfd.FileName = "outputfile";  

            if (sfd.ShowDialog() == DialogResult.OK)
            {
               
                string inputPath = PathMainfile;
                string markerText = "===START:" + hash.Hash(textBox2.Text) + "===";
                byte[] marker = Encoding.UTF8.GetBytes(markerText);
                byte[] fileBytes = File.ReadAllBytes(inputPath);

                int markerIndex = FindMarkerIndex(fileBytes, marker);
                if (markerIndex < 0)
                {
                    MessageBox.Show("❌ کد نادرست است یا داده‌ای برای استخراج وجود ندارد.", "عملیات ناموفق", MessageBoxButtons.OK);
                    return;
                }

                int extStart = markerIndex + marker.Length;
                if (fileBytes[extStart] != (byte)'/')
                {
                    MessageBox.Show("❌ فرمت فایل شناسایی نشد.", "خطا", MessageBoxButtons.OK);
                    return;
                }

                int extEnd = Array.IndexOf(fileBytes, (byte)'/', extStart + 1);
                if (extEnd == -1)
                {
                    MessageBox.Show("❌ پایان پسوند یافت نشد.", "خطا", MessageBoxButtons.OK);
                    return;
                }

                string extension = Encoding.UTF8.GetString(fileBytes, extStart + 1, extEnd - extStart - 1);
                string finalPath = Path.ChangeExtension(sfd.FileName, extension);

                int dataStart = extEnd + 1;
                byte[] fileData = new byte[fileBytes.Length - dataStart];
                Array.Copy(fileBytes, dataStart, fileData, 0, fileData.Length);

                File.WriteAllBytes(finalPath, fileData);
                MessageBox.Show("✅ فایل با موفقیت استخراج شد!\n\nمسیر:\n" + finalPath, "عملیات موفق", MessageBoxButtons.OK);
            }

            // تابع کمکی برای پیدا کردن مارکر
            int FindMarkerIndex(byte[] data, byte[] marker)
            {
                for (int i = 0; i <= data.Length - marker.Length; i++)
                {
                    bool match = true;
                    for (int j = 0; j < marker.Length; j++)
                    {
                        if (data[i + j] != marker[j])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match) return i;
                }
                return -1;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }




       
    }
}

