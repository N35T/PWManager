using PWManager.Application.Services.Interfaces;
using PWManager.UI;
using System;

namespace PWManager.Avalonia.Models {
    public class GroupDisplayModel {

        public string Id { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;

        public bool IsMainGroup { get; set; }
        public bool IsActiveGroup { get; set; }
        public bool IsRemoteGroup { get; set; }

        public bool IsNormalGroup { get => !IsMainGroup && !IsRemoteGroup; }

        public void OnClick() {
            if (!IsActiveGroup) {
                IoC.Resolve<IGroupService>().SwitchGroup(Identifier);
            }
        }

        public void OnDelete() {

        }
    }
}
