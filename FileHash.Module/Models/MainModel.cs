using Reactive.Bindings;
using System.Collections.Frozen;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileHash.Module.Models
{
    class MainModel
    {
        public MainModel()
        {
        }

        public ObservableCollection<string> SupportedHashAlgorithms { get; } = ["MD5", "SHA1", "SHA256", "SHA384", "SHA512"];

        public ReactivePropertySlim<string> HashAlgorithmName { get; } = new("MD5");

        public ReactivePropertySlim<string> FileName { get; } = new();

        public ReactivePropertySlim<string> ComputedFileHashString { get; } = new();

        public ReactivePropertySlim<string> CompareFileHashString { get; } = new();

        private readonly FrozenDictionary<string, Func<HashAlgorithm>> HashAlgorithmStrategy = new Dictionary<string, Func<HashAlgorithm>>()
        {
            { "MD5", () => MD5.Create() },
            { "SHA1", () => SHA1.Create() },
            { "SHA256", () => SHA256.Create() },
            { "SHA384", () => SHA384.Create() },
            { "SHA512", () => SHA512.Create() }
        }.ToFrozenDictionary();

        public void ComputeFileHash()
        {
            using var algorithm = HashAlgorithmStrategy[HashAlgorithmName.Value]();
            using var stream = new FileStream(FileName.Value, FileMode.Open, FileAccess.Read);
            var hash = algorithm.ComputeHash(stream);
            ComputedFileHashString.Value = hash.Select(b => b.ToString("x2"))
                                               .Aggregate(new StringBuilder(hash.Length * 2),
                                                          (sb, s) => sb.Append(s),
                                                          sb => sb.ToString());
        }

        public bool CompareFileHash()
        {
            return string.Compare(ComputedFileHashString.Value, CompareFileHashString.Value, true) == 0;
        }
    }
}
