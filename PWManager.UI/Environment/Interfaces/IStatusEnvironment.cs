
namespace PWManager.UI.Environment.Interfaces {
    public interface IStatusEnvironment {

        bool Connected { get; set; }
        bool Synchronizing { get; set; }
        string CurrentGroup { get; set; }

        event Action<string> StatusEnvironmentUpdated;
    }
}
