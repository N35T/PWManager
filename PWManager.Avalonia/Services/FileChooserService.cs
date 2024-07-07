using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using PWManager.UI.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PWManager.Avalonia.Services {
    internal class FileChooserService : IChooseFile {
        public async Task<string> OpenFileChooser() {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel((Visual?)global::Avalonia.Application.Current.ApplicationLifetime);

            // Start async operation to open the dialog.
            var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
                Title = "Open your scuml database",
                AllowMultiple = false
            });

            if (file != null) {
                return file.First().Path.ToString();
            }
            return "";
        }

        public Task<string> SaveFileChooser() {
            throw new System.NotImplementedException();
        }

    }
}

