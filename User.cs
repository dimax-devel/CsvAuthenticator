using System.ComponentModel;

namespace CsvAuthenticator
{
    // https://mohmongar.net/?p=1876 と同様クラス（ユーザクラス）を実装
    class User : INotifyPropertyChanged
    {
        // ↓↓ 初期化用コンストラクタ　↓↓
        public User(string id, string pass)
        {
            _id = id;
            _pass = pass;
        }
        // ↑↑　　　　　　　　　　　　↑↑

        // ↓↓ データバインディングのため以下の実装が必要　↓↓
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        // ↑↑　　　　　　　　　　　　　　　　　　　　　　↑↑

        // ↓↓　ユーザ情報として持たせたい変数とプロパティ（リストボックスに表示したい項目もここ）
        private string _id = string.Empty;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                Notify(nameof(Id));
            }
        }

        private string _pass = string.Empty;
        public string Pass
        {
            get { return _pass; }
            set
            {
                _pass = value;
                Notify(nameof(Pass));
            }
        }

    }
}
