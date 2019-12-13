using SimpleJSON;
using System;
using System.Windows.Forms;

namespace WebAccessor
{
    public partial class SigninForm : Form
    {
        public SigninForm()
        {

            InitializeComponent();

        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.id.Text.Length == 0)
                    throw new Exception("아이디를 입력하세요");

                if(this.pw.Text.Length == 0)
                    throw new Exception("비밀번호를 입력하세요.");

                var instance = Naver.Instance;
                //var instance = Daum.Instance;
                var result = string.Empty;
                if(instance.login(this.id.Text, this.pw.Text) == false)
                    throw new Exception("아이디 혹은 비밀번호가 올바르지 않습니다.");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(this, exc.Message);
            }
        }
    }
}