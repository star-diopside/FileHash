using FileHash.Module.Models;
using Microsoft.Win32;
using Prism.Commands;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FileHash.Module.ViewModels
{
    class MainViewModel
    {
        private readonly MainModel _model;

        public MainViewModel(MainModel model)
        {
            _model = model;
            SelectFileCommand = new DelegateCommand(SelectFile);
            ComputeFileHashCommand = new DelegateCommand(ComputeFileHash);
        }

        /// <summary>使用可能なハッシュアルゴリズム</summary>
        public ObservableCollection<string> SupportedHashAlgorithms => _model.SupportedHashAlgorithms;

        /// <summary>選択したハッシュアルゴリズム名</summary>
        public ReactivePropertySlim<string> SelectedHashAlgorithm => _model.HashAlgorithmName;

        /// <summary>ファイル名</summary>
        public ReactivePropertySlim<string> FileName => _model.FileName;

        /// <summary>ファイルのハッシュ値</summary>
        public ReactivePropertySlim<string> FileHashString => _model.FileHashString;

        /// <summary>ファイル選択コマンド</summary>
        public ICommand SelectFileCommand { get; }

        /// <summary>ハッシュ値算出コマンド</summary>
        public ICommand ComputeFileHashCommand { get; }

        public void SelectFile()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                FileName.Value = dialog.FileName;
            }
        }

        private void ComputeFileHash()
        {
            _model.ComputeFileHash();
        }
    }
}
