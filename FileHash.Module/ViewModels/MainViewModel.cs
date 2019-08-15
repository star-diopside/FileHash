using FileHash.Module.Models;
using System.Collections.ObjectModel;

namespace FileHash.Module.ViewModels
{
    class MainViewModel
    {
        /// <summary>
        /// モデルオブジェクト
        /// </summary>
        private MainModel _model = new MainModel();

        /// <summary>
        /// 使用可能なハッシュアルゴリズム
        /// </summary>
        public ObservableCollection<string> HashAlgorithm
        {
            get
            {
                return new ObservableCollection<string>() { "MD5", "SHA1" };
            }
        }

        /// <summary>
        /// 選択されたハッシュアルゴリズムのインデックス
        /// </summary>
        public int HashAlgorithmSelectedIndex
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
    }
}
