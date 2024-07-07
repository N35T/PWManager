namespace PWManager.UI.Interfaces {
    public interface IChooseFile {
        public Task<string> OpenFileChooser();
        public Task<string> SaveFileChooser();
    }
}
