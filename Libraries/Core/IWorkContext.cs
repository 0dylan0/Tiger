using Core.Domain;
using Core.Domain.Common;
using Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IWorkContext
    {
        User CurrentUser { get; set; }

        //Hotel CurrentHotel { get; set; }

        //Language CurrentLanguage { get; set; }

        bool IsAlreadyLogin();
    }
}
