﻿using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FileHash.Module.Models
{
    class MainModel
    {
        public MainModel()
        {
        }

        public ObservableCollection<string> SupportedHashAlgorithms { get; } =
            new ObservableCollection<string>() { "MD5", "SHA1", "SHA256", "SHA384", "SHA512" };

        public ReactivePropertySlim<string> HashAlgorithmName { get; } = new ReactivePropertySlim<string>("MD5");

        public ReactivePropertySlim<string> FileName { get; } = new ReactivePropertySlim<string>();

        public ReactivePropertySlim<string> FileHashString { get; } = new ReactivePropertySlim<string>();

        public void ComputeFileHash()
        {
            using var algorithm = HashAlgorithm.Create(HashAlgorithmName.Value);
            using var stream = new FileStream(FileName.Value, FileMode.Open, FileAccess.Read);
            var hash = algorithm.ComputeHash(stream);
            FileHashString.Value = hash.Select(b => b.ToString("x2"))
                                       .Aggregate(new StringBuilder(), (sb, s) => sb.Append(s), sb => sb.ToString());
        }
    }
}
