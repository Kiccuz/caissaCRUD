using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CaissaApp.ViewModels
{
    public partial class CommentViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime createdAt ;
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string text;

        
    }
}
