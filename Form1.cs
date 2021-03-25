using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CsvAuthenticator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // csvから行ごとにデータを取得し、リストにする。
            var contents = System.IO.File.ReadAllLines("list.csv");
            // Selectメソッド（LINQ）でcsvから読み込んだ内容を行ごとにカンマで区切る（Split）
            // 次にWhereメソッド（LINQ）カンマごとに区切った2次元リストに対して、
            // 要素数が2個の要素のみを抽出する（パスワードなし等をはじくため）
            var csvData = contents.Select(_ => _.Split(new char[] { ',' })).Where(_ => _.Length == 2);
            // カンマ区切りのデータになったリストの各要素（IDとパスワードのリスト）を
            // 使ってユーザクラスをインスタンス化
            // 最後にToListメソッドを使ってListクラスのインスタンスに変換
            var list = csvData.Select(row => new User(row[0], row[1])).ToList();
            // リスト要素のデータバインディング用クラス（BindingList）に↑で作成したリストを
            // セットする
            var displayData = new BindingList<User>(list);
            // リストボックスに紐づけるデータ（Userクラスインスタンス）の中から表示用に使用する
            // メンバ（Id）を指定
            listBox1.DisplayMember = nameof(User.Id);
            // リストボックスにデータ（Userクラスインスタンスのリスト）を紐づけする
            listBox1.DataSource = displayData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ボタン押下時に認証処理を実行
            Authenticate();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // テキストボックス入力中にEnterを押した際に認証処理を実行
            if (e.KeyCode == Keys.Enter)
            {
                Authenticate();
            }
        }

        private void Authenticate()
        {
            try
            {
                // リストボックスで選択した項目（object型）をUserクラスオブジェクト
                // コンストラクタの最後でUserクラスインスタンスのリストを紐づけているため
                // listBox1.SelectedItemはUserクラスインスタンスとなる
                var selectedUser = listBox1.SelectedItem as User;
                // 念のためキャストに失敗していないかチェックする
                if (selectedUser == null)
                {
                    MessageBox.Show("unknown error");
                }
                else
                {
                    // テキストボックスから入力したパスワードを取得
                    // テキストボックスはPasswordCharを設定すると入力したキーが見えなくなる
                    // （設定文字に置き換わる）
                    var inputedPassword = textBox1.Text;
                    // 入力したパスワードとリストボックスで選択したユーザのパスワードを比較
                    if (selectedUser.Pass == inputedPassword)
                    {
                        MessageBox.Show("authenticated");
                    }
                    else
                    {
                        MessageBox.Show("invalid password");
                    }
                }
            }
            catch (Exception ex)
            {
                // 念のための例外処理
                // 上記実装で発生することはないはず
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
