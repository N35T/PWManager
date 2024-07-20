
using PWManager.Domain.Entities;

namespace PWManager.UI.Environment.Interfaces {
    public interface IStatusEnvironment {

        bool Connected { get; set; }
        bool Synchronizing { get; set; }
        Group? CurrentGroup { get; set; }

        event Action<string> StatusEnvironmentUpdated;
        event Action CurrentGroupUpdated;

        void OnAccountFilterUpdated(string filtervalue);
        event Action<string> AccountFilterUpdated;
    }
}
