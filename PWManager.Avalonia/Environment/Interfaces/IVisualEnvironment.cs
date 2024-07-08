using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWManager.Avalonia.Environment.Interfaces {
    public interface IVisualEnvironment {
        public TopLevel? CurrentTopLevel { get; set; }
    }
}
