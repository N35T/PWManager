using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using PWManager.Avalonia.Environment.Interfaces;
using PWManager.UI;
using PWManager.UI.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PWManager.Avalonia.Services {
    internal class FileChooserService : IChooseFile {
        private readonly IVisualEnvironment _visEnv;
        private FilePickerFileType Scuml { get; } = new("Scuml") {
            Patterns = new[] { "*.scuml" },
            AppleUniformTypeIdentifiers = new[] { "public.scuml" },
            MimeTypes = new[] { "scuml/*" }
        };

        public FileChooserService()
        {
            _visEnv = IoC.Resolve<IVisualEnvironment>();
        }
        public async Task<string> OpenFileChooser() {
            var file = await _visEnv.CurrentTopLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
                Title = "Open your scuml database",
                AllowMultiple = false,
                FileTypeFilter = new[] { Scuml }
            });

            if (file != null) {
                var folder = await file.First().GetParentAsync();
                return folder!.Path.LocalPath; // Kommt in fehler wenn nichts ausgewählt...
            }
            return "";
        }

        public async Task<string> SaveFileChooser() {
            var file = await _visEnv.CurrentTopLevel!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions {
                Title = "Choose where your database will be stored",
                AllowMultiple = false
            });

            if (file != null) {
                return file.First().Path.LocalPath; // Kommt in fehler wenn nichts ausgewählt...
            }
            return "";
        }

    }
}

