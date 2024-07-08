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
        public FileChooserService()
        {
            _visEnv = IoC.Resolve<IVisualEnvironment>();
        }
        public async Task<string> OpenFileChooser() {
            var file = await _visEnv.CurrentTopLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
                Title = "Open your scuml database",
                AllowMultiple = false
            });

            if (file != null) {
                return file.First().Path.ToString(); // Kommt in fehler wenn nichts ausgewählt...
            }
            return "";
        }

        public Task<string> SaveFileChooser() {
            throw new System.NotImplementedException();
        }

    }
}

